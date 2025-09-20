using UnityEngine;

public class Bullect : MonoBehaviour
{
    public GameObject net;
    public GameObject star;
    public GameObject star1;
    public GameObject star2;
    public GameObject explosions;
    public Transform CreatePos;

    public int level;
    public float moveSpeed;
    private float timeVal;
    public float defineTime;
    private float timeVal1;
    public float defineTime1;
    private float timeVal2;
    public float defineTime2;
    public float attackValue;

    void Update()
    {
        UpdateBullect();
    }

    private void UpdateBullect()
    {
        timeVal = InistStar(timeVal, defineTime, star);
        timeVal1 = InistStar(timeVal1, defineTime1, star1);
        timeVal2 = InistStar(timeVal2, defineTime2, star2);
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// 刷新炮弹的尾翼/星星
    /// </summary>
    /// <param name="timeVals"></param>
    /// <param name="defineTimes"></param>
    /// <param name="stars"></param>
    /// <returns></returns>
    private float InistStar(float timeVals, float defineTimes, GameObject stars)
    {
        if (timeVals >= defineTimes)
        {
            Instantiate(stars, CreatePos.transform.position, Quaternion.Euler(CreatePos.transform.eulerAngles.x, CreatePos.transform.eulerAngles.y, CreatePos.transform.eulerAngles.z + Random.Range(-40f, 40f)));
            timeVals = 0;
        }
        else timeVals += Time.deltaTime;

        return timeVals;
    }

    #region 碰撞到物体
    /// <summary>
    /// 进行碰撞检测
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Qipao") CollisionBubble();
        else if (other.tag == "missile") CollisionMissile(other);
        else if (other.tag == "Shell") CollisionShell(other);
        else if (other.tag == "Wall") CollisionWall(other);
        else if (other.tag == "fish" || other.tag == "boss") CollisionFish(other);
    }
    /// <summary>
    /// 击中气泡
    /// </summary>
    private void CollisionBubble()
    {
        GameObject go = Instantiate(net, transform.position + new Vector3(0, 1, 0), transform.rotation);
        go.transform.localScale = new Vector3(level, level, level);
        Instantiate(explosions, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    /// <summary>
    /// 击中美人鱼
    /// 会调用美人鱼身上Missile里的Lucky方法
    /// </summary>
    /// <param name="co"></param>
    private void CollisionMissile(Collider co)
    {
        co.SendMessage("Lucky", attackValue);
        GameObject go = Instantiate(net, transform.position + new Vector3(0, 1, 0), transform.rotation);
        go.transform.localScale = new Vector3(level, level, level);
        Instantiate(explosions, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    /// <summary>
    /// 击中贝壳
    /// 会调用贝壳身上的GetEffects方法
    /// </summary>
    /// <param name="co"></param>
    private void CollisionShell(Collider co)
    {
        co.SendMessage("GetEffects");
        GameObject go = Instantiate(net, transform.position + new Vector3(0, 1, 0), transform.rotation);
        go.transform.localScale = new Vector3(level, level, level);
        Instantiate(explosions, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    /// <summary>
    /// 击中墙
    /// </summary>
    /// <param name="co"></param>
    private void CollisionWall(Collider co)
    {
        float angleValue = Vector3.Angle(transform.up, co.transform.up);
        if (angleValue < 90)
            transform.eulerAngles += new Vector3(0, 0, 2 * angleValue);
        else if (Vector3.Angle(transform.up, co.transform.up) > 90)
            transform.eulerAngles -= new Vector3(0, 0, 360 - 2 * angleValue);
        else
            transform.eulerAngles += new Vector3(0, 0, 180);
    }
    /// <summary>
    /// 击中普通鱼或boss鱼
    /// 会调用Boss和Fish脚本里的TakeDamage方法
    /// </summary>
    /// <param name="co"></param>
    private void CollisionFish(Collider co)
    {
        co.SendMessage("TakeDamage", attackValue);
        GameObject go = Instantiate(net, transform.position + new Vector3(0, 1, 0), transform.rotation);
        go.transform.localScale = new Vector3(level, level, level);
        Instantiate(explosions, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    #endregion
}
