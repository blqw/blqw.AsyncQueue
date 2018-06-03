# AsyncQueue
ʹ��.net�첽ģ��async/await + �ź���(`SemaphoreSlim`)ʵ�ֵ�**�첽����**

## [����](https://www.jianshu.com/p/a35499c00333)

## Demo
```C#
static void Main(string[] args)
{
    var queue = AsyncQueue.Create<string>(); // ��ʼ���첽����
    var source = new CancellationTokenSource(); // ��ʼ��ȡ����־
    var token = source.Token; 
    var senders = Enumerable.Range(0, 3).Select(index => new Sender(index, queue, (ConsoleColor)(index+13))).ToArray(); // ��ʼ��3��������
    var receivers = Enumerable.Range(0, 10).Select(index => new Receiver(index, queue, (ConsoleColor)(index + 5))).ToArray(); // ��ʼ��10��������

    Parallel.ForEach(receivers, async x => await x.Receive(token)); // ��������10��������

    Thread.Sleep(1000); // �ӳ�1�� �ȴ�������ȫ���������
    var message = 0;
    // ��������3�������ߣ�ÿ�������߷���10�Σ���������Ϊ��1��ʼ�������������֣�Ҳ����1~30
    Parallel.ForEach(senders, async x =>
    {
        for (var i = 0; i < 10; i++)
        {
            await x.Send(Interlocked.Increment(ref message).ToString());
        }
    });

    Console.ReadLine();
    source.Cancel(); // ֹͣ���н�����
    Console.ReadLine();
}
```

## ����˵��
#### [1.0.1.0] 2018.06.03
* ���������������

#### [1.0.0.0] 2018.06.03
* ��ʼ�汾