using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntLearnShared.Core.LearningTasks;

namespace InteractiveLearningTutor.TaskCostructors
{
    interface ITaskConstructor
    {
        void RequestTaskConstruction(Action<LearningTask> callback);
    }
}
