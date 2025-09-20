using UnityEngine;

/// <summary>
/// 普通鱼的类
/// </summary>
public class Fish : MonoBehaviour
{
    //属性
    public int hp = 5;
    public int GetCold = 10;
    public float moveSpeed = 2;
    public int GetDiamands = 10;

    //计时器
    private float timeVal;
    private float rotateTime;

    //引用
    public GameObject pao;
    public GameObject gold;
    public GameObject diamands;
    private GameObject fire;
    private GameObject ice;
    private Animator iceAni;
    private Animator gameObjectAni;
    private SpriteRenderer sr;

    //开关
    public bool isnet;
    private bool hasIce = false;
    private bool isDead = false;
    public bool cantRotate = false;//控制鱼群是否可以旋转，CreateFish里也有用到

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        fire = transform.Find("Fire").gameObject;
        ice = transform.Find("Ice").gameObject;
        iceAni = ice.transform.GetComponent<Animator>();
        gameObjectAni = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 20);
    }

    void Update()
    {
        if (timeVal >= 14 || isDead) sr.color -= new Color(0, 0, 0, Time.deltaTime);
        else timeVal += Time.deltaTime;

        if (isDead) return;

        FreezeFish();
        FiringFish();

        if (Gun.Instance.Ice) return;
        NetFish();
        FishMove();
    }

    /// <summary>
    /// 冰冻
    /// </summary>
    private void FreezeFish()
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
    /// 灼烧
    /// </summary>
    private void FiringFish()
    {
        if (Gun.Instance.Fire) fire.SetActive(true);
        else fire.SetActive(false);
    }
    /// <summary>
    /// 网住鱼
    /// </summary>
    private void NetFish()
    {
        if (isnet)
        {
            Invoke("DestoryNet", 0.5f);
            return;
        }
    }
    /// <summary>
    /// 鱼的移动
    /// </summary>
    public void FishMove()
    {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
        if (cantRotate) return;
        if (rotateTime >= 5)
        {
            transform.Rotate(transform.forward * Random.Range(0, 361), Space.World);
            rotateTime = 0;
        }
        else rotateTime += Time.deltaTime;
    }

    /// <summary>
    /// 解除网，NetFish里委托执行
    /// </summary>
    public void DestoryNet()
    {
        if (isnet) isnet = false;
    }

    /// <summary>
    /// 承受伤害
    /// Bullet脚本里碰撞检测到和Fish碰撞时执行：
    /// Collider.SendMessage("TakeDamage", attackValue);
    /// </summary>
    /// <param name="attackValue"></param>
    public void TakeDamage(int attackValue)
    {
        if (Gun.Instance.Fire) attackValue *= 2;
        hp -= attackValue;
        if (hp <= 0)
        {
            isDead = true;
            for (int i = 0; i < 9; i++)
            {
                Instantiate(pao, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 45 * i, 0)));
            }
            gameObjectAni.SetTrigger("Die");
            Invoke("Prize", 0.7f);
        }
    }

    /// <summary>
    /// 获得奖励，TakeDamage里委托执行
    /// </summary>
    private void Prize()
    {
        Gun.Instance.CurrencyChange("Gold", GetCold);
        if (GetDiamands != 0)
        {
            Gun.Instance.CurrencyChange("Diamand", GetDiamands);
            Instantiate(diamands, transform.position, transform.rotation);
        }

        Instantiate(gold, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
