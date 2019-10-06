//StartButtonGeneratorにGazeSelectorTarget.csをアタッチ
//OnSelectionCompleted()から、buttonPlay()を呼び出す
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSE : MonoBehaviour
{
    public bool DontDestroyEnabled = true;
    public AudioClip button;//ボタン選択時に鳴らすSE
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if(DontDestroyEnabled)
            DontDestroyOnLoad(this);//シーンを遷移してもオブジェクトが消えないようにする

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonPlay()
    {
        audioSource.PlayOneShot(button);//buttonを1回鳴らす
    }
}
