using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneChangeManager : MonoBehaviour
{
    private UnityEvent OnSelectionCompleted = new UnityEvent();
    [SerializeField] private string NextScene = "stage01";

    /// <summary>
    /// 注視入力されたら画面を
    /// </summary>
    public void SceneChange()
    {
        SceneManager.LoadScene(NextScene);
    }
}
