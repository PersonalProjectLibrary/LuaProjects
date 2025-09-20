using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏技能父类
/// </summary>
public class GameSkill : MonoBehaviour
{
    public Slider cdSlider;

    protected Button but;
    protected bool canUse = true;
    protected int reduceDiamands = 10;
    protected float timeVal = 15;
    protected float totalTime = 15;

    public virtual void Awake()
    {
        but = transform.GetComponent<Button>();
        but.onClick.AddListener(UseSkill);
    }

    public virtual void Start()
    {
        reduceDiamands = 10;
    }

    public virtual void Update()
    {
        MonitorState();
    }

    /// <summary>
    /// 监测技能状态
    /// </summary>
    public virtual void MonitorState()
    {
        if (timeVal >= 15) timeVal = 15;
        cdSlider.value = timeVal / totalTime;

        if (timeVal >= 15)
        {
            canUse = true;
            cdSlider.transform.Find("Background").gameObject.SetActive(false);
        }
        else timeVal += Time.deltaTime;
    }

    /// <summary>
    /// 使用技能
    /// </summary>
    public virtual void UseSkill() { }

    /// <summary>
    /// 关闭技能
    /// </summary>
    public virtual void CloseSkill() { }
}
