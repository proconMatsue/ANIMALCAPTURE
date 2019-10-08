//gameシーン内にAudioSource「CountdownSE」を作り、そこにアタッチさせています

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownSE : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject UICanvas;//Timerフォルダの中にあるオブジェクト「UICanvas」を格納する変数
    timecontroller script;//スクリプト「timecontroller」が入る変数
    public AudioClip before10;//残り時間10秒前から鳴らすSE
    public AudioClip timeup;//時間切れ時に鳴らすSE
    private AudioSource audioSources;
    int second;//スクリプト「timecontroller」の変数secondを参照するための変数
    int oldsecond = 0;//更新される前のsecondの値を格納する変数

    void Start()
    {
        UICanvas = GameObject.Find("UICanvas");//オブジェクト「UICanvas」を名前から取得して変数に格納
        script = UICanvas.GetComponent<timecontroller>();//オブジェクト「UICanvas」にアタッチされているスクリプト「timecontroller」を取得して変数に格納する
        audioSources = GetComponent<AudioSource>();  
     
    }

    // Update is called once per frame
    void Update()
    {
        second = script.second;//変数secondの中にスクリプト「timecontroller」の変数secondを代入する
 

        if( second % 60 <= 10 && second % 60 >= 1 && second / 60 == 0)//残り10秒から1秒ずつSE(audioSource1)を鳴らす
        {
            if(second != oldsecond)//secondの値が変わったら
                audioSources.PlayOneShot(before10);//before10を1回鳴らす
            oldsecond = second;//26行目で更新されるsecondの値と比較するため、変数oldsecondに値を残しておく
        }
        else if(second == 0)//0秒になったらSE(timeup)を鳴らす
        {
            if (second != oldsecond)//secondの値が変わったら
                audioSources.PlayOneShot(timeup);//timeupを1回鳴らす
            oldsecond = second;//26行目で更新されるsecondの値と比較するため、変数oldsecondに値を残しておく

        }
        oldsecond = second;//26行目で更新されるsecondの値と比較するため、変数oldsecondに値を残しておく
    }
}
