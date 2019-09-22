using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonGenerator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("スタートボタンのオブジェクト")]
    private GameObject StartButton;

    /// <summary>
    /// ただ, スタートボタンを生み出すだけの関数
    /// </summary>
    public void generateStartButton(object source, System.EventArgs args)
    {
        GameObject start = Instantiate<GameObject>(
            StartButton, 
            this.gameObject.transform);
    }
}
