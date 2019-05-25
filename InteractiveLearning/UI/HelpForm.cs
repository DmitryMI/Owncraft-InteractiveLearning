using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InteractiveLearning.HelpProviding;

namespace InteractiveLearning.UI
{
    public partial class HelpForm : Form
    {
        private HelpTextProvider _helpProvider;
        private IList<TextPair> _helpText;

        public HelpForm()
        {
            InitializeComponent();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            _helpProvider = HelpTextProvider.GetInstance();

            _helpText = _helpProvider.GetHelpValues();

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string filter = SearchBox.Text;
            UpdateList(filter);
        }

        private void UpdateList(string filter)
        {
            TermsListBox.Items.Clear();
            
            for (int i = 0; i < _helpText.Count; i++)
            {
                string title = _helpText[i].Title;
                if (String.IsNullOrEmpty(filter) || title.Contains(filter))
                {
                    IndexedItem item = new IndexedItem(i);
                    item.Text = title;
                    TermsListBox.Items.Add(item);
                }
            }
        }

        private void PrintHelp(TextPair text)
        {
            HelpTextBox.Text = text.Title + "\n\n" + text.Text;
        }

        private void TermsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItems = TermsListBox.SelectedItems;
            if(selectedItems.Count == 0)
                return;
            
            IndexedItem item = (IndexedItem) selectedItems[0];

            PrintHelp(_helpText[item.Value]);
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }
    }
}
