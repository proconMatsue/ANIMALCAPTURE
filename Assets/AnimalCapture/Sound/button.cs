using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		AudioSource = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		if()////注視入力が完了したら
		{
			////buttonを鳴らす
			AudioSource.PlayOneShot(button);
		}
    }
}
