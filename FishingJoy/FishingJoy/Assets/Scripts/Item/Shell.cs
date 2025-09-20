using UnityEngine;

/// <summary>
/// 贝壳
/// </summary>
public class Shell : MonoBehaviour
{
    //计时器
    private float rotateTime;
    private float timeVal = 0;//无敌状态计时器

    //属性
    public float moveSpeed = 5;

    //开关
    private bool isDeffend = true;
    private bool hasIce = false;

    //引用
    public GameObject card;
    private GameObject fire;
    private GameObject ice;
    private Animator iceAni;
    private Animator gameObjectAni;
    private SpriteRenderer sr;
    private float timeVals;

    void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        fire = transform.Find("Fire").gameObject;
        ice = transform.Find("Ice").gameObject;
        iceAni = ice.transform.GetComponent<Animator>();
        gameObjectAni = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 10);
    }

    void Update()
    {
        FreshShell();
    }
    /// <summary>
    /// 更新贝壳状态
    /// </summary>
    private void FreshShell()
    {
        if (timeVals >= 9) sr.color -= new Color(0, 0, 0, Time.deltaTime);
        else timeVals += Time.deltaTime;

        FiringShell();
        FreezeShell();

        if (Gun.Instance.Ice) return;

        ShellMove();
    }

    /// <summary>
    /// 冰冻效果
    /// </summary>
    private void FreezeShell()
    {
        if (Gun.Instance.Ice)
        {
            gameObjectAni.enabled = false;
            ice.SetActive(true);
            if (!hasIce)
            {
                iceAni.SetTrigger("Ice");
                hasIce = true;
            }
        }
        else
        {
            gameObjectAni.enabled = true;
            hasIce = false;
            ice.SetActive(false);
        }
    }
    /// <summary>
    /// 贝壳灼烧效果
    /// </summary>
    private void FiringShell()
    {
        if (Gun.Instance.Fire) fire.SetActive(true);
        else fire.SetActive(false);
    }
    /// <summary>
    /// 贝壳移动和旋转
    /// </summary>
    private void ShellMove()
    {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
        if (rotateTime >= 5)
        {
            transform.Rotate(transform.forward * Random.Range(0, 361), Space.World);
            rotateTime = 0;
        }
        else rotateTime += Time.deltaTime;
        if (timeVal < 1) timeVal += Time.deltaTime;
        else if (timeVal >= 1 && timeVal < 1.5)
        {
            timeVal += Time.deltaTime;
            isDeffend = false;
        }
        else if (timeVal >= 1.5)
        {
            isDeffend = true;
            timeVal = 0;
        }
    }

    /// <summary>
    /// 贝壳击中后显示buff：免费射击/金币加倍/连续射击
    /// 和子弹发生碰撞时，子弹身上的碰撞检测会调用该方法
    /// Collider.SendMessage("GetEffects");
    /// </summary>
    public void GetEffects()
    {
        if (isDeffend) return;//无敌状态
        else
        {
            int num = Random.Range(0, 3);
            switch (num)
            {
                case 0: CanShootForFree(); break;
                case 1: CanGetDoubleGold(); break;
                case 2: CanShootNoCD(); break;
                default: break;
            }
            GameObject go = Instantiate(card, transform.position, card.transform.rotation);
            go.GetComponent<Card>().num = num;
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// 免费射击
    /// </summary>
    private void CanShootForFree()
    {
        Gun.Instance.CanShootForFree = true;
        Invoke("CantShootForFree", 5);
    }
    public void CantShootForFree() { Gun.Instance.CanShootForFree = false; }
    /// <summary>
    /// 双倍金币
    /// </summary>
    private void CanGetDoubleGold()
    {
        Gun.Instance.CanGetDoubleGold = true;
        Invoke("CantGetDoubleGold", 5);
    }
    public void CantGetDoubleGold() { Gun.Instance.CanGetDoubleGold = false; }
    /// <summary>
    /// 连续射击
    /// </summary>
    private void CanShootNoCD()
    {
        Gun.Instance.CanShootNoCD = true;
        Invoke("CantShootNoCD", 5);
    }
    public void CantShootNoCD() { Gun.Instance.CanShootNoCD = false; }
}
