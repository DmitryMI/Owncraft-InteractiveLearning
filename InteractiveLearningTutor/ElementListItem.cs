using System.Windows.Forms;
using IntLearnShared.Core;

namespace InteractiveLearningTutor
{
    class ElementListItem : ListViewItem
    {
        public BaseElement Element { get; set; }

        public ElementListItem(BaseElement element)
        {
            Element = element;
        }
    }
}
