using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneChangeManager : MonoBehaviour
{
    private UnityEvent OnSelectionCompleted = new UnityEvent();
    [SerializeField, Tooltip("遷移先のシーン名")] private string NextScene = "stage01";
    [SerializeField, Tooltip("時間によって画面が遷移するかどうか")] private bool useTimer = false;
    [SerializeField, Tooltip("画面が遷移するまでの時間")] private float sceneChangeTime = 0.0f;

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
        //次のシーンで被ってしまうオブジェクトをシーン直前に削除
        if (MixedRealityCameraParent != null)
        {
            Destroy(MixedRealityCameraParent);
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
            //次のシーンで被ってしまうオブジェクトをシーン直前に削除
            if (MixedRealityCameraParent != null)
            {
                Destroy(MixedRealityCameraParent);
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
