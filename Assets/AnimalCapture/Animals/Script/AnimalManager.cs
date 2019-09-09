using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class animals
{
    private GameObject animalObject;
    private float angerTime;
    private bool isAnger;

    public GameObject AnimalObject { set; get; }
    public float AngerTime { get; set; }
    public bool IsAangry { set; get; }
}

public class AnimalManager : MonoBehaviour
{
    //動物オブジェクトに対する列挙型
    public enum Animals
    {
        Squirrel,           //リス
        Cat,                //ネコ
        Rabbit,             //ウサギ
        Lion,               //ライオン
        Unexpect = 0xff     //想定外の値
    }

    //ノーマル状態の動物オブジェクトのプレハブ
    [SerializeField] private GameObject squirrelPrefab_normal;
    [SerializeField] private GameObject catPrefab_normal;
    [SerializeField] private GameObject rabbitPrefab_normal;
    [SerializeField] private GameObject lionPrefab_normal;

    //怒り状態の動物オブジェクトのプレハブ
    [SerializeField] private GameObject squirrelPrefab_angry;
    [SerializeField] private GameObject catPrefab_angry;
    [SerializeField] private GameObject rabbitPrefab_angry;
    [SerializeField] private GameObject lionPrefab_angry;

    //存在できる動物の数の上限
    [SerializeField] private readonly int MaxNumberAnimals = 5;

    //フィールド上の動物を格納する変数
    List<animals> Animal = new List<animals>();


    private void Start()
    {
        //最初に5匹フィールドに出す
        for(int i = 0; i < MaxNumberAnimals; i++)
        {
            animals a = new animals();
            a.AnimalObject = Instantiate<GameObject>(
                RandomAnimalObject(),
                new Vector3(Random.Range(-3.0f, 3.0f), 0.0f, Random.Range(-3.0f, 3.0f)),
                this.gameObject.transform.rotation);
            a.AnimalObject.transform.parent = transform;
            a.AngerTime = Random.Range(10.0f, 60.0f);
            a.IsAangry = false;
            Animal.Add(a);
        }
    }

    private void Update()
    {
        for(int i = 0; i < MaxNumberAnimals; i++)
        {
            if(Animal[i].AngerTime >= Time.deltaTime){ Animal[i].IsAangry = true; }
        }
    } 

    /// <summary>
    /// ランダムに動物オブジェクトを返す関数
    /// </summary>
    /// <returns> ランダムに選ばれた動物オブジェクト </returns>
    private GameObject RandomAnimalObject()
    {
        Animals a = (Animals)Random.Range((int)Animals.Squirrel, (int)Animals.Lion + 1);
        return AnimalTable(a, true);
    }

    /// <summary>
    /// 動物についての列挙型とその動物の状態から変化後の動物オブジェクトのプレハブを返す関数(テーブル)
    /// </summary>
    /// <param name="animals"> 動物についての列挙型 </param>
    /// <param name="isAngry"> 動物の状態 </param>
    /// <returns></returns>
    private GameObject AnimalTable(Animals animals, bool isAngry)
    {
        switch (animals)
        {
            case Animals.Squirrel:
                if (isAngry) { return squirrelPrefab_normal; }
                else { return squirrelPrefab_angry; }
            case Animals.Cat:
                if (isAngry) { return catPrefab_normal; }
                else { return catPrefab_angry; }
            case Animals.Rabbit:
                if (isAngry) { return rabbitPrefab_normal; }
                else { return rabbitPrefab_angry; }
            case Animals.Lion:
                if (isAngry) { return lionPrefab_normal; }
                else { return lionPrefab_angry; }
            default: Debug.LogWarning("unexpected return."); return null;
        }
    }

    /// <summary>
    /// 動物の状態を反転させる関数(通常 ⇔ 怒り)
    /// 状態を変換させるときのみに呼ばれることを予想している
    /// </summary>
    /*public void ChangeAnimalState(Generator.Animals animals, bool isAngry)
    {
        Transform temp_transform;
        //動物が怒っていたら
        if (isAngry)
        {
            temp_transform = AnimalObject.transform;
            Destroy(animalObject);
            AnimalObject = Instantiate<GameObject>(
               AnimalTable(animals, isAngry) , temp_transform.position, temp_transform.rotation);
        }
        //怒っていないとき
        else
        {
            temp_transform = AnimalObject.transform;
            Destroy(animalObject);
            AnimalObject = Instantiate<GameObject>(
               AnimalTable(animals, isAngry), temp_transform.position, temp_transform.rotation);
        }

        //状態の反転
        isAngry = !isAngry;
    }*/

}
