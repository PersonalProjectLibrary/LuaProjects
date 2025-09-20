using UnityEngine;

/// <summary>
/// 产生UI泡泡
/// </summary>
public class CreatePao : MonoBehaviour
{
    public GameObject pao;
    public Transform panel;
    private float timeVal = 6;

    void Update()
    {
        CreateBubble();
    }

    /// <summary>
    /// 距离上次泡泡生成有超过6秒才生成下一次泡泡
    /// </summary>
    private void CreateBubble()
    {
        if (timeVal >= 6)
        {
            for (int i = 0; i < 4; i++)
            {
                Invoke("InstPao", 1);
            }
            timeVal = 0;
        }
        else timeVal += Time.deltaTime;
    }

    /// <summary>
    /// 生成气泡对象 CreateBubble里协程调用
    /// </summary>
    private void InstPao()
    {
        GameObject itemGo = Instantiate(pao, transform.position, Quaternion.Euler(0, 0, Random.Range(-80, 0))) as GameObject;
        itemGo.transform.SetParent(panel);
    }
}
