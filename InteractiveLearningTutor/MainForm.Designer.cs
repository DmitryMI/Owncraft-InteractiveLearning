namespace InteractiveLearningTutor
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.NetworkReadTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.descriptionBox = new System.Windows.Forms.TextBox();
            this.backButton = new System.Windows.Forms.Button();
            this.categoryCollectionList = new System.Windows.Forms.ListView();
            this.refreshButton = new System.Windows.Forms.Button();
            this.CurrentCategoryLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ElementNameBox = new System.Windows.Forms.TextBox();
            this.AddCategoryButton = new System.Windows.Forms.Button();
            this.AddTaskButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NetworkReadTimer
            // 
            this.NetworkReadTimer.Enabled = true;
            this.NetworkReadTimer.Interval = 1000;
            this.NetworkReadTimer.Tick += new System.EventHandler(this.NetworkReadTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(326, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Description:";
            // 
            // descriptionBox
            // 
            this.descriptionBox.Location = new System.Drawing.Point(325, 81);
            this.descriptionBox.Margin = new System.Windows.Forms.Padding(2);
            this.descriptionBox.Multiline = true;
            this.descriptionBox.Name = "descriptionBox";
            this.descriptionBox.Size = new System.Drawing.Size(303, 33);
            this.descriptionBox.TabIndex = 11;
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(9, 369);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(63, 23);
            this.backButton.TabIndex = 10;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // categoryCollectionList
            // 
            this.categoryCollectionList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.categoryCollectionList.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.categoryCollectionList.LabelWrap = false;
            this.categoryCollectionList.Location = new System.Drawing.Point(12, 28);
            this.categoryCollectionList.MultiSelect = false;
            this.categoryCollectionList.Name = "categoryCollectionList";
            this.categoryCollectionList.ShowItemToolTips = true;
            this.categoryCollectionList.Size = new System.Drawing.Size(309, 338);
            this.categoryCollectionList.TabIndex = 9;
            this.categoryCollectionList.UseCompatibleStateImageBehavior = false;
            this.categoryCollectionList.View = System.Windows.Forms.View.List;
            this.categoryCollectionList.ItemActivate += new System.EventHandler(this.categoryCollectionList_ItemActivate);
            this.categoryCollectionList.SelectedIndexChanged += new System.EventHandler(this.categoryCollectionList_SelectedIndexChanged);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(77, 369);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(63, 23);
            this.refreshButton.TabIndex = 8;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // CurrentCategoryLabel
            // 
            this.CurrentCategoryLabel.AutoSize = true;
            this.CurrentCategoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrentCategoryLabel.Location = new System.Drawing.Point(9, 5);
            this.CurrentCategoryLabel.Name = "CurrentCategoryLabel";
            this.CurrentCategoryLabel.Size = new System.Drawing.Size(154, 20);
            this.CurrentCategoryLabel.TabIndex = 7;
            this.CurrentCategoryLabel.Text = "Available categories:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(326, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Category name:";
            // 
            // ElementNameBox
            // 
            this.ElementNameBox.Location = new System.Drawing.Point(325, 28);
            this.ElementNameBox.Margin = new System.Windows.Forms.Padding(2);
            this.ElementNameBox.Multiline = true;
            this.ElementNameBox.Name = "ElementNameBox";
            this.ElementNameBox.Size = new System.Drawing.Size(303, 32);
            this.ElementNameBox.TabIndex = 13;
            // 
            // AddCategoryButton
            // 
            this.AddCategoryButton.Location = new System.Drawing.Point(665, 28);
            this.AddCategoryButton.Name = "AddCategoryButton";
            this.AddCategoryButton.Size = new System.Drawing.Size(114, 23);
            this.AddCategoryButton.TabIndex = 15;
            this.AddCategoryButton.Text = "Add category";
            this.AddCategoryButton.UseVisualStyleBackColor = true;
            this.AddCategoryButton.Click += new System.EventHandler(this.AddCategoryButton_Click);
            // 
            // AddTaskButton
            // 
            this.AddTaskButton.Location = new System.Drawing.Point(665, 57);
            this.AddTaskButton.Name = "AddTaskButton";
            this.AddTaskButton.Size = new System.Drawing.Size(114, 23);
            this.AddTaskButton.TabIndex = 16;
            this.AddTaskButton.Text = "Add task";
            this.AddTaskButton.UseVisualStyleBackColor = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(325, 119);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 17;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(665, 86);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(114, 23);
            this.DeleteButton.TabIndex = 18;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 411);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.AddTaskButton);
            this.Controls.Add(this.AddCategoryButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ElementNameBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.descriptionBox);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.categoryCollectionList);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.CurrentCategoryLabel);
            this.Name = "MainForm";
            this.Text = "Turor\'s office";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer NetworkReadTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox descriptionBox;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.ListView categoryCollectionList;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Label CurrentCategoryLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ElementNameBox;
        private System.Windows.Forms.Button AddCategoryButton;
        private System.Windows.Forms.Button AddTaskButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button DeleteButton;
    }
}

