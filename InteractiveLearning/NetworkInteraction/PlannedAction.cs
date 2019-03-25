using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveLearning.NetworkInteraction
{
    class PlannedAction
    {
        public PlannedAction(Action<object> action, object actionData)
        {
            Action = action;
            ActionData = actionData;
        }


        public object ActionData { get; }

        public Action<object> Action { get; }

        public void Invoke()
        {
            Action.Invoke(ActionData);
        }
    }
}
