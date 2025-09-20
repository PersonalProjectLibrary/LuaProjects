using UnityEngine;

public class Star : MonoBehaviour
{
    public float moveSpeed = 1;

    void Start()
    {
        Destroy(gameObject, Random.Range(0.4f, 1));
    }
}
