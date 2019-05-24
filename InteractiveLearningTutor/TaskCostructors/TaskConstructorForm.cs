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

        private LearningTask CreateSimpleTask(string title, string text, string answer)
        {
            TaskWithAnswer task = new TaskWithAnswer();
            task.CorrectAnswer = answer;
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
                string answer = simpleAnswerBox.Text;

                task = CreateSimpleTask(title, text, answer);
            }

            _callback?.Invoke(task);

            Close();
        }
    }
}
