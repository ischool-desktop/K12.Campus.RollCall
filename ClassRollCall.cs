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

namespace K12.Campus.RollCall
{
    public partial class ClassRollCall : BaseForm
    {
        public ClassRollCall(string classID,string period)
        {
            InitializeComponent();

            dateTimeLb.Text = DateTime.Today.ToLongDateString();

            periodLb.Text = period;

            ClassRecord cr = Class.SelectByID(classID);
            classLb.Text = cr.Name;


            // Init DataGridView
            string sql = string.Format(@"SELECT id,name,seat_no,student_number FROM student WHERE ref_class_id = {0} ORDER BY seat_no", classID);

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);

                int index = 0;
                datarow.Cells[2].Value = cr.Name;
                datarow.Cells[3].Value = "" + dr["seat_no"];
                datarow.Cells[4].Value = "" + dr["student_number"];
                datarow.Cells[5].Value = "" + dr["Name"];
                datarow.Tag = "" + dr["id"]; // 記錄學生ID

                dataGridViewX1.Rows.Add(datarow);
            }
        }
    }
}
