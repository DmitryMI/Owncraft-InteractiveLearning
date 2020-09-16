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
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Help = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CurrentCategoryLabel
            // 
            this.CurrentCategoryLabel.AutoSize = true;
            this.CurrentCategoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrentCategoryLabel.Location = new System.Drawing.Point(9, 9);
            this.CurrentCategoryLabel.Name = "CurrentCategoryLabel";
            this.CurrentCategoryLabel.Size = new System.Drawing.Size(178, 20);
            this.CurrentCategoryLabel.TabIndex = 0;
            this.CurrentCategoryLabel.Text = "Доступные категории:";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(77, 373);
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
            this.categoryCollectionList.HideSelection = false;
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
            this.categoryCollectionList.SelectedIndexChanged += new System.EventHandler(this.categoryCollectionList_SelectedIndexChanged);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(9, 373);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(63, 23);
            this.backButton.TabIndex = 4;
            this.backButton.Text = "Назад";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(325, 32);
            this.descriptionBox.Margin = new System.Windows.Forms.Padding(2);
            this.descriptionBox.Multiline = true;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.ReadOnly = true;
            this.descriptionBox.Size = new System.Drawing.Size(303, 338);
            this.descriptionBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(325, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Описание:";
            // 
            // Help
            // 
            this.Help.Location = new System.Drawing.Point(553, 373);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(75, 23);
            this.Help.TabIndex = 7;
            this.Help.Text = "Справочник";
            this.Help.UseVisualStyleBackColor = true;
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // TestSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 405);
            this.Controls.Add(this.Help);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.descriptionBox);
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
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Help;
    }
}