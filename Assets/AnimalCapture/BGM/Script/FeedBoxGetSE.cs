//AudioSource「FeedBoxGetSE」を作り、そこにアタッチさせています
//Feedbox.csで呼び出しています
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBoxGetSE : MonoBehaviour
{
    public AudioClip FeedBoxGet;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FeedBoxGetPlay()
    {
        audioSource.PlayOneShot(FeedBoxGet);
    }
}
