using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace blqw
{
    /// <summary>
    /// 异步队列接口
    /// </summary>
    /// <typeparam name="T">指定队列中元素的类型。</typeparam>
    public interface IAsyncQueue<T>: IDisposable
    {
        /// <summary>
        /// 最大容量
        /// </summary>
        int MaxCapacity { get; set; }
        /// <summary>
        /// 元素溢出规则
        /// </summary>
        OverflowRule OverflowRule { get; set; }
        /// <summary>
        /// 清空队列。
        /// </summary>
        /// <param name="token">可用于取消工作的取消标记</param>
        Task Clear(CancellationToken token);
        /// <summary>
        /// 移除并返回位于队列开始处的对象。
        /// </summary>
        /// <param name="token">可用于取消工作的取消标记</param>
        Task<T> Dequeue(CancellationToken token);
        /// <summary>
        /// 将对象添加到队列的结尾处。
        /// </summary>
        /// <param name="item">要添加到的对象。</param>
        /// <param name="token">可用于取消工作的取消标记</param>
        Task Enqueue(T item, CancellationToken token);
    }
}
