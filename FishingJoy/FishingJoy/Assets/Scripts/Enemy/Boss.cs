using UnityEngine;

/// <summary>
/// boss脚本
/// </summary>
public class Boss : MonoBehaviour
{
    public int hp = 50;
    public int GetGold = 10;
    public int GetDiamands = 10;
    public float moveSpeed = 2;

    protected bool hasIce;
    protected bool isAttack;
    private float timeVal;
    private float rotateTime;

    public GameObject gold;
    public GameObject diamands;
    public GameObject deadEeffect;

    protected int m_reduceGold;
    protected int m_reduceDiamond;
    protected GameObject fire;
    protected GameObject ice;
    protected Animator iceAni;
    protected Animator gameObjectAni;
    protected AudioSource bossAudio;
    protected Transform playerTransform;

    void Start()
    {
        InitializeBoss();
        m_reduceGold = 10;
        m_reduceDiamond = 0;
    }

    public void InitializeBoss()
    {
        fire = transform.Find("Fire").gameObject;
        ice = transform.Find("Ice").gameObject;
        iceAni = ice.transform.GetComponent<Animator>();
        gameObjectAni = GetComponent<Animator>();
        bossAudio = GetComponent<AudioSource>();
        playerTransform = Gun.Instance.transform;
    }

    public virtual void Update()
    {
        MonitorBoss();
    }

    /// <summary>
    /// 检测Boss状态
    /// </summary>
    public void MonitorBoss()
    {
        BossFreeze();
        BossFiring();

        if (Gun.Instance.Ice) return;

        BossAction();
    }

    /// <summary>
    /// 冰冻效果
    /// </summary>
    public void BossFreeze()
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
    /// 灼烧效果
    /// </summary>
    public void BossFiring()
    {
        if (Gun.Instance.Fire) fire.SetActive(true);
        else fire.SetActive(false);


    }

    /// <summary>
    /// Boss的行为
    /// </summary>
    public virtual void BossAction()
    {
        BossAttackPlayer(m_reduceGold, m_reduceDiamond);
        if (!isAttack) BossFishMove();
    }

    /// <summary>
    /// Boss移动
    /// </summary>
    public void BossFishMove()
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
    /// 承受伤害，子弹碰撞检测时会调用：
    /// Collider.SendMessage("TakeDamage", attackValue);
    /// </summary>
    /// <param name="attackValue"></param>
    public virtual void TakeDamage(int attackValue)
    {
        if (Gun.Instance.Fire) attackValue *= 2;

        hp -= attackValue;
        if (hp <= 0)
        {
            Instantiate(deadEeffect, transform.position, transform.rotation);
            Gun.Instance.CurrencyChange("Gold", GetGold * 10);
            Gun.Instance.CurrencyChange("Diamand", GetDiamands * 10);
            //穿插着生成一圈金币和钻石：18 + 36 * (i - 1)，36 + 36 * (i - 1)
            for (int i = 0; i < 11; i++)
            {
                GameObject itemGo = Instantiate(gold, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 18 + 36 * (i - 1), 0)));
                itemGo.GetComponent<Gold>().bossPrize = true;
            }
            for (int i = 0; i < 11; i++)
            {
                GameObject itemGo = Instantiate(diamands, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 36 + 36 * (i - 1), 0)));
                itemGo.GetComponent<Gold>().bossPrize = true;
            }
            //Debug.Log("TakeDamage");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Boss攻击
    /// </summary>
    /// <param name="reduceGold"></param>
    /// <param name="reduceDiamond"></param>
    public void BossAttackPlayer(int reduceGold, int reduceDiamond)
    {
        if (timeVal > 20)
        {
            transform.LookAt(playerTransform);//要正对着玩家
            transform.eulerAngles += new Vector3(90, -90, 0);

            isAttack = true;
            timeVal = 0;
        }
        else timeVal += Time.deltaTime;

        if (isAttack)
        {
            gameObjectAni.SetBool("isAttack", true);//播放攻击动画
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, 1 / Vector3.Distance(transform.position, playerTransform.position) * Time.deltaTime * moveSpeed);
            //boss和玩家距离小于4时，攻击成功
            if (Vector3.Distance(transform.position, playerTransform.position) <= 4)
            {
                if (reduceGold != 0) Gun.Instance.CurrencyChange("Gold", reduceGold);

                if (reduceDiamond != 0) Gun.Instance.CurrencyChange("Diamand", reduceDiamond);

                gameObjectAni.SetBool("isAttack", false);//切回idle状态
                isAttack = false;
                Gun.Instance.BossAttack();
                rotateTime = 0;
                Invoke("ReturnAngle", 4);
            }
        }
    }

    /// <summary>
    /// 角度回正，保证行进方向不会乱掉
    /// Attack里委托执行
    /// </summary>
    public void ReturnAngle()
    {
        transform.eulerAngles = new Vector3(90, 0, 0);
    }
}
