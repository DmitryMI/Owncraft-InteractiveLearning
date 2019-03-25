using System.Drawing;

namespace IntLearnShared.Core
{
    public class LearningTask : BaseElement
    {
        public delegate bool CheckFunction(object answer);

        public string TaskText { get; set; }
        public Image Picture { get; set; }
    }
}
