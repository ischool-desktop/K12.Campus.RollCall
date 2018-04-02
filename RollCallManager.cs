using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using System.Xml.Linq;
using K12.Data;
using FISCA.Data;

namespace K12.Campus.RollCall
{
    public partial class RollCallManager : BaseForm
    {
        private Dictionary<string, Dictionary<string, string>> _RollCallDic = new Dictionary<string, Dictionary<string, string>>();
        private Dictionary<int, string> _AbsenceDic = new Dictionary<int, string>();
        private Dictionary<string, List<string>> _AllowAbsence = new Dictionary<string, List<string>>();

        private Dictionary<string, Dictionary<string, int>> _ClassCount = new Dictionary<string, Dictionary<string, int>>();
        private Dictionary<string, List<DataRow>> _ClassDetial = new Dictionary<string, List<DataRow>>();
        private List<string> _PeriodList = new List<string>();

        public RollCallManager()
        {
            InitializeComponent();

            // Init DataeTimeLb
            dateTimeInput1.Text = DateTime.Today.ToString("yyyy/MM/dd");

            #region 整理選項
            // Init Period combobox - 取得節次對照表
            XDocument periodData = XDocument.Parse(K12.Data.School.Configuration["節次對照表"].PreviousData.OuterXml);
            List<XElement> periodList = periodData.Element("Periods").Elements("Period").ToList();

            filterCbx.Items.Add("全部");
            filterCbx.Items.Add("未完成點名");
            filterCbx.Items.Add("有學生缺曠");

            foreach (XElement period in periodList)
            {
                periodCbx.Items.Add(period.Attribute("Name").Value);
                _PeriodList.Add(period.Attribute("Name").Value);
            }
            periodCbx.Items.Insert(0, "全天");
            #endregion

            #region Init DataGridView2 

            foreach (XElement element in periodList)
            {
                DataGridViewTextBoxColumn dgvCol = new DataGridViewTextBoxColumn();
                dgvCol.Name = element.Attribute("Name").Value;
                dgvCol.HeaderText = element.Attribute("Name").Value;
                dgvCol.Width = 66;
                dgvCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridViewX2.Columns.Add(dgvCol);
            }
            
            #endregion

            #region 設定選項預設值
            if (periodCbx.Items.Count > 0)
                periodCbx.SelectedIndex = 0;
            filterCbx.SelectedIndex = 0;
            #endregion

            if (periodCbx.SelectedIndex == 0)
            {
                dataGridViewX1.Visible = false;
                dataGridViewX2.Visible = true;
                filterCbx.Visible = false;
                labelX4.Visible = false;
            }

            #region 整理假別欄位
            // Init datagridview column
            QueryHelper qh = new QueryHelper();
            string sql = @"
SELECT
    allow_absence.name
    , allow_absence.period
FROM
    (
        SELECT
	        unnest(xpath('/Absence/@Name', each_absence.absence))::text as name
			, row_number() OVER () as row_number
        FROM (
	        SELECT unnest(xpath('/AbsenceList/Absence', xmlparse(content content))) as absence
	        FROM list 
	        WHERE name = '假別對照表'
        ) as each_absence
    ) as absence
    LEFT OUTER JOIN (
        SELECT 
            period
            , json_array_elements(absence :: json) ->>'Name' AS name  
			, row_number() OVER () as row_number2
        FROM 
            $campus.rollcall.allow_absence
    ) as allow_absence
        on allow_absence.name = absence.name
WHERE
    allow_absence.period is not null
ORDER BY row_number, row_number2
"; // 取得老師可以點缺曠別
            DataTable dt = qh.Select(sql);
            foreach (DataRow dr in dt.Rows)
            {
                if (!_AllowAbsence.ContainsKey("" + dr["name"]))
                {
                    _AllowAbsence.Add("" + dr["name"], new List<string>());

                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.HeaderText = "" + dr["name"];
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    col.MinimumWidth = 59;

                    dataGridViewX1.Columns.Add(col);
                }
                _AllowAbsence["" + dr["name"]].Add("" + dr["period"]);
            }
            #endregion

            ReloadDataGridViewX2();
        }

