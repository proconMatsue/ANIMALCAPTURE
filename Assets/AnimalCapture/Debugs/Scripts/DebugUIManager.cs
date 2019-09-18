//LogMessageの出力は
//http://bluebirdofoz.hatenablog.com/entry/2017/10/10/002406
//を参考

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DebugUIManager : MonoBehaviour
{
    // デバッグログ用テキストオブジェクト
    [SerializeField, Tooltip("デバッグログ用テキストオブジェクト")]
    private GameObject p_TargetDebugPanelObject;

    // デバッグログ用テキスト
    private Text p_Text;

    // 表示行数
    [SerializeField, Tooltip("表示行数")]
    private int p_LineNum = 17;

    // 保持テキスト
    private string p_TextMessage;

    /// <summary>
    /// 初期化関数
    /// </summary>
    private void Start()
    {
        // Logメッセージイベント追加
        Application.logMessageReceived += LogMessageOutput;

        p_Text = p_TargetDebugPanelObject.GetComponent<UnityEngine.UI.Text>();
    }

    /// <summary>
    /// Logメッセージイベント処理
    /// </summary>
    private void LogMessageOutput(string condition, string stackTrace, LogType type)
    {
        string textmessage = p_TextMessage;
        textmessage += condition + System.Environment.NewLine;

        string newline = System.Environment.NewLine;
        string[] lines = textmessage.Split(new string[] { newline }, System.StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length > p_LineNum)
        {
            textmessage = "";
            for (int line = lines.Length - p_LineNum; line < lines.Length; line++)
            {
                textmessage += lines[line] + System.Environment.NewLine;
            }
        }

        p_TextMessage = textmessage;
        p_Text.text = textmessage;
    }
}
