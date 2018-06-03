using blqw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Receiver
    {
        private readonly int _index;
        private readonly IAsyncQueue<string> _queue;
        private readonly ConsoleColor _foreground;

        public Receiver(int index, IAsyncQueue<string> queue, ConsoleColor foreground)
        {
            _index = index;
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
            _foreground = foreground;
        }

        public async Task Receive(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    var str = await _queue.Dequeue(token);
                    ColorConsole.WriteLine($"{_index}号接收者获取到:{str}", foregroundColor: _foreground);
                    await Task.Delay(100 + Math.Abs(new object().GetHashCode() % 300));
                }
            }
            catch (OperationCanceledException)
            {
                ColorConsole.WriteLine($"{_index}号接收者关闭", foregroundColor: _foreground);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
