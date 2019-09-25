using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Billboard))]
[RequireComponent(typeof(SphereBasedTagalong))]

public class ResultUIManager : MonoBehaviour
{
    [SerializeField, Tooltip("結果を表示するテキストオブジェクト")]
    private GameObject textObject;

    private int score = 0;
    public static int Score{ get; set; }

    /// <summary>
    /// 結果を出力
    /// </summary>
    private void Start()
    {
        Text text = textObject.GetComponent<Text>();
        text.text = "あなたの結果 : " + Score;
    }
}
