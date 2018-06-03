﻿using System.Collections.Concurrent;
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
            return _queue.TryDequeue(out var val) ? val : throw new System.InvalidOperationException();
        }

        public async Task Enqueue(T item, CancellationToken token)
        {
            await _in.WaitAsync(token);
            try
            {
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
    }
}