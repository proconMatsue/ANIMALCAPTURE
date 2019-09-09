using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//boxのPrefabにアタッチするスクリプトです
//boxには"feedbox"というtagをつけてください(tagがなければ新しく定義してください)
//HoloLensCameraに餌boxとの衝突判定用のBoxColliderがついていなかったらつけてください
//BoxColliderのscaleはひとまず(1,3,1)でお願いします
public class Feedbox : MonoBehaviour
{

    GameObject GeneratorManager;
    // Start is called before the first frame update
    void Start()
    {
        GeneratorManager = GameObject.Find("GeneratorManager");//スクリプトを呼び出すためにGeneratorManagerオブジェクトを取得
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)//プレイヤ(ホロレンズカメラ)とのあたり判定
    {
        if(other.gameObject.tag == "MainCamera")//タグがMainCameraのときに判定を行う
        {
            //Debug.Log("trigger");
            GeneratorManager.GetComponent<GenerateFeedBoxAndEffect>().CallGenerateFeedBox();//遅れて餌を生成する
            GeneratorManager.GetComponent<FeedInventory>().IncreaseFeed();//ランダムに餌の所持数を増やす
            Destroy(this.gameObject);//自分自身をDestory
        }
    }



}
