 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] animalObject;//生成する動物オブジェクトを格納する配列
    public GameObject[] feedBoxObject;//生成する餌オブジェクトを格納する配列
    public GameObject[] effectObject;//生成するエフェクトオブジェクトを格納する配列

    public float intervalTime;//生成間隔のインターバルタイム設定用関数

    public int fieldAnimalLimit;
    public int fieldFeedBoxLimit;//フィールド場に出現させる餌boxの上限数

    public float fieldPozX;//フィールドの座標範囲
    public float fieldPozY;
    public float fieldPozZ;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int CheckFieldObject(string objecttag)//フィールド場の指定したオブジェクトの数を返す関数
    {
        int countNum;
        GameObject[] countObject = GameObject.FindGameObjectsWithTag(objecttag);//タグでオブジェクトを指定
        countNum = countObject.Length;
        return countNum;

    }


    public void GenerateAnimal(int num)//実際に動物を生成する関数 引数でanimalObjectの場所を指定
    {
        Instantiate(animalObject[num], new Vector3( UnityEngine.Random.Range(0,fieldPozX),
         UnityEngine.Random.Range(0, fieldPozY),fieldPozZ),Quaternion.identity);//xy座標はフィールド範囲内でランダムで生成
    }


    public void GenerateFeedBox()//餌boxを生成する関数 引数でanimalObjectの場所を指定
    {
        while (CheckFieldObject("feedBox") < fieldFeedBoxLimit)//フィールド場の餌boxを数え、減っている数だけ生成する
        {
            Instantiate(feedBoxObject, new Vector3(UnityEngine.Random.Range(0, fieldPozX),
             UnityEngine.Random.Range(0, fieldPozY), fieldPozZ), Quaternion.identity);//xy座標はランダムで生成
        }
    }


}
