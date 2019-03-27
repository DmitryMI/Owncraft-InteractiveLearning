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
using IntLearnShared.Core;
using IntLearnShared.Core.LearningTasks;
using IntLearnShared.Networking;

namespace InteractiveLearningTutor
{
    public partial class MainForm : Form
    {
        public const string DataFileName = "catalogue.xml";

        private Category _currentCategory;
        private ElementListItem _lastSelectedElement;

        public MainForm()
        {
            InitializeComponent();
        }

        private void FirstLaunch()
        {
            if (!File.Exists(DataFileName))
            {
                // This is the first launch of program.
                Category root = PrebuiltTaskCreator.GetPrebuiltTasks_Alexandr();
                Category merge = Category.MergeTrees(PrebuiltTaskCreator.GetPrebuiltTasks(), root);

                _currentCategory = merge;
                SaveToFile();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            NetworkHelper.GetInstance().StartListener();

            FirstLaunch();

            LoadFromFile();
        }

        private void NetworkReadTimer_Tick(object sender, EventArgs e)
        {
            NetworkHelper net = NetworkHelper.GetInstance();

            if (net.PackageQueueCount() > 0)
            {
                NetPackage package = net.PeekPackage();
                ProcessNetworkCommand(package.NetCommand, package.Sender);
            }
        }

        private void ProcessNetworkCommand(NetCommand cmd, IPAddress sender)
        {
            if (cmd.CmdType == NetCommand.CommandType.SeekServer)
            {
                NetworkHelper.GetInstance().PopPackage();
                NetworkHelper.GetInstance().SendCommand(NetCommand.SeekWhoIsPreset, sender);

                Debug.WriteLine("Seek server response to " + sender.ToString());
            }

            if (cmd.CmdType == NetCommand.CommandType.TaskListRequest)
            {
                NetworkHelper.GetInstance().PopPackage();
                //Category root = PrebuiltTaskCreator.GetDebugTree();

                Category root = _currentCategory;
                while (root.ParentCategory != null)
                    root = root.ParentCategory;

                string serialized = Serializer.Serialize(root);

                NetCommand response = new NetCommand(NetCommand.TaskListResponseHeader, serialized);
                NetworkHelper.GetInstance().SendCommand(response, sender);

                Debug.WriteLine("Task list response to " + sender.ToString());
            }
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
            if (clickedElement is Category)
            {
                _currentCategory = (Category)clickedElement;
                DisplayCurrentCategory();
            }
            else if (clickedElement is LearningTask)
            {
                LearningTask task = (LearningTask)clickedElement;

                Hide();
                // TODO Open task editor
                MessageBox.Show("TASK EDITOR PLACEHOLDER");
                Show();
            }
        }

        private void LoadFromFile()
        {
            try
            {
                byte[] fileData = File.ReadAllBytes(DataFileName);
                string data = Encoding.Unicode.GetString(fileData);
                _currentCategory = Serializer.Deserialize(data);
                DisplayCurrentCategory();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

        private void SaveToFile()
        {
            Category cat = _currentCategory;
            while (cat.ParentCategory != null)
                cat = cat.ParentCategory;

            string ser = Serializer.Serialize(cat);
            File.WriteAllBytes(DataFileName, Encoding.Unicode.GetBytes(ser));
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadFromFile();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (_currentCategory.ParentCategory != null)
            {
                _currentCategory = _currentCategory.ParentCategory;
                DisplayCurrentCategory();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            BaseElement edited = _lastSelectedElement.Element;
            edited.Name = ElementNameBox.Text;
            edited.Description = descriptionBox.Text;

            DisplayCurrentCategory();

            SaveToFile();
        }

        private void AddCategoryButton_Click(object sender, EventArgs e)
        {
            Category nCat = new Category() { Name = "New category", Description = "N/A", Thumbnail = null };
            _currentCategory.Add(nCat);

            DisplayCurrentCategory();

            categoryCollectionList.Select();

            categoryCollectionList.SelectedItems.Clear();
            categoryCollectionList.SelectedIndices.Add(categoryCollectionList.Items.Count - 1);
            categoryCollectionList.FocusedItem = categoryCollectionList.Items[categoryCollectionList.Items.Count - 1];

            SaveToFile();
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
                _currentCategory.Remove(_lastSelectedElement.Element);
                DisplayCurrentCategory();
                SaveToFile();
            }
        }
    }
}
