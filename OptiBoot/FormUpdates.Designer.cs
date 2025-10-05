namespace OptiBoot
{
    partial class FormUpdates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUpdates));
            buttonCheck = new Button();
            buttonDownload = new Button();
            pictureBoxLoad = new PictureBox();
            lblUpdateStatus = new Label();
            progressBar1 = new ProgressBar();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLoad).BeginInit();
            SuspendLayout();
            // 
            // buttonCheck
            // 
            buttonCheck.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonCheck.FlatStyle = FlatStyle.Flat;
            buttonCheck.Location = new Point(322, 173);
            buttonCheck.Name = "buttonCheck";
            buttonCheck.Size = new Size(96, 31);
            buttonCheck.TabIndex = 0;
            buttonCheck.Text = "Check";
            buttonCheck.UseVisualStyleBackColor = true;
            buttonCheck.Click += buttonCheck_Click;
            // 
            // buttonDownload
            // 
            buttonDownload.Enabled = false;
            buttonDownload.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonDownload.FlatStyle = FlatStyle.Flat;
            buttonDownload.Location = new Point(220, 173);
            buttonDownload.Name = "buttonDownload";
            buttonDownload.Size = new Size(96, 31);
            buttonDownload.TabIndex = 1;
            buttonDownload.Text = "Download";
            buttonDownload.UseVisualStyleBackColor = true;
            buttonDownload.Visible = false;
            buttonDownload.Click += buttonDownload_Click;
            // 
            // pictureBoxLoad
            // 
            pictureBoxLoad.Image = (Image)resources.GetObject("pictureBoxLoad.Image");
            pictureBoxLoad.Location = new Point(30, 60);
            pictureBoxLoad.Name = "pictureBoxLoad";
            pictureBoxLoad.Size = new Size(69, 64);
            pictureBoxLoad.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLoad.TabIndex = 2;
            pictureBoxLoad.TabStop = false;
            // 
            // lblUpdateStatus
            // 
            lblUpdateStatus.Font = new Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblUpdateStatus.Location = new Point(105, 76);
            lblUpdateStatus.Name = "lblUpdateStatus";
            lblUpdateStatus.Size = new Size(296, 32);
            lblUpdateStatus.TabIndex = 3;
            lblUpdateStatus.Text = "Checking for Updates, please wait...";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 183);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(174, 21);
            progressBar1.TabIndex = 4;
            progressBar1.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 189);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 5;
            label1.Text = "label1";
            // 
            // FormUpdates
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(440, 216);
            Controls.Add(label1);
            Controls.Add(progressBar1);
            Controls.Add(lblUpdateStatus);
            Controls.Add(pictureBoxLoad);
            Controls.Add(buttonDownload);
            Controls.Add(buttonCheck);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(456, 255);
            Name = "FormUpdates";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Updates";
            FormClosed += FormUpdates_FormClosed;
            Load += FormUpdates_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxLoad).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonCheck;
        private Button buttonDownload;
        private PictureBox pictureBoxLoad;
        private Label lblUpdateStatus;
        private ProgressBar progressBar1;
        private Label label1;
    }
}