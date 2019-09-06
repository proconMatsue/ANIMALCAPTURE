using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class feedUI : MonoBehaviour
{
    /// <summary>
    /// 所持する餌を表示する
    /// </summary>
    private void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        Text text = this.gameObject.GetComponent<Text>();
        Generator generator = new Generator();
        text.text = "どんぐり : " + generator.HoldFeedNum(0) + " " + "さんま : " + generator.HoldFeedNum(1) + " "
            + "ニンジン : " + generator.HoldFeedNum(2) + " " + "肉 : " + generator.HoldFeedNum(3) + " ";
    }
}
