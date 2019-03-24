using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveLearning.Core
{
    class Category : BaseElement, IList<BaseElement>
    {
        private readonly List<BaseElement> _subElements = new List<BaseElement>();
        public IEnumerator<BaseElement> GetEnumerator()
        {
            return _subElements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _subElements).GetEnumerator();
        }

        public void Add(BaseElement item)
        {
            _subElements.Add(item);
            if(item is Category)
                ((Category) item).ParentCategory = this;
        }

        public void Clear()
        {
            _subElements.Clear();
        }

        public bool Contains(BaseElement item)
        {
            return _subElements.Contains(item);
        }

        public void CopyTo(BaseElement[] array, int arrayIndex)
        {
            _subElements.CopyTo(array, arrayIndex);
        }

        public bool Remove(BaseElement item)
        {
            return _subElements.Remove(item);
        }

        public int Count
        {
            get { return _subElements.Count; }
        }

        public bool IsReadOnly => false;

        public int IndexOf(BaseElement item)
        {
            return _subElements.IndexOf(item);
        }

        public void Insert(int index, BaseElement item)
        {
            _subElements.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _subElements.RemoveAt(index);
        }

        public Category ParentCategory { get; private set; }

        public BaseElement this[int index]
        {
            get => _subElements[index];
            set => _subElements[index] = value;
        }
    }
}
