using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animal_temp : MonoBehaviour
{
    [SerializeField] GameObject EnterMarkPrefab;
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject childObject = Instantiate(EnterMarkPrefab) as GameObject;
        childObject.transform.parent = this.gameObject.transform;
    }
}
