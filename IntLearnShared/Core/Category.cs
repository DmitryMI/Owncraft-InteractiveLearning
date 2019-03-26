using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace IntLearnShared.Core
{
    public class Category : BaseElement, IList<BaseElement>, ICloneable
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

        /// <summary>
        /// Merges two trees. Categories with same name will be merged into one.
        /// </summary>
        /// <param name="rootA"></param>
        /// <param name="rootB"></param>
        /// <returns></returns>
        public static Category MergeTrees(Category rootA, Category rootB)
        {
            Category cloneA = (Category)rootA.Clone();

            foreach (var childB in rootB)
            {
                bool wasAdded = false;
                foreach (BaseElement childA in rootA)
                {
                    if (childA.Name == childB.Name)
                    {
                        if (childA is Category && childB is Category)
                        {
                            Category mergeResult = MergeTrees((Category)childA, (Category)childB);
                            cloneA.Add(mergeResult);
                            cloneA.Remove(childA);
                            wasAdded = true;
                        }
                    }
                }

                if (wasAdded == false)
                {
                    cloneA.Add(childB);
                }
            }

            return cloneA;
        }

        public virtual object Clone()
        {
            Category clone = new Category() { Name = Name, Description = Description, Thumbnail = Thumbnail };

            foreach (var child in _subElements)
            {
                if (child is ICloneable childCloneable)
                {
                    object childClone = (childCloneable).Clone();

                    clone.Add((BaseElement)childClone);
                }
                else
                {
                    clone.Add(child);
                }
            }

            return clone;
        }
    }
}
