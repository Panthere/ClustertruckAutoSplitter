namespace CTPatcher
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbByLevel = new System.Windows.Forms.RadioButton();
            this.rbByWorld = new System.Windows.Forms.RadioButton();
            this.btnPatch = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.txtPipeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSleepMax = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtInstall = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbByLevel);
            this.groupBox1.Controls.Add(this.rbByWorld);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 98);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Split Settings";
            // 
            // rbByLevel
            // 
            this.rbByLevel.AutoSize = true;
            this.rbByLevel.Location = new System.Drawing.Point(24, 51);
            this.rbByLevel.Name = "rbByLevel";
            this.rbByLevel.Size = new System.Drawing.Size(71, 17);
            this.rbByLevel.TabIndex = 3;
            this.rbByLevel.Text = "By Levels";
            this.rbByLevel.UseVisualStyleBackColor = true;
            // 
            // rbByWorld
            // 
            this.rbByWorld.AutoSize = true;
            this.rbByWorld.Checked = true;
            this.rbByWorld.Location = new System.Drawing.Point(24, 28);
            this.rbByWorld.Name = "rbByWorld";
            this.rbByWorld.Size = new System.Drawing.Size(68, 17);
            this.rbByWorld.TabIndex = 2;
            this.rbByWorld.TabStop = true;
            this.rbByWorld.Text = "By World";
            this.rbByWorld.UseVisualStyleBackColor = true;
            // 
            // btnPatch
            // 
            this.btnPatch.Location = new System.Drawing.Point(245, 145);
            this.btnPatch.Name = "btnPatch";
            this.btnPatch.Size = new System.Drawing.Size(91, 23);
            this.btnPatch.TabIndex = 1;
            this.btnPatch.Text = "Patch/Update";
            this.btnPatch.UseVisualStyleBackColor = true;
            this.btnPatch.Click += new System.EventHandler(this.btnPatch_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(12, 145);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 2;
            this.btnRestore.Text = "Unpatch";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // txtPipeName
            // 
            this.txtPipeName.Location = new System.Drawing.Point(74, 54);
            this.txtPipeName.Name = "txtPipeName";
            this.txtPipeName.Size = new System.Drawing.Size(63, 20);
            this.txtPipeName.TabIndex = 0;
            this.txtPipeName.Text = "LiveSplit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pipe Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sleep (ms)";
            // 
            // txtSleepMax
            // 
            this.txtSleepMax.Location = new System.Drawing.Point(74, 28);
            this.txtSleepMax.Name = "txtSleepMax";
            this.txtSleepMax.Size = new System.Drawing.Size(63, 20);
            this.txtSleepMax.TabIndex = 4;
            this.txtSleepMax.Text = "10";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSleepMax);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtPipeName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(177, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(159, 98);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other Settings";
            // 
            // txtInstall
            // 
            this.txtInstall.Location = new System.Drawing.Point(69, 15);
            this.txtInstall.Name = "txtInstall";
            this.txtInstall.Size = new System.Drawing.Size(237, 20);
            this.txtInstall.TabIndex = 7;
            this.txtInstall.TextChanged += new System.EventHandler(this.txtInstall_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Game File";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(312, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(24, 23);
            this.btnBrowse.TabIndex = 9;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 176);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtInstall);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnPatch);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clustertruck AutoSplit Patcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPipeName;
        private System.Windows.Forms.Button btnPatch;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.RadioButton rbByLevel;
        private System.Windows.Forms.RadioButton rbByWorld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSleepMax;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtInstall;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBrowse;
    }
}