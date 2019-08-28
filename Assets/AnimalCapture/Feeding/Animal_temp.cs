using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_temp : MonoBehaviour
{
    /// <summary>
    /// 餌と動物が触れたら消滅させる
    /// </summary>
    /// <param name="collision">特に気にする必要はない</param>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision.gameObject.tag : " + collision.gameObject.tag);
        if(collision.gameObject.tag == "feed")
            Destroy(collision.gameObject);
    }
}
