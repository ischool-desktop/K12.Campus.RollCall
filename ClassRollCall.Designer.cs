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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.dateTimeLb = new DevComponents.DotNetBar.LabelX();
            this.periodLb = new DevComponents.DotNetBar.LabelX();
            this.classLb = new DevComponents.DotNetBar.LabelX();
            this.exportBtn = new DevComponents.DotNetBar.ButtonX();
            this.teacher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.course = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.className = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seatNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stuNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rollCallLog = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attendance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rollCallTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addBtn = new DevComponents.DotNetBar.ButtonX();
            this.detailLb = new DevComponents.DotNetBar.LabelX();
            this.clearBtn = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.AllowUserToResizeColumns = false;
            this.dataGridViewX1.AllowUserToResizeRows = false;
            this.dataGridViewX1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.attendance,
            this.rollCallTime});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.HighlightSelectedColumnHeaders = false;
            this.dataGridViewX1.Location = new System.Drawing.Point(12, 41);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.RowHeadersVisible = false;
            this.dataGridViewX1.RowTemplate.Height = 24;
            this.dataGridViewX1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewX1.Size = new System.Drawing.Size(821, 435);
            this.dataGridViewX1.TabIndex = 0;
            this.dataGridViewX1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewX1_MouseDown);
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
            // exportBtn
            // 
            this.exportBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.exportBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportBtn.BackColor = System.Drawing.Color.Transparent;
            this.exportBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.exportBtn.Location = new System.Drawing.Point(12, 484);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(75, 23);
            this.exportBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.exportBtn.TabIndex = 7;
            this.exportBtn.Text = "匯出";
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // teacher
            // 
            this.teacher.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.teacher.HeaderText = "教師";
            this.teacher.MinimumWidth = 85;
            this.teacher.Name = "teacher";
            this.teacher.Width = 85;
            // 
            // course
            // 
            this.course.HeaderText = "課程";
            this.course.MinimumWidth = 85;
            this.course.Name = "course";
            this.course.Visible = false;
            this.course.Width = 120;
            // 
            // className
            // 
            this.className.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.className.HeaderText = "班級";
            this.className.MinimumWidth = 85;
            this.className.Name = "className";
            this.className.Width = 85;
            // 
            // seatNo
            // 
            this.seatNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.seatNo.HeaderText = "座號";
            this.seatNo.MinimumWidth = 85;
            this.seatNo.Name = "seatNo";
            this.seatNo.Width = 85;
            // 
            // stuNumber
            // 
            this.stuNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.stuNumber.HeaderText = "學號";
            this.stuNumber.MinimumWidth = 85;
            this.stuNumber.Name = "stuNumber";
            this.stuNumber.Width = 85;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.name.HeaderText = "姓名";
            this.name.MinimumWidth = 85;
            this.name.Name = "name";
            this.name.Width = 85;
            // 
            // rollCallLog
            // 
            this.rollCallLog.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.rollCallLog.HeaderText = "老師點名紀錄";
            this.rollCallLog.MinimumWidth = 85;
            this.rollCallLog.Name = "rollCallLog";
            this.rollCallLog.Width = 111;
            // 
            // attendance
            // 
            this.attendance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.attendance.HeaderText = "目前缺曠紀錄";
            this.attendance.MinimumWidth = 85;
            this.attendance.Name = "attendance";
            this.attendance.Width = 111;
            // 
            // rollCallTime
            // 
            this.rollCallTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.rollCallTime.HeaderText = "點名時間";
            this.rollCallTime.MinimumWidth = 120;
            this.rollCallTime.Name = "rollCallTime";
            this.rollCallTime.Width = 120;
            // 
            // addBtn
            // 
            this.addBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.addBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addBtn.BackColor = System.Drawing.Color.Transparent;
            this.addBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.addBtn.Location = new System.Drawing.Point(93, 484);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(92, 23);
            this.addBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.addBtn.TabIndex = 8;
            this.addBtn.Text = "加入待處理";
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // detailLb
            // 
            this.detailLb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.detailLb.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.detailLb.BackgroundStyle.Class = "";
            this.detailLb.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.detailLb.Location = new System.Drawing.Point(289, 484);
            this.detailLb.Name = "detailLb";
            this.detailLb.Size = new System.Drawing.Size(200, 23);
            this.detailLb.TabIndex = 9;
            this.detailLb.Visible = false;
            // 
            // clearBtn
            // 
            this.clearBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.clearBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearBtn.BackColor = System.Drawing.Color.Transparent;
            this.clearBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.clearBtn.Location = new System.Drawing.Point(191, 484);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(92, 23);
            this.clearBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.clearBtn.TabIndex = 10;
            this.clearBtn.Text = "清空待處理";
            this.clearBtn.Visible = false;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // ClassRollCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 512);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.detailLb);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.exportBtn);
            this.Controls.Add(this.classLb);
            this.Controls.Add(this.periodLb);
            this.Controls.Add(this.dateTimeLb);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.dataGridViewX1);
            this.DoubleBuffered = true;
            this.MaximizeBox = true;
            this.Name = "ClassRollCall";
            this.Text = "班級課堂點名明細";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX dateTimeLb;
        private DevComponents.DotNetBar.LabelX periodLb;
        private DevComponents.DotNetBar.LabelX classLb;
        private DevComponents.DotNetBar.ButtonX exportBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn teacher;
        private System.Windows.Forms.DataGridViewTextBoxColumn course;
        private System.Windows.Forms.DataGridViewTextBoxColumn className;
        private System.Windows.Forms.DataGridViewTextBoxColumn seatNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn stuNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn rollCallLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn attendance;
        private System.Windows.Forms.DataGridViewTextBoxColumn rollCallTime;
        private DevComponents.DotNetBar.ButtonX addBtn;
        private DevComponents.DotNetBar.LabelX detailLb;
        private DevComponents.DotNetBar.ButtonX clearBtn;
    }
}