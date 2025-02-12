using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "▃▀Unity Tree▃▀/▃▀コントローラー▃▀", fileName = "ｺﾝﾄﾛｰﾗｰ")]
public class ﾂﾘｰ : ScriptableObject
{
    //bool
    public bool _スイッチ = false; 
    public bool _ツリー = false; 
    public bool _ボーダーライン = true;




    // ツリー　カラー
    public Color[] _ライトモード_ﾂﾘｰ = new Color[5]
    {
        new Color(1.0f,   1.0f,   1.0f,   1.0f),//////////白
        new Color(0.94f,  0.94f,  0.94f,  1.0f),
        new Color(0.596f, 0.596f, 0.596f, 1.0f),
        new Color(0.26f,   0.26f,   0.26f,   1.0f),
        new Color(0.10f,   0.10f,   0.10f,   1.0f)//黒
    };
    public Color[] _ダークモード_ﾂﾘｰ = new Color[5]
    {
        new Color(1.0f,   1.0f,   1.0f,   1.0f),//////////白
        new Color(0.825f, 0.825f, 0.825f, 1.0f),
        new Color(0.596f, 0.596f, 0.596f, 1.0f),
        new Color(0.1f,   0.1f,   0.1f,   1.0f),
        new Color(0.0f,   0.0f,   0.0f,   1.0f)/////黒
    };





    // ボーダー　カラー
    public Color[] _ライトモード_ﾎﾞｰﾀﾞｰ = new Color[5]
    {
        new Color(0.91f,  0.91f,  0.91f,  1.0f),
        new Color(0.91f,  0.91f,  0.91f,  1.0f),
        new Color(0.61f, 0.61f, 0.61f, 1.0f),
        new Color(0.91f,  0.91f,  0.91f,  1.0f),
        new Color(0.91f,  0.91f,  0.91f,  1.0f)
    };
    public Color[] _ダークモード_ﾎﾞｰﾀﾞｰ = new Color[5]
    {
        new Color(0.33f,  0.33f,  0.33f,  1.0f),
        new Color(0.45f,  0.45f,  0.45f,  1.0f),
        new Color(0.33f,  0.33f,  0.33f,  1.0f),
        new Color(0.45f,  0.45f,  0.45f,  1.0f),
        new Color(0.45f,  0.45f,  0.45f,  1.0f)
    };






    // トップツリー　カラー
    public Color _トップツリーカラー = new Color(1f, 1f, 1f, 1.0f);



    // ツリー濃度(0~4)　インデックス
    public int _現在の段階インデックス = 2;
    // ボーダー濃度(0~4)　インデックス
    public int _ボーダー現在の段階インデックス = 2;





    //チェックボックス　インデックス
    public bool[] _ラベルカラー_チェックボックス = new bool[5];
    //入力フィールド　インデックス
    public string[] _ラベルカラー_文字列 = new string[5];


    // ラベルカラー　固定カラー
    public static readonly Color[] _ラベルカラー = new Color[5]
    {
        new Color(0.4f, 0.4f, 1.0f, 0.3f), //：あお

        new Color(0.8f, 0.4f, 1.0f, 0.3f), //：むらさき
        new Color(0.55f, 1.0f, 0.55f, 0.3f), //：みどり

        new Color(1.0f, 1.0f, 0.5f, 0.3f), //：きいろ
        new Color(0.0f, 0.8f, 1.0f, 0.3f)  //：水
    };
}







































//以下、スクリプタブルオブジェクト、インスペクター　


//---------------------------------------------------------------------------------------------------------------------------------------------------------
// インスペクター　コントローラー
[CustomEditor(typeof(ﾂﾘｰ))]
public class ツリー設定エディター : Editor
{

