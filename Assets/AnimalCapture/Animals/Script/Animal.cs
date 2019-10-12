using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    //動物オブジェクトを格納しておく変数
    private GameObject animalObject_normal;
    private GameObject animalObject_anger;

    [SerializeField, Tooltip("動物の種類")] public int animaltype = 0;
    [SerializeField, Tooltip("エサを与えたときのポイント")]
    [Range(0, 5)]
    public float point = 5;
    [SerializeField, Tooltip("空腹になる確率")] public float hungryRate = 20;
    [SerializeField, Tooltip("空腹になるかの確認のタイミング")] public float checkHungryTime = 30.0f;
    [SerializeField, Tooltip("方向転換のタイミング\n(前回の方向決定をした時間からcheckTime秒なので，実際に進むのは(checkTime-moveStopTime)秒の間になる")]
    public float checkTime = 3.0f;
    [SerializeField, Tooltip("移動速度")] public float moveSpeed = 5.0f;
    [SerializeField, Tooltip("最高速度")] public float maxSpeed = 30.0f;
    [SerializeField, Tooltip("方向を変えてから動き出すまでの時間")] public float moveStopTime = 3.0f;
    [SerializeField, Tooltip("エサを与えられるプレイヤーとの距離?")] public float eatDistance = 3.0f;
    [SerializeField, Tooltip("動物の種類の最大数")] private int maxAnimalType = 4;
    [HideInInspector] public bool isHungry = false;      //おなかがすいているか
    private float preCheckTime = 0;             //前回方向変更をした時間
    private float stopCounts = 0;               //停止してからの時間
    private float preHungryCheckTime = 0;       //前回空腹チェックをした時間
    private float rotateY = 0;
    private int direction = 0;                  //動物の向き
    //  key:animaltype values:{point, hungryRate, checkHungryTime, checkTime, moveSpeed, maxSpeed, moveStopTime}
    //  animalType 0:リス 1:猫 2:ウサギ 3:ライオン
    private Dictionary<int, List<float>> data = new Dictionary<int, List<float>>(){ { 0, new List<float> { 5, 50, 6.0f, 4.5f, 8.0f, 1.5f, 3.5f}},
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

    //動物の状態
    enum state{
        normal, // 通常
        anger,  // 怒り
    }

    //動物のアニメーション
    private Animator animator_normal;
    private Animator animator_anger;

    //幸せアニメーションを再生するために使用する
    private bool isHappyTrigger;

    // Start is called before the first frame update
    private void Start()
    {
        //動物ごとにステータス設定
        StatusInit();

        //このスクリプトがアタッチされているオブジェクトの子オブジェクトを取得している
        //なお, 参照方法は上から順に0から数えていく.
        animalObject_normal = this.gameObject.transform.GetChild((int)state.normal).gameObject;
        animalObject_anger = this.gameObject.transform.GetChild((int)state.anger).gameObject;

        //アクティブ状態を初期設定
        animalObject_normal.SetActive(true);
        animalObject_anger.SetActive(false);

        isHappyTrigger = false;

        //子オブジェクトのanimetorスクリプトを取得
        animator_normal = animalObject_normal.GetComponent<Animator>();
        animator_anger = animalObject_anger.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        HungryCheck();
        Move();
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
        isHungry = false;           //通常状態からスタートさせる
        preCheckTime = Time.time + Random.Range(-0.5f, 0.5f);//方向転換のタイミングにばらつきを作る
    }

    //回転の種類
    enum turn
    {
        straight,   //直進(無回転)
        half,       //180度
        right,      //右90度
        left,       //左90度
    }

    /// <summary>
    /// 方向と時間を決定
    /// 歩きアニメーションの制御
    /// </summary>
    void Move()
    {

        if (Time.time - preCheckTime > checkTime || preCheckTime == 0)
        {
            direction = Random.Range(0, 4);
            if (stopCounts == 0){ stopCounts = Time.time; }
            preCheckTime = Time.time + Random.Range(-0.5f, 0.5f);//方向転換のタイミングにばらつきを作る
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        if (Time.time - stopCounts < moveStopTime)
        {
            //方向の決定
            switch ((turn)direction)
            {
                case turn.straight: break;                                                  //直進
                case turn.half: rotateY += Time.deltaTime * 180.0f / moveStopTime; break;   //半回転
                case turn.right: rotateY += Time.deltaTime * 90.0f / moveStopTime; break;   //右90度回転
                case turn.left: rotateY += Time.deltaTime * -90.0f / moveStopTime; break;   //左90度回転
            } transform.rotation = Quaternion.Euler(0, rotateY, 0);

            //歩きのアニメーションを実装
            if (!isHungry) { animator_normal.SetBool("isWalk", false); }
            else { animator_anger.SetBool("isWalk", false); }
        }
        else
        {
            stopCounts = 0;
            if (Mathf.Sqrt(Mathf.Pow(gameObject.GetComponent<Rigidbody>().velocity.x, 2) + Mathf.Pow(gameObject.GetComponent<Rigidbody>().velocity.z, 2)) < maxSpeed)
            { 
                this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed, ForceMode.Acceleration);
            }
            transform.rotation = Quaternion.Euler(0, rotateY, 0);

            if (!isHungry) { animator_normal.SetBool("isWalk", true); }
            else { animator_anger.SetBool("isWalk", true); }
        }

    }

    /// <summary>
    /// 動物の状態を変化させる( 常に呼ばれる )
    /// ランダムに怒り状態に変化させる
    /// </summary>
    void HungryCheck()
    {
        if (Time.time - preHungryCheckTime > checkHungryTime)
        {
            preHungryCheckTime = Time.time;
            if (isHungry == false)
            {
                //ランダムでおなかがすいた状態にする
                if (Random.Range(0, 100) < hungryRate){ isHungry = true; }
                else{ isHungry = false; }
            }
        }
        if (isHungry)
        {
            StateNormalAnimal(false);
        }
        else
        {
            StateNormalAnimal(true);
            if (isHappyTrigger) { animator_normal.SetTrigger("isHappy"); isHappyTrigger = false; }
        }
    }

    /// <summary>
    /// 動物オブジェクトの通常状態を表示させるか, させないかを操作する関数
    /// </summary>
    /// <param name="s"> true: 通常状態を表示, false: 怒り状態を表示 </param>
    private void StateNormalAnimal(bool s)
    {
        animalObject_normal.SetActive(s);
        animalObject_anger.SetActive(!s);
    }
    
    /// <summary>
    /// エサを食べる
    /// </summary>
    public void Eat()
    {
        isHungry = false;           //通常状態に戻す
        isHappyTrigger = true;      //幸せアニメーションを再生
        ResultUIManager.Score++;    //スコアを加算する
    }


    /// <summary>
    /// 餌と動物が触れたら消滅させる
    /// エサを食べる処理を実装
    /// </summary>
    /// <param name="collision">特に気にする必要はない</param>
    private void OnCollisionEnter(Collision collision)
    {
        string mytag = this.gameObject.tag;
        string colltag = collision.gameObject.tag;

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