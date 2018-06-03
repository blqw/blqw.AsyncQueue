using blqw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Sender
    {
        private readonly int _index;
        private readonly IAsyncQueue<string> _queue;
        private readonly ConsoleColor _background;

        public Sender(int index, IAsyncQueue<string> queue, ConsoleColor background)
        {
            _index = index;
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
            _background = background;
        }

        public async Task Send(string message)
        {
            ColorConsole.WriteLine($"{_index}号发送者写入{message}", backgroundColor: _background);
            await Task.Delay(100 + Math.Abs(new object().GetHashCode() % 300));
            await _queue.Enqueue(message);
        }

    }
}
