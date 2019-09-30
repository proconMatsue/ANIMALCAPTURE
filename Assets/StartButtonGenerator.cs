using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("スタートボタンのオブジェクト")]
    private GameObject StartButton;

    [SerializeField, Tooltip("プレーヤとスタートボタンとの距離")]
    [Range(0.0f, 5.0f)]
    private float DistanceToPlayer = 1.0f;

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

        //生成場所を原点にする
        Vector3 start_vec = start.transform.position;
        start_vec = new Vector3(0.0f, 0.0f, DistanceToPlayer);
        start.transform.position = start_vec;

        //スタートボタンをマネージャの子オブジェクトとする
        start.gameObject.transform.parent = this.gameObject.transform;
    }
}
