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
        public Dictionary<string, Dictionary<string, string>> rollCallDic = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<int, string> absenceDic = new Dictionary<int, string>();

        public RollCallManager()
        {
            InitializeComponent();

            // Init DataeTimeLb
            dateTimeLb.Text = DateTime.Today.ToLongDateString();

            QueryHelper qh = new QueryHelper();

            // Init Period combobox - 取得節次對照表
            K12.Data.Configuration.ConfigData _periodData = K12.Data.School.Configuration["節次對照表"];
            XDocument periodData = XDocument.Parse(_periodData.PreviousData.OuterXml);
            List<XElement> periodList = periodData.Element("Periods").Elements("Period").ToList();

            foreach (XElement period in periodList)
            {
                periodCbx.Items.Add(period.Attribute("Name").Value);
            }

            // Init datagridview
            string sql = "SELECT DISTINCT json_array_elements(absence :: json) ->>'Name' AS name  FROM $campus.rollcall.allow_absence"; // 取得老師可以點缺曠別
            DataTable dt = qh.Select(sql);
            int index = 0;
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.HeaderText = "" + dr["name"];
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                col.MinimumWidth = 59;
                
                dataGridViewX1.Columns.Add(col);

                // 紀錄缺曠別
                absenceDic.Add(index++,"" + dr["name"]);
            }

            #region 班級、班級人數SQL
            string sql3 = @"
SELECT DISTINCT 
    student.ref_class_id
	,class.class_name
	,class.grade_year
	,count(student.id)
FROM
    student

    LEFT OUTER JOIN(
        SELECT id, class_name, grade_year FROM class
	) class ON class.id = student.ref_class_id
WHERE

    status = 1 
	AND ref_class_id IS NOT NULL
GROUP BY student.ref_class_id

    ,class.class_name
	,class.grade_year
ORDER BY class.grade_year";
            #endregion
            //Data

            // 取得點名紀錄:缺曠數
            string sql2 = "SELECT ref_log_id,absence ,count(ref_student_id) FROM $k12.campus.rollcall_detail  GROUP BY ref_log_id,absence"; // 取得點名紀錄

            DataTable dt2 = qh.Select(sql2);
            foreach (DataRow dr in dt2.Rows)
            {
                if (rollCallDic.ContainsKey("" + dr["ref_log_id"]))
                {
                    rollCallDic["" + dr["ref_log_id"]].Add("" + dr["absence"], "" + dr["count"]);
                }
                if (!rollCallDic.ContainsKey("" + dr["ref_log_id"]))
                {
                    Dictionary<string, string> absenceCountDic = new Dictionary<string, string>();
                    absenceCountDic.Add("" + dr["absence"], "" + dr["count"]);

                    rollCallDic.Add("" + dr["ref_log_id"], absenceCountDic);
                }
            }
        }

        public void ReloadDataGridView()
        {
            if (periodCbx.Text != string.Empty)
            {
                dataGridViewX1.Rows.Clear();
                #region SQL
                string sql = string.Format(@"
SELECT DISTINCT 
	student.ref_class_id 
	,class.class_name
	,class.grade_year
	,count(student.id) AS class_student_count
	,count(detail.ref_student_id) AS class_rollcall_count
	,log.uid AS ref_log_id
	,log.period
	,log.date
FROM 
	student
	LEFT OUTER JOIN(
		SELECT 
			id
			,class_name
			,grade_year
		FROM
			class
	) class ON class.id = student.ref_class_id
	LEFT OUTER JOIN (
		SELECT * FROM $k12.campus.rollcall_detail
	) detail ON detail.ref_student_id:: int = student.id
	LEFT OUTER JOIN(
		SELECT * FROM $k12.campus.rollcall_log
	) log ON log.uid = detail.ref_log_id :: bigint
WHERE status = 1 AND log.period = '{0}' OR log.uid IS NULL
GROUP BY student.ref_class_id,class.class_name,class.grade_year, log.uid,log.period,log.date
ORDER BY class.grade_year,ref_class_id
                ", periodCbx.Text);
                #endregion
                QueryHelper qh = new QueryHelper();
                DataTable dt = qh.Select(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    int index = 0;
                    DataGridViewRow datarow = new DataGridViewRow();
                    datarow.CreateCells(dataGridViewX1);

                    datarow.Cells[index++].Value = "" + dr["class_name"];
                    datarow.Cells[index++].Value = "" + dr["class_student_count"];
                    if ("" + dr["class_student_count"] != "" + dr["class_rollcall_count"])
                    {
                        datarow.Cells[index].Style.ForeColor = Color.Red;
                    }
                    datarow.Cells[index++].Value = "" + dr["class_rollcall_count"];
                    if (rollCallDic.ContainsKey("" + dr["ref_log_id"])) //-- 如果有點名紀錄
                    {
                        foreach (int _index in absenceDic.Keys)
                        {
                            if (rollCallDic["" + dr["ref_log_id"]].ContainsKey(absenceDic[_index]))
                            {
                                if (absenceDic[_index] == "曠課")
                                {
                                    datarow.Cells[index].Style.ForeColor = Color.Red;
                                }
                                datarow.Cells[index++].Value = rollCallDic["" + dr["ref_log_id"]][absenceDic[_index]];
                            }
                            index++;
                        }
                        //if (rollCallDic["" + dr["ref_log_id"]].ContainsKey("曠課"))
                        //{
                        //    datarow.Cells[index++].Value = rollCallDic["" + dr["ref_log_id"]]["曠課"];
                        //}
                        //if (!rollCallDic["" + dr["ref_log_id"]].ContainsKey("曠課"))
                        //{
                        //    datarow.Cells[index++].Value = "";
                        //}
                        //if (rollCallDic["" + dr["ref_log_id"]].ContainsKey("遲到"))
                        //{
                        //    datarow.Cells[index++].Value = rollCallDic["" + dr["ref_log_id"]]["遲到"];
                        //}
                        //if (!rollCallDic["" + dr["ref_log_id"]].ContainsKey("遲到"))
                        //{
                        //    datarow.Cells[index++].Value = "";
                        //}

                    }
                    datarow.Tag = "" + dr["ref_class_id"];

                    dataGridViewX1.Rows.Add(datarow);
                }
            }
        }
        
        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string classID = "" + dataGridViewX1.Rows[e.RowIndex].Tag;

            (new ClassRollCall(classID,periodCbx.Text)).ShowDialog(); 

        }

        private void periodCbx_TextChanged(object sender, EventArgs e)
        {
            ReloadDataGridView();
        }
    }
    class RollCallRecord
    {
        public string RefLogID { get; set; }
        public string Absence { get; set; }
        public string Count { get; set; }
    }
}
