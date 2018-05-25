using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using System.Xml.Linq;

namespace K12.Campus.RollCall
{
    public partial class OnlineRollCall : BaseForm
    {
        Dictionary<int, string> abIndexDic = new Dictionary<int, string>();
        int _endRowIndex;
        bool initFinish = false;
        public OnlineRollCall()
        {
            InitializeComponent();
            
            ///  取得上次設定的資料
            AccessHelper access = new AccessHelper();
            List<AbsenceUDT> SettingData = access.Select<AbsenceUDT>();

            // 取得假別對照表
            K12.Data.Configuration.ConfigData _attendType = K12.Data.School.Configuration["假別對照表"];
            XDocument attendType = XDocument.Parse(_attendType.PreviousData.OuterXml);
            List<XElement> absenceList = attendType.Element("AbsenceList").Elements("Absence").ToList();
            
            // 取得節次對照表
            K12.Data.Configuration.ConfigData _period = K12.Data.School.Configuration["節次對照表"];
            XDocument period = XDocument.Parse(_period.PreviousData.OuterXml);
            List<XElement> periodList = period.Element("Periods").Elements("Period").ToList();

            _endRowIndex = periodList.Count();

            #region Init DataGridView

            #region col

            int index = 2;
            foreach (XElement absence in absenceList)
            {
                DataGridViewCheckBoxColumn CheckBoxColumn = new DataGridViewCheckBoxColumn();
                CheckBoxColumn.Name = absence.Attribute("Abbreviation").Value;  // Abbr
                CheckBoxColumn.HeaderText = absence.Attribute("Name").Value; // Name
                CheckBoxColumn.Width = 42;
                dataGridViewX1.Columns.Insert(index, CheckBoxColumn);
                // 紀錄缺曠類別第幾個欄位
                abIndexDic.Add(index++, absence.Attribute("Name").Value);
            }

            DataGridViewComboBoxColumn dgvcbx = new DataGridViewComboBoxColumn();
            dgvcbx.Items.Add("班導師");
            dgvcbx.Items.Add("授課教師");
            dgvcbx.Name = "actor";
            dgvcbx.HeaderText = "點名對象";
            dataGridViewX1.Columns.Insert(index, dgvcbx);

            #endregion

            #region row
            int row = 0;
            int _actor = dataGridViewX1.Columns["actor"].Index;
            int _starTime = dataGridViewX1.Columns["star_time"].Index;
            int _endTime = dataGridViewX1.Columns["end_time"].Index;

            foreach (XElement p in periodList)
            {
                DataGridViewRow datarow = new DataGridViewRow();
                datarow.CreateCells(dataGridViewX1);
                datarow.Cells[0].Value = p.Attribute("Name").Value;
                List<string> abbrList = new List<string>();

                int column = 0;
                foreach (AbsenceUDT data in SettingData)
                {

                    // 節次符合
                    if (data.Period == p.Attribute("Name").Value && data.Absence != null)
                    {
                        for (int i = 2; i < abIndexDic.Count() + 2 ; i++)
                        {
                            string search = data.Absence;
                            // 在上設定中搜尋到相同缺曠類別設定，checkbox勾起來
                            if (search.IndexOf("\"Name\":\"" + abIndexDic[i] + "\"") > 0)
                            {
                                datarow.Cells[i].Value = true;
                                abbrList.Add(dataGridViewX1.Columns[i].Name);
                                //datarow.Cells[1].Value += dataGridViewX1.Columns[i].Name + " ";
                            }
                        }
                        datarow.Cells[_actor].Value = data.Actor == "" ? "授課教師" : data.Actor;
                        datarow.Cells[_starTime].Value = data.StarTime.ToString() == "0001/1/1 上午 12:00:00" ? "00:00:00" : data.StarTime.ToString("%H:mm:ss");
                        datarow.Cells[_endTime].Value = data.EndTime.ToString() == "0001/1/1 上午 12:00:00" ? "00:00:00" : data.EndTime.ToString("%H:mm:ss");
                    }
                    
                    datarow.Cells[1].Value = string.Join("、", abbrList);

                    column++;
                }
                row++;
                dataGridViewX1.Rows.Add(datarow);
            }

            dataGridViewX1.Rows[0].Cells[_starTime].ReadOnly = false;
            dataGridViewX1.Rows[0].Cells[_starTime].Style.BackColor = Color.White;

            #endregion
            
            #endregion

            initFinish = true;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            AccessHelper access = new AccessHelper();
            List<AbsenceUDT> absenceList = access.Select<AbsenceUDT>();
            access.DeletedValues(absenceList);

            List<AbsenceUDT> InsertAbList = new List<AbsenceUDT>();
            foreach (DataGridViewRow dr in dataGridViewX1.Rows)
            {
                AbsenceUDT udt = new AbsenceUDT();
                List<string> jsonList = new List<string>();

                for (int i = 2;i < dr.Cells["actor"].ColumnIndex;i++)
                {
                    if (dr.Cells[i].Value != null)
                    {
                        if (bool.Parse("" + dr.Cells[i].Value) == true)
                        {
                            string name = dataGridViewX1.Columns[i].HeaderText;
                            string abbr = dataGridViewX1.Columns[i].Name;
                            jsonList .Add( "{" + string.Format("\"Abbr\":\"{0}\",\"Name\":\"{1}\"", abbr, name) + "}");
                        }
                    }
                }
                
                if (jsonList.Count > 0)
                {
                    udt.Absence = "[" +  string.Join(",", jsonList) + "]";
                    udt.Period = "" + dr.Cells["period"].Value;
                    InsertAbList.Add(udt);
                }

                udt.Actor = "" + dr.Cells["actor"].Value;
                udt.StarTime = DateTime.Parse(("" + dr.Cells["star_time"].Value) == "" ? "00:00:00" : ("" + dr.Cells["star_time"].Value));
                udt.EndTime = DateTime.Parse(("" + dr.Cells["end_time"].Value) == "" ? "00:00:00" : ("" + dr.Cells["end_time"].Value));
            }
            access.InsertValues(InsertAbList);
            MsgBox.Show("儲存成功!!");
            this.Close();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewX1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (initFinish)
            {
                if (e.RowIndex >= 0 && (abIndexDic.Count() + 1) >= e.ColumnIndex && e.ColumnIndex > 1)
                {
                    List<string> abbrList = new List<string>();
                    dataGridViewX1.Rows[e.RowIndex].Cells[1].Value = "";

                    for (int i = 2; i < (2 + abIndexDic.Count()); i++)
                    {
                        if (dataGridViewX1.Rows[e.RowIndex].Cells[i].Value != null)
                        {
                            if (bool.Parse("" + dataGridViewX1.Rows[e.RowIndex].Cells[i].Value) == true)
                            {
                                abbrList.Add(dataGridViewX1.Columns[i].Name);
                            }
                        }
                    }
                    dataGridViewX1.Rows[e.RowIndex].Cells[1].Value = string.Join("、", abbrList);
                }
                if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewX1.Columns["end_time"].Index && e.RowIndex + 1 < _endRowIndex)
                {
                    int col = dataGridViewX1.Columns["star_time"].Index;

                    dataGridViewX1.Rows[e.RowIndex + 1].Cells[col].Value = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }
            }
            
        }
    }
}
