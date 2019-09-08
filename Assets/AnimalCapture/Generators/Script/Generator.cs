using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    //餌に対する列挙型
    private enum Feeding
    {
        Acorn,          //どんぐり( リス用 )
        Pike,           //秋刀魚( ネコ用 )
        Carrot,         //人参( ウサギ用 )
        Meat           //肉( ライオン用 ) 
    };

    //餌Boxのオブジェクト
    [SerializeField] private GameObject feedBoxPrefab;
    //プレーヤのオブジェクト
    [SerializeField] private GameObject cameraPrefab;
    
    //餌の種類
    [SerializeField] public GameObject AornPrefab;
    [SerializeField] public GameObject PikePrefab;
    [SerializeField] public GameObject CarrotPrefab;
    [SerializeField] public GameObject MeatPrefab;

    [SerializeField] private GameObject textObject;

    //餌を射出するときのスピード
    [SerializeField] private float speed = 300.0f;
    //餌オブジェクトを破壊するまでにかかる時間
    [SerializeField] private float DestryTime = 20.0f;

    //所持できる餌の数の上限
    [SerializeField] private int MaxNumberFeed = 4;

    /*public GameObject[] animalObject;//生成する動物オブジェクトを格納する配列
    public GameObject[] foodBoxObject;//生成する餌Boxオブジェクトを格納する配列
    public GameObject[] effectObject;//生成するエフェクトオブジェクトを格納する配列*/

    //所持している餌の数(key: 餌の種類, value: 餌の数)
    [SerializeField] private Dictionary<Feeding, int> FeedDictionary = new Dictionary<Feeding, int>();

    /*public float intervalTime;//生成間隔のインターバルタイム設定用関数

    public int fieldAnimalLimit;
    public int fieldFoodLimit;

    public float fieldPozX;//フィールドの座標範囲
    public float fieldPozY;
    public float fieldPozZ;*/

    /// <summary>
    /// 初期化を行う関数
    /// </summary>
    private void Init()
    {


        //所有している餌を初期化
        FeedDictionary.Add(Feeding.Acorn, 0);
        FeedDictionary.Add(Feeding.Carrot, 0);
        FeedDictionary.Add(Feeding.Meat, 0);
        FeedDictionary.Add(Feeding.Pike, 0);
    }

    /// <summary>
    /// ゲームスタート時に餌を5つプレーヤーに用意
    /// </summary>
    private void Start()
    {
        Init();

        //最初に持っている餌を取得
        Feeding feedingType;
        for (int i = 0; i < MaxNumberFeed; i++)
        {
            feedingType = GenerateRandamFeeding();
            FeedDictionary[feedingType]++;
        }
    }

    /// <summary>
    /// 左下のUI部に所持している餌を表示するための関数
    /// 要移動
    /// </summary>
    private void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        Text text = textObject.GetComponent<Text>();
        text.text = "どんぐり : " + FeedDictionary[Feeding.Acorn] + " " + "さんま : " + FeedDictionary[Feeding.Pike] + " \n"
                + "ニンジン : " + FeedDictionary[Feeding.Carrot] + " " + "肉 : " + FeedDictionary[Feeding.Meat] + " ";
    }

    public int CheckFieldObject(string objecttag)//フィールド場の指定したオブジェクトの数を返す関数
    {
        int countNum;
        GameObject[] countObject = GameObject.FindGameObjectsWithTag(objecttag);//タグでオブジェクトを指定
        countNum = countObject.Length;
        return countNum;
    }


    /*public void GenerateAnimal(int num)//実際に動物を生成する関数 引数でanimalObjectの場所を指定
    {
        Instantiate(
            animalObject[num], 
            new Vector3( 
                UnityEngine.Random.Range(0,fieldPozX),
                UnityEngine.Random.Range(0, fieldPozY),
                fieldPozZ),
            Quaternion.identity
        );//xy座標はランダムで生成
    }*/

    /// <summary>
    /// ランダムに餌を出現させる
    /// </summary>
    /// <returns>ランダムな餌を一つ返す</returns>
    private Feeding GenerateRandamFeeding()
    {
        Feeding feed = (Feeding)Random.Range((int)Feeding.Acorn, (int)Feeding.Meat+1);
        return feed;
    }

    /// <summary>
    /// 自身(動物オブジェクト)が注視されたら,
    /// 餌を自身に向けて, 餌を投げる.
    /// (カメラが餌を投げる指示を出すのではなく, 動物オブジェクト自身から餌を投げるようにしている)
    /// </summary>
    public void Feeing()
    {
        Feeding animalFeed = AnimalTable(GazeManager.Instance.HitObject);
        Debug.Log("animalFeed : " + animalFeed);

        Debug_FeedingDictionary();
            
        //餌を持ってかったときの処理を書く
        if (FeedDictionary[animalFeed] <= 0)
        {
            
        }
        else
        {
            GameObject g = FeedTable(animalFeed);
            Debug.Log("FeedTable : " + g.tag);

            GameObject feedInstance = Instantiate<GameObject>(
                FeedTable(animalFeed), cameraPrefab.transform.position, cameraPrefab.transform.rotation);
            

            //投げるゲームオブジェクトはリスとから消しておく
            FeedDictionary[animalFeed]--;

            feedInstance.GetComponent<Rigidbody>().AddForce(feedInstance.transform.forward * speed);

            //一定時間フィールド上に存在する餌オブジェクトは削除するようにする.
            Destroy(feedInstance, DestryTime);
        }
    }

    /// <summary>
    /// 餌に対応する餌のオブジェクトを取得するための関数(テーブル)
    /// </summary>
    /// <param name="feed"> 餌を表す列挙型 </param>
    /// <returns> 引数で受け取った餌に対応する餌ゲームオブジェクト </returns>
    private GameObject FeedTable(Feeding feed)
    {
        switch (feed)
        {
            case Feeding.Acorn: return AornPrefab;
            case Feeding.Carrot: return CarrotPrefab;
            case Feeding.Meat: return MeatPrefab;
            case Feeding.Pike: return PikePrefab;
            default: Debug.LogWarning("unexpected return."); return null;
        }
    }

    /// <summary>
    /// 動物オブジェクトに対応する餌の列挙型を返す関数(テーブル)
    /// </summary>
    /// <param name="animal"> フォーカスを当てている動物のオブジェクト </param>
    /// <returns> 動物に対応する列挙型の餌を返す </returns>
    private Feeding AnimalTable(GameObject animal)
    {
        switch (animal.gameObject.tag)
        {
            case "rabbit": Debug.Log(Feeding.Carrot); return Feeding.Carrot;
            case "cat": Debug.Log(Feeding.Pike);  return Feeding.Pike;
            case "squirrel": Debug.Log(Feeding.Acorn); return Feeding.Acorn;
            case "lion": Debug.Log(Feeding.Meat); return Feeding.Meat;
            default: Debug.LogWarning("unexpected return."); return (Feeding)5;
        }
    }

    private void Debug_FeedingDictionary()
    {
        for(Feeding i = Feeding.Acorn; i <= Feeding.Meat; i++)
        {
            Debug.Log("aaaaa");
            Debug.Log("FeedDictionary : " + FeedDictionary);
            Debug.Log(i + " : " + FeedDictionary[i]);
        }
    }
}