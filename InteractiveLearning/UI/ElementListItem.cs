using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IntLearnShared.Core;

namespace InteractiveLearning.UI
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
