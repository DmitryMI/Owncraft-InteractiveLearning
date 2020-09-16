namespace InteractiveLearning.UI
{
    partial class LearningTaskDisplay
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
            this.TaskNameLabel = new System.Windows.Forms.Label();
            this.DescriptionBox = new System.Windows.Forms.RichTextBox();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.CheckButton = new System.Windows.Forms.Button();
            this.AnswerBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RandomizeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TaskNameLabel
            // 
            this.TaskNameLabel.AutoSize = true;
            this.TaskNameLabel.Location = new System.Drawing.Point(9, 8);
            this.TaskNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TaskNameLabel.Name = "TaskNameLabel";
            this.TaskNameLabel.Size = new System.Drawing.Size(27, 13);
            this.TaskNameLabel.TabIndex = 0;
            this.TaskNameLabel.Text = "N/A";
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.Location = new System.Drawing.Point(8, 23);
            this.DescriptionBox.Margin = new System.Windows.Forms.Padding(2);
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.Size = new System.Drawing.Size(671, 64);
            this.DescriptionBox.TabIndex = 1;
            this.DescriptionBox.Text = "";
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(8, 91);
            this.PictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(668, 193);
            this.PictureBox.TabIndex = 2;
            this.PictureBox.TabStop = false;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(11, 424);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(2);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(61, 26);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "OK";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // CheckButton
            // 
            this.CheckButton.Location = new System.Drawing.Point(605, 424);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(75, 26);
            this.CheckButton.TabIndex = 4;
            this.CheckButton.Text = "Проверить";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // AnswerBox
            // 
            this.AnswerBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AnswerBox.Location = new System.Drawing.Point(353, 424);
            this.AnswerBox.Name = "AnswerBox";
            this.AnswerBox.Size = new System.Drawing.Size(246, 26);
            this.AnswerBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(277, 427);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ответ: ";
            // 
            // RandomizeButton
            // 
            this.RandomizeButton.Location = new System.Drawing.Point(89, 424);
            this.RandomizeButton.Name = "RandomizeButton";
            this.RandomizeButton.Size = new System.Drawing.Size(82, 26);
            this.RandomizeButton.TabIndex = 7;
            this.RandomizeButton.Text = "Randomize";
            this.RandomizeButton.UseVisualStyleBackColor = true;
            this.RandomizeButton.Click += new System.EventHandler(this.RandomizeButton_Click);
            // 
            // LearningTaskDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 458);
            this.Controls.Add(this.RandomizeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AnswerBox);
            this.Controls.Add(this.CheckButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.DescriptionBox);
            this.Controls.Add(this.TaskNameLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LearningTaskDisplay";
            this.Text = "LearninTaskDisplay";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TaskNameLabel;
        private System.Windows.Forms.RichTextBox DescriptionBox;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button CheckButton;
        private System.Windows.Forms.TextBox AnswerBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RandomizeButton;
    }
}