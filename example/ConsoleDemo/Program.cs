using blqw;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = AsyncQueue.Create<string>();
            var source = new CancellationTokenSource();
            var token = source.Token;
            queue.Enqueue("1").Wait();
            queue.Enqueue("2").Wait();
            queue.Enqueue("3").Wait();
            queue.Enqueue("4").Wait();
            queue.Enqueue("5").Wait();
            queue.Enqueue("6").Wait();
            queue.Enqueue("7").Wait();
            queue.MaxCapacity = 2;
            queue.OverflowRule = OverflowRule.DiscardFirst;
            queue.Enqueue("8").Wait();



            var senders = Enumerable.Range(0, 3).Select(index => new Sender(index, queue, (ConsoleColor)(index + 13))).ToArray();
            var receivers = Enumerable.Range(0, 10).Select(index => new Receiver(index, queue, (ConsoleColor)(index + 5))).ToArray();

            Parallel.ForEach(receivers, async x => await x.Receive(token));

            Thread.Sleep(1000);
            var message = 0;

            Parallel.ForEach(senders, async x =>
            {
                for (var i = 0; i < 2000; i++)
                {
                    await x.Send(Interlocked.Increment(ref message).ToString());
                }
            });
            Console.ReadLine();
            source.Cancel();
            Console.ReadLine();
        }

    }
}
