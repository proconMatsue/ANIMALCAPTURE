﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// animalObject: 動物のオブジェクト
/// </summary>
class animals
{
    private GameObject animalObject;
    //private float angerTime;
    //private bool isAnger;

    public GameObject AnimalObject { set; get; }
    //public float AngerTime { get; set; }
    //public bool IsAangry { set; get; }
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
    [SerializeField, Tooltip("ノーマル状態のリスのプレハブ")] private GameObject squirrelPrefab_normal;
    [SerializeField, Tooltip("ノーマル状態のネコのプレハブ")] private GameObject catPrefab_normal;
    [SerializeField, Tooltip("ノーマル状態のウサギのプレハブ")] private GameObject rabbitPrefab_normal;
    [SerializeField, Tooltip("ノーマル状態のライオンのプレハブ")] private GameObject lionPrefab_normal;

    //怒り状態の動物オブジェクトのプレハブ
    /*[SerializeField, Tooltip("怒り状態のリスのプレハブ")] private GameObject squirrelPrefab_angry;
    [SerializeField, Tooltip("怒り状態のネコのプレハブ")] private GameObject catPrefab_angry;
    [SerializeField, Tooltip("怒り状態のウサギのプレハブ")] private GameObject rabbitPrefab_angry;
    [SerializeField, Tooltip("怒り状態のライオンのプレハブ")] private GameObject lionPrefab_angry;*/

    //存在できる動物の数の上限
    [SerializeField, Tooltip("フィールド上に存在できる動物の数")]
    [Range(0, 10)]
    private readonly int MaxNumberAnimals = 5;

    //生成する動物の生成位置
    [SerializeField, Tooltip("動物の生成位置のX成分\n(プレーヤを中心として指定した範囲に動物が生成される)")]
    [Range(0.0f, 5.0f)]
    private float generatePosRange_x = 1.0f;
    [SerializeField, Tooltip("動物の生成位置のZ成分\n(プレーヤを中心として指定した範囲に動物が生成される)")]
    [Range(0.0f, 5.0f)]
    private float generatePosRange_z = 1.0f;

    [SerializeField, Tooltip("動物の大きさ\n(1から何倍するかを表している)")]
    [Range(0.0f, 1.0f)]
    private float AnimalScale = 0.1f;

    //フィールド上の動物を格納する変数
    List<animals> Animal = new List<animals>();
            
