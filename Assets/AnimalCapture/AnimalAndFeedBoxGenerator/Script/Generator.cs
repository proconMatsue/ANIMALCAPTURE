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
    [SerializeField] private GameObject feedBoxPrefab;
    [SerializeField] private GameObject cameraPrefab;
    //餌の種類
    [SerializeField] public GameObject AornPrefab;
    [SerializeField] public GameObject PikePrefab;
    [SerializeField] public GameObject CarrotPrefab;
    [SerializeField] public GameObject MeatPrefab;

    //餌を射出するときのスピード
    [SerializeField] private float speed = 300.0f;
    //オブジェクトを破壊するまでにかかる時間
    [SerializeField] private float DestryTime = 20.0f;

    public GameObject[] animalObject;//生成する動物オブジェクトを格納する配列
    public GameObject[] foodBoxObject;//生成する餌Boxオブジェクトを格納する配列
    public GameObject[] effectObject;//生成するエフェクトオブジェクトを格納する配列

    //所持している餌
    private List<GameObject> InventoryFeed = new List<GameObject>();

    public float intervalTime;//生成間隔のインターバルタイム設定用関数

    public int fieldAnimalLimit;
    public int fieldFoodLimit;

    public float fieldPozX;//フィールドの座標範囲
    public float fieldPozY;
    public float fieldPozZ;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject feed = new GameObject();
        for (int i = 0; i < 5; i++)
        {
            feed = GenerateRandamFeeding();
            Debug.Log("feed\n" + feed);
            InventoryFeed.Add(feed);
        }
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
    public GameObject GenerateRandamFeeding() {
        GameObject gameObject = new GameObject();
        feeding feed = (feeding)Random.Range((int)feeding.Acorn, (int)feeding.Meat);

        Debug.Log("into GenerateRandamFeeding\n" + (int)feed);

        switch (feed)
        {
            case feeding.Acorn:     return AornPrefab;
            case feeding.Carrot:    return CarrotPrefab;
            case feeding.Meat:      return MeatPrefab;
            case feeding.Pike:      return PikePrefab;
            default:
                Debug.Log("[WORNING] Generator.GenerateRandamFeeding()のswitch文内でreturnするgameObjectがnullになっています.\n");
                return null;
        }
    }

    /// <summary>
    /// 餌Boxの生成
    /// </summary>
    /// <param name="trans"> 餌Boxを生成する際の状態 </param>
    public void generateBox(Transform trans)
    {
        //生成位置を高くする
        float HeightBias = -100.0f;

        Vector3 pos = trans.position;
        pos.y += HeightBias;
        trans.position = pos;

        GameObject feedBoxInstance = Instantiate<GameObject>(feedBoxPrefab, trans.position, trans.rotation);
        feedBoxInstance.GetComponent<Rigidbody>();

        //一定時間フィールド上に存在する餌Boxオブジェクトは削除するようにする.
        Destroy(feedBoxInstance, 10.0f);
    }

    /// <summary>
    /// 自身(動物オブジェクト)が注視されたら,
    /// 餌を自身に向けて, 餌を投げる.
    /// (カメラが餌を投げる指示を出すのではなく, 動物オブジェクト自身から餌を投げるようにしている)
    /// </summary>
    private void Feeing()
    {
        GameObject feedInstance = Instantiate<GameObject>(InventoryFeed[0], cameraPrefab.transform.position, cameraPrefab.transform.rotation);
        //投げるゲームオブジェクトはリスとから消しておく
        InventoryFeed.RemoveAt(0);
        feedInstance.GetComponent<Rigidbody>().AddForce(feedInstance.transform.forward * speed);

        //一定時間フィールド上に存在する餌オブジェクトは削除するようにする.
        Destroy(feedInstance, DestryTime);
    }

    private void FeedWidow()
    {

    }
}
