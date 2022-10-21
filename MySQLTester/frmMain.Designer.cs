namespace MySQLTester
{
    partial class frmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAddr = new System.Windows.Forms.TextBox();
            this.pnTop = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPw = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbElapsedTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbTotalRows = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTest.Location = new System.Drawing.Point(615, 25);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(113, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Connection Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Address";
            // 
            // txtAddr
            // 
            this.txtAddr.Location = new System.Drawing.Point(19, 25);
            this.txtAddr.Name = "txtAddr";
            this.txtAddr.Size = new System.Drawing.Size(120, 21);
            this.txtAddr.TabIndex = 1;
            this.txtAddr.Text = "localhost";
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.label3);
            this.pnTop.Controls.Add(this.label5);
            this.pnTop.Controls.Add(this.label2);
            this.pnTop.Controls.Add(this.label4);
            this.pnTop.Controls.Add(this.label1);
            this.pnTop.Controls.Add(this.txtPw);
            this.pnTop.Controls.Add(this.txtPort);
            this.pnTop.Controls.Add(this.txtDBName);
            this.pnTop.Controls.Add(this.txtId);
            this.pnTop.Controls.Add(this.txtAddr);
            this.pnTop.Controls.Add(this.btnTest);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(733, 58);
            this.pnTop.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "DB Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(445, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "Pw";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(347, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "Id";
            // 
            // txtPw
            // 
            this.txtPw.Location = new System.Drawing.Point(447, 25);
            this.txtPw.Name = "txtPw";
            this.txtPw.Size = new System.Drawing.Size(162, 21);
            this.txtPw.TabIndex = 5;
            this.txtPw.UseSystemPasswordChar = true;
            this.txtPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(145, 25);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(58, 21);
            this.txtPort.TabIndex = 2;
            this.txtPort.Text = "3306";
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(209, 25);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(120, 21);
            this.txtDBName.TabIndex = 3;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(347, 25);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(94, 21);
            this.txtId.TabIndex = 4;
            this.txtId.Text = "root";
            this.txtId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgv.ColumnHeadersHeight = 27;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv.Location = new System.Drawing.Point(0, 116);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.ShowCellErrors = false;
            this.dgv.ShowCellToolTips = false;
            this.dgv.ShowEditingIcon = false;
            this.dgv.ShowRowErrors = false;
            this.dgv.Size = new System.Drawing.Size(733, 253);
            this.dgv.TabIndex = 2;
            this.dgv.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_DataError);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtQuery);
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 58);
            this.panel1.TabIndex = 0;
            // 
            // txtQuery
            // 
            this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuery.Location = new System.Drawing.Point(0, 0);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(631, 58);
            this.txtQuery.TabIndex = 6;
            // 
            // btnRun
            // 
            this.btnRun.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRun.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRun.Location = new System.Drawing.Point(631, 0);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(102, 58);
            this.btnRun.TabIndex = 7;
            this.btnRun.Text = "Run(F9)";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lbElapsedTime,
            this.toolStripStatusLabel3,
            this.lbTotalRows});
            this.statusStrip1.Location = new System.Drawing.Point(0, 369);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(733, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("굴림", 9F);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(93, 17);
            this.toolStripStatusLabel1.Text = "쿼리 응답 시간: ";
            // 
            // lbElapsedTime
            // 
            this.lbElapsedTime.Font = new System.Drawing.Font("굴림", 9F);
            this.lbElapsedTime.Name = "lbElapsedTime";
            this.lbElapsedTime.Size = new System.Drawing.Size(41, 17);
            this.lbElapsedTime.Text = "대기중";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel3.Text = "|";
            // 
            // lbTotalRows
            // 
            this.lbTotalRows.Font = new System.Drawing.Font("굴림", 9F);
            this.lbTotalRows.Name = "lbTotalRows";
            this.lbTotalRows.Size = new System.Drawing.Size(11, 17);
            this.lbTotalRows.Text = "-";
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(733, 391);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(749, 277);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MySQL Tester - HMIPS CodeReaper";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.pnTop.ResumeLayout(false);
            this.pnTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAddr;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPw;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbElapsedTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lbTotalRows;
    }
}

