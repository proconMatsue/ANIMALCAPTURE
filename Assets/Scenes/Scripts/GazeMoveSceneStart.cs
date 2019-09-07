using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine.SceneManagement;


namespace HoloToolkitExtension.Input
{

    public class GazeMoveSceneStart : MonoBehaviour, IFocusable
    {

        public UnityEvent OnSelectionCompleted = new UnityEvent();

        public void OnFocusEnter()
        {
            //オブジェクトに視線が当たったらゲージがいっぱいになった時のイベントを登録
            SelectionRadial.Instance.OnSelectionComplete += OnSelectionComplete;
            SelectionRadial.Instance.Show();
            SelectionRadial.Instance.HandleDown();
        }

        public void OnFocusExit()
        {//オブジェクトに視線が外れたらゲージがいっぱいになった時のイベントを解除
            SelectionRadial.Instance.OnSelectionComplete -= OnSelectionComplete;
            SelectionRadial.Instance.HandleUp();
            SelectionRadial.Instance.Hide();

        }

        private void OnSelectionComplete()
        {
            //ここにゲージがいっぱいになったら実行される処理を書く。
            //SelectionRadial.Instance.OnSelectionComplete -= OnSelectionComplete;
            //OnSelectionCompleted.Invoke();

            //ゲージがいっぱいになったら青に変更
            SceneManager.LoadScene("game");
        }
    }

}