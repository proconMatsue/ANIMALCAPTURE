using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickStartButtonScript : MonoBehaviour
{
	public AudioSource startbutton_AudioSource;
    // Start is called before the first frame update
    void Start()
    {
		startbutton_AudioSource = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void StartButtonClick()
	{
		startbutton_AudioSource.PlayOneShot (startbutton_AudioSource.clip);
	}
}
