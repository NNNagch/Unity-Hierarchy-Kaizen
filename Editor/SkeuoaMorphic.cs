using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameObject))]
public class ヒエラルキーオブジェクトアイコンにグラデーションを描画 : Editor
{
    // グラデーションを作成するメソッド
    static Texture2D _グラデーションテクスチャ;

    // ヒエラルキーにアイコンを描画するメソッド
    [InitializeOnLoadMethod]
    static void 初期化()
    {
        // グラデーションのテクスチャを作成（上色、下色、グラデーションの高さ
        _グラデーションテクスチャ = グラデーションを作成(new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 0.1f), 10);
        
        // ヒエラルキーのアイコン描画イベントを購読
        EditorApplication.hierarchyWindowItemOnGUI += ヒエラルキーアイコン描画;
    }

    // グラデーションを作成するメソッド
    static Texture2D グラデーションを作成(Color 上色, Color 下色, int 高さ)
    {
        // 高さに合わせたテクスチャを作成
        Texture2D _テクスチャ = new Texture2D(1, 高さ);

        // ピクセルごとに色を設定 
        for (int y = 0; y < 高さ; y++)
        {
            // 上色から下色に向けて色を補間
            Color _補間色 = Color.Lerp(上色, 下色, (float)y / (float)高さ);
            _テクスチャ.SetPixel(0, y, _補間色);
        }

        // 変更を反映
        _テクスチャ.Apply();

        return _テクスチャ;
    }

    // アイコンにグラデーションを描画するメソッド
    static void ヒエラルキーアイコン描画(int _id, Rect _rect)
    {
        // ヒエラルキーで対象となるオブジェクトを取得
        GameObject _ゲームオブジェクト = EditorUtility.InstanceIDToObject(_id) as GameObject;

        // オブジェクトが存在し、アイコンが表示される場合
        if (_ゲームオブジェクト != null)
        {
            // 階層レベルが1でない場合は描画しない
            if (_ゲームオブジェクト.transform.parent != null)
            {
                return;
            }

            // アイコンの描画開始位置を32pxに調整
            Rect _矩形 = new Rect(32, _rect.y, EditorGUIUtility.currentViewWidth - 32, _rect.height / 1);

            // グラデーションを描画
            GUI.DrawTexture(_矩形, _グラデーションテクスチャ);
        }
    }
}
