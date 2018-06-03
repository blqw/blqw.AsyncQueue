# AsyncQueue
使用.net异步模型async/await + 信号量(`SemaphoreSlim`)实现的**异步队列**

## [博客](https://www.jianshu.com/p/a35499c00333)

## Demo
```C#
static void Main(string[] args)
{
    var queue = AsyncQueue.Create<string>(); // 初始化异步队列
    var source = new CancellationTokenSource(); // 初始化取消标志
    var token = source.Token; 
    var senders = Enumerable.Range(0, 3).Select(index => new Sender(index, queue, (ConsoleColor)(index+13))).ToArray(); // 初始化3个发送者
    var receivers = Enumerable.Range(0, 10).Select(index => new Receiver(index, queue, (ConsoleColor)(index + 5))).ToArray(); // 初始化10个接收者

    Parallel.ForEach(receivers, async x => await x.Receive(token)); // 并行启动10个接收者

    Thread.Sleep(1000); // 延迟1秒 等待接收者全部启动完成
    var message = 0;
    // 并行启动3个发送者，每个发送者发送10次，发送内容为从1开始自增的整型数字，也就是1~30
    Parallel.ForEach(senders, async x =>
    {
        for (var i = 0; i < 10; i++)
        {
            await x.Send(Interlocked.Increment(ref message).ToString());
        }
    });

    Console.ReadLine();
    source.Cancel(); // 停止所有接收者
    Console.ReadLine();
}
```

## 更新说明
#### [1.0.1.0] 2018.06.03
* 增加最大容量限制

#### [1.0.0.0] 2018.06.03
* 初始版本