using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace blqw
{
    /// <summary>
    /// 异步队列
    /// </summary>
    public static class AsyncQueue
    {
        public static IAsyncQueue<T> Create<T>() => new AsyncQueue<T>();
    }
}
