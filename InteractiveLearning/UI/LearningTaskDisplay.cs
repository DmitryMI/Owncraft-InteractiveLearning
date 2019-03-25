using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteractiveLearning.UI
{
    public partial class LearningTaskDisplay : Form
    {
        Core.LearningTask _task;

        public LearningTaskDisplay()
        {
            InitializeComponent();
        }

        public LearningTaskDisplay(Core.LearningTask selectedTask)
        {
            InitializeComponent();

            _task = selectedTask;

            TaskNameLabel.Text = _task.Name;
            DescriptionBox.Text = _task.Description;
            PictureBox.Image = _task.Picture;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
