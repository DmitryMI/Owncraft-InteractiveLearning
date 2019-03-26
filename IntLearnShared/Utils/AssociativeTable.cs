using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntLearnShared.Utils
{
    class AssociativeTable<TKey, TVal> : IEnumerable<Pair<TKey, TVal>>
    {
        private List<TKey> _keyList;
        private List<TVal> _valList;

        public AssociativeTable(int initialCapacity)
        {
            _keyList = new List<TKey>(initialCapacity);
            _valList = new List<TVal>(initialCapacity);
        }

        public AssociativeTable()
        {
            _keyList = new List<TKey>();
            _valList = new List<TVal>();
        }

        public void Add(TKey key, TVal value)
        {
            if(_keyList.Contains(key))
                throw new NotUniqueKeyException();

            _keyList.Add(key);
            _valList.Add(value);
        }

        public void Remove(TKey key)
        {
            int index = _keyList.IndexOf(key);
            if (index >= 0)
            {
                _keyList.RemoveAt(index);
                _valList.RemoveAt(index);
            }
        }


        public void RemoveAt(int index)
        {
            _keyList.RemoveAt(index);
            _valList.RemoveAt(index);
        }

        public Pair<TKey, TVal> this[int i]
        {
            get => new Pair<TKey, TVal>(){Val1 = _keyList[i], Val2 =  _valList[i]};
            set
            {
                TKey key = value.Val1;
                TVal val= value.Val2;
                _keyList[i] = key;
                _valList[i] = val;
            }
        }

        public bool IsKeyInTable(TKey key)
        {
            return _keyList.IndexOf(key) != -1;
        }

        public int GetKeyIndex(TKey key)
        {
            return _keyList.IndexOf(key);
        }

        public TVal this[TKey key]
        {
            get
            {
                int index = _keyList.IndexOf(key);
                return _valList[index];
            }

            set
            {
                int index = _keyList.IndexOf(key);
                _valList[index] = value;
            }
        }

        public class NotUniqueKeyException : Exception
        {
            
        }

        public int Count => _keyList.Count;

        
        public IEnumerator<Pair<TKey, TVal>> GetEnumerator()
        {
            return new AssociativeTableEnumerator<TKey, TVal>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
