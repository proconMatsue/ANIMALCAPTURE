using UnityEngine;

/// <summary>
/// 各オブジェクトの注視後のブロックの色を管理する
/// </summary>
public class ColorChangeDirector : MonoBehaviour
{
    /// <summary>
    /// 注視されたオブジェクトの色をランダムに変える
    /// </summary>
    /// <param name="renderer">特に気にしなくてよい</param>
    public void ColorChangeRandam(Renderer renderer)
    {
        renderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    /// <summary>
    /// 注視されたオブジェクトの色を緑色に変える
    /// </summary>
    /// <param name="renderer">特に気にしなくてよい</param>
    public void ColorChangeGreen(Renderer renderer)
    {
        renderer.material.color = Color.green;
    }
}
