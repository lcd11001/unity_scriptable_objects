using Inventory.Model;
using UnityEditor;
using UnityEngine;

// https://www.sunnyvalleystudio.com/blog/unity-2d-sprite-preview-inspector-custom-editor
[CustomEditor(typeof(ConsumableItemSO))]
[CanEditMultipleObjects]
public class ItemEditor : Editor
{
    ItemSO item;

    private void OnEnable()
    {
        item = target as ItemSO;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (item.Image != null)
        {
            DrawSpritePreview(item.Image);
        }
    }

    private void DrawSpritePreview(Sprite sprite)
    {
        // convert sprite to texture
        Texture2D texture = AssetPreview.GetAssetPreview(sprite);

        GUIStyle style = new GUIStyle(GUI.skin.box);
        style.alignment = TextAnchor.UpperLeft;
        style.imagePosition = ImagePosition.ImageAbove;

        GUILayout.Box(sprite.name, style, GUILayout.Width(80), GUILayout.Height(80), GUILayout.ExpandWidth(true));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture, ScaleMode.ScaleToFit);
    }
}
