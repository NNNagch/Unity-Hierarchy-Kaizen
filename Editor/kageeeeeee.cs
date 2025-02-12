using UnityEditor;
using UnityEngine;

public class ヒエラルキー座標取得 : EditorWindow
{
    // ヒエラルキーウィンドウの位置やサイズを保持する変数
    private Rect _ヒエラキーウィンドウ位置;

    [MenuItem("Tools/ヒエラルキー座標取得")]
    static void ヒエラルキー座標取得メニュー()
    {
        // ヒエラルキーウィンドウを取得
        var _ヒエラキーウィンドウ = EditorWindow.GetWindow(System.Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor"));

        // ヒエラルキーウィンドウが見つかった場合、ウィンドウを表示する
        if (_ヒエラキーウィンドウ != null)
        {
            _ヒエラキーウィンドウ.Show();
        }
        else
        {
            Debug.LogError("ヒエラルキーウィンドウが取得できませんでした。");
        }
    }

    // OnGUIメソッド内で描画処理を行う
    void OnGUI()
    {
        // ヒエラルキーウィンドウの位置とサイズを取得
        var _ヒエラキーウィンドウ = EditorWindow.GetWindow(System.Type.GetType("UnityEditor.SceneHierarchyWindow,UnityEditor"));
        
        if (_ヒエラキーウィンドウ != null)
        {
            // ヒエラルキーウィンドウの位置とサイズを格納
            _ヒエラキーウィンドウ位置 = _ヒエラキーウィンドウ.position;

            // ヒエラルキーウィンドウの下端の座標
            float _下端座標 = _ヒエラキーウィンドウ位置.y + _ヒエラキーウィンドウ位置.height;

            // Y座標に30を加算して、描画位置を決定
            float _描画Y座標 = _下端座標 + 30;

            // 描画するレクトの位置とサイズを指定（横幅はウィンドウ幅、縦幅は30ピクセル）
            Rect _描画するレクト = new Rect(_ヒエラキーウィンドウ位置.x, _描画Y座標, _ヒエラキーウィンドウ位置.width, 30);

            // 描画するレクトを赤色で描画
            Handles.BeginGUI();
            GUI.color = Color.red; // レクトの色を赤に設定
            GUI.DrawTexture(_描画するレクト, Texture2D.whiteTexture); // レクトを描画
            Handles.EndGUI();
        }
    }
}
