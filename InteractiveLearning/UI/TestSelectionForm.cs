using System;
using System.Windows.Forms;
using InteractiveLearning.NetworkInteraction;
using IntLearnShared.Core;

namespace InteractiveLearning.UI
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
            refreshButton.Enabled = false;
            RefreshList();
        }

        private void RefreshList()
        {
            Networker.GetInstance().RequestDataFromServer(OnNetworkerReadingFinish, OnNetworkerReadingError);
        }

        private void DisplayCurrentCategory()
        {
            categoryCollectionList.Clear();

            if (_currentCategory.ParentCategory == null)
            {
                CurrentCategoryLabel.Text = "Available categories:";
            }
            else
            {
                CurrentCategoryLabel.Text = _currentCategory.Name + ":";
            }

            foreach (BaseElement element in _currentCategory)
            {
                ElementListItem item = new ElementListItem(element);
                item.Text = element.Name;
                item.ToolTipText = element.Description;

                categoryCollectionList.Items.Add(item);
            }
        }

        private void TestSelectionForm_Load(object sender, EventArgs e)
        {
            RefreshList();
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

                Hide();
                new LearningTaskDisplay(task).ShowDialog();
                Show();
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

        private void OnNetworkerReadingFinish(Category root)
        {
            refreshButton.Enabled = true;
            _currentCategory = root;

            DisplayCurrentCategory();

            MessageBox.Show("List refreshed!");
        }

        private void OnNetworkerReadingError(string msg)
        {
            MessageBox.Show(msg);
            refreshButton.Enabled = true;
        }

        private void categoryCollectionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryCollectionList.SelectedItems.Count == 0)
                return;
            ElementListItem item = (ElementListItem)categoryCollectionList.SelectedItems[0];
            descriptionBox.Text = item.Element.Description;
        }
    }
}
