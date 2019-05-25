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
using IntLearnShared.Core.LearningTasks;

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
            LoadTaskData();
        }

        void LoadTaskData()
        {
            TaskNameLabel.Text = _task.Name;
            DescriptionBox.Text = _task.RenderTaskDescription();
            PictureBox.Image = _task.Picture;
            RandomizeButton.Enabled = _task.IsRandomizable;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_task.CheckAnswer(AnswerBox.Text))
                {
                    MessageBox.Show("Это правильный ответ!");
                }
                else
                {
                    MessageBox.Show("Неверно! Попробуйте еще раз.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный формат ввода! Попробуйте еще раз.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void RandomizeButton_Click(object sender, EventArgs e)
        {
            if (_task.IsRandomizable)
            {
                _task.Randomize();
                LoadTaskData();
            }
        }
    }
}
