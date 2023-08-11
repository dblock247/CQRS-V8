using System;
using System.Collections.Generic;
using System.Linq;
using CQRS.Application.Interfaces;

namespace CQRS.Application.Helpers;

public class Rank
{
    /// <summary>
    /// Rank objects in ascending order where the lowest values produce the best rank.
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> RankAscending<K, T>(IEnumerable<T> data) where T : class, IComparable<T>, IRankable<K>, new() where K : IComparable<K>
    {
        var groups = data
            .OrderBy(o => o.Value)
            .GroupBy(o => o.Value)
            .ToDictionary(o => o.Key, o => o.ToList());

        return Sort(groups);
    }

    /// <summary>
    /// Rank objects in descending order where the highest values produce the best rank.
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> RankDescending<K, T>(IEnumerable<T> data) where T : class, IComparable<T>, IRankable<K>, new() where K : IComparable<K>
    {
        var groups = data
            .OrderByDescending(o => o.Value)
            .GroupBy(o => o.Value)
            .ToDictionary(o => o.Key, o => o.ToList());

        return Sort(groups);
    }

    private static List<T> Sort<K, T>(Dictionary<K, List<T>> groups) where T: class, IComparable<T>, IRankable<K>, new() where K : IComparable<K>
    {
        var rank = 1;
        foreach (var (key, value) in groups)
        {
            foreach (var user in value)
                user.Rank = rank;

            rank += value.Count();
        }

        return groups
            .SelectMany(o => o.Value)
            .ToList();
    }
}
