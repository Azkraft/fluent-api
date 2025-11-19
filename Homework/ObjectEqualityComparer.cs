using System.Diagnostics.CodeAnalysis;

namespace Homework;

public class ObjectEqualityComparer : IEqualityComparer<object>
{
    public new bool Equals(object? x, object? y)
    {
        return ReferenceEquals(x, y);
    }

    public int GetHashCode([DisallowNull] object obj)
    {
        return obj.GetHashCode();
    }
}