    public override void OnInspectorGUI()
    {
        ﾂﾘｰ _設定 = (ﾂﾘｰ)target;

        // スイッチ、ツリー、ボーダー
        _設定._スイッチ = EditorGUILayout.Toggle("スイッチ", _設定._スイッチ);
        _設定._ツリー = EditorGUILayout.Toggle("ツリー", _設定._ツリー);
        _設定._ボーダーライン = EditorGUILayout.Toggle("ボーダーライン", _設定._ボーダーライン);

        EditorGUILayout.Space();

        // ツリー濃度
        bool _ダークモード = EditorGUIUtility.isProSkin;
        Color[] _ブランチ参照カラー = _ダークモード ? _設定._ダークモード_ﾂﾘｰ : _設定._ライトモード_ﾂﾘｰ;


        // カスタムスタイル作成
        GUIStyle _ラベルスタイル = new GUIStyle(EditorStyles.boldLabel);
        _ラベルスタイル.richText = true; // Rich Textを有効にする

        EditorGUILayout.LabelField(
            $"ツリー濃度(<color=green>{_設定._現在の段階インデックス + 1}</color>)", 
            _ラベルスタイル
        );








        // ツリー濃度//////////////////////////////////////////////////////////////////////////////////
        EditorGUILayout.BeginHorizontal();
        for (int i = 0; i < 5; i++)
        {
            Color _originalColor = GUI.color;
            GUI.color = _ブランチ参照カラー[i];
            if (GUILayout.Button((i + 1) + ""))
            {
                _設定._現在の段階インデックス = i;
            }
            GUI.color = _originalColor;
        }
        EditorGUILayout.EndHorizontal();








        EditorGUILayout.Space();
        // ボーダー濃度////////////////////////////////////////////////////
        Color[] _ボーダー参照カラー = _ダークモード ? _設定._ダークモード_ﾎﾞｰﾀﾞｰ : _設定._ライトモード_ﾎﾞｰﾀﾞｰ;
        EditorGUILayout.LabelField(
            $"ボーダー濃度(<color=green>{_設定._ボーダー現在の段階インデックス + 1}</color>)", 
            _ラベルスタイル
        );
        EditorGUILayout.BeginHorizontal();//////////////////////////
        for (int i = 0; i < 5; i++)
        {
            Color _originalColor = GUI.color;
            GUI.color = _ボーダー参照カラー[i];
            if (GUILayout.Button((i + 1) + ""))
            {
                _設定._ボーダー現在の段階インデックス = i;
            }
            GUI.color = _originalColor;
        }
        EditorGUILayout.EndHorizontal();////////////////////////////













        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        // ラベルカラー　///////////////////////////////////////////////////////////////////////








///////////////////////////////////////////////////////////////////////
///

EditorGUILayout.LabelField("ラベルカラー", EditorStyles.boldLabel);

for (int i = 0; i < 5; i++)
{
    EditorGUILayout.BeginHorizontal(); // 水平方向に配置

    // 現在のGUIカラーを保存
    Color previousColor = GUI.backgroundColor;

    // ボタン専用の不透明な色（アルファ値1.0）を設定
    Color opaqueColor = ﾂﾘｰ._ラベルカラー[i];
    opaqueColor.a = 1.0f; // アルファ値を1.0（完全不透明）に設定

    // ボタン用の色を適用
    GUI.backgroundColor = opaqueColor;

    // ボタン描画（クリック時に処理を実行）
    if (GUILayout.Button(""))
    {
        // ボタンがクリックされたときの処理
        適用ラベルカラー(i, true);
    }

    // 入力フィールド用の不透明な色を適用
    GUI.backgroundColor = opaqueColor;

    // テキストフィールド（アルファ値1.0の不透明色で描画）
    GUILayout.Space(10); // レイアウトの余白を確保
    string newName = EditorGUILayout.TextField(_設定._ラベルカラー_文字列[i]);
    if (newName != _設定._ラベルカラー_文字列[i])
    {
        _設定._ラベルカラー_文字列[i] = newName;
    }

    // GUI.colorを元に戻す
    GUI.backgroundColor = previousColor;

    EditorGUILayout.EndHorizontal();
}

// ヒエラルキーにラベルカラーを適用する処理
void 適用ラベルカラー(int index, bool isActive)
{

}




EditorGUILayout.Space();
EditorGUILayout.LabelField("(対応する色で強調表示されます。)", EditorStyles.miniLabel);


EditorGUILayout.Space();
EditorGUILayout.Space();
EditorGUILayout.Space();
EditorGUILayout.Space();
EditorGUILayout.Space();
EditorGUILayout.Space();
EditorGUILayout.Space();
EditorGUILayout.Space();
EditorGUILayout.Space(100);
EditorGUILayout.Space(100);
EditorGUILayout.Space(100);
EditorGUILayout.Space(100);















        
        //描画更新///////////////////////////////////////////////////
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_設定);
            EditorApplication.RepaintHierarchyWindow();
            EditorApplication.RepaintProjectWindow();
        }
    }
}



















































