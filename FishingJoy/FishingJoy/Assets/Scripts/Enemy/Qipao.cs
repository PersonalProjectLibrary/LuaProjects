using UnityEngine;

/// <summary>
/// 挡子弹的气泡
/// </summary>
public class Qipao : MonoBehaviour
{
    public float moveSpeed = 2;
    private float rotateTime;

    void Start()
    {
        Destroy(gameObject, 14);
    }

    void Update()
    {
        FishMove();
    }

    public void FishMove()
    {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
        if (rotateTime >= 5)
        {
            transform.Rotate(transform.forward * Random.Range(0, 361), Space.World);
            rotateTime = 0;
        }
        else rotateTime += Time.deltaTime;
    }
}
