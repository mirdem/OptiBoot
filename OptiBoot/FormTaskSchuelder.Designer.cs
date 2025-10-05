namespace OptiBoot
{
    partial class FormTaskSchuelder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTaskSchuelder));
            textBoxSearch = new TextBox();
            buttonRefresh = new Button();
            buttonDeleteTask = new Button();
            buttonTaskEdit = new Button();
            labelSearch = new Label();
            dataGridView1 = new DataGridView();
            ColumnName = new DataGridViewTextBoxColumn();
            ColumnDescription = new DataGridViewTextBoxColumn();
            ColumnTime = new DataGridViewTextBoxColumn();
            ColumnStatus = new DataGridViewTextBoxColumn();
            ColumnRepeat = new DataGridViewTextBoxColumn();
            buttonTaskNew = new Button();
            buttonExport = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(69, 154);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(203, 23);
            textBoxSearch.TabIndex = 0;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRefresh.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonRefresh.FlatStyle = FlatStyle.Flat;
            buttonRefresh.Image = (Image)resources.GetObject("buttonRefresh.Image");
            buttonRefresh.ImageAlign = ContentAlignment.MiddleLeft;
            buttonRefresh.Location = new Point(627, 155);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(127, 39);
            buttonRefresh.TabIndex = 1;
            buttonRefresh.Text = "Yenile";
            buttonRefresh.TextAlign = ContentAlignment.MiddleRight;
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // buttonDeleteTask
            // 
            buttonDeleteTask.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonDeleteTask.FlatStyle = FlatStyle.Flat;
            buttonDeleteTask.Image = (Image)resources.GetObject("buttonDeleteTask.Image");
            buttonDeleteTask.ImageAlign = ContentAlignment.MiddleLeft;
            buttonDeleteTask.Location = new Point(12, 12);
            buttonDeleteTask.Name = "buttonDeleteTask";
            buttonDeleteTask.Size = new Size(127, 39);
            buttonDeleteTask.TabIndex = 2;
            buttonDeleteTask.Text = "Görevi Sil";
            buttonDeleteTask.TextAlign = ContentAlignment.MiddleRight;
            buttonDeleteTask.UseVisualStyleBackColor = true;
            buttonDeleteTask.Click += buttonDeleteTask_Click;
            // 
            // buttonTaskEdit
            // 
            buttonTaskEdit.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonTaskEdit.FlatStyle = FlatStyle.Flat;
            buttonTaskEdit.Image = (Image)resources.GetObject("buttonTaskEdit.Image");
            buttonTaskEdit.ImageAlign = ContentAlignment.MiddleLeft;
            buttonTaskEdit.Location = new Point(145, 12);
            buttonTaskEdit.Name = "buttonTaskEdit";
            buttonTaskEdit.Size = new Size(127, 39);
            buttonTaskEdit.TabIndex = 3;
            buttonTaskEdit.Text = "Görevi Düzenle";
            buttonTaskEdit.TextAlign = ContentAlignment.MiddleRight;
            buttonTaskEdit.UseVisualStyleBackColor = true;
            buttonTaskEdit.Click += buttonTaskEdit_Click;
            // 
            // labelSearch
            // 
            labelSearch.AutoSize = true;
            labelSearch.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelSearch.Location = new Point(12, 155);
            labelSearch.Name = "labelSearch";
            labelSearch.Size = new Size(29, 17);
            labelSearch.TabIndex = 4;
            labelSearch.Text = "Ara";
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { ColumnName, ColumnDescription, ColumnTime, ColumnStatus, ColumnRepeat });
            dataGridView1.Location = new Point(12, 196);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(752, 417);
            dataGridView1.TabIndex = 5;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            // 
            // ColumnName
            // 
            ColumnName.HeaderText = "Adı";
            ColumnName.Name = "ColumnName";
            ColumnName.Width = 150;
            // 
            // ColumnDescription
            // 
            ColumnDescription.HeaderText = "Açıklama";
            ColumnDescription.Name = "ColumnDescription";
            ColumnDescription.Width = 250;
            // 
            // ColumnTime
            // 
            ColumnTime.HeaderText = "Zaman";
            ColumnTime.Name = "ColumnTime";
            // 
            // ColumnStatus
            // 
            ColumnStatus.HeaderText = "Durum";
            ColumnStatus.Name = "ColumnStatus";
            // 
            // ColumnRepeat
            // 
            ColumnRepeat.HeaderText = "Tekrar";
            ColumnRepeat.Name = "ColumnRepeat";
            // 
            // buttonTaskNew
            // 
            buttonTaskNew.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonTaskNew.FlatStyle = FlatStyle.Flat;
            buttonTaskNew.Image = (Image)resources.GetObject("buttonTaskNew.Image");
            buttonTaskNew.ImageAlign = ContentAlignment.MiddleLeft;
            buttonTaskNew.Location = new Point(12, 65);
            buttonTaskNew.Name = "buttonTaskNew";
            buttonTaskNew.Size = new Size(127, 39);
            buttonTaskNew.TabIndex = 6;
            buttonTaskNew.Text = "Yeni Görev";
            buttonTaskNew.TextAlign = ContentAlignment.MiddleRight;
            buttonTaskNew.UseVisualStyleBackColor = true;
            buttonTaskNew.Click += buttonTaskNew_Click;
            // 
            // buttonExport
            // 
            buttonExport.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            buttonExport.FlatStyle = FlatStyle.Flat;
            buttonExport.Image = (Image)resources.GetObject("buttonExport.Image");
            buttonExport.ImageAlign = ContentAlignment.MiddleLeft;
            buttonExport.Location = new Point(145, 65);
            buttonExport.Name = "buttonExport";
            buttonExport.Size = new Size(127, 39);
            buttonExport.TabIndex = 7;
            buttonExport.Text = "Export";
            buttonExport.UseVisualStyleBackColor = true;
            buttonExport.Click += buttonExport_Click;
            // 
            // FormTaskSchuelder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(778, 625);
            Controls.Add(buttonExport);
            Controls.Add(buttonTaskNew);
            Controls.Add(dataGridView1);
            Controls.Add(labelSearch);
            Controls.Add(buttonTaskEdit);
            Controls.Add(buttonDeleteTask);
            Controls.Add(buttonRefresh);
            Controls.Add(textBoxSearch);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(794, 664);
            Name = "FormTaskSchuelder";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Görev Zamanlayıcı";
            FormClosed += FormTaskSchuelder_FormClosed;
            Load += FormTaskSchuelder_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxSearch;
        private Button buttonRefresh;
        private Button buttonDeleteTask;
        private Button buttonTaskEdit;
        private Label labelSearch;
        private DataGridView dataGridView1;
        private Button buttonTaskNew;
        private DataGridViewTextBoxColumn ColumnName;
        private DataGridViewTextBoxColumn ColumnDescription;
        private DataGridViewTextBoxColumn ColumnTime;
        private DataGridViewTextBoxColumn ColumnStatus;
        private DataGridViewTextBoxColumn ColumnRepeat;
        private Button buttonExport;
    }
}