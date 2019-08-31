using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingManager : MonoBehaviour
{
    //カメラオブジェクト( 餌を投げる時の初期値 )
    [SerializeField] private GameObject camera;
    //餌オブジェクトのプレハブ
    [SerializeField] private GameObject feedPrefab;
    //餌を射出するときのスピード
    [SerializeField] private float speed = 300.0f;
    //オブジェクトを破壊するまでにかかる時間
    [SerializeField] private float DestryTime = 20.0f;

    //手持ちの餌を格納するためのリスト
    private List<GameObject> NumberOfFeedding;

    //
    enum feeding {
        Acorn,          //どんぐり( リス用 )
        Pike,           //秋刀魚( ネコ用 )
        Carrot,         //人参( ウサギ用 )
        Meat            //肉( ライオン用 )
    }

    /// <summary>
    /// 手持ちの餌をランダムに決定する
    /// </summary>
    private void Start()
    {
        
    }

    /// <summary>
    /// 自身(動物オブジェクト)が注視されたら,
    /// 餌を自身に向けて, 餌を投げる.
    /// (カメラが餌を投げる指示を出すのではなく, 動物オブジェクト自身から餌を投げるようにしている)
    /// </summary>
    public void Feeing() {
        GameObject feedInstance = Instantiate<GameObject>(feedPrefab, camera.transform.position, camera.transform.rotation);
        feedInstance.GetComponent<Rigidbody>().AddForce(feedInstance.transform.forward * speed);

        //一定時間フィールド上に存在する餌オブジェクトは削除するようにする.
        Destroy(feedInstance, DestryTime);  
    }


    /// <summary>
    /// プレーヤーが持っている餌の数を返すだけのプロパティ
    /// </summary>
    public int GetNumberOfFeeding{
        get { return NumberOfFeedding.Count; }
    }
}