//以下、メイン処理




//---------------------------------------------------------------------------------------------------------------------------------------------------------
// ツリー (ヒエラルキー描画処理)
[InitializeOnLoad]
public static class ツリー
{
    private static ﾂﾘｰ _コントローラー;
    private static Transform _staticTransform;
    

    static ツリー()
    {
        初期化設定読み込み();
        初期化();
        EditorApplication.hierarchyChanged += 階層変更時処理;
        EditorApplication.projectChanged += プロジェクト更新時処理;
    }

    private static void プロジェクト更新時処理()
    {
        初期化設定読み込み();
        EditorApplication.RepaintHierarchyWindow();
        EditorApplication.RepaintProjectWindow();
    }

    private static void 初期化設定読み込み()
    {
        string[] _候補ファイル = AssetDatabase.FindAssets("ｺﾝﾄﾛｰﾗｰ", new[] { "Assets" });
        if (_候補ファイル.Length > 0)
        {
            string _ファイルパス = AssetDatabase.GUIDToAssetPath(_候補ファイル[0]);
            _コントローラー = AssetDatabase.LoadAssetAtPath<ﾂﾘｰ>(_ファイルパス);
        }
    }

    public static void 初期化()
    {
        EditorApplication.hierarchyWindowItemOnGUI += 階層GUI処理;
    }

    private static void 階層変更時処理()
    {
        EditorApplication.RepaintHierarchyWindow();
    }

    private static Color 現在ブランチカラー取得()
    {
        if (_コントローラー == null) return Color.white;
        bool _ダークモード = EditorGUIUtility.isProSkin;
        Color[] _参照カラー = _ダークモード ? _コントローラー._ダークモード_ﾂﾘｰ : _コントローラー._ライトモード_ﾂﾘｰ;
        int _idx = _コントローラー._現在の段階インデックス;
        if (_idx < 0 || _idx >= _参照カラー.Length) _idx = 2;
        return _参照カラー[_idx];
    }

    private static Color 現在ボーダーカラー取得()
    {
        if (_コントローラー == null) return Color.gray;
        bool _ダークモード = EditorGUIUtility.isProSkin;
        Color[] _参照カラー = _ダークモード ? _コントローラー._ダークモード_ﾎﾞｰﾀﾞｰ : _コントローラー._ライトモード_ﾎﾞｰﾀﾞｰ;
        int _idx = _コントローラー._ボーダー現在の段階インデックス;
        if (_idx < 0 || _idx >= _参照カラー.Length) _idx = 2;
        return _参照カラー[_idx];
    }

    private static Color トップツリーカラー取得()
    {
        if (_コントローラー == null) return Color.white;
        return _コントローラー._トップツリーカラー;
    }

