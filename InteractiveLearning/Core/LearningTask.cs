using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveLearning.Core
{
    public class LearningTask : BaseElement
    {
        public delegate bool CheckFunction(object answer);

        public string TaskText { get; set; }
        public Image Picture { get; set; }
    }
}
