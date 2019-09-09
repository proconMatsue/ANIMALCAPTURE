using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFeedBoxAndEffect : MonoBehaviour
{

    [SerializeField] private GameObject feedBoxPrefab;//餌boxのオブジェクト
    [SerializeField] private GameObject[] effectPrefab;//生成するエフェクトオブジェクトを格納する配列

    [SerializeField] private int fieldFeedBoxLimit = 10; //フィールドに生成する餌boxの上限数
    [SerializeField] private float intervalTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        while (CheckFieldObject("feedbox") < fieldFeedBoxLimit)//フィールド場の餌boxを数え、減っている数だけ生成する
        {
            GenerateFeedBox();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Instantiate(feedBoxPrefab, new Vector3(UnityEngine.Random.Range(-3.0f, 3.0f),
         0.0f, UnityEngine.Random.Range(-3.0f, 3.0f)), Quaternion.Euler(-90.0f,0f,0f));//xy座標はフィールド範囲内でランダムで生成
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


    public void CallGenerateFeedBox()//餌を取得しbox後,遅らせて新しい餌boxを生成する関数
    {
        Invoke("GenerateFeedBox", 3.0f);//3秒後に新しい餌boxを生成
    }


}