    // オブジェクト名に応じたカラー付与判定
    // 入力フィールドに文字列があり、チェックが入っていて、オブジェクト名にその文字列が含まれていた場合、
    // 該当行のカラーを戻す。なければnullを戻す。
    private static Color? ラベルカラー文字列判定(string objName)
    {
        if (_コントローラー == null) return null;
        for (int i = 0; i < 5; i++)
        {
            if (_コントローラー._ラベルカラー_チェックボックス[i] && !string.IsNullOrEmpty(_コントローラー._ラベルカラー_文字列[i]))
            {
                if (objName.Contains(_コントローラー._ラベルカラー_文字列[i]))
                {
                    return ﾂﾘｰ._ラベルカラー[i];
                }
            }
        }
        return null;
    }
























private static void 階層GUI処理(int _インスタンスID, Rect _選択矩形)
{
    if (_コントローラー == null) return;

    GameObject _オブジェクト = EditorUtility.InstanceIDToObject(_インスタンスID) as GameObject;
    if (_オブジェクト == null) return;

    int _ネストレベル = ネストレベル取得(_オブジェクト.transform);
    _staticTransform = _オブジェクト.transform;

    Color _ブランチカラー = 現在ブランチカラー取得();
    Color _ボーダーカラー = 現在ボーダーカラー取得();
    Color _トップツリーカラー = トップツリーカラー取得();

    // ボーダーライン描画
    if (_ネストレベル == 0 && _コントローラー._ボーダーライン)
    {
        ブランチレンダラー.ボーダー描画(_選択矩形, 1.0f, _ボーダーカラー);
    }

    // 階層レベル0はブランチを描画しない
    if (_ネストレベル == 0) return;

    // ブランチ+ボックス描画
    if (_コントローラー._ツリー)
    {
        bool _最下層 = 最下層かどうか(_オブジェクト.transform);

        // 通常ブランチカラー
        ブランチレンダラー.水平ブランチ描画(_選択矩形, _ネストレベル, _最下層, _オブジェクト.transform.childCount > 0, _ブランチカラー);

        // 垂直ブランチ全描画
        垂直ブランチ全描画(_選択矩形, _ブランチカラー, _ネストレベル);

        // 子無し・ネスト>0ボックス描画
        if (_オブジェクト.transform.childCount == 0 && _ネストレベル > 0)
        {
            ブランチレンダラー.ボックス描画(_選択矩形, _ネストレベル, _ブランチカラー);
        }
    }

    // ラベルカラーによるアイコンカラー付与
    Color? _名称カラー = ラベルカラー文字列判定(_オブジェクト.name);
    if (_名称カラー.HasValue)
    {
        // オブジェクト名左側にアイコン相当の正方形を描画(高さ＝行高さ)
        Rect iconRect = new Rect(_選択矩形.x - 1, _選択矩形.y, _選択矩形.height, _選択矩形.height);
        EditorGUI.DrawRect(iconRect, _名称カラー.Value);
    }
}











    private static int ネストレベル取得(Transform _変換)
    {
        int _レベル = 0;
        while (_変換.parent != null)
        {
            _レベル++;
            _変換 = _変換.parent;
        }
        return _レベル;
    }

    private static bool 最下層かどうか(Transform _変換)
    {
        if (_変換.parent == null) return false;
        Transform _親 = _変換.parent;
        return _親.childCount > 0 && _親.GetChild(_親.childCount - 1) == _変換;
    }









private static void 垂直ブランチ全描画(Rect _選択矩形, Color _カラー, int _ネストレベル)
{
    List<Transform> _ancestorList = new List<Transform>();
    Transform _temp = _staticTransform;

    while (_temp.parent != null)
    {
        _ancestorList.Insert(0, _temp.parent);
        _temp = _temp.parent;
    }

    for (int i = 0; i < _ancestorList.Count; i++)
    {
        Transform _parent = _ancestorList[i].parent;

        // 階層レベル0の親に対する垂直ブランチは描画しない
        if (_parent == null || ネストレベル取得(_ancestorList[i]) == 0)
            continue;

        bool _最後の子か = false;
        if (_parent != null && _parent.childCount > 0 && _parent.GetChild(_parent.childCount - 1) == _ancestorList[i])
        {
            _最後の子か = true;
        }

        if (!_最後の子か)
        {
            int _ancestorLevel = ネストレベル取得(_ancestorList[i]);
            ブランチレンダラー.垂直ブランチ描画(_選択矩形, _ancestorLevel, false, _カラー);
        }
    }
}











