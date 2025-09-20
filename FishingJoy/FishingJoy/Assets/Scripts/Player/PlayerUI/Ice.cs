using UnityEngine;

/// <summary>
/// 冰冻
/// </summary>
public class Ice : GameSkill
{
    private AudioSource fireAudio;

    public override void Awake()
    {
        base.Awake();
        fireAudio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 执行冰冻效果
    /// </summary>
    public override void UseSkill()
    {
        if (canUse)
        {
            if (!Gun.Instance.Fire && !Gun.Instance.Ice)
            {

                if (Gun.Instance.diamands <= reduceDiamands) return;
                if (fireAudio.isPlaying) return;
                fireAudio.Play();
                Gun.Instance.CurrencyChange("Diamand", -reduceDiamands);
                Gun.Instance.Ice = true;
                canUse = false;
                cdSlider.transform.Find("Background").gameObject.SetActive(true);
                timeVal = 0;
                Invoke("CloseSkill", 4);
            }
        }
    }

    /// <summary>
    /// 关闭冰冻效果,Ice里委托执行
    /// </summary>
    public override void CloseSkill()
    {
        Gun.Instance.Ice = false;
    }
}
