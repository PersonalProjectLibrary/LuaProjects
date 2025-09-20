using UnityEngine;

/// <summary>
/// 渔网
/// </summary>
public class FishNet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fish") other.GetComponent<Fish>().isnet = true;
    }
}