        public void ReloadDataGridViewX1()
        {
            if (periodCbx.Text == "" || filterCbx.Text == "")
                return;

            _ClassCount.Clear();
            _ClassDetial.Clear();
            dataGridViewX1.Rows.Clear();
            foreach (DataGridViewColumn col in dataGridViewX1.Columns)
            {
                if (_AllowAbsence.ContainsKey(col.HeaderText))
                {
                    col.Visible = _AllowAbsence[col.HeaderText].Contains(periodCbx.Text);
                }
            }
            #region 整理資料

            #region SQL
            string sql = string.Format(@"
SELECT
    class.id as class_id
    , class.grade_year
    , class.class_name
    , student_count.count as student_count
    , student.id as student_id
    , student.name
    , detail.absence
    , detail.batch_id
FROM
    class
    LEFT OUTER JOIN student 
        ON student.ref_class_id = class.id
        AND student.status in ( 1, 2 )
    LEFT OUTER JOIN (
        SELECT
            ref_class_id
            , count(*)
        FROM 
            student
        WHERE
            student.status in ( 1, 2 )
        GROUP BY ref_class_id
    ) as student_count
        ON  student_count.ref_class_id = class.id
    LEFT OUTER JOIN (
        SELECT
            log.uid as batch_id
            , log.ref_course_id
            , log.ref_teacher_id
            , detail.ref_student_id
            , detail.absence
        FROM
            $campus.rollcall.log.batch AS log 
            LEFT OUTER JOIN $campus.rollcall.log.detail AS detail 
                ON detail.ref_log_id = log.uid
        WHERE 
            log.date::date = '{1}'::date
            AND period = '{0}'
    ) as detail
        ON detail.ref_student_id = student.id
WHERE
    student_count.count > 0
ORDER BY 
    class.grade_year
    , class.display_order
    , class.class_name
    , student.seat_no
    , student.id
    , detail.batch_id
                ", periodCbx.Text,dateTimeInput1.Value.ToString("yyyy/MM/dd"));
            #endregion
            Dictionary<string, string> dicClassName = new Dictionary<string, string>();
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);
            foreach (DataRow row in dt.Rows)
            {
                var classID = "" + row["class_id"];
                if (!_ClassCount.ContainsKey(classID))
                {
                    dicClassName.Add(classID, "" + row["class_name"]);

                    _ClassDetial.Add(classID, new List<DataRow>());

                    _ClassCount.Add(classID, new Dictionary<string, int>());
                    _ClassCount[classID].Add("人數", int.Parse("" + row["student_count"]));
                    _ClassCount[classID].Add("已點人數", 0);
                    foreach (var absence in _AllowAbsence.Keys)
                    {
                        _ClassCount[classID].Add(absence, 0);
                    }
                }
                _ClassDetial[classID].Add(row);
                if ("" + row["batch_id"] != "")
                {
                    _ClassCount[classID]["已點人數"]++;
                    if (_AllowAbsence.ContainsKey("" + row["absence"]) && _AllowAbsence["" + row["absence"]].Contains(periodCbx.Text))
                        _ClassCount[classID]["" + row["absence"]]++;
                }
            }

            #endregion

            #region 填入資料
            foreach (var classID in _ClassCount.Keys)
            {
                int index = 0;
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);
                datarow.Cells[index++].Value = dicClassName[classID];
                datarow.Cells[index++].Value = _ClassCount[classID]["人數"];
                if(_ClassCount[classID]["人數"] > _ClassCount[classID]["已點人數"])
                    datarow.Cells[index].Style.ForeColor = Color.Red;
                datarow.Cells[index++].Value = _ClassCount[classID]["已點人數"];
                foreach (var absence in _AllowAbsence.Keys)
                {
                    if(_ClassCount[classID][absence] > 0)
                        datarow.Cells[index].Style.ForeColor = Color.Red;
                    datarow.Cells[index++].Value = _ClassCount[classID][absence];
                }

                datarow.Tag = classID;
                if(filterCbx.Text=="全部")
                dataGridViewX1.Rows.Add(datarow);
                if (filterCbx.Text == "未完成點名" && _ClassCount[classID]["人數"]!= _ClassCount[classID]["已點人數"])
                    dataGridViewX1.Rows.Add(datarow);
                if (filterCbx.Text == "有學生缺曠")
                {
                    foreach (var absence in _AllowAbsence.Keys)
                    {
                        if (_ClassCount[classID][absence] > 0)
                        {
                            
                            dataGridViewX1.Rows.Add(datarow);
                            break;
                        }
                    }
                }
            } 
            #endregion

        }

