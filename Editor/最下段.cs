using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class FirsBottomLineDrawer
{
    static FirsBottomLineDrawer()
    {
        // ヒエラルキーウィンドウの描画イベントにハンドラを追加
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        // Repaint イベント時のみ処理する
        if (Event.current.type != EventType.Repaint)
            return;

        // InstanceID から GameObject を取得
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj == null)
            return;


        // アクティブシーンのルートオブジェクト一覧を取得
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        if (rootObjects == null || rootObjects.Length == 0)
            return;

        // 現在のオブジェクトが、ルートオブジェクト配列の最後の要素（＝最下段）かを判定
        int index = System.Array.IndexOf(rootObjects, obj);
        if (index != rootObjects.Length - 1)
            return;

        // 最下段のルートオブジェクトの底面に、ウィンドウ全幅 1px のラインを描画
        // ※ 変数名 lineRect を Oライン に変更
        float offsetX = 32f; // Xオフセットを32pxに設定
        Rect Oライン = new Rect(offsetX, selectionRect.y + selectionRect.height - 1, EditorGUIUtility.currentViewWidth - offsetX, 1);
        // 最初のラインの色を (0.63f, 0.63f, 0.63f, 1f) に設定
        EditorGUI.DrawRect(Oライン, new Color(0.63f, 0.63f, 0.63f, 1f));

        // Oラインの下にもう一本ラインを描画（同じオフセットで）
        Rect Oライン2 = new Rect(offsetX, selectionRect.y + selectionRect.height, EditorGUIUtility.currentViewWidth - offsetX, 1);
        // ２本目のラインの色を (0.91f, 0.91f, 0.91f, 1f) に設定
        EditorGUI.DrawRect(Oライン2, new Color(0.91f, 0.91f, 0.91f, 0f));
    }
}
