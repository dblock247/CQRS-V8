using System;

namespace CQRS.Application.Interfaces;

public interface IRankable<out T> where T: IComparable<T>
{
    T Value { get; }
    int Rank { get; set; }
}
