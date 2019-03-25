using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IntLearnShared.Core;

namespace InteractiveLearning.UI
{
    public partial class LearningTaskDisplay : Form
    {
        LearningTask _task;

        public LearningTaskDisplay()
        {
            InitializeComponent();
        }

        public LearningTaskDisplay(LearningTask selectedTask)
        {
            InitializeComponent();

            _task = selectedTask;

            TaskNameLabel.Text = _task.Name;
            DescriptionBox.Text = _task.TaskText;
            PictureBox.Image = _task.Picture;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
