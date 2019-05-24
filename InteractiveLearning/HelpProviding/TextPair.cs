using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveLearning.HelpProviding
{
    class TextPair
    {
        private string _title, _text;


        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public TextPair(string title, string text)
        {
            _title = title;
            _text = text;
        }
    }
}
