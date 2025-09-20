using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 枪
/// </summary>
public class Gun : MonoBehaviour
{
    [Header("枪：")]
    public int level = 1;
    public int gunLevel = 1;//初始枪的等级1
    public float attackCD = 1;
    private float GunCD = 4;
    private float rotateSpeed = 5f;
    public GameObject[] Bullects;//3种枪
    public GunChange[] gunChange;//升级/降级枪的等级
    public AudioClip[] bullectAudios;//射击时的音乐
    private AudioSource bullectAudio;

    [Header("其他对象：")]
    public GameObject net;//渔网
    public int gold = 100;//初始金币1000
    public int diamands = 50;//初始钻石1000
    public Text goldText;//金币显示文本
    public Text diamandsText;//钻石显示文本
    public Transform attackPos;//攻击位置
    public Transform goldPlace;//捕鱼生成的金币移动位置
    public Transform diamondsPlace;//捕鱼生成的钻石移动位置
    public Transform imageGoldPlace;//宝箱生成的金币移动位置
    public Transform imageDiamandsPlace;//宝箱生成的钻石移动位置

    [Header("开关：")]
    public bool attack = false;
    public bool Fire = false;
    public bool Ice = false;
    public bool Butterfly = false;
    public bool bossAttack = false;
    public bool changeAudio;
    public bool canChangeGun = true;

    private bool canShootNoCD = false;
    /// <summary>
    /// 连续射击
    /// </summary>
    public bool CanShootNoCD
    {
        get { return canShootNoCD; }
        set { canShootNoCD = value; }
    }

    private bool canShootForFree = false;
    /// <summary>
    /// 是否免费射击
    /// </summary>
    public bool CanShootForFree
    {
        get { return canShootForFree; }
        set { canShootForFree = value; }
    }

    private bool canGetDoubleGold = false;
    /// <summary>
    /// 是否获得双倍金币
    /// </summary>
    public bool CanGetDoubleGold
    {
        get { return canGetDoubleGold; }
        set { canGetDoubleGold = value; }
    }

    private static Gun instance;
    public static Gun Instance
    {
        get { return instance; }

        set { instance = value; }
    }

    private void Awake()
    {
        instance = this;
        gold = 1000;
        diamands = 1000;
        level = 2;
        bullectAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        goldText.text = gold.ToString();
        diamandsText.text = diamands.ToString();

        RotateGun();
        MonitorGunState();
    }

    #region 枪的方法
    /// <summary>
    /// 监测枪的状态
    /// </summary>
    private void MonitorGunState()
    {
        if (GunCD <= 0)
        {
            canChangeGun = true;
            GunCD = 4;
        }
        else GunCD -= Time.deltaTime;

        if (canShootNoCD)
        {
            /*Debug.Log("连续射击");*/
            Attack();
            attack = true;
            return;
        }
        /* else { Debug.Log("不连续射击"); }*/

        if (attackCD >= 1 - gunLevel * 0.3)
        {
            Attack();
            attack = true;
        }
        else attackCD += Time.deltaTime;
    }

    ///<summary>
    /// 枪支旋转
    /// </summary>
    private void RotateGun()
    {
        float h = Input.GetAxisRaw("Mouse Y");
        float v = Input.GetAxisRaw("Mouse X");
        transform.Rotate(-Vector3.forward * v * rotateSpeed);
        transform.Rotate(Vector3.forward * h * rotateSpeed);
        ClampAngle();
    }
    /// <summary>
    /// 设置枪的旋转角度
    /// </summary>
    private void ClampAngle()
    {
        float y = transform.eulerAngles.y;
        if (y <= 35) y = 35;
        else if (y >= 150) y = 150;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z);
    }

    /// <summary>
    /// 切枪：Up 升级枪，Down：降级枪
    /// </summary>
    /// <param name="change"></param>
    public void SwitchGun(string change)
    {
        if (change == "Up") gunLevel += 1;
        else if (change == "Down") gunLevel -= 1;

        if (gunLevel == 4) gunLevel = 1;
        else if (gunLevel == 0) gunLevel = 3;

        gunChange[0].ToGray();
        gunChange[1].ToGray();
        canChangeGun = false;
    }

    /// <summary>
    /// 进行捕鱼
    /// </summary>
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bullectAudio.clip = bullectAudios[gunLevel - 1];
            bullectAudio.Play();

            if (Butterfly)
            {
                Instantiate(Bullects[gunLevel - 1], attackPos.position, attackPos.rotation * Quaternion.Euler(0, 0, 20));
                Instantiate(Bullects[gunLevel - 1], attackPos.position, attackPos.rotation * Quaternion.Euler(0, 0, -20));
            }

            Instantiate(Bullects[gunLevel - 1], attackPos.position, attackPos.rotation);

            if (!canShootForFree)
            {
                CurrencyChange("Gold", -1 - (gunLevel - 1) * 2);
                /*Debug.Log("花钱射击");*/
            }
            /* else { Debug.Log("免费射击"); }*/

            attackCD = 0;
            attack = false;
        }
    }
    #endregion

    /// <summary>
    /// 允许boss攻击枪架
    /// </summary>
    public void BossAttack() { bossAttack = true; }
    /// <summary>
    /// 货币增减：
    /// type=="Diamand" 钻石增减
    /// type=="Gold"    金钱增减
    /// </summary>
    /// <param name="type"></param>
    /// <param name="num"></param>
    public void CurrencyChange(string type, int num)
    {
        if (type == "Diamand") diamands += num;
        else if (type == "Gold")
        {
            if (canGetDoubleGold && num > 0)
            {
                num *= 2;
                /*Debug.Log("金币加倍");*/
            }
            /* else { Debug.Log("正常金币");}*/
            gold += num;
        }
    }
}
