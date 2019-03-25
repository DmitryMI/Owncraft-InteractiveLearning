using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntLearnShared.Networking
{
    public class OwcQueue<T>
    {
        public const int BufferSize = 32;

        private T[] _buffer = new T[BufferSize];

        private int _pushPosition = 0;
        private int _popPosition = -1;

        public void Push(T val)
        {
            if(_pushPosition == _popPosition)
                throw new Exception("Buffer full");

            _buffer[_pushPosition] = val;

            if (_popPosition == -1)
                _popPosition = _pushPosition;
            _pushPosition = (_pushPosition + 1) % BufferSize;
            
        }

        public T Pop()
        {
            if(_popPosition == -1)
                throw new Exception("Buffer empty");

            T ret = _buffer[_popPosition];
            _popPosition = (_popPosition + 1) % BufferSize;
            if (_popPosition == _pushPosition)
                _popPosition = -1;
            return ret;
        }

        public T Peek()
        {
            if(Count > 0)
                return _buffer[_popPosition];

            throw new Exception("Buffer empty");
        }

        public int Count => _pushPosition - _popPosition;

        public bool IsEmpty()
        {
            return _popPosition == -1;
        }

        public bool IsFull()
        {
            return (_pushPosition) % BufferSize == _popPosition;
        }
    }
}
