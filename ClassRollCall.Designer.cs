namespace K12.Campus.RollCall
{
    partial class ClassRollCall
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.dateTimeLb = new DevComponents.DotNetBar.LabelX();
            this.periodLb = new DevComponents.DotNetBar.LabelX();
            this.classLb = new DevComponents.DotNetBar.LabelX();
            this.teacher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.course = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.className = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seatNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stuNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rollCallLog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attendance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.AllowUserToResizeColumns = false;
            this.dataGridViewX1.AllowUserToResizeRows = false;
            this.dataGridViewX1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewX1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.teacher,
            this.course,
            this.className,
            this.seatNo,
            this.stuNumber,
            this.name,
            this.rollCallLog,
            this.attendance});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(12, 41);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowHeadersVisible = false;
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.Size = new System.Drawing.Size(744, 436);
            this.dataGridViewX1.TabIndex = 0;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(38, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "日期:";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(174, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(38, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "節次:";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(327, 12);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(38, 23);
            this.labelX3.TabIndex = 3;
            this.labelX3.Text = "班級:";
            // 
            // dateTimeLb
            // 
            this.dateTimeLb.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.dateTimeLb.BackgroundStyle.Class = "";
            this.dateTimeLb.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeLb.ForeColor = System.Drawing.Color.Blue;
            this.dateTimeLb.Location = new System.Drawing.Point(56, 12);
            this.dateTimeLb.Name = "dateTimeLb";
            this.dateTimeLb.Size = new System.Drawing.Size(112, 23);
            this.dateTimeLb.TabIndex = 4;
            // 
            // periodLb
            // 
            this.periodLb.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.periodLb.BackgroundStyle.Class = "";
            this.periodLb.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.periodLb.ForeColor = System.Drawing.Color.Blue;
            this.periodLb.Location = new System.Drawing.Point(219, 12);
            this.periodLb.Name = "periodLb";
            this.periodLb.Size = new System.Drawing.Size(75, 23);
            this.periodLb.TabIndex = 5;
            // 
            // classLb
            // 
            this.classLb.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.classLb.BackgroundStyle.Class = "";
            this.classLb.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.classLb.ForeColor = System.Drawing.Color.Blue;
            this.classLb.Location = new System.Drawing.Point(372, 12);
            this.classLb.Name = "classLb";
            this.classLb.Size = new System.Drawing.Size(75, 23);
            this.classLb.TabIndex = 6;
            // 
            // teacher
            // 
            this.teacher.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.teacher.HeaderText = "教師";
            this.teacher.Name = "teacher";
            this.teacher.Width = 59;
            // 
            // course
            // 
            this.course.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.course.HeaderText = "課程";
            this.course.Name = "course";
            this.course.Width = 59;
            // 
            // className
            // 
            this.className.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.className.HeaderText = "班級";
            this.className.Name = "className";
            this.className.Width = 59;
            // 
            // seatNo
            // 
            this.seatNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.seatNo.HeaderText = "座號";
            this.seatNo.Name = "seatNo";
            this.seatNo.Width = 59;
            // 
            // stuNumber
            // 
            this.stuNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.stuNumber.HeaderText = "學號";
            this.stuNumber.Name = "stuNumber";
            this.stuNumber.Width = 59;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.name.HeaderText = "姓名";
            this.name.Name = "name";
            this.name.Width = 59;
            // 
            // rollCallLog
            // 
            this.rollCallLog.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.rollCallLog.HeaderText = "點名紀錄";
            this.rollCallLog.Name = "rollCallLog";
            this.rollCallLog.Width = 85;
            // 
            // attendance
            // 
            this.attendance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.attendance.HeaderText = "系統紀錄";
            this.attendance.Name = "attendance";
            this.attendance.Width = 85;
            // 
            // ClassRollCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 509);
            this.Controls.Add(this.classLb);
            this.Controls.Add(this.periodLb);
            this.Controls.Add(this.dateTimeLb);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.dataGridViewX1);
            this.DoubleBuffered = true;
            this.Name = "ClassRollCall";
            this.Text = "課堂點名清單";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher;
        private System.Windows.Forms.DataGridViewTextBoxColumn course;
        private System.Windows.Forms.DataGridViewTextBoxColumn className;
        private System.Windows.Forms.DataGridViewTextBoxColumn seatNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn stuNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn rollCallLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn attendance;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX dateTimeLb;
        private DevComponents.DotNetBar.LabelX periodLb;
        private DevComponents.DotNetBar.LabelX classLb;
    }
}