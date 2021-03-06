﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public GameObject animalObject;
    public GameObject angerObject;
    public int animaltype = 0;          //動物の種類
    public float point = 5;             //エサを与えたときのポイント?
    public float hungryRate = 20;       //空腹になる確率
    public float checkHungryTime = 30.0f;//空腹になるかの確認のタイミング
    public float checkTime = 3.0f;     //方向転換のタイミング(前回の方向決定をした時間からcheckTime秒なので，実際に進むのは(checkTime-moveStopTime)秒の間になる
    public float moveSpeed = 5.0f;      //移動速度
    public float maxSpeed = 30.0f;      //最高速度
    public float moveStopTime = 3.0f;   //方向を変えてから動き出すまでの時間
    public float eatDistance = 3.0f;    //エサを与えられるプレイヤーとの距離?
    int maxAnimalType = 4;              //動物の種類の最大数
    public bool isHungry = false;              //おなかがすいているか
    float preCheckTime = 0;             //前回方向変更をした時間
    float stopCounts = 0;               //停止してからの時間
    float preHungryCheckTime = 0;       //前回空腹チェックをした時間
    float rotateY = 0;
    int direction = 0;                  //動物の向き
    //  key:animaltype values:{point, hungryRate, checkHungryTime, checkTime, moveSpeed, maxSpeed, moveStopTime}
    //  animalType 0:リス 1:猫 2:ウサギ 3:ライオン
    Dictionary<int, List<float>> data = new Dictionary<int, List<float>>(){ { 0, new List<float> { 5, 50, 6.0f, 4.5f, 8.0f, 1.5f, 3.5f}},
                                                                            { 1, new List<float> { 5, 50, 6.0f, 5.0f, 15.0f, 1.5f, 3.5f}},
                                                                            { 2, new List<float> { 5, 50, 6.0f, 5.5f, 8.0f, 1.5f, 3.5f}},
                                                                            { 3, new List<float> { 5, 50, 6.0f, 6.0f, 15.0f, 1.5f, 3.5f}}};
    enum parameter
    {
        point,
        hungryRate,
        checkHungryTime,
        checkTime,
        moveSpeed,
        maxSpeed,
        moveStopTime
    }

    Animator animator_normal;
    Animator animator_anger;
    bool isHappyTrigger;

    // Start is called before the first frame update
    void Start()
    {
        //動物ごとにステータス設定
        StatusInit();

        animalObject = this.gameObject.transform.GetChild(0).gameObject;
        angerObject = this.gameObject.transform.GetChild(1).gameObject;

        animator_normal = animalObject.GetComponent<Animator>();
        animator_anger = angerObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isHungry);
        Move();
        if (MeasureDistance() < eatDistance)
        {
            //頭の上に三角表示
            DisplayTriangle();
        }
        HungryCheck();

    }

    void StatusInit()
    {
        if (animaltype < maxAnimalType)
        {
            point = data[animaltype][(int)parameter.point];                     //エサを与えたときのポイント
            hungryRate = data[animaltype][(int)parameter.hungryRate];           //空腹になる確率
            checkHungryTime = data[animaltype][(int)parameter.checkHungryTime]; //空腹になるかの確認のタイミング
            checkTime = data[animaltype][(int)parameter.checkTime];             //方向転換のタイミング     
            moveSpeed = data[animaltype][(int)parameter.moveSpeed];             //移動速度
            maxSpeed = data[animaltype][(int)parameter.maxSpeed];               //最高速度
            moveStopTime = data[animaltype][(int)parameter.moveStopTime];       //動き出すまでの時間
        }
        isHungry = false;           //おなかがすいているか
        preCheckTime = Time.time + Random.Range(-0.5f, 0.5f);//方向転換のタイミングにばらつきを作る
        if (animaltype == (int)AnimalManager.Animals.Squirrel) {
            this.gameObject.tag = "squirrel";
        }
        if (animaltype == (int)AnimalManager.Animals.Cat)
        {
            this.gameObject.tag = "cat";
        }
        if (animaltype == (int)AnimalManager.Animals.Rabbit)
        {
            this.gameObject.tag = "rabbit";
        }
        if (animaltype == (int)AnimalManager.Animals.Lion)
        {
            this.gameObject.tag = "lion";
        }
    }

    //動物の移動
    void Move()
    {

        if (Time.time - preCheckTime > checkTime || preCheckTime == 0)
        {
            //方向を変える
            /*
             * 方向を変えるなら進行方向よりもrotateをいじって回転させるべきか
             */
            //Debug.Log(Time.time);
            direction = Random.Range(0, 4);
            if (stopCounts == 0)
            {
                stopCounts = Time.time;
            }
            preCheckTime = Time.time + Random.Range(-0.5f, 0.5f);//方向転換のタイミングにばらつきを作る
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        if (Time.time - stopCounts < moveStopTime)
        {
            //Debug.Log(direction);
            //Debug.Log(Time.deltaTime);
            if (direction == 0)
            {

            }
            else if (direction == 1)
            {
                rotateY += Time.deltaTime * 180.0f / moveStopTime;
            }
            else if (direction == 2)
            {
                rotateY += Time.deltaTime * 90.0f / moveStopTime;
            }
            else if (direction == 3)
            {
                rotateY += Time.deltaTime * -90.0f / moveStopTime;
            }
            transform.rotation = Quaternion.Euler(0, rotateY, 0);

            if (!isHungry) { animator_normal.SetBool("isWalk", false); }
            else { animator_anger.SetBool("isWalk", false); }
        }
        else
        {
            stopCounts = 0;
            if (Mathf.Sqrt(Mathf.Pow(gameObject.GetComponent<Rigidbody>().velocity.x, 2) + Mathf.Pow(gameObject.GetComponent<Rigidbody>().velocity.z, 2)) < maxSpeed)
            {
                //gameObject.GetComponent<Rigidbody>().AddForce(moveForce);
                gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed, ForceMode.Acceleration);
            }
            transform.rotation = Quaternion.Euler(0, rotateY, 0);

            if (!isHungry) { animator_normal.SetBool("isWalk", true); }
            else { animator_anger.SetBool("isWalk", true); }
        }

    }

    //おなかがすいたかのチェック（checkTimeの周期）
    void HungryCheck()
    {
        if (Time.time - preHungryCheckTime > checkHungryTime)
        {
            preHungryCheckTime = Time.time;
            if (isHungry == false)
            {
                if (Random.Range(0, 100) < hungryRate)   //ランダムでおなかがすいた状態にする
                {
                    isHungry = true;
                }
                else
                {
                    isHungry = false;
                }
            }
        }
        if (isHungry)
        {
            animalObject.SetActive(false);
            angerObject.SetActive(true);
        }
        else
        {
            animalObject.SetActive(true);
            angerObject.SetActive(false);

            if (isHappyTrigger) { animator_normal.SetTrigger("isHappy"); isHappyTrigger = false; }
        }
    }

    //エサを食べる（得点の獲得とisHungryをfalse）
    public void Eat()
    {
        if (isHungry)
        {
            isHungry = false;
            ResultUIManager.Score++;
            isHappyTrigger = true;
        }
    }

    //時間切れのとき動物の削除
    void Delete()
    {

    }

    //プレイヤーと動物の距離を測る
    float MeasureDistance()
    {
        return 100.0f;
    }

    //頭の上に三角を表示
    void DisplayTriangle()
    {

    }

    /// <summary>
    /// 餌と動物が触れたら消滅させる
    /// </summary>
    /// <param name="collision">特に気にする必要はない</param>
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision.gameObject.tag : " + collision.gameObject.tag);
        var mytag = this.gameObject.tag;
        var colltag = collision.gameObject.tag;

        if (
            (mytag == "cat" && colltag == "pike") ||
            (mytag == "squirrel" && colltag == "acorn") ||
            (mytag == "lion" && colltag == "meat") ||
            (mytag == "rabbit" && colltag == "carrot")
            )
        {
            Destroy(collision.gameObject);
            Eat();
        }
    }
}