namespace StudentNotesFeedClient
{
    partial class StudentNotesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.StudentIdInput = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.FeedFormatInput = new System.Windows.Forms.ComboBox();
            this.ContentTextBox = new System.Windows.Forms.RichTextBox();
            this.FetchButton = new System.Windows.Forms.Button();
            this.RawContentTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.StudentIdInput)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Student ID";
            // 
            // StudentIdInput
            // 
            this.StudentIdInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StudentIdInput.Location = new System.Drawing.Point(18, 41);
            this.StudentIdInput.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.StudentIdInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.StudentIdInput.Name = "StudentIdInput";
            this.StudentIdInput.Size = new System.Drawing.Size(120, 30);
            this.StudentIdInput.TabIndex = 1;
            this.StudentIdInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(176, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Feed Format";
            // 
            // FeedFormatInput
            // 
            this.FeedFormatInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.FeedFormatInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.FeedFormatInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FeedFormatInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FeedFormatInput.FormattingEnabled = true;
            this.FeedFormatInput.Location = new System.Drawing.Point(181, 41);
            this.FeedFormatInput.Name = "FeedFormatInput";
            this.FeedFormatInput.Size = new System.Drawing.Size(121, 33);
            this.FeedFormatInput.TabIndex = 3;
            // 
            // ContentTextBox
            // 
            this.ContentTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ContentTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ContentTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ContentTextBox.Location = new System.Drawing.Point(18, 94);
            this.ContentTextBox.Name = "ContentTextBox";
            this.ContentTextBox.ReadOnly = true;
            this.ContentTextBox.Size = new System.Drawing.Size(520, 447);
            this.ContentTextBox.TabIndex = 4;
            this.ContentTextBox.Text = "";
            // 
            // FetchButton
            // 
            this.FetchButton.BackColor = System.Drawing.SystemColors.Info;
            this.FetchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FetchButton.Location = new System.Drawing.Point(339, 13);
            this.FetchButton.Name = "FetchButton";
            this.FetchButton.Size = new System.Drawing.Size(667, 61);
            this.FetchButton.TabIndex = 5;
            this.FetchButton.Text = "FETCH";
            this.FetchButton.UseVisualStyleBackColor = false;
            this.FetchButton.Click += new System.EventHandler(this.FetchButton_Click);
            // 
            // RawContentTextBox
            // 
            this.RawContentTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RawContentTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RawContentTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RawContentTextBox.Location = new System.Drawing.Point(559, 94);
            this.RawContentTextBox.Name = "RawContentTextBox";
            this.RawContentTextBox.ReadOnly = true;
            this.RawContentTextBox.Size = new System.Drawing.Size(447, 447);
            this.RawContentTextBox.TabIndex = 6;
            this.RawContentTextBox.Text = "";
            // 
            // StudentNotesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 560);
            this.Controls.Add(this.RawContentTextBox);
            this.Controls.Add(this.FetchButton);
            this.Controls.Add(this.ContentTextBox);
            this.Controls.Add(this.FeedFormatInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StudentIdInput);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "StudentNotesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Student Notes Form";
            ((System.ComponentModel.ISupportInitialize)(this.StudentIdInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown StudentIdInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox FeedFormatInput;
        private System.Windows.Forms.RichTextBox ContentTextBox;
        private System.Windows.Forms.Button FetchButton;
        private System.Windows.Forms.RichTextBox RawContentTextBox;
    }
}

