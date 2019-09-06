using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

enum feeding
{
    Acorn,          //どんぐり( リス用 )
    Pike,           //秋刀魚( ネコ用 )
    Carrot,         //人参( ウサギ用 )
    Meat,           //肉( ライオン用 )
    Unexpect        //想定外の値   
};

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject feedBoxPrefab;
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
    [SerializeField] private int MaxNumberFeed = 5;
    
    public GameObject[] animalObject;//生成する動物オブジェクトを格納する配列
    public GameObject[] foodBoxObject;//生成する餌Boxオブジェクトを格納する配列
    public GameObject[] effectObject;//生成するエフェクトオブジェクトを格納する配列

    //所持している餌
    //private List<GameObject> InventoryFeed = new List<GameObject>();
    private Dictionary<feeding, int> FeedDictionary = new Dictionary<feeding, int>();

    public float intervalTime;//生成間隔のインターバルタイム設定用関数

    public int fieldAnimalLimit;
    public int fieldFoodLimit;

    public float fieldPozX;//フィールドの座標範囲
    public float fieldPozY;
    public float fieldPozZ;

    /// <summary>
    /// 初期化を行う関数
    /// </summary>
    private void Init()
    {
        //所有している餌を初期化
        FeedDictionary.Add(feeding.Acorn, 0);
        FeedDictionary.Add(feeding.Carrot, 0);
        FeedDictionary.Add(feeding.Meat, 0);
        FeedDictionary.Add(feeding.Pike, 0);
    }

    /// <summary>
    /// ゲームスタート時に餌を5つプレーヤーに用意
    /// </summary>
    private void Start()
    {
        Init();
        feeding feedingType;
        for (int i = 0; i < MaxNumberFeed; i++)
        {
            feedingType = GenerateRandamFeeding();
            FeedDictionary[feedingType]++;
            /*feed = GenerateRandamFeeding();
            Debug.Log("feed\n" + feed);
            InventoryFeed.Add(feed);*/
        }
        for(feeding i = 0; (int)i < 4; i++)
        {
            Debug.Log("feeding : " + i + "\n" + "FeedDictionary : " + FeedDictionary[i]);
        }
    }

    /// <summary>
    /// 左下のUI部に所持している餌を表示するための関数
    /// </summary>
    private void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        Text text = textObject.GetComponent<Text>();
        text.text = "どんぐり : " + FeedDictionary[feeding.Acorn] + " " + "さんま : " + FeedDictionary[feeding.Pike] + " \n"
                + "ニンジン : " + FeedDictionary[feeding.Carrot] + " " + "肉 : " + FeedDictionary[feeding.Meat] + " ";
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

    /// <summary>
    /// ランダムに餌を出現させる
    /// </summary>
    /// <returns>ランダムな餌を一つ返す</returns>
    private feeding GenerateRandamFeeding()
    {
        feeding feed = (feeding)Random.Range((int)feeding.Acorn, (int)feeding.Meat);
        return feed;
    }

    /// <summary>
    /// 自身(動物オブジェクト)が注視されたら,
    /// 餌を自身に向けて, 餌を投げる.
    /// (カメラが餌を投げる指示を出すのではなく, 動物オブジェクト自身から餌を投げるようにしている)
    /// </summary>
    public void Feeing()
    {
        feeding animalFeed = AnimalTable(GazeManager.Instance.HitObject);
        Debug.Log("animalFeed : " + animalFeed);

        if (FeedDictionary[animalFeed] <= 0)
        {

        }
        else
        {
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
    private GameObject FeedTable(feeding feed)
    {
        switch (feed)
        {
            case feeding.Acorn: return AornPrefab;
            case feeding.Carrot: return CarrotPrefab;
            case feeding.Meat: return MeatPrefab;
            case feeding.Pike: return PikePrefab;
            default: Debug.Log("unexpected return."); return null;
        }
    }

    /// <summary>
    /// 動物オブジェクトに対応する餌の列挙型を返す関数(テーブル)
    /// </summary>
    /// <param name="animal"> フォーカスを当てている動物のオブジェクト </param>
    /// <returns> 動物に対応する列挙型の餌を返す </returns>
    private feeding AnimalTable(GameObject animal)
    {
        switch (animal.gameObject.tag)
        {
            case "rabbit": return feeding.Carrot;
            case "cat": return feeding.Pike;
            case "squirrel": return feeding.Acorn;
            case "lion": return feeding.Meat;
            default: Debug.Log("unexpected return."); return feeding.Unexpect;
        }
    }
    /// <summary>
    /// 即席に作成した餌の所持を表すために使用する関数
    /// </summary>
    /// <param name="num"> 餌を示す番号 </param>
    /// <returns> 対応する餌の数を返す </returns>
    public int HoldFeedNum(int num)
    {
        switch (num)
        {
            case 0: return FeedDictionary[feeding.Acorn];
            case 1: return FeedDictionary[feeding.Pike];
            case 2: return FeedDictionary[feeding.Carrot];
            case 3: return FeedDictionary[feeding.Meat];
            default: Debug.Log("unexpeted return."); return 0xff;
        }
    }
}