        public void ReloadDataGridViewX2()
        {
            #region SQL
            string sql = string.Format(@"

SELECT 
	class.id AS ref_class_id
	, class.class_name
	, student_count.count AS student_count
--	, student.id AS ref_student_id
--	, detail.ref_student_id AS rollcall_student
	, detail.date
	, detail.period
	, detail.absence
	, count(detail.ref_student_id) AS absence_count
	, detail.ref_log_id
	, detail.ref_course_id
	, detail.ref_teacher_id
	--, student.id AS ref_student_id
FROM 
	class
	LEFT OUTER JOIN (
		SELECT 
			student.ref_class_id
			, count(id)
		FROM
			student
		WHERE
			status IN (1,2)
		GROUP BY
			ref_class_id
	) student_count ON student_count.ref_class_id = class.id
	LEFT OUTER JOIN (
		SELECT 
			*
		FROM 
			student
		WHERE 
			status in (1,2)
	) student ON student.ref_class_id = class.id
	LEFT OUTER JOIN(
		SELECT 
			detail.absence
			, detail.ref_log_id
			, detail.ref_student_id
			, log.date
			, log.period
			, log.ref_course_id
			, log.ref_teacher_id
		FROM
			$campus.rollcall.log.detail AS detail
			LEFT OUTER JOIN $campus.rollcall.log.batch AS log
				ON log.uid = detail.ref_log_id
		WHERE log.date = '{0}'::date
	) detail ON detail.ref_student_id = student.id
	WHERE student_count IS NOT NULL
	GROUP BY
		class.id
		, class.class_name
		, student_count.count
		, detail.ref_log_id
		, detail.absence
		, detail.date
		, detail.period
		, detail.ref_course_id
		, detail.ref_teacher_id
	ORDER BY 
		class.grade_year
		, class.id
		, class.display_order
		
            ", dateTimeInput1.Value.ToString("yyyy/MM/dd"));
            #endregion

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            Dictionary<string, RollCallTotalView> classDic = new Dictionary<string, RollCallTotalView>();

            #region 整理資料
            foreach (DataRow row in dt.Rows)
            {
                string classID = "" + row["ref_class_id"];
                string className = "" + row["class_name"];
                string studentCount = "" + row["student_count"];
                string period = "" + row["period"];
                string absence = "" + row["absence"];
                int absenceCount = ("" + row["absence_count"]) == "" ? 0 : int.Parse("" + row["absence_count"]);

                if (classDic.ContainsKey(classID))
                {
                    if (classDic[classID].periodDic.ContainsKey(period) && classDic[classID].periodDic[period].ContainsKey(absence)) // 已有節次 已有缺曠
                    {
                        classDic[classID].periodDic[period][absence] += absenceCount;
                    }
                    if (classDic[classID].periodDic.ContainsKey(period) && !classDic[classID].periodDic[period].ContainsKey(absence)) // 已有節次 沒有缺曠
                    {
                        classDic[classID].periodDic[period].Add(absence, absenceCount);
                    }
                    if (!classDic[classID].periodDic.ContainsKey(period))
                    {
                        classDic[classID].periodDic.Add(period, new Dictionary<string, int>());
                        classDic[classID].periodDic[period].Add(absence, absenceCount);
                    }
                }
                if (!classDic.ContainsKey(classID))
                {
                    classDic.Add(classID, new RollCallTotalView());

                    classDic[classID].classID = classID;
                    classDic[classID].className = className;
                    classDic[classID].studentCount = studentCount;
                    classDic[classID].periodDic = new Dictionary<string, Dictionary<string, int>>();
                    classDic[classID].absenceDic = new Dictionary<string, int>();
                    if (classDic[classID].periodDic.ContainsKey(period))
                    {
                        classDic[classID].periodDic[period].Add(absence, absenceCount);
                    }
                    if (!classDic[classID].periodDic.ContainsKey(period))
                    {
                        classDic[classID].periodDic.Add(period, new Dictionary<string, int>());
                        classDic[classID].periodDic[period].Add(absence, absenceCount);
                    }
                }
            }
            #endregion

            foreach (string classID in classDic.Keys)
            {
                DataGridViewRow dgvRow = new DataGridViewRow();
                dgvRow.CreateCells(dataGridViewX2);
  
                int index = 0;
                dgvRow.Cells[index++].Value = classDic[classID].className; 
                dgvRow.Cells[index++].Value = classDic[classID].studentCount;
                dgvRow.Tag = classID;
                foreach (string period in _PeriodList)
                {
                    if (classDic[classID].periodDic == null)
                    {
                        dgvRow.Cells[index].Value = "";
                        //dgvRow.Cells[index++].Style.ForeColor = Color.Red;
                    }
                    if (classDic[classID].periodDic != null)
                    {
                        if (!classDic[classID].periodDic.ContainsKey(period))
                        {
                            dgvRow.Cells[index].Value = "";
                            //dgvRow.Cells[index++].Style.ForeColor = Color.Red;
                        }
                        if (classDic[classID].periodDic.ContainsKey(period))
                        {
                            int 缺曠數 = 0;
                            foreach (string absnece in classDic[classID].periodDic[period].Keys)
                            {
                                if (absnece != "") // absenece == "" 代表實到學生
                                {
                                    缺曠數 += classDic[classID].periodDic[period][absnece];
                                }
                            }
                            if (缺曠數 > 0 )
                            {
                                dgvRow.Cells[index].Value = classDic[classID].periodDic[period][""] + 缺曠數 + "(" + 缺曠數 + ")"; // 點名人數 : 實到 + 缺曠
                                dgvRow.Cells[index].Style.ForeColor = Color.Red;
                            }
                            if (缺曠數 == 0 )
                            {
                                dgvRow.Cells[index].Value = classDic[classID].periodDic[period][""];
                            }
                            if (classDic[classID].periodDic[period][""] + 缺曠數 != int.Parse(classDic[classID].studentCount)) // 如果點名人數 與 班級人數不符
                            {
                                dgvRow.Cells[index].Style.ForeColor = Color.Red;
                            }
                        }
                    }
                    index++;
                }
                dataGridViewX2.Rows.Add(dgvRow);
            }
        }

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string classID = "" + dataGridViewX1.Rows[e.RowIndex].Tag;

            (new ClassRollCall(classID, periodCbx.Text,dateTimeInput1.Value)).ShowDialog();
        }

