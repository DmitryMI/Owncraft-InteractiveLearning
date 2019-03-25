using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IntLearnShared.Networking;

namespace Test
{
    class Program
    {
        static OwcQueue<int> _queue = new OwcQueue<int>();

        static void Main(string[] args)
        {
            Thread t  = new Thread(ThreadAdded);
            t.Start();

            bool popped = false;

            while (!popped)
            {
                lock (_queue)
                {
                    if (!_queue.IsEmpty())
                    {
                        Console.WriteLine(_queue.Pop());
                        popped = true;
                    }
                    else
                    {
                        Console.WriteLine("Queue empty!");
                    }
                }
            }

            Console.ReadKey();
        }

        static void ThreadAdded()
        {
            lock(_queue)
                _queue.Push(9999);
        }
    }
}
