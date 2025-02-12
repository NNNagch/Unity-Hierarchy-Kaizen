using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class HierarchyLineDrawer
{
    static HierarchyLineDrawer()
    {
        // ヒエラルキーが描画される前にコールバックを登録
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGUI;
    }

    private static void OnHierarchyWindowItemGUI(int instanceID, Rect selectionRect)
    {
        // Xオフセットを32pxに設定
        float offsetX = 32f;

        // ヒエラルキーウィンドウの全体幅を取得
        float width = Screen.width;

        // インスタンスIDからオブジェクトを取得
        GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (go != null && go.transform.parent == null && go.transform.childCount > 0)
        {
            // 階層レベル1で子オブジェクトを持っている場合にのみラインを描画
            Rect lineRect = new Rect(offsetX, selectionRect.y + selectionRect.height - 1, width - offsetX, 1);
            // 0.1, 0.1, 0.1, 0.1 のカラー（透明度が低い黒）でラインを描画
            EditorGUI.DrawRect(lineRect, new Color(0f, 0f, 0f, 0.2f));
        }
    }
}
