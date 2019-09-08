using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneChangeManager : MonoBehaviour
{
    private UnityEvent OnSelectionCompleted = new UnityEvent();
    [SerializeField] private string NextScene = "stage01";



    GameObject gazeCircle;
    GameObject MixedRealityCameraParent;


    /*public void CallSceneChange()
    {
        
        Invoke("SceneChange", 2);
    }*/
    private void Start()
    {
        MixedRealityCameraParent = GameObject.Find("MixedRealityCameraParent");
        gazeCircle = GameObject.Find("UISelectionBar");
        gazeCircle.SetActive(false);
    }

    /// <summary>
    /// 注視入力されたら画面を
    /// </summary>
    public void SceneChange()
    {
        Destroy(MixedRealityCameraParent);
        SceneManager.LoadScene(NextScene);
        //gazeCircle.SetActive(false);
    }


    /*public void GetCameraPos()
    {
        GameObject camera = GameObject.Find("MixedRealityCamera");

    }*/

}
