using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IntLearnShared.Core.LearningTasks;

namespace InteractiveLearningTutor.TaskCostructors
{
    public partial class TaskConstructorForm : Form, ITaskConstructor
    {
        private Action<LearningTask> _callback;

        public TaskConstructorForm()
        {
            InitializeComponent();
        }

        private void TaskConstructorForm_Load(object sender, EventArgs e)
        {

        }

        private LearningTask CreateSimpleTask(string title, string text)
        {
            if (title.Length == 0 || text.Length == 0)
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            TaskWithAnswer task = new TaskWithAnswer();
            task.TaskText = text;
            task.Name = title;

            return task;
        }

        public void RequestTaskConstruction(Action<LearningTask> callback)
        {
            _callback = callback;
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
            LearningTask task = null;

            if (TabControl.SelectedTab == SimpleTaskTab)
            {
                string title = simpleNameBox.Text;
                string text = simpleTextBox.Text;

                task = CreateSimpleTask(title, text);
            }

            if (task != null)
            {
                _callback?.Invoke(task);

                Close();
            }
        }
    }
}
