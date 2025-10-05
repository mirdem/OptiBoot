namespace OptiBoot
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            labelDesc = new Label();
            pictureBox1 = new PictureBox();
            linkLabelXLink = new LinkLabel();
            linkLabelAllProjects = new LinkLabel();
            linkLabelFeedback = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // labelDesc
            // 
            labelDesc.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelDesc.Location = new Point(23, 198);
            labelDesc.Name = "labelDesc";
            labelDesc.Size = new Size(255, 180);
            labelDesc.TabIndex = 0;
            labelDesc.Text = "A lightweight PC utility that lets you easily manage startup applications, control system services, and view detailed hardware information—all in one place.";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(49, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(168, 163);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // linkLabelXLink
            // 
            linkLabelXLink.AutoSize = true;
            linkLabelXLink.LinkColor = Color.FromArgb(192, 192, 255);
            linkLabelXLink.Location = new Point(15, 388);
            linkLabelXLink.Name = "linkLabelXLink";
            linkLabelXLink.Size = new Size(109, 15);
            linkLabelXLink.TabIndex = 2;
            linkLabelXLink.TabStop = true;
            linkLabelXLink.Text = "X (formerly Twitter)";
            linkLabelXLink.LinkClicked += linkLabelXLink_LinkClicked;
            // 
            // linkLabelAllProjects
            // 
            linkLabelAllProjects.AutoSize = true;
            linkLabelAllProjects.LinkColor = Color.FromArgb(192, 192, 255);
            linkLabelAllProjects.Location = new Point(15, 415);
            linkLabelAllProjects.Name = "linkLabelAllProjects";
            linkLabelAllProjects.Size = new Size(66, 15);
            linkLabelAllProjects.TabIndex = 2;
            linkLabelAllProjects.TabStop = true;
            linkLabelAllProjects.Text = "All Projects";
            linkLabelAllProjects.LinkClicked += linkLabelAllProjects_LinkClicked;
            // 
            // linkLabelFeedback
            // 
            linkLabelFeedback.AutoSize = true;
            linkLabelFeedback.LinkColor = Color.FromArgb(192, 192, 255);
            linkLabelFeedback.Location = new Point(15, 445);
            linkLabelFeedback.Name = "linkLabelFeedback";
            linkLabelFeedback.Size = new Size(57, 15);
            linkLabelFeedback.TabIndex = 2;
            linkLabelFeedback.TabStop = true;
            linkLabelFeedback.Text = "Feedback";
            linkLabelFeedback.LinkClicked += linkLabelFeedback_LinkClicked;
            // 
            // FormAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(290, 473);
            Controls.Add(linkLabelFeedback);
            Controls.Add(linkLabelAllProjects);
            Controls.Add(linkLabelXLink);
            Controls.Add(pictureBox1);
            Controls.Add(labelDesc);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FormAbout";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "About";
            Load += FormAbout_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelDesc;
        private PictureBox pictureBox1;
        private LinkLabel linkLabelXLink;
        private LinkLabel linkLabelAllProjects;
        private LinkLabel linkLabelFeedback;
    }
}