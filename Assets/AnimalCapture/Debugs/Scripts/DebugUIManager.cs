//LogMessageの出力は
//http://bluebirdofoz.hatenablog.com/entry/2017/10/10/002406
//を参考

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DebugUIManager : MonoBehaviour
{
    //デバッグ時のみデバッグキャンバスが出てくるようにする
#if DEBUG
    [SerializeField]
    [Tooltip("デバッグログ用キャンバスオブジェクト")]
    private GameObject DebugCanvasObject;

    //デバッグログ用テキストオブジェクト
    private GameObject DebugTextObject;

    // デバッグログ用テキスト
    private Text Text;

    // 表示行数
    [SerializeField]
    [Tooltip("表示行数")]
    [Range(1, 9)]
    private int LineNum = 9;

    // 保持テキスト
    private string TextMessage;

    /// <summary>
    /// 初期化関数
    /// </summary>
    private void Start()
    {
        // Logメッセージイベント追加
        Application.logMessageReceived += LogMessageOutput;

        //デバッグ用のキャンバスを用意
        generateDebugCanvas();

        Text = DebugTextObject.GetComponent<UnityEngine.UI.Text>();
    }

    /// <summary>
    /// スクリプト上でキャンバスを生成するようにする
    /// デバッグ時以外に生成されないようにするため
    /// </summary>
    private void generateDebugCanvas()
    {
        //キャンバスを作成
        GameObject debugCanvas = Instantiate<GameObject>(
                DebugCanvasObject,
                this.gameObject.transform
            );

        //キャンバスをマネージャの子オブジェクトとして設定
        debugCanvas.transform.parent = this.gameObject.transform;

        //キャンバスの子オブジェクトである, Textオブジェクトを取得
        DebugTextObject = debugCanvas.gameObject.transform.Find("LogText").gameObject;
    }

    /// <summary>
    /// Logメッセージイベント処理
    /// </summary>
    private void LogMessageOutput(string condition, string stackTrace, LogType type)
    {
        string textmessage = TextMessage;
        textmessage += condition + System.Environment.NewLine;

        string newline = System.Environment.NewLine;
        string[] lines = textmessage.Split(new string[] { newline }, System.StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length > LineNum)
        {
            textmessage = "";
            for (int line = lines.Length - LineNum; line < lines.Length; line++)
            {
                textmessage += lines[line] + System.Environment.NewLine;
            }
        }

        TextMessage = textmessage;
        Text.text = textmessage;
    }
#endif
}