    static class ブランチレンダラー
    {
        public static float _バー幅 = 1; 
        private static float _ネストオフセット = 14f; 
        private static float _ボックス横オフセット = 12.5f;
        private static float _ボックス縦オフセット = 0f;
        private static float _ボックス幅 = 3.0f;
        private static float _ボックス高さ = 2.5f;
        private static float _水平ブランチ基準長さ = _ネストオフセット; 
        private static float _水平ブランチ子なし追加長さ = 10f;          
        private static float _水平ブランチ長さ割り算 = 1.5f;            
        private static float _垂直ブランチ全高さ補正 = 1.0f;    
        private static float _垂直ブランチ半高さ補正 = 1.0f;    
        private static float _ボーダーライン基本高さ = 1.0f; 
        private static float _ボーダーライン高さ補正 = 1.0f;
        private static float _ベースレベルライン高さ補正 = 1.0f;

        public static float 開始X座標取得(Rect _元の矩形, int _ネストレベル)
        {
            return 38 + _ネストレベル * _ネストオフセット; 
        }

        public static void 垂直ブランチ描画(Rect _元の矩形, int _ネストレベル, bool _最下層, Color _カラー)
        {
            float _元の高さ = _最下層 ? (_元の矩形.height / 2f) : _元の矩形.height;
            float _高さ = _最下層 ? (_元の高さ * _垂直ブランチ半高さ補正) : (_元の高さ * _垂直ブランチ全高さ補正);
            EditorGUI.DrawRect(
                new Rect(
                    開始X座標取得(_元の矩形, _ネストレベル),
                    _元の矩形.y,
                    _バー幅,
                    _高さ
                ),
                _カラー
            );
        }

        public static void ベースレベルライン描画(Rect _元の矩形, Color _カラー)
        {
            float _高さ = _元の矩形.height * _ベースレベルライン高さ補正;
            EditorGUI.DrawRect(
                new Rect(
                    開始X座標取得(_元の矩形, 0),
                    _元の矩形.y,
                    _バー幅,
                    _高さ
                ),
                _カラー
            );
        }

        public static void 水平ブランチ描画(Rect _元の矩形, int _ネストレベル, bool _最下層, bool _子オブジェクトあり, Color _カラー)
        {
            float _水平長さ = _水平ブランチ基準長さ;
            if (!_子オブジェクトあり)
            {
                _水平長さ += _水平ブランチ子なし追加長さ;
            }

            EditorGUI.DrawRect(
                new Rect(
                    開始X座標取得(_元の矩形, _ネストレベル),
                    _元の矩形.y + _元の矩形.height / 2f,
                    _水平長さ / _水平ブランチ長さ割り算,
                    _バー幅
                ),
                _カラー
            );

            float _元の垂直高さ = _最下層 ? (_元の矩形.height / 2f) : _元の矩形.height;
            float _垂直高さ = _最下層 ? (_元の垂直高さ * _垂直ブランチ半高さ補正) : (_元の垂直高さ * _垂直ブランチ全高さ補正);

            EditorGUI.DrawRect(
                new Rect(
                    開始X座標取得(_元の矩形, _ネストレベル),
                    _元の矩形.y,
                    _バー幅,
                    _垂直高さ
                ),
                _カラー
            );
        }




public static void ボーダー描画(Rect _選択矩形, float _任意のボーダー高さ, Color _カラー)
{
    if (_任意のボーダー高さ <= 0) return;
    
    // ダークモードかどうかの判定
    bool _ダークモード = EditorGUIUtility.isProSkin;
    
    // 既存のボーダーライン描画
    float _補正後高さ = _任意のボーダー高さ * _ボーダーライン高さ補正 * _ボーダーライン基本高さ; 
    Rect _ボーダー矩形 = new Rect(32, _選択矩形.y - _補正後高さ / 1f,
    _選択矩形.width + (_選択矩形.x - 0),
    _補正後高さ);
    EditorGUI.DrawRect(_ボーダー矩形, _カラー);

    // 2つ目のボーダーラインの描画
    Color _新しいボーダー色 = _ダークモード ? new Color(0.09f, 0.09f, 0.09f, 1.0f) : new Color(0.91f,  0.91f,  0.91f,  1.0f); // ライトモードの色を0.668fに修正

    // 連接するように新しいボーダーラインを描画（1px下に連続して描画）
    Rect _新しいボーダー矩形 = new Rect(
        32, 
        _選択矩形.y - _補正後高さ / 1f + _補正後高さ, // 既存ボーダーの直下に配置
        _選択矩形.width + (_選択矩形.x - 0),
        1f // 新しいボーダーの高さは1px
    );
    EditorGUI.DrawRect(_新しいボーダー矩形, _新しいボーダー色); // ダークモードかライトモードかによって色を変更
}





