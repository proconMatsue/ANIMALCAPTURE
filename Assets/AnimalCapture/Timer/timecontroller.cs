using UnityEngine;
using UnityEngine.UI;

public class timecontroller : MonoBehaviour
{
    public Text time;//時間（秒）
    private float delta;
    public int second = 180;
    public GameObject timeUpText;//timeupの表示
    private float gameEndTime;
    public GameObject readyText;//Ready...GO!の表示
    int readyCount = 3;

    //ゲームがスタートしたかどうかを判断するフラグ
    private bool _isGameStart = false;
    //上の変数のプロパティ(クラス外では変更不可, 参照は可能)
    public bool isGameStart{ get; set; }

    //MyPlaySpaceManager myPlaySpaceManager;

    /*private void Start()
    {
       // myPlaySpaceManager = GameObject.Find("SpatialProcessing").GetComponent<MyPlaySpaceManager>();
    }*/

    void Update()
    {
        //if (myPlaySpaceManager.MeshesToPlanesCompleted)
        //{
            delta += Time.deltaTime;
            isGameStart = GameReady();
            if (isGameStart && second > 0)
            {
                if (delta >= 1.0f)
                {
                    second--;
                    if (second % 60 >= 10)
                        this.time.text = "TIME:" + second / 60 + ":" + second % 60;
                    else
                        this.time.text = "TIME:" + second / 60 + ":0" + second % 60;

                    delta = 0.0f;
                }
            }
            //0になったらタイムアップのシーンへ
            if (second == 0)
            {
                if (TimeUp())
                {
                    GameObject.Find("UICanvas/SceneChangeManager").GetComponent<SceneChangeManager>().SceneChange();
                    //SceneManager.LoadScene("finish");
                }
            }
        //}
        //else
        //{
        //    readyText.GetComponent<Text>().text = "Please Wait!";
        //}
    }

    bool GameReady()//ゲーム開始前はfalse、開始後はtrueを返す
    {
        if(delta >= 1.0f && readyCount > 0)
        {
            readyCount--;
            delta = 0.0f;
        }

        if(readyCount <= 0)
        {
            readyText.SetActive(false);
        }
        if (readyCount == 1)
        {
            readyText.GetComponent<Text>().text = "Go!";
        }
        else if (readyCount >= 2)
        {
            readyText.GetComponent<Text>().text = "Ready...";
        }

        if(readyCount > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool TimeUp()
    {
        if (!timeUpText.active)
        {
            timeUpText.SetActive(true);
            gameEndTime = Time.time;
        }
        if(Time.time - gameEndTime >= 3.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }   
}
