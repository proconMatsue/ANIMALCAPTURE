//AudioSource「FeedingSE」を作り、そこにアタッチさせています
//FeedInventory.cs　Feeing()から呼び出しています
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedingSE : MonoBehaviour
{
    public AudioClip Feeding;
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

    public void FeedingPlay()
    {
        audioSource.PlayOneShot(Feeding);
    }
}