        public static void ボックス描画(Rect _元の矩形, int _ネストレベル, Color _カラー)
        {
            float _x = 開始X座標取得(_元の矩形, _ネストレベル) + _ボックス横オフセット;
            float _y = _元の矩形.y + (_元の矩形.height / 2f) + _ボックス縦オフセット - (_ボックス高さ / 2f);
            EditorGUI.DrawRect(new Rect(_x, _y, _ボックス幅, _ボックス高さ), _カラー);
        }
    }
}   






















































//---------------------------------------------------------------------------------------------------------------------------------------------------------
// スイッチ（チェックボックス） (描画)
[InitializeOnLoad]
public static class スイッチ
{
    private static ﾂﾘｰ _コントローラー;

    // 左に表示するかどうかを制御するbool変数
    private static bool _左に表示 = true; // trueなら左、falseなら右

    static スイッチ()
    {
        // コントローラーの初期ロード
        string[] _候補ファイル = AssetDatabase.FindAssets("ｺﾝﾄﾛｰﾗｰ", new[] { "Assets" });
        if (_候補ファイル.Length > 0)
        {
            string _ファイルパス = AssetDatabase.GUIDToAssetPath(_候補ファイル[0]);
            _コントローラー = AssetDatabase.LoadAssetAtPath<ﾂﾘｰ>(_ファイルパス);
        }

        // イベント登録
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        EditorApplication.projectChanged += OnProjectChanged;
    }

    private static void OnProjectChanged()
    {
        // コントローラーの再ロード
        string[] _候補ファイル = AssetDatabase.FindAssets("ｺﾝﾄﾛｰﾗｰ", new[] { "Assets" });
        if (_候補ファイル.Length > 0)
        {
            string _ファイルパス = AssetDatabase.GUIDToAssetPath(_候補ファイル[0]);
            _コントローラー = AssetDatabase.LoadAssetAtPath<ﾂﾘｰ>(_ファイルパス);
        }

        if (_コントローラー != null)
        {
            EditorApplication.RepaintHierarchyWindow();
        }
    }

    private static void OnHierarchyGUI(int _インスタンスID, Rect _レクト)
    {
        if (_コントローラー == null || !_コントローラー._スイッチ) return;

        GameObject _オブジェクト = EditorUtility.InstanceIDToObject(_インスタンスID) as GameObject;
        if (_オブジェクト != null)
        {
            // チェックボックスのX座標を左/右で切り替える
            float _固定X座標;
            if (_左に表示)
            {
                _固定X座標 = 32f; // 左側の固定位置
            }
            else
            {
                _固定X座標 = _レクト.xMax - 30f; // 右側に表示する場合
            }

            Rect _チェックボックス位置 = new Rect(_固定X座標, _レクト.y, 16, _レクト.height);

            // チェックボックスの描画
            bool _表示状態 = _オブジェクト.activeSelf;
            bool _新しい表示状態 = GUI.Toggle(_チェックボックス位置, _表示状態, GUIContent.none);

            if (_新しい表示状態 != _表示状態)
            {
                _オブジェクト.SetActive(_新しい表示状態);
                EditorApplication.RepaintHierarchyWindow();
            }
        }
    }
}


















