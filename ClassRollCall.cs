using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.Data;
using K12.Data;
using System.Xml.Linq;

namespace K12.Campus.RollCall
{
    public partial class ClassRollCall : BaseForm
    {
        public ClassRollCall(string classID,string period,DateTime date)
        {
            InitializeComponent();

            #region Init Label
            dateTimeLb.Text = date.ToString("yyyy/MM/dd");

            periodLb.Text = period;

            ClassRecord cr = Class.SelectByID(classID);
            classLb.Text = cr.Name;
            #endregion

            // Init DataGridView
            string sql = string.Format(@"

SELECT 
	student.id AS ref_student_id 
	, student.seat_no
	, student.student_number
	, student.name
	, detail.absence
	, teacher.teacher_name
	, attendance.detail
FROM 
	student
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
            AND period = '{2}'
    ) as detail
        ON detail.ref_student_id = student.id
	LEFT OUTER JOIN teacher
		ON teacher.id = detail.ref_teacher_id
	LEFT OUTER JOIN (
		SELECT 
            ref_student_id, detail 
        FROM 
            attendance 
        WHERE 
            ref_student_id IN (
                SELECT 
                    id 
                FROM 
                    student 
                WHERE 
                    ref_class_id = {0} 
            ) 
            AND occur_date = '{1}'::date 
	) attendance ON attendance.ref_student_id = student.id
WHERE
	student.ref_class_id = {0} 
ORDER BY seat_no
", classID,date.ToString("yyyy/MM/dd"),period);

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);

                int index = 0;
                datarow.Cells[index++].Value = "" + dr["teacher_name"];
                index++; // 課程
                datarow.Cells[index++].Value = cr.Name;
                datarow.Cells[index++].Value = "" + dr["seat_no"];
                datarow.Cells[index++].Value = "" + dr["student_number"];
                datarow.Cells[index++].Value = "" + dr["name"];
                datarow.Cells[index++].Value = "" + dr["absence"];
                if ("" + dr["detail"] != "")
                {
                    XDocument attendance = XDocument.Parse("<Root>" + dr["detail"] + "</Root>");
                    List<XElement> periods = attendance.Element("Root").Element("Attendance").Elements("Period").ToList();
                    foreach (XElement pNode in periods)
                    {
                        if (pNode.Value == period)
                        {
                            datarow.Cells[index++].Value = pNode.Attribute("AbsenceType").Value;
                        }
                    }
                }
                datarow.Tag = "" + dr["ref_student_id"]; // 記錄學生ID

                dataGridViewX1.Rows.Add(datarow);
            }
        }
    }
}
