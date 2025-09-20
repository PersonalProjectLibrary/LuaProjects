using UnityEngine;

/// <summary>
/// 美人鱼
/// </summary>
public class Missile : MonoBehaviour
{
    //属性
    public int hp = 15;
    public int GetGold = 10;
    public float moveSpeed = 5;

    //引用
    public GameObject gold;
    private GameObject fire;
    private GameObject ice;
    private Animator iceAni;
    private Animator gameObjectAni;
    public GameObject deadEeffect;
    private SpriteRenderer sr;

    //计时器
    private float timeVal;
    private float rotateTime;
    private bool hasIce = false;

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
        Destroy(this.gameObject, 8);
    }

    void Update()
    {
        if (timeVal >= 7) sr.color -= new Color(0, 0, 0, Time.deltaTime);
        else timeVal += Time.deltaTime;

        Freeze();
        Firing();

        if (Gun.Instance.Ice) return;
        FishMove();
    }
    /// <summary>
    /// 灼烧效果
    /// </summary>
    private void Firing()
    {
        if (Gun.Instance.Fire) fire.SetActive(true);
        else fire.SetActive(false);
    }
    /// <summary>
    /// 冰冻效果
    /// </summary>
    private void Freeze()
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
    /// 美人鱼移动旋转
    /// </summary>
    private void FishMove()
    {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
        if (rotateTime >= 5)
        {
            transform.Rotate(transform.forward * Random.Range(0, 361), Space.World);
            rotateTime = 0;
        }
        else rotateTime += Time.deltaTime;
    }

    /// <summary>
    /// 攻击到美人鱼
    /// 和子弹碰撞，子弹身上的Bullect碰撞检测时会调用：
    /// Collider.SendMessage("Lucky", attackValue);
    /// </summary>
    /// <param name="attckValue"></param>
    public void Lucky(int attckValue)
    {
        Gun.Instance.CurrencyChange("Gold", GetGold);
        Instantiate(gold, transform.position, transform.rotation);
        if (Gun.Instance.Fire) attckValue *= 2;
        hp -= attckValue;
        if (hp <= 0)
        {
            Instantiate(deadEeffect, transform.position, transform.rotation);
            gameObjectAni.SetTrigger("Die");
            Invoke("Prize", 0.7f);
        }
    }

    /// <summary>
    /// 获得奖励，Lucky里委托调用
    /// </summary>
    private void Prize()
    {
        Gun.Instance.CurrencyChange("Gold", GetGold * 10);
        for (int i = 0; i < 5; i++)
        {
            Instantiate(gold, transform.position + new Vector3(-5f + i, 0, 0), transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
