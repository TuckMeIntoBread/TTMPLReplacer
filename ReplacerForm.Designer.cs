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
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.btnConvert = new System.Windows.Forms.Button();
            this.cBoxSkin = new System.Windows.Forms.ComboBox();
            this.cBoxPube = new System.Windows.Forms.ComboBox();
            this.labelSkin = new System.Windows.Forms.Label();
            this.labelConvert = new System.Windows.Forms.Label();
            this.labelPube = new System.Windows.Forms.Label();
            this.cBoxConversion = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(195, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(593, 426);
            this.propertyGrid1.TabIndex = 0;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(12, 409);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(159, 29);
            this.btnConvert.TabIndex = 3;
            this.btnConvert.Text = "Convert Files";
            this.btnConvert.UseVisualStyleBackColor = true;
            // 
            // cBoxSkin
            // 
            this.cBoxSkin.FormattingEnabled = true;
            this.cBoxSkin.Location = new System.Drawing.Point(7, 27);
            this.cBoxSkin.Name = "cBoxSkin";
            this.cBoxSkin.Size = new System.Drawing.Size(182, 23);
            this.cBoxSkin.TabIndex = 4;
            // 
            // cBoxPube
            // 
            this.cBoxPube.FormattingEnabled = true;
            this.cBoxPube.Items.AddRange(new object[] {
            "Gen 3",
            "Bibo+"});
            this.cBoxPube.Location = new System.Drawing.Point(7, 71);
            this.cBoxPube.Name = "cBoxPube";
            this.cBoxPube.Size = new System.Drawing.Size(182, 23);
            this.cBoxPube.TabIndex = 5;
            // 
            // labelSkin
            // 
            this.labelSkin.AutoSize = true;
            this.labelSkin.Location = new System.Drawing.Point(7, 9);
            this.labelSkin.Name = "labelSkin";
            this.labelSkin.Size = new System.Drawing.Size(74, 15);
            this.labelSkin.TabIndex = 6;
            this.labelSkin.Text = "Old Skin Slot";
            // 
            // labelConvert
            // 
            this.labelConvert.AutoSize = true;
            this.labelConvert.Location = new System.Drawing.Point(7, 110);
            this.labelConvert.Name = "labelConvert";
            this.labelConvert.Size = new System.Drawing.Size(64, 15);
            this.labelConvert.TabIndex = 7;
            this.labelConvert.Text = "Convert To";
            // 
            // labelPube
            // 
            this.labelPube.AutoSize = true;
            this.labelPube.Location = new System.Drawing.Point(7, 53);
            this.labelPube.Name = "labelPube";
            this.labelPube.Size = new System.Drawing.Size(79, 15);
            this.labelPube.TabIndex = 8;
            this.labelPube.Text = "Old Pube Slot";
            // 
            // cBoxConversion
            // 
            this.cBoxConversion.FormattingEnabled = true;
            this.cBoxConversion.Items.AddRange(new object[] {
            "Gen 3",
            "Bibo+"});
            this.cBoxConversion.Location = new System.Drawing.Point(7, 128);
            this.cBoxConversion.Name = "cBoxConversion";
            this.cBoxConversion.Size = new System.Drawing.Size(182, 23);
            this.cBoxConversion.TabIndex = 9;
            // 
            // ReplacerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cBoxConversion);
            this.Controls.Add(this.labelPube);
            this.Controls.Add(this.labelConvert);
            this.Controls.Add(this.labelSkin);
            this.Controls.Add(this.cBoxPube);
            this.Controls.Add(this.cBoxSkin);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.propertyGrid1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplacerForm";
            this.Text = "TexTools MPL Replacer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.ComboBox cBoxSkin;
        private System.Windows.Forms.ComboBox cBoxPube;
        private System.Windows.Forms.Label labelSkin;
        private System.Windows.Forms.Label labelConvert;
        private System.Windows.Forms.Label labelPube;
        private System.Windows.Forms.ComboBox cBoxConversion;
    }
}