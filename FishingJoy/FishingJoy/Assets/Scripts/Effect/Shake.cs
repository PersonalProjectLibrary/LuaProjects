using UnityEngine;

/// <summary>
/// boss攻击玩家产生的震动方法
/// </summary>
public class Shake : MonoBehaviour
{
    private float cameraShake = 2;
    public GameObject UI;

    void Update()
    {
        ShakeCamera();
    }

    /// <summary>
    /// 镜头撞击效果
    /// </summary>
    private void ShakeCamera()
    {
        if (Gun.Instance.bossAttack)
        {
            UI.SetActive(true);
            transform.position = new Vector3((Random.Range(0f, cameraShake)) - cameraShake * 0.5f, transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, (Random.Range(0f, cameraShake)) - cameraShake * 0.5f);
            cameraShake = cameraShake / 1.05f;
            if (cameraShake < 0.05f)
            {
                cameraShake = 0;
                UI.SetActive(false);
                Gun.Instance.bossAttack = false;
            }
        }
        else cameraShake = 5;
    }
}
