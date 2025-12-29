namespace ClashOfClaps.Data.Framework;

public class LimitedQueue<T> : Queue<T>
{
    public int Limit { get; }

    public LimitedQueue(int limit) : base(limit)
    {
        Limit = limit;
    }

    public new void Enqueue(T item)
    {
        while (Count > Limit)
            Dequeue();
        
        base.Enqueue(item);
    }
}