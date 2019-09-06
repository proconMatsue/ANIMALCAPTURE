using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_temp : MonoBehaviour
{
    [SerializeField] private GameObject cameraPrefab;

    /// <summary>
    /// プレーヤーを見続ける
    /// </summary>
    /*private void Update()
    {
        //this.transform.LookAt(cameraPrefab.transform);
    }*/

    /// <summary>
    /// 餌と動物が触れたら消滅させる
    /// </summary>
    /// <param name="collision">特に気にする必要はない</param>
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision.gameObject.tag : " + collision.gameObject.tag);
        var mytag = this.gameObject.tag;
        var colltag = collision.gameObject.tag;

        if (
            (mytag == "cat" && colltag == "pike") ||
            (mytag == "squirrel" && colltag == "acorn") || 
            (mytag == "lion" && colltag == "meat") ||
            (mytag == "" && colltag == "meat")
            )
        {
            Destroy(collision.gameObject);
        }
    }
}
