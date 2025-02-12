using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

// FirstLevelBottomLineDrawer クラス
// ※ ヒエラルキーウィンドウ内で固定された位置に
//    矩形を 1 回だけ描画したい例です。
//    これにより、ウィンドウがスクロールされても描画が消えません。
//    さらに、複数のアイテム（GameObject）に対して呼び出されても
//    重ね塗りが発生しないように工夫しています。
[InitializeOnLoad]
public static class FirstLevelBottomLineDrawer
{
    // レクト描画のX方向オフセット量を調整する変数
    private static float _オフセットX = 0f;

    // レクト描画のY方向オフセット量を調整する変数
    private static float _オフセットY = -4f;

    // すでにこのフレームの Repaint で描画済みかどうかを判定する変数
    private static bool _すでに描画済みか = false;

    // グラデーション用テクスチャ
    private static Texture2D _グラデーションテクスチャ;

    // 静的コンストラクタ
    static FirstLevelBottomLineDrawer()
    {
        // ヒエラルキーウィンドウの描画イベントにハンドラを追加
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;

        // グラデーションのテクスチャを作成（上色、下色、グラデーションの高さ）
        _グラデーションテクスチャ = グラデーションを作成(new Color(1f, 1f, 0.9f, 1.0f), new Color(1f, 1f, 0.9f, 1.0f), 0); // 16px 高さ
    }

    // ヒエラルキー上の各項目描画時に呼ばれる関数
    static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        // Layout イベントのタイミングで「_すでに描画済みか」をリセット
        if (Event.current.type == EventType.Layout)
        {
            _すでに描画済みか = false;
        }

        // Repaint イベント時のみ処理を実行し、かつまだ描画していない場合のみ描画する
        if (Event.current.type == EventType.Repaint && !_すでに描画済みか)
        {
            _すでに描画済みか = true;

            // 1px の赤いラインを描画
            Rect Oライン = new Rect(
                0 + _オフセットX,
                20 + _オフセットY,
                EditorGUIUtility.currentViewWidth,
                1
            );
            EditorGUI.DrawRect(Oライン, new Color(0.0f, 0.0f, 0.0f, 0.0f));

            // ダークモードかライトモードかを判定
            bool _ダークモード = EditorGUIUtility.isProSkin;

            // 最初の矩形を描画
            Rect 新矩形 = new Rect(
                Oライン.x,
                Oライン.y + 16,
                EditorGUIUtility.currentViewWidth,
                16
            );

            // グラデーションを描画
            GUI.DrawTexture(新矩形, _グラデーションテクスチャ);

            // ここでは 50 回、グラデーション矩形を下方向へ並べて描画
            // 直前の矩形の底面から 16px 下に次の矩形を作るため、重なり合いは発生しません。
            Rect 現在の矩形 = 新矩形;
            for (int _回数 = 0; _回数 < 50; _回数++)
            {
                Rect 次矩形 = new Rect(
                    現在の矩形.x,
                    現在の矩形.y + 現在の矩形.height + 0,
                    EditorGUIUtility.currentViewWidth,
                    16
                );
                // グラデーションを描画
                GUI.DrawTexture(次矩形, _グラデーションテクスチャ);
                現在の矩形 = 次矩形;
            }
        }
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
}
