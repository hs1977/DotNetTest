namespace WinFormStudy
{
	partial class TestDlg
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
            this.m_BtnOK = new System.Windows.Forms.Button();
            this.m_BtnCancel = new System.Windows.Forms.Button();
            this.m_EditText = new System.Windows.Forms.TextBox();
            this.m_BtnFileDialog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_BtnOK
            // 
            this.m_BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_BtnOK.Location = new System.Drawing.Point(270, 332);
            this.m_BtnOK.Name = "m_BtnOK";
            this.m_BtnOK.Size = new System.Drawing.Size(159, 58);
            this.m_BtnOK.TabIndex = 0;
            this.m_BtnOK.Text = "OK";
            this.m_BtnOK.UseVisualStyleBackColor = true;
            // 
            // m_BtnCancel
            // 
            this.m_BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_BtnCancel.Location = new System.Drawing.Point(435, 332);
            this.m_BtnCancel.Name = "m_BtnCancel";
            this.m_BtnCancel.Size = new System.Drawing.Size(159, 58);
            this.m_BtnCancel.TabIndex = 0;
            this.m_BtnCancel.Text = "Cancel";
            this.m_BtnCancel.UseVisualStyleBackColor = true;
            // 
            // m_EditText
            // 
            this.m_EditText.Location = new System.Drawing.Point(193, 104);
            this.m_EditText.Name = "m_EditText";
            this.m_EditText.Size = new System.Drawing.Size(205, 21);
            this.m_EditText.TabIndex = 1;
            // 
            // m_BtnFileDialog
            // 
            this.m_BtnFileDialog.Location = new System.Drawing.Point(270, 259);
            this.m_BtnFileDialog.Name = "m_BtnFileDialog";
            this.m_BtnFileDialog.Size = new System.Drawing.Size(159, 53);
            this.m_BtnFileDialog.TabIndex = 2;
            this.m_BtnFileDialog.Text = "FileDialog";
            this.m_BtnFileDialog.UseVisualStyleBackColor = true;
            this.m_BtnFileDialog.Click += new System.EventHandler(this.m_BtnFileDialog_Click);
            // 
            // TestDlg
            // 
            this.AcceptButton = this.m_BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_BtnCancel;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_BtnFileDialog);
            this.Controls.Add(this.m_EditText);
            this.Controls.Add(this.m_BtnCancel);
            this.Controls.Add(this.m_BtnOK);
            this.Name = "TestDlg";
            this.Text = "TestDlg";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button m_BtnOK;
		private System.Windows.Forms.Button m_BtnCancel;
		public System.Windows.Forms.TextBox m_EditText;
        private System.Windows.Forms.Button m_BtnFileDialog;
    }
}