//---------------------------------------------------------------------------------------------------------------------------------------------------------
// フォルダーカラー描画 (Unity Treeフォルダ用)
[InitializeOnLoad]
public static class フォルダーカラー描画
{
    static フォルダーカラー描画()
    {
        EditorApplication.projectWindowItemOnGUI += プロジェクトフォルダ描画処理;
    }

    private static bool UnityTreeフォルダ判定(string _アセットパス, out bool _サブフォルダ判定)
    {
        if (_アセットパス == "Assets/Unity Tree")
        {
            _サブフォルダ判定 = false;
            return true;
        }
        else if (_アセットパス.StartsWith("Assets/Unity Tree/"))
        {
            _サブフォルダ判定 = true;
            return true;
        }
        _サブフォルダ判定 = false;
        return false;
    }

    private static void プロジェクトフォルダ描画処理(string _guid, Rect _選択範囲)
    {
        string _アセットパス = AssetDatabase.GUIDToAssetPath(_guid);
        if (UnityTreeフォルダ判定(_アセットパス, out bool _サブフォルダ))
        {
            var _元カラー = GUI.color;
            float _不透明度 = EditorGUIUtility.isProSkin ? 0.6f : 0.1f;
            if (_サブフォルダ) _不透明度 = 0f;

            Color _色 = new Color(2f, 1f, 0f, _不透明度); 
            GUI.color = _色; 
            GUI.Box(_選択範囲, string.Empty);
            GUI.color = _元カラー;
        }
    }
}














//---------------------------------------------------------------------------------------------------------------------------------------------------------
// ｽｸﾘﾌﾟﾀﾌﾞﾙｵﾌﾞｼﾞｪｸﾄ　ハイライト
[InitializeOnLoad]
public static class フォルダーネームハイライト
{
    private static string _コントローラー名 = "";

    // 背景描画用オフセット/サイズ
    private static float _背景オフセットX = 2f;
    private static float _背景オフセットY = 0f;
    private static float _背景追加幅 = -4f;   
    private static float _背景追加高さ = 0f;  

    static フォルダーネームハイライト()
    {
        再読込();
        EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        EditorApplication.projectChanged += OnProjectChanged;
    }

    private static void OnProjectChanged()
    {
        再読込();
        EditorApplication.RepaintProjectWindow();
    }

    private static void 再読込()
    {
        string[] _候補ファイル = AssetDatabase.FindAssets("ｺﾝﾄﾛｰﾗｰ", new[] { "Assets" });
        if (_候補ファイル.Length > 0)
        {
            string _ファイルパス = AssetDatabase.GUIDToAssetPath(_候補ファイル[0]);
            ﾂﾘｰ _ctrl = AssetDatabase.LoadAssetAtPath<ﾂﾘｰ>(_ファイルパス);
            if (_ctrl != null)
            {
                _コントローラー名 = _ctrl.name; 
            }
        }
    }

    private static void OnProjectWindowItemGUI(string guid, Rect selectionRect)
    {
        if (string.IsNullOrEmpty(_コントローラー名)) return;

        string path = AssetDatabase.GUIDToAssetPath(guid);
        if (string.IsNullOrEmpty(path)) return;

        string assetName = Path.GetFileNameWithoutExtension(path);

        if (assetName == _コントローラー名)
        {
            Color _originalColor = GUI.color;
            GUI.color = new Color(0f, 0f, 1f, 0.2f);
            Rect _背景Rect = new Rect(
                selectionRect.x + _背景オフセットX,
                selectionRect.y + _背景オフセットY,
                selectionRect.width + _背景追加幅,
                selectionRect.height + _背景追加高さ
            );
            GUI.Box(_背景Rect, GUIContent.none, EditorStyles.helpBox);
            GUI.color = _originalColor;
        }
    }
}
