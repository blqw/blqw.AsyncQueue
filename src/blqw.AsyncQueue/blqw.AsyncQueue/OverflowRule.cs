namespace blqw
{
    /// <summary>
    /// 元素溢出规则
    /// </summary>
    public enum OverflowRule
    {
        /// <summary>
        /// 抛出异常
        /// </summary>
        ThrowException = 0,
        /// <summary>
        /// 丢弃第一个
        /// </summary>
        DiscardFirst = 1,
        /// <summary>
        /// 丢弃最后一个
        /// </summary>
        DiscardLast = 2,
    }
}
