using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteractiveLearning.UI
{
    class IndexedItem : ListViewItem
    {
        private int _value;

        public IndexedItem(int value)
        {
            Value = value;
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
