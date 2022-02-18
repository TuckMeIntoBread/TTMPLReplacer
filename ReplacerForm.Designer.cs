namespace TTMPLReplacer
{
    partial class ReplacerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplacerForm));
            this.btnConvert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.gBoxSkin = new System.Windows.Forms.GroupBox();
            this.cBoxSkinGen3 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cBoxSkinBibo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gBoxPube = new System.Windows.Forms.GroupBox();
            this.cBoxPubeGen3 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cBoxPubeBibo = new System.Windows.Forms.ComboBox();
            this.gBoxSkin.SuspendLayout();
            this.gBoxPube.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(23, 310);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(182, 29);
            this.btnConvert.TabIndex = 3;
            this.btnConvert.Text = "Select File(s)";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Version";
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(17, 390);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(126, 23);
            this.txtVersion.TabIndex = 11;
            this.txtVersion.TabStop = false;
            this.txtVersion.Text = "VersionNumber";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(149, 390);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(55, 23);
            this.btnHelp.TabIndex = 12;
            this.btnHelp.TabStop = false;
            this.btnHelp.Text = "Help!";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // gBoxSkin
            // 
            this.gBoxSkin.Controls.Add(this.cBoxSkinGen3);
            this.gBoxSkin.Controls.Add(this.label3);
            this.gBoxSkin.Controls.Add(this.label2);
            this.gBoxSkin.Controls.Add(this.cBoxSkinBibo);
            this.gBoxSkin.Location = new System.Drawing.Point(12, 12);
            this.gBoxSkin.Name = "gBoxSkin";
            this.gBoxSkin.Size = new System.Drawing.Size(211, 136);
            this.gBoxSkin.TabIndex = 16;
            this.gBoxSkin.TabStop = false;
            this.gBoxSkin.Text = "Skin Options";
            // 
            // cBoxSkinGen3
            // 
            this.cBoxSkinGen3.FormattingEnabled = true;
            this.cBoxSkinGen3.Items.AddRange(new object[] {
            "Gen 3",
            "Bibo+"});
            this.cBoxSkinGen3.Location = new System.Drawing.Point(11, 91);
            this.cBoxSkinGen3.Name = "cBoxSkinGen3";
            this.cBoxSkinGen3.Size = new System.Drawing.Size(182, 23);
            this.cBoxSkinGen3.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "Gen3 Slot";
            // 
            // cBoxSkinBibo
            // 
            this.cBoxSkinBibo.FormattingEnabled = true;
            this.cBoxSkinBibo.Items.AddRange(new object[] {
            "Gen 3",
            "Bibo+"});
            this.cBoxSkinBibo.Location = new System.Drawing.Point(11, 37);
            this.cBoxSkinBibo.Name = "cBoxSkinBibo";
            this.cBoxSkinBibo.Size = new System.Drawing.Size(182, 23);
            this.cBoxSkinBibo.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Bibo Slot";
            // 
            // gBoxPube
            // 
            this.gBoxPube.Controls.Add(this.cBoxPubeGen3);
            this.gBoxPube.Controls.Add(this.label4);
            this.gBoxPube.Controls.Add(this.label5);
            this.gBoxPube.Controls.Add(this.cBoxPubeBibo);
            this.gBoxPube.Location = new System.Drawing.Point(12, 154);
            this.gBoxPube.Name = "gBoxPube";
            this.gBoxPube.Size = new System.Drawing.Size(211, 136);
            this.gBoxPube.TabIndex = 21;
            this.gBoxPube.TabStop = false;
            this.gBoxPube.Text = "Pube Options";
            // 
            // cBoxPubeGen3
            // 
            this.cBoxPubeGen3.FormattingEnabled = true;
            this.cBoxPubeGen3.Items.AddRange(new object[] {
            "Gen 3",
            "Bibo+"});
            this.cBoxPubeGen3.Location = new System.Drawing.Point(11, 91);
            this.cBoxPubeGen3.Name = "cBoxPubeGen3";
            this.cBoxPubeGen3.Size = new System.Drawing.Size(182, 23);
            this.cBoxPubeGen3.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "Bibo Slot";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "Gen3 Slot";
            // 
            // cBoxPubeBibo
            // 
            this.cBoxPubeBibo.FormattingEnabled = true;
            this.cBoxPubeBibo.Items.AddRange(new object[] {
            "Gen 3",
            "Bibo+"});
            this.cBoxPubeBibo.Location = new System.Drawing.Point(11, 37);
            this.cBoxPubeBibo.Name = "cBoxPubeBibo";
            this.cBoxPubeBibo.Size = new System.Drawing.Size(182, 23);
            this.cBoxPubeBibo.TabIndex = 18;
            // 
            // ReplacerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 435);
            this.Controls.Add(this.gBoxPube);
            this.Controls.Add(this.gBoxSkin);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConvert);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplacerForm";
            this.Text = "TexTools MPL Replacer";
            this.Load += new System.EventHandler(this.ReplacerForm_Load);
            this.gBoxSkin.ResumeLayout(false);
            this.gBoxSkin.PerformLayout();
            this.gBoxPube.ResumeLayout(false);
            this.gBoxPube.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.GroupBox gBoxSkin;
        private System.Windows.Forms.ComboBox cBoxSkinGen3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBoxSkinBibo;
        private System.Windows.Forms.GroupBox gBoxPube;
        private System.Windows.Forms.ComboBox cBoxPubeGen3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cBoxPubeBibo;
    }
}