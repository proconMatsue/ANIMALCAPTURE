using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timecontroller : MonoBehaviour
{
    public Text time;//時間（秒）
    private float delta;
    public int second = 180;
    public GameObject timeUpText;//timeupの表示
    private float gameEndTime;
    public GameObject readyText;//Ready...GO!の表示
    int readyCount = 3;

    void Update()
    {
        delta += Time.deltaTime;
        if (GameReady() && second > 0)
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
                GameObject.Find("SceneChangeManager").GetComponent<SceneChangeManager>().SceneChange();
                //SceneManager.LoadScene("finish");
            }
        }
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