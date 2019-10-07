using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//boxのPrefabにアタッチするスクリプトです
//boxには"feedbox"というtagをつけてください(tagがなければ新しく定義してください)
//HoloLensCameraに餌boxとの衝突判定用のBoxColliderがついていなかったらつけてください
//BoxColliderのscaleはひとまず(1,3,1)でお願いします
public class Feedbox : MonoBehaviour
{
    private GameObject FeedBoxManager;
    private GameObject FeedInventoryManager;
    private GameObject FeedBoxGetSE;

    /// <summary>
    /// スクリプトを呼び出すためにFeedBoxManagerオブジェクトを取得
    /// </summary>
    private void Start()
    {
        FeedBoxManager = GameObject.Find("FeedBoxManager");
        FeedInventoryManager = GameObject.Find("FeedInventoryManager");
        FeedBoxGetSE = GameObject.Find("FeedBoxGetSE");
    }


    private void OnTriggerEnter(Collider other)//プレイヤ(ホロレンズカメラ)とのあたり判定
    {
        if(other.gameObject.tag == "MainCamera")//タグがMainCameraのときに判定を行う
        {
            //Debug.Log("trigger");
            FeedBoxManager.GetComponent<GenerateFeedBoxAndEffect>().CallGenerateFeedBox();//遅れて餌を生成する
            FeedInventoryManager.GetComponent<FeedInventory>().IncreaseFeed();//ランダムに餌の所持数を増やす
            FeedBoxGetSE.GetComponent<FeedBoxGetSE>().FeedBoxGetPlay();//餌取得時のSEを鳴らす
            Destroy(this.gameObject);//自分自身をDestory
        }
    }



}
