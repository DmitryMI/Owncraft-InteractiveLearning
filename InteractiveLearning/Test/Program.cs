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
        static void Main(string[] args)
        {
            OwcQueue<int> queue = new OwcQueue<int>();

            // Simple testing
            queue.Push(1);
            queue.Push(2);
            queue.Push(3);

            Console.WriteLine(queue.Pop()); // 1
            Console.WriteLine(queue.Pop()); // 2
            Console.WriteLine(queue.Pop()); // 3

            queue.Push(4);
            queue.Push(5);
            queue.Push(6);
            queue.Push(7);

            Console.WriteLine(queue.Pop()); // 4
            Console.WriteLine(queue.Pop()); // 5
            Console.WriteLine(queue.Pop()); // 6

            queue.Push(8);

            Console.WriteLine(queue.Pop()); // 7
            Console.WriteLine(queue.Pop()); // 8

            Console.WriteLine("Queue count: " + queue.Count);

            // Empty error
            try
            {
                Console.WriteLine(queue.Pop()); // ERROR
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
            }

            Console.WriteLine("==============\n");

            try
            {
                // Full error
                for (int i = 0; i < OwcQueue<int>.BufferSize + 1; i++)
                {
                    Console.WriteLine("Pushing: " + i);
                    queue.Push(i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            try
            {
                for (int i = 0; i < OwcQueue<int>.BufferSize; i++)
                {
                    int pop = queue.Pop();
                    Console.WriteLine(pop);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
            }

            Console.ReadKey();
        }
    }
}
