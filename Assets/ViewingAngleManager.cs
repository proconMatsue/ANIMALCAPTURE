using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class ViewingAngleManager : MonoBehaviour
{
    /// <summary>
    /// GazeManagerで注視入力の最大距離が設定できるため, 
    /// その距離を取得するために使用
    /// </summary>
    private GazeManager gazeManager;

    /// <summary>
    /// GazeManagerから得た注視入力の最大距離から
    /// オブジェクトの大きさを取得
    /// </summary>
    void Start()
    {
        this.transform.localScale = new Vector3(
            gazeManager.MaxGazeCollisionDistance,
            gazeManager.MaxGazeCollisionDistance,
            gazeManager.MaxGazeCollisionDistance);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
