using System.Reflection;

namespace ITranslateTrainer.Tests.Domain.Unit.Builders;

public abstract class Builder<T> where T : class
{
    protected T Value = null!;

    protected void SetPublicInstanceProperty(string name, object value)
    {
        var property = typeof(T).GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
        property!.SetValue(Value, value);
    }

    protected void SetPrivateInstanceField(string name, object value)
    {
        var field = typeof(T).GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
        field!.SetValue(Value, value);
    }

    public T Build() => Value;
}
