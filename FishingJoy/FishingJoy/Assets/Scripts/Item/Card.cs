using UnityEngine;

/// <summary>
/// 铺到贝壳的道具卡
/// </summary>
public class Card : MonoBehaviour
{
    public int num;
    /// <summary>
    /// 三种道具卡
    /// </summary>
    public Sprite[] cards;
    private SpriteRenderer sr;
    private AudioSource audios;

    void Start()
    {
        Destroy(this.gameObject, 1);
        audios = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = cards[num];
        audios.Play();
    }
}
