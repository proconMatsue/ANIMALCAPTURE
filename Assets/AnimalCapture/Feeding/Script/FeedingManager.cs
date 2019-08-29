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
    [SerializeField] private float speed = 10.0f;

    /// <summary>
    /// 自身(動物オブジェクト)が注視されたら,
    /// 餌を自身に向けて, 餌を投げる.
    /// (カメラが餌を投げる指示を出すのではなく, 動物オブジェクト自身から餌を投げるようにしている)
    /// </summary>
    public void Feeing() {
        var feedInstance = Instantiate<GameObject>(feedPrefab, camera.transform.position, camera.transform.rotation);
        feedInstance.GetComponent<Rigidbody>().AddForce(feedInstance.transform.forward * speed);
        Destroy(feedInstance, 5f);
    }
}
