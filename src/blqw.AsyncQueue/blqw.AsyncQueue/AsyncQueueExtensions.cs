using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace blqw
{
    /// <summary>
    /// 异步队列扩展方法
    /// </summary>
    public static class AsyncQueueExtensions
    {
        /// <summary>
        /// 清空队列。
        /// </summary>
        public static Task Clear<T>(this IAsyncQueue<T> aq) => aq.Clear(CancellationToken.None);

        /// <summary>
        /// 清空队列。
        /// </summary>
        public static async Task Clear<T>(this IAsyncQueue<T> aq, int millisecondsTimeout)
        {
            using (var source = new CancellationTokenSource(millisecondsTimeout))
            {
                await aq.Clear(source.Token);
            }
        }

        /// <summary>
        /// 清空队列。
        /// </summary>
        public static async Task Clear<T>(this IAsyncQueue<T> aq, TimeSpan timeout)
        {
            using (var source = new CancellationTokenSource(timeout))
            {
                await aq.Clear(source.Token);
            }
        }

        /// <summary>
        /// 移除并返回位于队列开始处的对象。
        /// </summary>
        public static Task<T> Dequeue<T>(this IAsyncQueue<T> aq) => aq.Dequeue(CancellationToken.None);

        /// <summary>
        /// 移除并返回位于队列开始处的对象。
        /// </summary>
        public static async Task<T> Dequeue<T>(this IAsyncQueue<T> aq, int millisecondsTimeout)
        {
            using (var source = new CancellationTokenSource(millisecondsTimeout))
            {
                return await aq.Dequeue(source.Token);
            }
        }

        /// <summary>
        /// 移除并返回位于队列开始处的对象。
        /// </summary>
        public static async Task<T> Dequeue<T>(this IAsyncQueue<T> aq, TimeSpan timeout)
        {
            using (var source = new CancellationTokenSource(timeout))
            {
                return await aq.Dequeue(source.Token);
            }
        }

        /// <summary>
        /// 将对象添加到队列的结尾处。
        /// </summary>
        /// <param name="item">要添加到的对象。</param>
        public static Task Enqueue<T>(this IAsyncQueue<T> aq, T item) => aq.Enqueue(item, CancellationToken.None);

        /// <summary>
        /// 将对象添加到队列的结尾处。
        /// </summary>
        /// <param name="item">要添加到的对象。</param>
        public static async Task Enqueue<T>(this IAsyncQueue<T> aq, T item, int millisecondsTimeout)
        {
            using (var source = new CancellationTokenSource(millisecondsTimeout))
            {
                await aq.Enqueue(item, CancellationToken.None);
            }
        }

        /// <summary>
        /// 将对象添加到队列的结尾处。
        /// </summary>
        /// <param name="item">要添加到的对象。</param>
        public static async Task Enqueue<T>(this IAsyncQueue<T> aq, T item, TimeSpan timeout)
        {
            using (var source = new CancellationTokenSource(timeout))
            {
                await aq.Enqueue(item, CancellationToken.None);
            }
        }


        /// <summary>
        /// 将对象添加到队列的结尾处。
        /// </summary>
        /// <param name="item">要添加到的对象。</param>
        public static async Task EnqueueRange<T>(this IAsyncQueue<T> aq, IEnumerable<T> items)
        {
            if (items != null)
            {
                foreach (var item in items)
                {
                    await aq.Enqueue(item, CancellationToken.None);
                }
            }
        }

        /// <summary>
        /// 将对象添加到队列的结尾处。
        /// </summary>
        /// <param name="item">要添加到的对象。</param>
        public static async Task EnqueueRange<T>(this IAsyncQueue<T> aq, IEnumerable<T> items, int millisecondsTimeout)
        {
            if (items != null)
            {
                using (var source = new CancellationTokenSource(millisecondsTimeout))
                {
                    foreach (var item in items)
                    {
                        await aq.Enqueue(item, CancellationToken.None);
                    }
                }
            }
        }

        /// <summary>
        /// 将对象添加到队列的结尾处。
        /// </summary>
        /// <param name="item">要添加到的对象。</param>
        public static async Task EnqueueRange<T>(this IAsyncQueue<T> aq, IEnumerable<T> items, TimeSpan timeout)
        {
            if (items != null)
            {
                using (var source = new CancellationTokenSource(timeout))
                {
                    foreach (var item in items)
                    {
                        await aq.Enqueue(item, CancellationToken.None);
                    }
                }
            }
        }
    }
}
