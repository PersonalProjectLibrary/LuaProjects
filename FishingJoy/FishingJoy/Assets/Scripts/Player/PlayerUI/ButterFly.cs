using UnityEngine;

/// <summary>
/// 散弹按钮
/// </summary>
public class ButterFly : GameSkill
{
    public GameObject uiView;

    public override void UseSkill()
    {
        if (canUse)
        {
            if (Gun.Instance.diamands <= reduceDiamands) return;
            Gun.Instance.CurrencyChange("Diamand", -reduceDiamands);

            Gun.Instance.Butterfly = true;
            canUse = false;
            cdSlider.transform.Find("Background").gameObject.SetActive(true);
            timeVal = 0;
            Invoke("CloseSkill", 8);
            uiView.SetActive(true);
        }
    }

    /// <summary>
    /// 关闭散弹效果,ScatterShot里委托执行
    /// </summary>
    public override void CloseSkill()
    {
        uiView.SetActive(false);
        Gun.Instance.Butterfly = false;
    }
}
