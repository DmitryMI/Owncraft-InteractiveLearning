using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InteractiveLearning.Core;
using InteractiveLearning.NetworkInteraction;
using InteractiveLearning.UI;

namespace InteractiveLearning
{
    public partial class TestSelectionForm : Form
    {
        private Category _currentCategory;

        public TestSelectionForm()
        {
            InitializeComponent();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            NetworkInteraction.Networker networker = Networker.GetInstance();

            _currentCategory = networker.ReadDataFromServer();

            DisplayCurrentCategory();
        }

        private void DisplayCurrentCategory()
        {
            categoryCollectionList.Clear();

            foreach (BaseElement element in _currentCategory)
            {
                /*Label nameLabel = new Label();
                nameLabel.Text = element.Name;
                Label descriptionLabel = new Label();
                descriptionLabel.Text = element.Description;
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                pictureBox.Image = element.Thumbnail;*/
                
                ElementListItem item = new ElementListItem(element);
                item.Text = element.Name;
                item.ToolTipText = element.Description;

                categoryCollectionList.Items.Add(item);
            }
        }

        private void TestSelectionForm_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void categoryCollectionList_ItemActivate(object sender, EventArgs e)
        {
            foreach (ListViewItem item in categoryCollectionList.SelectedItems)
            {
                if(item.Selected)
                    ProcessUserChoice(((ElementListItem)item).Element);
            }
        }

        private void ProcessUserChoice(BaseElement clickedElement)
        {
            if (clickedElement is Category)
            {
                _currentCategory = (Category)clickedElement;
                DisplayCurrentCategory();
            }
            else if (clickedElement is LearningTask)
            {
                LearningTask task = (LearningTask) clickedElement;
                // TODO Process task selection

                // Placeholder
                MessageBox.Show("You selected task: " + task.Name);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (_currentCategory.ParentCategory != null)
            {
                _currentCategory = _currentCategory.ParentCategory;
                DisplayCurrentCategory();
            }
        }
    }
}
