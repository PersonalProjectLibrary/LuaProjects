using UnityEngine;

/// <summary>
/// 水纹播放的特效,
/// 一些图片可能因导入损坏等原因无法做成动画，用该方法实现处动画效果
/// </summary>
public class Water : MonoBehaviour
{
    private int count = 0;
    public Sprite[] pictures;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ExchangeImg();
    }
    /// <summary>
    /// 轮播水纹图片组
    /// </summary>
    private void ExchangeImg()
    {
        sr.sprite = pictures[count];
        count++;
        if (count == pictures.Length) count = 0;
    }
}
