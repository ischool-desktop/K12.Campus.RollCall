using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace K12.Campus.RollCall
{
    public partial class ConfigFrom : BaseForm
    {
        public ConfigFrom()
        {
            InitializeComponent();

            //讀取UDT資料

            List<AbsenceUDT> AbList = tool._A.Select<AbsenceUDT>();
            foreach (AbsenceUDT each in AbList)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1);
                row.Cells[0].Value = each.Absence;
                row.Cells[1].Value = each.Period;

                dataGridViewX1.Rows.Add(row);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<AbsenceUDT> DeleteAbList = tool._A.Select<AbsenceUDT>();
            tool._A.DeletedValues(DeleteAbList);

            List<AbsenceUDT> InsertAbList = new List<AbsenceUDT>();

            foreach (DataGridViewRow each in dataGridViewX1.Rows)
            {
                if (each.IsNewRow)
                    continue;

                AbsenceUDT udt = new AbsenceUDT();
                udt.Absence = "" + each.Cells[0].Value;
                udt.Period = "" + each.Cells[1].Value;
                InsertAbList.Add(udt);
            }

            tool._A.InsertValues(InsertAbList);

            MsgBox.Show("新增成功!!");
            this.Close();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dataGridViewX1.Rows.Clear();

            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);
            row.Cells[0].Value = "";
            row.Cells[1].Value = "升旗";
            dataGridViewX1.Rows.Add(row);

            row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);
            row.Cells[0].Value = "";
            row.Cells[1].Value = "早讀";
            dataGridViewX1.Rows.Add(row);

            row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);
            row.Cells[0].Value = "";
            row.Cells[1].Value = "一";
            dataGridViewX1.Rows.Add(row);

            row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);
            row.Cells[0].Value = "";
            row.Cells[1].Value = "二";
            dataGridViewX1.Rows.Add(row);

            row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);
            row.Cells[0].Value = "";
            row.Cells[1].Value = "三";
            dataGridViewX1.Rows.Add(row);

            row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);
            row.Cells[0].Value = "";
            row.Cells[1].Value = "四";
            dataGridViewX1.Rows.Add(row);

            row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);
            row.Cells[0].Value = "";
            row.Cells[1].Value = "五";
            dataGridViewX1.Rows.Add(row);

            row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);
            row.Cells[0].Value = "";
            row.Cells[1].Value = "六";
            dataGridViewX1.Rows.Add(row);

            row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);
            row.Cells[0].Value = "";
            row.Cells[1].Value = "七";
            dataGridViewX1.Rows.Add(row);

            row = new DataGridViewRow();
            row.CreateCells(dataGridViewX1);
            row.Cells[0].Value = "";
            row.Cells[1].Value = "八";
            dataGridViewX1.Rows.Add(row);
        }
    }
}
