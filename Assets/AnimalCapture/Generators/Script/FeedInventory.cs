using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class FeedInventory : MonoBehaviour
{
    //餌に対する列挙型
    private enum Feeding
    {
        Acorn,          //どんぐり( リス用 )
        Pike,           //秋刀魚( ネコ用 )
        Carrot,         //人参( ウサギ用 )
        Meat,           //肉( ライオン用 )
        Unexpect = 0xff //想定外の値   
    };

    //餌Boxのオブジェクト
    //[SerializeField] private GameObject feedBoxPrefab;
    //プレーヤのオブジェクト
    [SerializeField, Tooltip("カメラオブジェクト\n(プレーヤの座標を得るため)")]
    private GameObject cameraPrefab;
    
    //餌の種類
    [SerializeField, Tooltip("ドングリのオブジェクト")]
    public GameObject AornPrefab;
    [SerializeField, Tooltip("秋刀魚のオブジェクト")]
    public GameObject PikePrefab;
    [SerializeField, Tooltip("ニンジンのオブジェクト")]
    public GameObject CarrotPrefab;
    [SerializeField, Tooltip("肉のオブジェクト")]
    public GameObject MeatPrefab;

    [SerializeField, Tooltip("餌を射出するときのスピード")]
    [Range(0.0f, 500.0f)]
    private float speed = 300.0f;

    [SerializeField, Tooltip("射出したエサを破壊するまでにかかる時間")]
    [Range(0.0f, 60.0f)]
    private float DestryTime = 20.0f;

    [SerializeField, Tooltip("所持できるエサの数の上限")]
    [Range(0, 10)]
    private int MaxNumberFeed = 5;

    //生成間隔のインターバルタイム設定用関数
    //[SerializeField] private float intervalTime;

    //所持している餌の数(key: 餌の種類, value: 餌の数)
    private Dictionary<Feeding, int> feedDictionary = new Dictionary<Feeding, int>();
    //SEを鳴らすため
    private GameObject FeedingSE;


    /// <summary>
    /// ゲームスタート時に餌を5つプレーヤーに用意
    /// </summary>
    private void Start()
    {
        FeedInventoryInit();

        //最初に持っている餌を取得
        Feeding feedingType;
        for (int i = 0; i < MaxNumberFeed; i++)
        {
            feedingType = GenerateRandamFeeding();
            feedDictionary[feedingType]++;
        }

        //スクリプトを呼び出すためにFeedingSEオブジェクトを取得
        FeedingSE = GameObject.Find("FeedingSE");
    }

    /// <summary>
    /// 所持しているエサの初期化を行う関数
    /// </summary>
    private void FeedInventoryInit()
    {
        //所有している餌を初期化
        feedDictionary.Add(Feeding.Acorn, 0);
        feedDictionary.Add(Feeding.Carrot, 0);
        feedDictionary.Add(Feeding.Meat, 0);
        feedDictionary.Add(Feeding.Pike, 0);
    }

    /// <summary>
    /// 所持してる餌の数を表示するための関数
    /// </summary>
    /// <param name="textObject"> 表示するためのテキストオブジェクト </param>
    public void FeedDisplay(GameObject textObject)
    {
        // オブジェクトからTextコンポーネントを取得
        Text text = textObject.GetComponent<Text>();
        text.text = "どんぐり : " + feedDictionary[Feeding.Acorn] + " " + "さんま : " + feedDictionary[Feeding.Pike] + " \n"
                + "ニンジン : " + feedDictionary[Feeding.Carrot] + " " + "肉 : " + feedDictionary[Feeding.Meat] + " ";
    }

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
        // Debug.Log(GazeManager.Instance.HitObject.GetComponent<Animal>().isHungry);
        if (GazeManager.Instance.HitObject.GetComponent<Animal>().isHungry) {
            if (feedDictionary[animalFeed] <= 0)
            {
                //餌を持ってかったときの処理を書く
            }
            else
            {
                //餌を投げるときのSEを鳴らす
                FeedingSE.GetComponent<FeedingSE>().FeedingPlay();

                GameObject feedInstance = Instantiate<GameObject>(
                    FeedTable(animalFeed), cameraPrefab.transform.position, cameraPrefab.transform.rotation);
                //投げるゲームオブジェクトはリスとから消しておく
                if (animalFeed == Feeding.Acorn) {
                    feedInstance.tag = "acorn";
                }
                if (animalFeed == Feeding.Pike)
                {
                    feedInstance.tag = "pike";
                }
                if (animalFeed == Feeding.Carrot)
                {
                    feedInstance.tag = "carrot";
                }
                if (animalFeed == Feeding.Meat)
                {
                    feedInstance.tag = "meat";
                }
                feedDictionary[animalFeed]--;

                feedInstance.GetComponent<Rigidbody>().AddForce(feedInstance.transform.forward * speed);

                //一定時間フィールド上に存在する餌オブジェクトは削除するようにする.
                Destroy(feedInstance, DestryTime);
            }
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
            case "rabbit": return Feeding.Carrot;
            case "cat": return Feeding.Pike;
            case "squirrel": return Feeding.Acorn;
            case "lion": return Feeding.Meat;
            default: Debug.LogWarning("unexpected return."); return Feeding.Unexpect;
        }
    }

    public int GetFeedNum(string s)
    {
        switch (s)
        {
            case "carrot":  return feedDictionary[Feeding.Carrot];
            case "pike":    return feedDictionary[Feeding.Pike];
            case "meat":    return feedDictionary[Feeding.Meat];
            case "acorn":   return feedDictionary[Feeding.Acorn];
            default:        Debug.LogError("ERROR : Unexpected reciev in GetFeeding"); return 256;
        }
    }

    public void IncreaseFeed()//餌boxを拾ったときに持っている餌を増やす関数
    {
        feedDictionary[GenerateRandamFeeding()]++;
    }


}
