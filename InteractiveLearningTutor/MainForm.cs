using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using InteractiveLearningTutor.TaskCostructors;
using IntLearnShared.Core;
using IntLearnShared.Core.LearningTasks;
using IntLearnShared.Networking;

namespace InteractiveLearningTutor
{
    public partial class MainForm : Form
    {        
        private ElementListItem _lastSelectedElement;

        private TutorNetworkHelper _networkHelper;
        private TaskManager _taskManager;

        public MainForm()
        {
            InitializeComponent();
        }

        private void FirstLaunch()
        {
            _taskManager.Init();

            DisplayCurrentCategory();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if(_taskManager == null)            
                _taskManager = new TaskManager();            

            if(_networkHelper == null)
                _networkHelper = new TutorNetworkHelper(_taskManager);

            _networkHelper.StartListening();

            FirstLaunch();            
        }
        

        private void DisplayCurrentCategory()
        {
            categoryCollectionList.Clear();

            if (_taskManager.Current.ParentCategory == null)
            {
                CurrentCategoryLabel.Text = "Available categories:";
            }
            else
            {
                CurrentCategoryLabel.Text = _taskManager.Current.Name + ":";
            }

            foreach (BaseElement element in _taskManager.Current)
            {
                ElementListItem item = new ElementListItem(element);
                item.Text = element.Name;
                item.ToolTipText = element.Description;

                categoryCollectionList.Items.Add(item);
            }
        }

        private void categoryCollectionList_ItemActivate(object sender, EventArgs e)
        {
            foreach (ListViewItem item in categoryCollectionList.SelectedItems)
            {
                if (item.Selected)
                    ProcessUserChoice(((ElementListItem)item).Element);
            }
        }

        private void categoryCollectionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryCollectionList.SelectedItems.Count == 0)
                return;
            ElementListItem item = (ElementListItem)categoryCollectionList.SelectedItems[0];
            descriptionBox.Text = item.Element.Description;
            ElementNameBox.Text = item.Element.Name;

            _lastSelectedElement = item;
        }

        private void ProcessUserChoice(BaseElement clickedElement)
        {
            //review: сделайте без if, вспомните ООП))
            if (clickedElement is Category)
            {
                _taskManager.Current = (Category)clickedElement;
                DisplayCurrentCategory();
            }
            else if (clickedElement is LearningTask)
            {
                LearningTask task = (LearningTask)clickedElement;

                Hide();
                // TODO Open task editor
                TaskConstructorForm constructorForm = new TaskConstructorForm();
                constructorForm.ShowDialog();
                //MessageBox.Show("TASK EDITOR PLACEHOLDER");
                Show();
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            _taskManager.Refresh();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (_taskManager.Current.ParentCategory != null)
            {
                _taskManager.Current = _taskManager.Current.ParentCategory;
                DisplayCurrentCategory();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_lastSelectedElement == null)
            {
                MessageBox.Show("Ни один элемент не выбран", "Ошибка", MessageBoxButtons.OK);
                return;
            }

            BaseElement edited = _lastSelectedElement.Element;

            if (String.IsNullOrEmpty(ElementNameBox.Text))
            {
                MessageBox.Show("Имя элемента не должно быть пустым", "Ошибка", MessageBoxButtons.OK);
            }
            else
            {

                edited.Name = ElementNameBox.Text;
                edited.Description = descriptionBox.Text;

                DisplayCurrentCategory();

                _taskManager.SaveToFile();
            }
        }

        private void AddCategoryButton_Click(object sender, EventArgs e)
        {
            Category nCat = new Category() { Name = "New category", Description = "N/A", Thumbnail = null };

            _taskManager.Current.Add(nCat);

            DisplayCurrentCategory();

            categoryCollectionList.Select();

            categoryCollectionList.SelectedItems.Clear();
            categoryCollectionList.SelectedIndices.Add(categoryCollectionList.Items.Count - 1);
            categoryCollectionList.FocusedItem = categoryCollectionList.Items[categoryCollectionList.Items.Count - 1];

            _taskManager.SaveToFile();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_lastSelectedElement == null)
                return;

            BaseElement element = _lastSelectedElement.Element;


            DialogResult result = MessageBox.Show("Deletion of a category removes all sub-items. Are you sure?",
                $"Deletion of {element.Name}",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                _taskManager.Current.Remove(_lastSelectedElement.Element);
                DisplayCurrentCategory();
                _taskManager.SaveToFile();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _networkHelper.StopListening();
        }

        private void TaskAddedHandler(LearningTask task)
        {
            _taskManager.Current.Add(task);

            DisplayCurrentCategory();

            categoryCollectionList.Select();

            categoryCollectionList.SelectedItems.Clear();
            categoryCollectionList.SelectedIndices.Add(categoryCollectionList.Items.Count - 1);
            categoryCollectionList.FocusedItem = categoryCollectionList.Items[categoryCollectionList.Items.Count - 1];

            _taskManager.SaveToFile();
        }

        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            TaskConstructorForm constructor = new TaskConstructorForm();
            constructor.RequestTaskConstruction(TaskAddedHandler);

            constructor.ShowDialog();
        }
    }
}
