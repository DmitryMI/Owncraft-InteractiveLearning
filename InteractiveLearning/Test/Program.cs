using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IntLearnShared.Networking;
using IntLearnShared.Utils;

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
            lock (_queue)
            {
                Console.WriteLine("Thead gains access to CR");
                Thread.Sleep(1000);
                _queue.Push(9999);
                Thread.Sleep(1000);
                Console.WriteLine("Thead returns access to CR");
            }
        }
    }
}
