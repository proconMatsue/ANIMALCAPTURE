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
    GameObject InputManager;
    GameObject SpatialMapping;


    /*public void CallSceneChange()
    {
        
        Invoke("SceneChange", 2);
    }*/
    private void Start()
    {
        MixedRealityCameraParent = GameObject.Find("MixedRealityCameraParent");
        gazeCircle = GameObject.Find("DefaultCursorWithGazeSelector");
        InputManager = GameObject.Find("InputManager");
        SpatialMapping = GameObject.Find("SpatialMapping");
        //gazeCircle.SetActive(false);
    }

    /// <summary>
    /// 注視入力されたら画面を
    /// </summary>
    public void SceneChange()
    {
        Destroy(MixedRealityCameraParent);//次のシーンで被ってしまうオブジェクトをシーン直前に削除
        Destroy(gazeCircle);
        Destroy(InputManager);
        Destroy(SpatialMapping);
        SceneManager.LoadScene(NextScene);
        //gazeCircle.SetActive(false);
    }


    /*public void GetCameraPos()
    {
        GameObject camera = GameObject.Find("MixedRealityCamera");

    }*/

}
