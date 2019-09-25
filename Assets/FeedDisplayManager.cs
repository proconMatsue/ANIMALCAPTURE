using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedDisplayManager : MonoBehaviour
{
    [SerializeField, Tooltip("エサの所持数を表示するためのオブジェクト")]
    private GameObject textObject;

    [SerializeField, Tooltip("エサを管理するマネージャのオブジェクト")]
    private GameObject feedInventoryManager;

    /// <summary>
    /// 毎フレーム餌を表示する
    /// </summary>
    private void Update()
    {
        //FeedInventoryManagerにアタッチされているFeedInventory.csを取得
        FeedInventory feedInventory = feedInventoryManager.GetComponent<FeedInventory>();
        //取得したコンポーネントから表示するための関数を呼び出し
        feedInventory.FeedDisplay(textObject);
    }
}
