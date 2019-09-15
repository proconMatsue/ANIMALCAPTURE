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
    void Update()
    {
        delta += Time.deltaTime;
        if (delta >= 1.0f)
        {
            second--;
            if (second % 60 >= 10)
                this.time.text = "TIME:" + second / 60 + ":" + second % 60;
            else
                this.time.text = "TIME:" + second / 60 + ":0" + second % 60;

            delta = 0.0f;
        }
        //0になったらタイムアップのシーンへ
        if (second == 0)
            GameObject.Find("SceneChangeManager").GetComponent<SceneChangeManager>().SceneChange();
            //SceneManager.LoadScene("finish");
    }
    
}