        private void periodCbx_TextChanged(object sender, EventArgs e)
        {
            if (periodCbx.SelectedIndex != 0)
            {
                filterCbx.Visible = true;
                labelX4.Visible = true;
                labelX1.Visible = false;
            }
            if (periodCbx.SelectedIndex == 0)
            {
                filterCbx.Visible = false;
                labelX4.Visible = false;
                labelX1.Visible = true;
            }
            ReloadDataGridViewX1();
        }

        private void filterCbx_TextChanged(object sender, EventArgs e)
        {
            ReloadDataGridViewX1();
        }

        private void dateTimeInput1_TextChanged(object sender, EventArgs e)
        {
            ReloadDataGridViewX1();
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void periodCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (periodCbx.SelectedIndex == 0)
            {
                dataGridViewX1.Visible = false;
                dataGridViewX2.Visible = true;
            }
            else
            {
                dataGridViewX1.Visible = true;
                dataGridViewX2.Visible = false;
            }
        }
        private void dataGridViewX2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 1)
            {
                string classID = "" + dataGridViewX2.Rows[e.RowIndex].Tag;
                string period = "" + dataGridViewX2.Columns[e.ColumnIndex].HeaderText;
                (new ClassRollCall(classID, period, dateTimeInput1.Value)).ShowDialog();
            }
        }
    }
}

class RollCallTotalView
{
    public string classID { get; set; }
    public string className { get; set; }
    public string studentCount { get; set; }
    //public Dictionary<string, Dictionary<string, Dictionary<string, int>>> teacherDic { get; set; }
    public Dictionary<string, Dictionary<string, int>> periodDic { get; set; }
    public Dictionary<string, int> absenceDic { get; set; }

}
