using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 切枪的按钮
/// </summary>
public class GunChange : MonoBehaviour
{
    /// <summary>
    /// 是否是升级按钮：“+”
    /// 反之是降级按钮：“-”
    /// </summary>
    public bool add;
    private Button button;
    private Image image;

    /// <summary>
    /// 切枪按钮图片：
    /// 0.+   1.灰色的+  2.-  3.灰色的-
    /// </summary>
    public Sprite[] buttonSprites;

    void Start()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(ChangeGunLevel);
        image = GetComponent<Image>();
    }

    void Update()
    {
        MonitorChangeGun();
    }

    /// <summary>
    /// 监测切枪状态，
    /// 允许切枪时，自动换成能点击的按钮图片
    /// </summary>
    private void MonitorChangeGun()
    {
        if (Gun.Instance.canChangeGun)
        {
            if (add) image.sprite = buttonSprites[0];
            else image.sprite = buttonSprites[2];
        }
    }

    /// <summary>
    /// 更改枪等级
    /// </summary>
    public void ChangeGunLevel()
    {
        if (Gun.Instance.canChangeGun)
        {
            if (add) Gun.Instance.SwitchGun("Up");
            else Gun.Instance.SwitchGun("Down");
        }
    }

    /// <summary>
    /// 使切枪按钮图片，切换成：灰色不能点击状态的图片
    /// </summary>
    public void ToGray()
    {
        if (add) image.sprite = buttonSprites[1];
        else image.sprite = buttonSprites[3];
    }
}
