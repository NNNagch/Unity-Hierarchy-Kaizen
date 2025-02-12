using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class HierarchyImageDrawer
{
    private static Texture2D _cornImage;
    private static float _xOffset = -11f; // Xオフセット値

    static HierarchyImageDrawer()
    {
        // ヒエラルキーの描画イベントに参加
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;

        // corn.png画像をロード
        _cornImage = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Picture/corn.png");
    }

    static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        // 画像を描画する位置を設定
        float xPosition = selectionRect.x + _xOffset;
        float yPosition = selectionRect.y + (selectionRect.height - _cornImage.height) / 2;

        // 描画範囲をRectとして定義
        Rect imageRect = new Rect(xPosition, yPosition, _cornImage.width, _cornImage.height);

        // マウスの位置を取得
        Vector2 mousePosition = Event.current.mousePosition;

        // マウスが画像の範囲内にホバーしているかを確認
        bool isMouseOverImage = imageRect.Contains(mousePosition);

        // マウスが画像範囲内にホバーしていない場合、描画しない
        if (isMouseOverImage && _cornImage != null)
        {
            GUI.DrawTexture(imageRect, _cornImage);
        }
    }
}
