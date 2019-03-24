namespace InteractiveLearning.UI
{
    partial class TestSelectionForm
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
            this.CurrentCategoryLabel = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.categoryCollectionList = new System.Windows.Forms.ListView();
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CurrentCategoryLabel
            // 
            this.CurrentCategoryLabel.AutoSize = true;
            this.CurrentCategoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrentCategoryLabel.Location = new System.Drawing.Point(81, 9);
            this.CurrentCategoryLabel.Name = "CurrentCategoryLabel";
            this.CurrentCategoryLabel.Size = new System.Drawing.Size(154, 20);
            this.CurrentCategoryLabel.TabIndex = 0;
            this.CurrentCategoryLabel.Text = "Available categories:";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(12, 3);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(63, 23);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // categoryCollectionList
            // 
            this.categoryCollectionList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.categoryCollectionList.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.categoryCollectionList.LabelWrap = false;
            this.categoryCollectionList.Location = new System.Drawing.Point(12, 32);
            this.categoryCollectionList.MultiSelect = false;
            this.categoryCollectionList.Name = "categoryCollectionList";
            this.categoryCollectionList.ShowItemToolTips = true;
            this.categoryCollectionList.Size = new System.Drawing.Size(309, 338);
            this.categoryCollectionList.TabIndex = 3;
            this.categoryCollectionList.UseCompatibleStateImageBehavior = false;
            this.categoryCollectionList.View = System.Windows.Forms.View.List;
            this.categoryCollectionList.ItemActivate += new System.EventHandler(this.categoryCollectionList_ItemActivate);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(258, 3);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(63, 23);
            this.backButton.TabIndex = 4;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // TestSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 382);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.categoryCollectionList);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.CurrentCategoryLabel);
            this.Name = "TestSelectionForm";
            this.Text = "TestSelectionForm";
            this.Load += new System.EventHandler(this.TestSelectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CurrentCategoryLabel;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ListView categoryCollectionList;
        private System.Windows.Forms.Button backButton;
    }
}