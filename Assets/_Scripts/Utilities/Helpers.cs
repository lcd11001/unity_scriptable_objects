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
}
