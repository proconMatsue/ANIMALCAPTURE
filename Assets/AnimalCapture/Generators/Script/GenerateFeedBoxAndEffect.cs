using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFeedBoxAndEffect : MonoBehaviour
{

    [SerializeField, Tooltip("エサボックスのオブジェクト")]
    private GameObject feedBoxPrefab;
    [SerializeField, Tooltip("生成するエフェクトオブジェクトを格納する配列")]
    private GameObject[] effectPrefab;

    //生成する動物の生成位置
    [SerializeField, Tooltip("エサボックスの生成位置のX成分\n(プレーヤを中心として指定した範囲にエサボックスが生成される)")]
    [Range(0.0f, 5.0f)]
    private float generatePosRange_x = 1.0f;
    [SerializeField, Tooltip("エサボックスの生成位置のZ成分\n(プレーヤを中心として指定した範囲にエサボックスが生成される)")]
    [Range(0.0f, 5.0f)]
    private float generatePosRange_z = 1.0f;

    [SerializeField, Tooltip("フィールドに生成するエサボックスの上限数")]
    [Range(0, 10)]
    private int fieldFeedBoxLimit = 10;

    [SerializeField, Tooltip("エサボックスの大きさ\n(1から何倍するかを表している)")]
    [Range(0.0f, 1.0f)]
    private float FeedBoxScale = 1.0f;

    /// <summary>
    /// ゲームがスタートした かつ フィールド上にエサボックスが一つもない
    /// </summary>
    void Update()
    {
        //ゲーム中に動いている timecontrollerスクリプト を取得
        timecontroller time = GameObject.Find("UICanvas").GetComponent<timecontroller>();
        if (time == null) { Debug.LogWarning("UICanvasが正しく見つかっていません."); return; }

        //ゲーム中に動いている MyPlaySpaceManagerスクリプト を取得
        MyPlaySpaceManager myPlaySpaceManager = GameObject.Find("SpatialProcessing").GetComponent<MyPlaySpaceManager>();
        if (myPlaySpaceManager == null) { Debug.LogWarning("SpatialProcessingが正しく見つかっていません."); return; }


        if (time.isGameStart                                //ゲームがスタートした 
            && myPlaySpaceManager.MeshesToPlanesCompleted   //フィールド上が正しく用意された
            && CheckFieldObject("feedbox") == 0)            //フィールド上のエサボックスが0個である
        {
            GenerateFeedBox();
        }
    }

    //フィールド場の指定したオブジェクトの数を返す関数
    public int CheckFieldObject(string objecttag)//タグで数えるオブジェクトを指定
    {
        int countNum;
        GameObject[] countObject = GameObject.FindGameObjectsWithTag(objecttag);
        countNum = countObject.Length;
        return countNum;
    }

    
    //餌boxを生成する関数
    public void GenerateFeedBox()//餌boxを生成する関数 引数でanimalObjectの場所を指定
    {
        //無限ループを防ぐための変数
        int RoopCount = 0;                      //カウント変数
        int RoopLimit = fieldFeedBoxLimit * 5;  //カウントの上限数

        //フィールド場の餌boxを数え、減っている数だけ生成する
        //カウント上限数を超えれば終了
        while (CheckFieldObject("feedbox") < fieldFeedBoxLimit && RoopCount <= RoopLimit)
        {
            RoopCount++;

            GameObject feedBox = Instantiate(
                feedBoxPrefab, 
                new Vector3(
                    UnityEngine.Random.Range(-generatePosRange_x, generatePosRange_x), 
                    1.0f, 
                    UnityEngine.Random.Range(-generatePosRange_z, generatePosRange_z)
                ), //xy座標はフィールド範囲内でランダムで生成 
                Quaternion.identity
                );
            feedBox.transform.parent = this.gameObject.transform;
        }

        //ループカウントがカウント上限数を超えれば, エラー表示をしておく
        if(RoopCount >= RoopLimit) { Debug.LogWarning("無限ループが発生しています"); }
    }

    //エフェクトを生成する関数
    public void GenerateEffect(int effectNum, GameObject effectTarget)//effectnumで生成するエフェクトの種類を指定
                                                                      //effectTargetでエフェクトを発生する対象のオブジェクトを指定
    {
        Instantiate(effectPrefab[effectNum],
        new Vector3(
                    effectTarget.transform.position.x,//対象のオブジェクトの座標にエフェクトを発生させる
                    effectTarget.transform.position.y,
                    effectTarget.transform.position.z),
                    Quaternion.identity
                    );
    }


}
