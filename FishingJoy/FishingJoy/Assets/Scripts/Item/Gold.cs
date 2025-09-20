using UnityEngine;

/// <summary>
/// 金币，钻石
/// </summary>
public class Gold : MonoBehaviour
{
    /// <summary>
    /// 物品类型
    /// </summary>
    public enum PlaceType
    {
        /// <summary>
        /// 捕鱼开出来的金币，要去的地方
        /// </summary>
        gold,
        /// <summary>
        /// 捕鱼开出来的钻石，要去的地方
        /// </summary>
        diamands,
        /// <summary>
        /// 宝箱开出来的金币，要去的地方
        /// </summary>
        imageGold,
        /// <summary>
        /// 宝箱开出来的钻石，要去的地方
        /// </summary>
        imageDiamands
    }
    public PlaceType thePlaceTo;

    private Transform playerTransform;
    public float moveSpeed = 3;
    public GameObject star2;

    private AudioSource audios;
    public AudioClip goldAudio;
    public AudioClip diamandsAudio;

    private float timeVal2;
    public float defineTime2;
    private float timeBecome;
    private float timeVal3;

    public bool bossPrize = false;
    private bool beginMove = false;

    private void Awake()
    {
        audios = GetComponent<AudioSource>();
        InitPlaceAndAudio();
        audios.Play();
    }

    /// <summary>
    /// 初始化：移动位置和配乐设置
    /// </summary>
    private void InitPlaceAndAudio()
    {
        switch (thePlaceTo)
        {
            case PlaceType.gold:
                playerTransform = Gun.Instance.goldPlace;
                audios.clip = goldAudio;
                break;
            case PlaceType.diamands:
                playerTransform = Gun.Instance.diamondsPlace;
                audios.clip = diamandsAudio;
                break;
            case PlaceType.imageGold:
                playerTransform = Gun.Instance.imageGoldPlace;
                audios.clip = goldAudio;
                break;
            case PlaceType.imageDiamands:
                playerTransform = Gun.Instance.imageDiamandsPlace;
                audios.clip = diamandsAudio;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (timeBecome >= 0.5f) beginMove = true;
        else timeBecome += Time.deltaTime;

        if (beginMove) GoldBeginMove();
        else GoldMoving();
    }

    /// <summary>
    /// 金币开始移动
    /// </summary>
    private void GoldBeginMove()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position, 1 / Vector3.Distance(transform.position, playerTransform.position) * Time.deltaTime * moveSpeed);

        if (thePlaceTo == PlaceType.imageDiamands || thePlaceTo == PlaceType.imageGold)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) <= 2) Destroy(gameObject);

            return;
        }
        if (transform.position == playerTransform.position) Destroy(gameObject);

        timeVal2 = InistStar(timeVal2, defineTime2, star2);
    }

    /// <summary>
    /// 金币在移动中
    /// </summary>
    private void GoldMoving()
    {
        transform.localScale += new Vector3(Time.deltaTime * 3, Time.deltaTime * 3, Time.deltaTime * 3);

        if (bossPrize)
        {
            if (timeVal3 <= 0.3f)
            {
                timeVal3 += Time.deltaTime;
                transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    /// <summary>
    /// 生成星星
    /// </summary>
    /// <param name="timeVals"></param>
    /// <param name="defineTimes"></param>
    /// <param name="stars"></param>
    /// <returns></returns>
    private float InistStar(float timeVals, float defineTimes, GameObject stars)
    {
        if (timeVals >= defineTimes)
        {
            Instantiate(stars, transform.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + Random.Range(-40f, 40f)));
            timeVals = 0;
        }
        else timeVals += Time.deltaTime;

        return timeVals;
    }
}
