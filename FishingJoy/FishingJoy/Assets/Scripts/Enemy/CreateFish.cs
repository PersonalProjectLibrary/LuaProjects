using UnityEngine;

/// <summary>
/// 产鱼器：x(-26,26）z(-16,16)
/// </summary>

public class CreateFish : MonoBehaviour
{
    private int num;
    private int ItemNum;
    private int placeNum;//鱼群生成位置
    private int CreateMorden;//产生鱼群的模式
    private float timeVals = 0;
    private float ItemtimeVal = 0;
    private float createManyFish;

    /// <summary>
    /// 7种鱼，3种普通鱼，3种2级鱼，1种3级鱼
    /// </summary>
    public GameObject[] fishList;
    /// <summary>
    /// 道具列表：
    /// 贝壳，2种美人鱼，气泡
    /// </summary>
    public GameObject[] item;
    /// <summary>
    /// 三种boss鱼
    /// </summary>
    public GameObject[] bosses;
    /// <summary>
    /// 鱼的两个生成点
    /// </summary>
    public Transform[] CreateFishPlace;

    void Update()
    {
        CreateALotOfFish();
        CreateSingleFish();
    }

    /// <summary>
    /// 单个鱼类生成
    /// </summary>
    private void CreateSingleFish()
    {
        if (ItemtimeVal >= 0.5)
        {
            num = Random.Range(0, 4);
            ItemNum = Random.Range(1, 101);

            if (ItemNum < 20) CreateBubble();
            if (ItemNum <= 42) CreateFirstFish();
            else if (ItemNum >= 43 && ItemNum < 72) CreateSecondFish();
            else if (ItemNum >= 73 && ItemNum < 84) CreateThirdFish();
            else if (ItemNum >= 84 && ItemNum < 86) CreateGameObject(bosses[1]);//第二种boss鱼
            else if (ItemNum >= 94 && ItemNum <= 98) CreateGameObject(item[1]);//第一种美人鱼5%，95-98
            else if (ItemNum > 98 && ItemNum < 100) CreateFirstBoss();
            else CreateSecondBoss();
            ItemtimeVal = 0;
        }
        else ItemtimeVal += Time.deltaTime;
    }
    /// <summary>
    /// 生成鱼群
    /// </summary>
    private void CreateALotOfFish()
    {
        if (createManyFish >= 15)//时间大于15生成鱼群
        {
            if (CreateMorden == 0 || CreateMorden == 1) CreateFirstFishes();
            else if (CreateMorden == 2) CreateSecondFishes();
        }
        else//时间累加
        {
            createManyFish += Time.deltaTime;
            placeNum = Random.Range(0, 2);
            CreateMorden = Random.Range(0, 3);
        }
    }

    /// <summary>
    /// 随机位置
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private Vector3 RandomPos(int num)
    {
        Vector3 Vpositon = new Vector3();

        switch (num)
        {
            case 0: Vpositon = new Vector3(-24, 1, Random.Range(-14f, 14f)); break;//-30  -  30
            case 1: Vpositon = new Vector3(Random.Range(-24f, 24f), 1, 14); break;//60 - 120
            case 2: Vpositon = new Vector3(24, 1, Random.Range(-14f, 14f)); break;//150-210
            case 3: Vpositon = new Vector3(Random.Range(-24f, 24f), 1, -14); break;//-60-  -120
            default: break;
        }
        return Vpositon;
    }
    /// <summary>
    /// 随机角度
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private Vector3 RandomAngle(int num)
    {
        Vector3 Vangle = new Vector3();
        switch (num)
        {
            case 0: Vangle = new Vector3(0, Random.Range(-30f, 30f), 0); break;//-30  -  30
            case 1: Vangle = new Vector3(0, Random.Range(60f, 120f), 0); break;//60 - 120
            case 2: Vangle = new Vector3(0, Random.Range(150f, 210f), 0); break;//150-210
            case 3: Vangle = new Vector3(0, Random.Range(-60f, -120f), 0); break;//-60-  -120
            default: break;
        }
        return Vangle;
    }
    /// <summary>
    /// 创建游戏对象
    /// </summary>
    /// <param name="go"></param>
    private void CreateGameObject(GameObject go)
    {
        Instantiate(go, RandomPos(num), Quaternion.Euler(RandomAngle(num) + go.transform.eulerAngles));
    }

    #region CreateALotFishMethon
    private void CreateFirstFishes()
    {
        createManyFish += Time.deltaTime;
        if (createManyFish >= 18) createManyFish = 0;

        if (timeVals >= 0.2f)//生成一片鱼状的鱼群
        {
            int num = Random.Range(0, 2);
            GameObject itemGo = Instantiate(fishList[num], CreateFishPlace[placeNum].position + new Vector3(0, 0, Random.Range(-2, 2)), CreateFishPlace[placeNum].rotation);
            itemGo.GetComponent<Fish>().cantRotate = true;
            timeVals = 0;
        }
        else timeVals += Time.deltaTime;//18秒内，每0.2秒内生成一条小鱼，直到累加到18秒结束鱼群生成
    }
    private void CreateSecondFishes()
    {
        GameObject go = fishList[Random.Range(2, fishList.Length)];
        for (int i = 0; i < 11; i++)//生成放射状鱼群
        {
            GameObject itemGo = Instantiate(go, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 45 * i, 0)));
            itemGo.GetComponent<Fish>().cantRotate = true;
        }
        createManyFish = 0;
    }
    #endregion

    #region CreateSigleFishMethon
    /// <summary>
    /// 第5类的鱼和气泡
    /// </summary>
    private void CreateBubble()
    {
        CreateGameObject(item[3]);
        CreateGameObject(fishList[6]);
    }

    /// <summary>
    /// 第1类鱼和贝壳42% 42
    /// </summary>
    private void CreateFirstFish()
    {
        CreateGameObject(fishList[0]);
        CreateGameObject(item[0]);
        CreateGameObject(fishList[3]);
        CreateGameObject(item[0]);
    }

    /// <summary>
    /// 第二种鱼30% 43-72
    /// </summary>
    private void CreateSecondFish()
    {
        CreateGameObject(fishList[1]);
        CreateGameObject(item[0]);
        CreateGameObject(fishList[4]);
    }

    /// <summary>
    /// 第三种鱼10% 73-84
    /// </summary>
    private void CreateThirdFish()
    {
        CreateGameObject(fishList[2]);
        CreateGameObject(fishList[5]);
    }

    /// <summary>
    /// 第一种boss鱼和第二种美人鱼3%，99-100
    /// </summary>
    private void CreateFirstBoss()
    {
        CreateGameObject(item[2]);
        CreateGameObject(bosses[0]);
    }

    /// <summary>
    /// 生成第三种boss和贝壳,贝壳10% 85-94 
    /// </summary>
    private void CreateSecondBoss()
    {
        CreateGameObject(item[0]);
        CreateGameObject(bosses[2]);
    }
    #endregion
}