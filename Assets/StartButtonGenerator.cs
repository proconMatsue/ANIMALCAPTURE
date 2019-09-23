using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonGenerator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("スタートボタンのオブジェクト")]
    private GameObject StartButton;

    /// <summary>
    /// ゲームスタート時にスタートボタンを生成するようにする
    /// </summary>
    private void Start()
    {
        //スタートボタン生成関数
        generateStartButton();
    }

    /// <summary>
    /// ただ, スタートボタンを生み出すだけの関数
    /// </summary>
    public void generateStartButton()
    {
        GameObject start = Instantiate<GameObject>(
            StartButton, 
            this.gameObject.transform);
        
        //スタートボタンをマネージャの子オブジェクトとする
        start.gameObject.transform.parent = this.gameObject.transform;
    }
}
