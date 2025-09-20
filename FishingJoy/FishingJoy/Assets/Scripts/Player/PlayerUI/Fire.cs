/// <summary>
/// 灼烧
/// </summary>
public class Fire : GameSkill
{
    /// <summary>
    /// 执行灼烧效果
    /// </summary>
    public override void UseSkill()
    {
        if (canUse)
        {
            if (!Gun.Instance.Ice && !Gun.Instance.Fire)
            {
                if (Gun.Instance.diamands <= reduceDiamands) return;
                Gun.Instance.CurrencyChange("Diamand", -reduceDiamands);
                Gun.Instance.Fire = true;
                canUse = false;
                cdSlider.transform.Find("Background").gameObject.SetActive(true);
                timeVal = 0;
                Invoke("CloseSkill", 6);
            }
        }
    }

    /// <summary>
    /// 结束灼烧效果，Firing里委托调用
    /// </summary>
    public override void CloseSkill()
    {
        Gun.Instance.Fire = false;
    }
}
