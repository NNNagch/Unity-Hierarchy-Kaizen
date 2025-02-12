using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class 全体設定
{
    public static bool 機能全体有効 = true;
    public static Color ツリーカラー;
    // ITL画像やブロックの第二描画用のカラー（右に1pxずらした場合の色）
    public static Color 代替描画色 = new Color(1.0f, 1.0f, 1.0f, 0.0000000000000078f);

    static 全体設定()
    {
        // EditorGUIUtility.isProSkin が true の場合はダークモード、false の場合はライトモード
        if (EditorGUIUtility.isProSkin)
        {
            ツリーカラー = new Color(0.8f, 0.8f, 0.8f, 1f);//ダーク
        }
        else
        {
            ツリーカラー = new Color(0.33f, 0.33f, 0.33f, 1f);//ライト
        }
    }
}

[InitializeOnLoad]
public static class ヒエラルキーハイライト
{
    private static Texture2D _I画像;

    static ヒエラルキーハイライト()
    {
        EditorApplication.hierarchyWindowItemOnGUI += _Iを描画;
        _I画像 = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Picture/I.png");
    }

    private static void _Iを描画(int _インスタンスID, Rect _描画範囲)
    {
        if (!全体設定.機能全体有効) return;
        GameObject _オブジェクト = EditorUtility.InstanceIDToObject(_インスタンスID) as GameObject;
        if (_オブジェクト == null) return;
        int _c階層レベル = 取得階層レベル(_オブジェクト);
        GameObject[] _祖先のB配列 = 取得祖先B一覧(_オブジェクト);
        foreach (GameObject _bオブジェクト in _祖先のB配列)
        {
            int _b階層レベル = 取得階層レベル(_bオブジェクト);
            if (_b階層レベル == 1) continue;
            int _d値 = _c階層レベル - _b階層レベル;
            int _e値 = (_d値 * 14) + 8;
            Rect _画像描画範囲 = new Rect(_描画範囲.x - _e値, _描画範囲.y - 1, 16, 16);
            _Iを描画(_画像描画範囲, 全体設定.ツリーカラー);
        }
    }

    private static void _Iを描画(Rect _対象範囲, Color _指定色)
    {
        if (_I画像 != null)
        {
            Color _保存色 = GUI.color;
            // 最初の画像を描画
            GUI.color = _指定色;
            GUI.DrawTexture(_対象範囲, _I画像);
            // 右に1pxずらした位置に第二の画像を描画（ITLの第二描画用カラーを再利用）
            GUI.color = 全体設定.代替描画色;
            Rect _対象範囲2 = new Rect(_対象範囲.x + 1, _対象範囲.y, _対象範囲.width, _対象範囲.height); // 右に1pxオフセットした範囲を作成
            GUI.DrawTexture(_対象範囲2, _I画像);
            GUI.color = _保存色;
        }
    }

    private static int 取得階層レベル(GameObject _オブジェクト)
    {
        int _階層レベル = 0;
        Transform _現在の = _オブジェクト.transform;
        while (_現在の != null)
        {
            _階層レベル++;
            _現在の = _現在の.parent;
        }
        return _階層レベル;
    }

    private static GameObject[] 取得祖先B一覧(GameObject _子オブジェクト)
    {
        List<GameObject> _bリスト = new List<GameObject>();
        Transform _現在の = _子オブジェクト.transform.parent;
        while (_現在の != null)
        {
            if (判定B(_現在の.gameObject)) _bリスト.Add(_現在の.gameObject);
            _現在の = _現在の.parent;
        }
        return _bリスト.ToArray();
    }

    private static bool 判定B(GameObject _オブジェクト)
    {
        if (_オブジェクト == null) return false;
        GameObject[] _ルートオブジェクト = GetRootObjectsWithChildren();
        bool _階層1条件を満たす = _ルートオブジェクト.Length >= 2 && System.Array.Exists(_ルートオブジェクト, o => o == _オブジェクト);
        Transform _親 = _オブジェクト.transform.parent;
        bool _最下段ではない = _親 != null && IsNotLastSibling(_オブジェクト);
        bool _同階層条件を満たす = _親 != null && _オブジェクト.transform.childCount > 0 && _親.childCount >= 2 && _最下段ではない;
        return (_階層1条件を満たす || _同階層条件を満たす);
    }

    private static GameObject[] GetRootObjectsWithChildren()
    {
        GameObject[] _全オブジェクト = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        return System.Array.FindAll(_全オブジェクト, o => o.transform.childCount > 0);
    }

    private static bool IsNotLastSibling(GameObject _オブジェクト)
    {
        Transform _親 = _オブジェクト.transform.parent;
        if (_親 == null) return false;
        return _親.GetChild(_親.childCount - 1) != _オブジェクト.transform;
    }
}

public static class _ツリー
{
    private static float よこブロックオフセット = -8f;
    private static float たてブロックオフセット = 8f;
    private static Dictionary<Transform, Rect> _親子関係マップ = new Dictionary<Transform, Rect>();

