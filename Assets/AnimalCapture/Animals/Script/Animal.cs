using System.Collections;
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
    public float checkTime = 3.0f;     //方向転換のタイミング
    public float moveSpeed = 5.0f;      //移動速度
    public float maxSpeed = 30.0f;      //最高速度
    public float moveStopTime = 3.0f;   //方向を変えてから動き出すまでの時間
    public float eatDistance = 3.0f;    //エサを与えられるプレイヤーとの距離?
    int maxAnimalType = 4;              //動物の種類の最大数
    bool isHungry = false;              //おなかがすいているか
    float preCheckTime = 0;             //前回方向変更をした時間
    float stopCounts = 0;               //停止してからの時間
    float preHungryCheckTime = 0;       //前回空腹チェックをした時間
    float rotateY = 0;
    int direction = 0;                  //動物の向き
    //  key:animaltype values:{point, hungryRate, checkHungryTime, checkTime, moveSpeed, maxSpeed, moveStopTime}
    Dictionary<int, List<float>> data = new Dictionary<int, List<float>>(){ { 0, new List<float> { 5, 50, 10.0f, 5.0f, 10.0f, 10.0f, 3.0f}},
                                                                            { 1, new List<float> { 5, 50, 10.0f, 5.0f, 15.0f, 30.0f, 3.0f}},
                                                                            { 2, new List<float> { 5, 50, 10.0f, 5.0f, 15.0f, 30.0f, 3.0f}},
                                                                            { 3, new List<float> { 5, 50, 10.0f, 5.0f, 15.0f, 30.0f, 3.0f}}};
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

    // Start is called before the first frame update
    void Start()
    {
        //動物ごとにステータス設定
        StatusInit();
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
        else
        {
            if (isHungry == false)  //おなかがすいていない時おなかがすくかどうかを判定
            {
                isHungry = HungryCheck();
            }
        }
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
        preCheckTime = 0;
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
            preCheckTime = Time.time;
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
        }

    }

    //おなかがすいたかのチェック（checkTimeの周期）
    bool HungryCheck()
    {
        if (Time.time - preHungryCheckTime > checkHungryTime)
        {
            preHungryCheckTime = Time.time;
            if (Random.Range(0, 100) < hungryRate)   //ランダムでおなかがすいた状態にする
            {
                animalObject.SetActive(false);
                angerObject.SetActive(true);
                return true;
            }
            animalObject.SetActive(true);
            angerObject.SetActive(false);
            return false;
        }
        return isHungry;
    }

    //エサを食べる（得点の獲得とisHungryをfalse）
    public void Eat()
    {
        isHungry = false;
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
            (mytag == "" && colltag == "meat")
            )
        {
            Destroy(collision.gameObject);

            ResultUIManager.Score++;
        }
    }
}