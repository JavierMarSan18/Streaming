namespace Netflixs2.Domain.Interfaces;

public interface INetflixs2Result
{
    public bool IsSuccess { get; }
    public string Message { get; }
}

#pragma warning disable CS8618
public class Netflixs2Result<T> : INetflixs2Result
{
    #region Fields
    private readonly T _value;
    #endregion
    #region Properties
    public bool IsSuccess { get; }
    public string Message { get; }
    public T? Value
    {
        get
        {
            if (!IsSuccess)
                return default;
            return _value;
        }
    }
    #endregion
    #region Constructors
    public Netflixs2Result(T value)
    {
        IsSuccess = true;
        _value = value;
    }
    #endregion
}