    [InitializeOnLoadMethod]
    private static void 初期化()
    {
        EditorApplication.hierarchyWindowItemOnGUI -= ヒエラルキーアイテム描画;
        EditorApplication.hierarchyWindowItemOnGUI += ヒエラルキーアイテム描画;
    }

    private static void ヒエラルキーアイテム描画(int _インスタンスID, Rect _選択範囲)
    {
        if (!全体設定.機能全体有効) return;
        if (Event.current.type != EventType.Repaint) return;
        UnityEngine.Object _対象オブジェクト = EditorUtility.InstanceIDToObject(_インスタンスID);
        if (!(_対象オブジェクト is GameObject)) return;
        GameObject _ゲームオブジェクト = (GameObject)_対象オブジェクト;
        Transform _トランスフォーム = _ゲームオブジェクト.transform;
        if (!_親子関係マップ.ContainsKey(_トランスフォーム))
        {
            _親子関係マップ.Add(_トランスフォーム, _選択範囲);
        }
        else
        {
            _親子関係マップ[_トランスフォーム] = _選択範囲;
        }
        if (_トランスフォーム.parent == null) return;
        Transform _親 = _トランスフォーム.parent;
        if (!_親子関係マップ.ContainsKey(_親)) return;
        Rect _子Rect = _親子関係マップ[_トランスフォーム];
        Handles.BeginGUI();

        // ブロックのカラーもツリーカラーで指定
        Rect _青ブロック = new Rect(_選択範囲.xMin + よこブロックオフセット, _選択範囲.center.y - たてブロックオフセット, 1f, 1f);
        int _階層レベル = 取得階層レベル(_ゲームオブジェクト);

        if (_トランスフォーム.childCount > 0 && _階層レベル != 1)
        {
            // 最初の青ブロックを描画
            EditorGUI.DrawRect(_青ブロック, 全体設定.ツリーカラー);
            // 右に1pxずらした位置に第二の青ブロックを描画（ITLの第二描画用カラーを再利用）
            EditorGUI.DrawRect(new Rect(_青ブロック.x + 1, _青ブロック.y, _青ブロック.width, _青ブロック.height), 全体設定.代替描画色);
        }

        Handles.color = new Color(0.28f, 0.28f, 0.28f, 1.0f);
        bool _葉ノード = (_トランスフォーム.childCount == 0);
        bool _最下層 = (_トランスフォーム.GetSiblingIndex() == _トランスフォーム.parent.childCount - 1);
        if (_葉ノード)
        {
            if (_最下層)
            {
                _Lを描画(_子Rect, 全体設定.ツリーカラー);
            }
            else
            {
                _Tを描画(_子Rect, 全体設定.ツリーカラー);
            }
        }
        Handles.EndGUI();
    }

    private static void _Tを描画(Rect _対象範囲, Color _指定色)
    {
        Rect _画像位置 = new Rect(_対象範囲.xMin - 8f, _対象範囲.yMin - 1f, 20f, _対象範囲.height);
        Texture2D _T画像 = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Picture/T.png");
        if (_T画像 != null)
        {
            Color _保存色 = GUI.color;
            GUI.color = _指定色;
            GUI.DrawTexture(_画像位置, _T画像);
            // 右に1pxずらした位置に第二の画像を描画（ITLの第二描画用カラーを再利用）
            GUI.color = 全体設定.代替描画色;
            Rect _画像位置2 = new Rect(_画像位置.x + 1, _画像位置.y, _画像位置.width, _画像位置.height); // 右に1pxオフセットした位置を作成
            GUI.DrawTexture(_画像位置2, _T画像);
            GUI.color = _保存色;
        }
    }

    private static void _Lを描画(Rect _対象範囲, Color _指定色)
    {
        Rect _画像位置 = new Rect(_対象範囲.xMin - 8f, _対象範囲.yMin - 2f, 20f, _対象範囲.height);
        Texture2D _L画像 = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/Picture/L.png");
        if (_L画像 != null)
        {
            Color _保存色 = GUI.color;
            GUI.color = _指定色;
            GUI.DrawTexture(_画像位置, _L画像);
            // 右に1pxずらした位置に第二の画像を描画（ITLの第二描画用カラーを再利用）
            GUI.color = 全体設定.代替描画色;
            Rect _画像位置2 = new Rect(_画像位置.x + 1, _画像位置.y, _画像位置.width, _画像位置.height); // 右に1pxオフセットした位置を作成
            GUI.DrawTexture(_画像位置2, _L画像);
            GUI.color = _保存色;
        }
    }

    private static int 取得階層レベル(GameObject _オブジェクト)
    {
        int _階層レベル = 0;
        Transform _現在の = _オブジェクト.transform;
        while (_現在の != null)
        {
            _階層レベル++;
            _現在の = _現在の.parent;
        }
        return _階層レベル;
    }
}
