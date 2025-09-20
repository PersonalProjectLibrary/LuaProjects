using UnityEngine;

/// <summary>
/// 游戏中产生的泡泡
/// </summary>
public class Pao : MonoBehaviour
{
    public int moveSpeed;
    public bool isGamePao;

    void Start()
    {
        InitializedBubble();
    }

    private void InitializedBubble()
    {
        if (isGamePao)
        {
            moveSpeed = Random.Range(2, 4);
            Destroy(this.gameObject, Random.Range(0.5f, 1f));
        }
        else
        {
            moveSpeed = Random.Range(40, 100);
            Destroy(this.gameObject, Random.Range(7f, 10f));
        }
    }

    void Update()
    {
        BubbleMove();
    }

    private void BubbleMove()
    {
        transform.Translate(-transform.right * moveSpeed * Time.deltaTime, Space.World);
    }
}
