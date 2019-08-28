using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterMarkManager : MonoBehaviour
{
    [SerializeField] GameObject EnterMarkPrefab;

    void EnterMarkGenerate(GameObject GazedObject) {
        GameObject childObject = Instantiate(EnterMarkPrefab) as GameObject;
        childObject.transform.parent = GazedObject.transform;
    }
}
