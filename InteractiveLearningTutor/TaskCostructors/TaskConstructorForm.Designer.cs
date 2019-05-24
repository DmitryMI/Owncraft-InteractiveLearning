namespace InteractiveLearningTutor.TaskCostructors
{
    partial class TaskConstructorForm
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.SimpleTaskTab = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.simpleAnswerBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.simpleTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.simpleNameBox = new System.Windows.Forms.TextBox();
            this.FinishButton = new System.Windows.Forms.Button();
            this.TabControl.SuspendLayout();
            this.SimpleTaskTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.SimpleTaskTab);
            this.TabControl.Location = new System.Drawing.Point(12, 12);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(645, 381);
            this.TabControl.TabIndex = 0;
            // 
            // SimpleTaskTab
            // 
            this.SimpleTaskTab.Controls.Add(this.label3);
            this.SimpleTaskTab.Controls.Add(this.simpleAnswerBox);
            this.SimpleTaskTab.Controls.Add(this.label2);
            this.SimpleTaskTab.Controls.Add(this.simpleTextBox);
            this.SimpleTaskTab.Controls.Add(this.label1);
            this.SimpleTaskTab.Controls.Add(this.simpleNameBox);
            this.SimpleTaskTab.Location = new System.Drawing.Point(4, 22);
            this.SimpleTaskTab.Name = "SimpleTaskTab";
            this.SimpleTaskTab.Padding = new System.Windows.Forms.Padding(3);
            this.SimpleTaskTab.Size = new System.Drawing.Size(637, 355);
            this.SimpleTaskTab.TabIndex = 0;
            this.SimpleTaskTab.Text = "Задача с фикс. ответом";
            this.SimpleTaskTab.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 332);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ответ:";
            // 
            // simpleAnswerBox
            // 
            this.simpleAnswerBox.Location = new System.Drawing.Point(69, 329);
            this.simpleAnswerBox.Name = "simpleAnswerBox";
            this.simpleAnswerBox.Size = new System.Drawing.Size(562, 20);
            this.simpleAnswerBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Текст:";
            // 
            // simpleTextBox
            // 
            this.simpleTextBox.Location = new System.Drawing.Point(69, 40);
            this.simpleTextBox.Name = "simpleTextBox";
            this.simpleTextBox.Size = new System.Drawing.Size(562, 268);
            this.simpleTextBox.TabIndex = 2;
            this.simpleTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название:";
            // 
            // simpleNameBox
            // 
            this.simpleNameBox.Location = new System.Drawing.Point(69, 14);
            this.simpleNameBox.Name = "simpleNameBox";
            this.simpleNameBox.Size = new System.Drawing.Size(562, 20);
            this.simpleNameBox.TabIndex = 0;
            // 
            // FinishButton
            // 
            this.FinishButton.Location = new System.Drawing.Point(578, 399);
            this.FinishButton.Name = "FinishButton";
            this.FinishButton.Size = new System.Drawing.Size(75, 23);
            this.FinishButton.TabIndex = 0;
            this.FinishButton.Text = "Сохранить";
            this.FinishButton.UseVisualStyleBackColor = true;
            this.FinishButton.Click += new System.EventHandler(this.FinishButton_Click);
            // 
            // TaskConstructorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 432);
            this.Controls.Add(this.FinishButton);
            this.Controls.Add(this.TabControl);
            this.Name = "TaskConstructorForm";
            this.Text = "Создать задачу";
            this.Load += new System.EventHandler(this.TaskConstructorForm_Load);
            this.TabControl.ResumeLayout(false);
            this.SimpleTaskTab.ResumeLayout(false);
            this.SimpleTaskTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage SimpleTaskTab;
        private System.Windows.Forms.Button FinishButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox simpleAnswerBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox simpleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox simpleNameBox;
    }
}