    /// <summary>
    /// ゲームがスタートし, スタートの表示が消えた時に動物を5体生成する
    /// </summary>
    private void Update()
    {
        //ゲーム中に動いている timecontrollerスクリプト を取得
        timecontroller time = GameObject.Find("UICanvas").GetComponent<timecontroller>();
        if(time == null) { Debug.LogWarning("UICanvasが正しく見つかっていません."); return; }

        //ゲーム中に動いている MyPlaySpaceManagerスクリプト を取得
        MyPlaySpaceManager myPlaySpaceManager = GameObject.Find("SpatialProcessing").GetComponent<MyPlaySpaceManager>();
        if(myPlaySpaceManager == null) { Debug.LogWarning("SpatialProcessingが正しく見つかっていません."); return; }

        if (time.isGameStart                                //ゲームがスタートした 
            && myPlaySpaceManager.MeshesToPlanesCompleted   //フィールド上が正しく用意された
            && Animal.Count == 0)                           //フィールド上の動物の数が0体である
        {
            //最初に5匹フィールドに出す
            //for (int i = 0; i < MaxNumberAnimals; i++){ generateAnimal(); }
            //アニマルクラスの変数のインスタンスを生成
            animals a = new animals();

            //オブジェクトを生成
            //x-z座標はインスペクタで指定した範囲内でランダム
            a.AnimalObject = Instantiate<GameObject>(
                squirrelPrefab_normal,
                new Vector3(
                    Random.Range(-generatePosRange_x, generatePosRange_x),
                    1.0f,
                    Random.Range(-generatePosRange_z, generatePosRange_z)
                ),
                this.gameObject.transform.rotation);

            //アニマルマネージャの子オブジェクトとする
            a.AnimalObject.transform.parent = transform;

            //動物オブジェクトの大きさを動的に変更
            a.AnimalObject.transform.localScale = new Vector3(AnimalScale, AnimalScale, AnimalScale);
            //a.AngerTime = Random.Range(10.0f, 60.0f);
            //a.IsAangry = false;

            //リストに格納しておく
            Animal.Add(a);

            //アニマルクラスの変数のインスタンスを生成
            a = new animals();

            //オブジェクトを生成
            //x-z座標はインスペクタで指定した範囲内でランダム
            a.AnimalObject = Instantiate<GameObject>(
                catPrefab_normal,
                new Vector3(
                    Random.Range(-generatePosRange_x, generatePosRange_x),
                    1.0f,
                    Random.Range(-generatePosRange_z, generatePosRange_z)
                ),
                this.gameObject.transform.rotation);

            //アニマルマネージャの子オブジェクトとする
            a.AnimalObject.transform.parent = transform;

            //動物オブジェクトの大きさを動的に変更
            a.AnimalObject.transform.localScale = new Vector3(AnimalScale, AnimalScale, AnimalScale);
            //a.AngerTime = Random.Range(10.0f, 60.0f);
            //a.IsAangry = false;

            //リストに格納しておく
            Animal.Add(a);

            //アニマルクラスの変数のインスタンスを生成
            a = new animals();

            //オブジェクトを生成
            //x-z座標はインスペクタで指定した範囲内でランダム
            a.AnimalObject = Instantiate<GameObject>(
                lionPrefab_normal,
                new Vector3(
                    Random.Range(-generatePosRange_x, generatePosRange_x),
                    1.0f,
                    Random.Range(-generatePosRange_z, generatePosRange_z)
                ),
                this.gameObject.transform.rotation);

            //アニマルマネージャの子オブジェクトとする
            a.AnimalObject.transform.parent = transform;

            //動物オブジェクトの大きさを動的に変更
            a.AnimalObject.transform.localScale = new Vector3(AnimalScale, AnimalScale, AnimalScale);
            //a.AngerTime = Random.Range(10.0f, 60.0f);
            //a.IsAangry = false;

            //リストに格納しておく
            Animal.Add(a);

            //アニマルクラスの変数のインスタンスを生成
            a = new animals();

            //オブジェクトを生成
            //x-z座標はインスペクタで指定した範囲内でランダム
            a.AnimalObject = Instantiate<GameObject>(
                rabbitPrefab_normal,
                new Vector3(
                    Random.Range(-generatePosRange_x, generatePosRange_x),
                    1.0f,
                    Random.Range(-generatePosRange_z, generatePosRange_z)
                ),
                this.gameObject.transform.rotation);

            //アニマルマネージャの子オブジェクトとする
            a.AnimalObject.transform.parent = transform;

            //動物オブジェクトの大きさを動的に変更
            a.AnimalObject.transform.localScale = new Vector3(AnimalScale, AnimalScale, AnimalScale);
            //a.AngerTime = Random.Range(10.0f, 60.0f);
            //a.IsAangry = false;

            //リストに格納しておく
            Animal.Add(a);
        }
    }


    /// <summary>
    /// 動物を生成し, 動物リストの中に格納するまでを行う関数
    /// </summary>
    private void generateAnimal()
    {
        //アニマルクラスの変数のインスタンスを生成
        animals a = new animals();

        //オブジェクトを生成
        //x-z座標はインスペクタで指定した範囲内でランダム
        a.AnimalObject = Instantiate<GameObject>(
            RandomAnimalObject(),
            new Vector3(
                Random.Range(-generatePosRange_x, generatePosRange_x), 
                1.0f, 
                Random.Range(-generatePosRange_z, generatePosRange_z)
            ),
            this.gameObject.transform.rotation);

        //アニマルマネージャの子オブジェクトとする
        a.AnimalObject.transform.parent = transform;

        //動物オブジェクトの大きさを動的に変更
        a.AnimalObject.transform.localScale = new Vector3(AnimalScale, AnimalScale, AnimalScale);
        //a.AngerTime = Random.Range(10.0f, 60.0f);
        //a.IsAangry = false;

        //リストに格納しておく
        Animal.Add(a);
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
                return squirrelPrefab_normal;
            //if (isAngry) { return squirrelPrefab_normal; }
            //else { return squirrelPrefab_angry; }
            case Animals.Cat:
                return catPrefab_normal;
            //if (isAngry) { return catPrefab_normal; }
            //else { return catPrefab_angry; }
            case Animals.Rabbit:
                return rabbitPrefab_normal;
            //if (isAngry) { return rabbitPrefab_normal; }
            //else { return rabbitPrefab_angry; }
            case Animals.Lion:
                return lionPrefab_normal;
            //if (isAngry) { return lionPrefab_normal; }
            //else { return lionPrefab_angry; }
            default: Debug.LogWarning("unexpected return."); return null;
        }
    }
}
