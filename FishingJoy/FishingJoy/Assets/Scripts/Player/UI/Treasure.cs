using UnityEngine;
using UnityEngine.UI;
using XLua;

/// <summary>
/// 宝藏
/// </summary>
[Hotfix]
public class Treasure : MonoBehaviour
{
    private Button but;
    private Image img;

    public GameObject gold;
    public GameObject diamonds;
    public Slider cdSlider;
    public GameObject cdView;
    public Transform canvas;
    private bool isToCoolDown;


    private void Awake()
    {
        but = GetComponent<Button>();
        but.onClick.AddListener(OpenTreasour);
        img = GetComponent<Image>();
    }

    /// <summary>
    /// 打开宝箱
    /// </summary>
    void OpenTreasour()
    {
        if (img.color.a != 1) return;//图片不完全显示时，即还处于冷却阶段，不能执行任何操作

        cdView.SetActive(true);
        cdSlider.gameObject.SetActive(true);
        cdSlider.value = 0;

        Gun.Instance.CurrencyChange(CurrencyType.Gold, Random.Range(100, 200));
        Gun.Instance.CurrencyChange(CurrencyType.Diamond, Random.Range(10, 50));
        CreatePrize();
        isToCoolDown = true;
    }

    /// <summary>
    /// 创建奖品：5个金币与5个钻石
    /// </summary>
    [LuaCallCSharp]
    private void CreatePrize()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(gold, transform.position + new Vector3(-10f + i * 30, 0, 0), transform.rotation);
            go.transform.SetParent(canvas);
            GameObject go1 = Instantiate(diamonds, transform.position + new Vector3(0, 30, 0) + new Vector3(-10f + i * 30, 0, 0), transform.rotation);
            go1.transform.SetParent(canvas);
        }
    }

    void Update()
    {
        RefreshImg();
    }

    /// <summary>
    /// 监测状态，更新宝藏状态
    /// </summary>
    private void RefreshImg()
    {
        if (isToCoolDown)
        {
            img.color -= new Color(0, 0, 0, Time.deltaTime * 10);
            if (img.color.a <= 0.2)//宝藏图片不显示
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
                isToCoolDown = false;//设置为不能获取宝藏
            }
        }
        else
        {
            img.color += new Color(0, 0, 0, Time.deltaTime * 0.01f);
            cdSlider.value = img.color.a;
            if (img.color.a >= 0.9) //宝藏图片完全显示
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
                //冷却结束
                cdView.SetActive(false);
                cdSlider.gameObject.SetActive(false);
            }
        }
    }
}
