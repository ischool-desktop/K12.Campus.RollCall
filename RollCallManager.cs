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
            }
            #endregion

            #region 設定選項預設值
            if (periodCbx.Items.Count > 0)
                periodCbx.SelectedIndex = 0;
            filterCbx.SelectedIndex = 0;
            #endregion

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
        }

        public void ReloadDataGridView()
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

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string classID = "" + dataGridViewX1.Rows[e.RowIndex].Tag;

            (new ClassRollCall(classID, periodCbx.Text,dateTimeInput1.Value)).ShowDialog();
        }

        private void periodCbx_TextChanged(object sender, EventArgs e)
        {
            ReloadDataGridView();
        }

        private void filterCbx_TextChanged(object sender, EventArgs e)
        {
            ReloadDataGridView();
        }

        private void dateTimeInput1_TextChanged(object sender, EventArgs e)
        {
            ReloadDataGridView();
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
