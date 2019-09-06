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

    /// <summary>
    /// 自身(動物オブジェクト)が注視されたら,
    /// 餌を自身に向けて, 餌を投げる.
    /// (カメラが餌を投げる指示を出すのではなく, 動物オブジェクト自身から餌を投げるようにしている)
    /// </summary>
    /*private void Feeing() {
        GameObject feedInstance = Instantiate<GameObject>(InventoryFeed[0] , camera.transform.position, camera.transform.rotation);
        //投げるゲームオブジェクトはリスとから消しておく
        InventoryFeed.RemoveAt(0);
        feedInstance.GetComponent<Rigidbody>().AddForce(feedInstance.transform.forward * speed);

        //一定時間フィールド上に存在する餌オブジェクトは削除するようにする.
        Destroy(feedInstance, DestryTime);  
    }*/
}
