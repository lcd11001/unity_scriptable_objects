using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    /// <summary>
    /// Destroy all child objects of this transform
    /// Use it like so:
    /// <code>
    /// transform.DestroyAllChildren();
    /// </code>
    /// </summary>
    /// <param name="parent"></param>
    public static void DestroyAllChildren(this Transform parent)
    {
        foreach (Transform child in parent)
        {
            Object.Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Swap value of two elements
    /// Use it like so:
    /// <code>
    /// list.Swap(index1, index2);
    /// </code>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    public static void Swap<T>(this List<T> list, int i, int j)
    {
        //Debug.Log($"Swap i {i} vs j {j} vs len {list.Count}");
        T temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
