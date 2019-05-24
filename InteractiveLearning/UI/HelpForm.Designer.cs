namespace InteractiveLearning.UI
{
    partial class HelpForm
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
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.HelpTextBox = new System.Windows.Forms.RichTextBox();
            this.TermsListBox = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search:";
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(63, 10);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(725, 20);
            this.SearchBox.TabIndex = 1;
            this.SearchBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            // 
            // HelpTextBox
            // 
            this.HelpTextBox.Location = new System.Drawing.Point(399, 36);
            this.HelpTextBox.Name = "HelpTextBox";
            this.HelpTextBox.ReadOnly = true;
            this.HelpTextBox.Size = new System.Drawing.Size(389, 407);
            this.HelpTextBox.TabIndex = 3;
            this.HelpTextBox.Text = "";
            // 
            // TermsListBox
            // 
            this.TermsListBox.Location = new System.Drawing.Point(12, 36);
            this.TermsListBox.Name = "TermsListBox";
            this.TermsListBox.Size = new System.Drawing.Size(381, 407);
            this.TermsListBox.TabIndex = 4;
            this.TermsListBox.UseCompatibleStateImageBehavior = false;
            this.TermsListBox.View = System.Windows.Forms.View.List;
            this.TermsListBox.SelectedIndexChanged += new System.EventHandler(this.TermsListBox_SelectedIndexChanged);
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TermsListBox);
            this.Controls.Add(this.HelpTextBox);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.label1);
            this.Name = "HelpForm";
            this.Text = "HelpForm";
            this.Load += new System.EventHandler(this.HelpForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.RichTextBox HelpTextBox;
        private System.Windows.Forms.ListView TermsListBox;
    }
}