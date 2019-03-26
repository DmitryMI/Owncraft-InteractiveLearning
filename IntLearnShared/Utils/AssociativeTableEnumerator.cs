using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntLearnShared.Utils
{
    class AssociativeTableEnumerator<T1, T2> : IEnumerator<Pair<T1, T2>>
    {
        private int _index = -1;
        private AssociativeTable<T1, T2> _table;

        public AssociativeTableEnumerator(AssociativeTable<T1, T2> table)
        {
            _table = table;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            bool ok = _index < _table.Count - 1;
            if (ok)
                _index++;
            return ok;
        }

        public void Reset()
        {
            _index = 0;
        }

        public Pair<T1, T2> Current => _table[_index];

        object IEnumerator.Current => Current;
    }
}
