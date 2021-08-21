namespace Services.AI.Interfaces
{
    public interface IComparison<T>
    {
        bool Check(T a, T b);
    }
}