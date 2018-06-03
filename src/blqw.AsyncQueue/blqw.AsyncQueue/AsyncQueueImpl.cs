using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace blqw
{
    sealed class AsyncQueue<T> : IAsyncQueue<T>
    {
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
        private readonly SemaphoreSlim _in = new SemaphoreSlim(1);
        private readonly SemaphoreSlim _out = new SemaphoreSlim(0);

        public async Task Clear(CancellationToken token)
        {
            await _in.WaitAsync(token);
            try
            {
                while (await _out.WaitAsync(100) || _out.CurrentCount > 0)
                {
                    _queue.TryDequeue(out _);
                }
            }
            finally
            {
                _in.Release();
            }
        }

        public async Task<T> Dequeue(CancellationToken token)
        {
            await _out.WaitAsync(token);
            return _queue.TryDequeue(out var val) ? val : throw new InvalidOperationException("队列取值失败");
        }

        public async Task Enqueue(T item, CancellationToken token)
        {
            await _in.WaitAsync(token);
            try
            {
                while (_queue.Count >= MaxCapacity)
                {
                    switch (OverflowRule)
                    {
                        case OverflowRule.DiscardFirst:
                            await Dequeue(token);
                            break;
                        case OverflowRule.DiscardLast:
                            return;
                        case OverflowRule.ThrowException:
                        default:
                            throw new IndexOutOfRangeException("队列堆积元素超过允许的最大值");
                    }
                }
                _queue.Enqueue(item);
                _out.Release();
            }
            finally
            {
                _in.Release();
            }
        }

        void DisposeSemaphoreSlim(SemaphoreSlim ss)
        {
            try
            {
                ss.Dispose();
            }
            catch { }
        }

        public void Dispose()
        {
            DisposeSemaphoreSlim(_in);
            DisposeSemaphoreSlim(_out);
        }

        public int MaxCapacity { get; set; }
        public OverflowRule OverflowRule { get; set; }
    }
}