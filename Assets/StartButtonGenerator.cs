using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("スタートボタンのオブジェクト")]
    private List<GameObject> StartButton;

    [SerializeField, Tooltip("プレーヤとスタートボタンとの距離")]
    [Range(0.0f, 5.0f)]
    private float DistanceToPlayer = 1.0f;

    /// <summary>
    /// ゲームスタート時にスタートボタンを生成するようにする
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < StartButton.Count; i++)
        {
            //スタートボタン生成関数
            generateStartButton(i);
        }
    }

    /// <summary>
    /// ただ, スタートボタンを生み出すだけの関数
    /// </summary>
    public void generateStartButton(int i)
    {
        GameObject start = Instantiate<GameObject>(
            StartButton[i], 
            this.gameObject.transform);

        //生成場所を原点にする
        Vector3 start_vec = start.transform.position;
        start_vec = new Vector3(0.0f, 0.0f, DistanceToPlayer);
        start.transform.position = start_vec;

        //スタートボタンをマネージャの子オブジェクトとする
        start.gameObject.transform.parent = this.gameObject.transform;
    }
}
