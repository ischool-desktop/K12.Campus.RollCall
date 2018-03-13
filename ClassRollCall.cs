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
using FISCA.Presentation;
using System.IO;
using Aspose.Cells;
using System.Diagnostics;

namespace K12.Campus.RollCall
{
    public partial class ClassRollCall : BaseForm
    {

        private ContextMenu menu = new ContextMenu();

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
            #region 待處理
            MenuItem item = new MenuItem("加入待處理");
            item.Click += delegate
            {
                List<string> list = new List<string>();
                foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
                {
                    list.Add("" + row.Tag);
                    //row.DefaultCellStyle.Font = new System.Drawing.Font("微軟正黑體", (float)9.75, FontStyle.Italic);
                    //row.DefaultCellStyle.ForeColor = Color.Red;
                }
                K12.Presentation.NLDPanels.Student.AddToTemp(list);

                MessageBox.Show(string.Format("新增{0}名學生於待處理", dataGridViewX1.SelectedRows.Count));

                if (K12.Presentation.NLDPanels.Student.TempSource.Count > 0)
                {
                    detailLb.Visible = true;
                    detailLb.Text = string.Format("待處理學生共{0}名學生", K12.Presentation.NLDPanels.Student.TempSource.Count);

                    clearBtn.Visible = true;
                }
            };
            menu.MenuItems.Add(item);
            MenuItem item2 = new MenuItem("清空待處理");
            item2.Click += delegate
            {
                K12.Presentation.NLDPanels.Student.RemoveFromTemp(K12.Presentation.NLDPanels.Student.TempSource);

                MessageBox.Show("已清除待處理所有學生");

                detailLb.Visible = false;
                clearBtn.Visible = false;
                //foreach (DataGridViewRow row in dataGridViewX1.Rows)
                //{
                //    row.DefaultCellStyle.Font = new System.Drawing.Font("微軟正黑體", (float)9.75, FontStyle.Regular);
                //    row.DefaultCellStyle.ForeColor = Color.Black;
                //}
            };
            menu.MenuItems.Add(item2);
            #endregion

            #region
            string sql = string.Format(@"

SELECT 
	student.id AS ref_student_id 
	, student.seat_no
	, student.student_number
	, student.name
	, detail.absence
	, teacher.teacher_name
	, attendance.detail
    , detail.last_update as log_time
FROM 
	student
    LEFT OUTER JOIN (
        SELECT
            log.uid as batch_id
            , log.ref_course_id
            , log.ref_teacher_id
            , detail.ref_student_id
            , detail.absence
            , log.last_update
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
", classID, date.ToString("yyyy/MM/dd"), period);
            #endregion

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
                            datarow.Cells[index].Value = pNode.Attribute("AbsenceType").Value;
                        }
                    }
                }
                index++;

                DateTime parseTime;
                datarow.Cells[index++].Value = DateTime.TryParse("" + dr["log_time"], out parseTime) ? parseTime.ToString("yyyy/MM/dd HH:mm:ss"):"";

                datarow.Tag = "" + dr["ref_student_id"]; // 記錄學生ID

                dataGridViewX1.Rows.Add(datarow);
            }
        }

        private void dataGridViewX1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menu.Show(dataGridViewX1,new Point(e.X,e.Y));
            }
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            // 建立範本
            Workbook template = new Workbook(new MemoryStream(Properties.Resources.班級課堂點名明細樣板));
            // 複製範本
            Workbook book = new Workbook();
            book.Copy(template);
            Worksheet sheet = book.Worksheets[0];

            sheet.Name = classLb.Text;

            // 填入資料
            int row = 1;
            Style style = sheet.Cells.GetCellStyle(0, 0);
            foreach (DataGridViewRow datarow in dataGridViewX1.Rows)
            {
                int col = 0;
                sheet.Cells[row, col].PutValue(datarow.Cells["teacher"].Value);
                sheet.Cells[row, col++].SetStyle(style);
                //sheet.Cells[row, col].PutValue(datarow.Cells["course"].Value);
                //sheet.Cells[row, col++].SetStyle(style);
                sheet.Cells[row, col].PutValue(datarow.Cells["className"].Value);
                sheet.Cells[row, col++].SetStyle(style);
                sheet.Cells[row, col].PutValue(datarow.Cells["seatNo"].Value);
                sheet.Cells[row, col++].SetStyle(style);
                sheet.Cells[row, col].PutValue(datarow.Cells["stuNumber"].Value);
                sheet.Cells[row, col++].SetStyle(style);
                sheet.Cells[row, col].PutValue(datarow.Cells["name"].Value);
                sheet.Cells[row, col++].SetStyle(style);
                sheet.Cells[row, col].PutValue(datarow.Cells["rollCallLog"].Value);
                sheet.Cells[row, col++].SetStyle(style);
                sheet.Cells[row, col].PutValue(datarow.Cells["attendance"].Value);
                sheet.Cells[row, col++].SetStyle(style);
                sheet.Cells[row, col].PutValue(datarow.Cells["rollCallTime"].Value);
                sheet.Cells[row, col++].SetStyle(style);

                row++;
            }

            string date = DateTime.Parse(dateTimeLb.Text).ToString("yyyy_MM_dd");
            char[] ca = periodLb.Text.ToCharArray();
            string period = "";
            int i = 0;
            foreach (char c in ca)
            {
                if (c == '/')
                {
                    ca[i] = '_';
                }
                period += ca[i];
                i++;
            }

            // 存檔
            SaveFileDialog SaveFileDialog = new SaveFileDialog();
            SaveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx|所有檔案 (*.*)|*.*";
            SaveFileDialog.FileName = string.Format("{0}_{1}_{2}_課堂點名", date, classLb.Text, period);
            try
            {
                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    book.Save(SaveFileDialog.FileName);
                    Process.Start(SaveFileDialog.FileName);
                    MotherForm.SetStatusBarMessage("課堂點名明細,列印完成!!");
                }
                else
                {
                    FISCA.Presentation.Controls.MsgBox.Show("檔案未儲存");
                    return;
                }
            }
            catch
            {
                FISCA.Presentation.Controls.MsgBox.Show("檔案儲存錯誤,請檢查檔案是否開啟中!!");
                MotherForm.SetStatusBarMessage("檔案儲存錯誤,請檢查檔案是否開啟中!!");
            }

        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
            {
                list.Add("" + row.Tag);
            }
            K12.Presentation.NLDPanels.Student.AddToTemp(list);

            MessageBox.Show(string.Format("新增{0}名學生於待處理", dataGridViewX1.SelectedRows.Count));

            if (K12.Presentation.NLDPanels.Student.TempSource.Count > 0)
            {
                detailLb.Visible = true;
                detailLb.Text = string.Format("待處理學生共{0}名學生", K12.Presentation.NLDPanels.Student.TempSource.Count);

                clearBtn.Visible = true;
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            K12.Presentation.NLDPanels.Student.RemoveFromTemp(K12.Presentation.NLDPanels.Student.TempSource);
            MessageBox.Show("已清除待處理所有學生");

            detailLb.Visible = false;
            clearBtn.Visible = false;
        }
    }
}
