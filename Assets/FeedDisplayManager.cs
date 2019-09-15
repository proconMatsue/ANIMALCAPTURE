using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedDisplayManager : MonoBehaviour
{
    //テキストを表示するためのオブジェクト
    [SerializeField] private GameObject textObject;

    //餌を管理するマネージャのオブジェクト
    [SerializeField] private GameObject feedInventoryManager;

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
