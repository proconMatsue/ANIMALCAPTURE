using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum feeding
{
    Acorn,          //どんぐり( リス用 )
    Pike,           //秋刀魚( ネコ用 )
    Carrot,         //人参( ウサギ用 )
    Meat            //肉( ライオン用 )
};

public class Generator : MonoBehaviour
{
    public GameObject[] animalObject;//生成する動物オブジェクトを格納する配列
    public GameObject[] foodObject;//生成する餌オブジェクトを格納する配列
    public GameObject[] effectObject;//生成するエフェクトオブジェクトを格納する配列

    public float intervalTime;//生成間隔のインターバルタイム設定用関数

    public int fieldAnimalLimit;
    public int fieldFoodLimit;

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
         UnityEngine.Random.Range(0, fieldPozY),fieldPozZ),Quaternion.identity);//xy座標はランダムで生成
    }

    public GameObject GenerateRandamFeeding() {
        GameObject gameObject;
        feeding feed;

        feed = (feeding)Random.Range((int)feeding.Acorn, (int)feeding.Meat+1);

        return this.gameObject;
    }
}
