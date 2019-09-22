using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneChangeManager : MonoBehaviour
{
    private UnityEvent OnSelectionCompleted = new UnityEvent();
    [SerializeField] private string NextScene = "stage01";
    [SerializeField] private float sceneChangeTime= 0.0f;
    [SerializeField] private bool useTimer = false;

    GameObject gazeCircle;
    GameObject MixedRealityCameraParent;
    GameObject InputManager;
    GameObject SpatialMapping;
    private float startTime = 0.0f;


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
        startTime = Time.time;
        //gazeCircle.SetActive(false);
    }

    private void Update()
    {
        if (useTimer)
        {
            AutoSceneChange();
        }
        
    }

    /// <summary>
    /// 注視入力されたら画面を
    /// </summary>
    public void SceneChange()
    {
        if (MixedRealityCameraParent != null)
        {
            Destroy(MixedRealityCameraParent);//次のシーンで被ってしまうオブジェクトをシーン直前に削除
        }
        if (gazeCircle != null)
        {
            Destroy(gazeCircle);
        }
        if (InputManager != null)
        {
            Destroy(InputManager);
        }
        if (SpatialMapping != null)
        {
            Destroy(SpatialMapping);
        }
        SceneManager.LoadScene(NextScene);
        //gazeCircle.SetActive(false);
    }

    public void AutoSceneChange()
    {
        if (Time.time - startTime > sceneChangeTime)
        {
            if (MixedRealityCameraParent != null)
            {
                Destroy(MixedRealityCameraParent);//次のシーンで被ってしまうオブジェクトをシーン直前に削除
            }
            if (gazeCircle != null)
            {
                Destroy(gazeCircle);
            }
            if (InputManager != null)
            {
                Destroy(InputManager);
            }
            if (SpatialMapping != null)
            {
                Destroy(SpatialMapping);
            }
            SceneManager.LoadScene(NextScene);
            //gazeCircle.SetActive(false);
        }
    }


    /*public void GetCameraPos()
    {
        GameObject camera = GameObject.Find("MixedRealityCamera");

    }*/

}
