﻿using System;
using System.Collections.Generic;

public static class DictionaryUtils
{
    // Apparently C# doesn't support operator constraints on generics,
    // so this ends up being like this

    /// <summary>
    ///   Sums the values in a dictionary
    /// </summary>
    /// <returns>The sum.</returns>
    /// <param name="items">Dictionary to sum things in</param>
    public static float SumValues<T>(this Dictionary<T, float> items)
    {
        float sum = 0.0f;

        foreach (var entry in items)
        {
            sum += entry.Value;
        }

        return sum;
    }

    /// <summary>
    ///   Merge the values in items and valuesToAdd. When both dictionaries
    ///   contain the same key the result in items has the sum of the values
    ///   under the same key.
    /// </summary>
    /// <param name="items">Items to add things to. As well as the result</param>
    /// <param name="valuesToAdd">Values to add to items.</param>
    public static void Merge<T>(this Dictionary<T, float> items,
        Dictionary<T, float> valuesToAdd)
    {
        foreach (var entry in valuesToAdd)
        {
            float existing = 0.0f;

            if (items.ContainsKey(entry.Key))
                existing = items[entry.Key];

            items[entry.Key] = entry.Value + existing;
        }
    }
}
