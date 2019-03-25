using System.Drawing;

namespace OwcLearningShared.Core
{
    public class LearningTask : BaseElement
    {
        public delegate bool CheckFunction(object answer);

        public string TaskText { get; set; }
        public System.Drawing.Image Picture { get; set; }
    }
}
