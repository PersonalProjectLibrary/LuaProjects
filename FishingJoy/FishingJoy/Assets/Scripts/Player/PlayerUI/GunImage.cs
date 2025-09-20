using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 负责UI显示的枪
/// </summary>
public class GunImage : MonoBehaviour
{
    private Image img;
    /// <summary>
    /// 三种枪
    /// </summary>
    public Sprite[] Guns;
    /// <summary>
    /// 空闲状态下的枪位置
    /// </summary>
    public Transform idlePos;
    /// <summary>
    /// 攻击时枪位置
    /// </summary>
    public Transform attackPos;

    private float rotateSpeed = 5f;

    private void Awake()
    {
        img = transform.GetComponent<Image>();
    }

    void Update()
    {
        RotateGun();

        img.sprite = Guns[Gun.Instance.gunLevel - 1];

        if (Gun.Instance.attack && Input.GetMouseButtonDown(0)) GunShifting();
    }

    /// <summary>
    /// 攻击时枪的位置移动
    /// </summary>
    private void GunShifting()
    {
        transform.position = Vector3.Lerp(transform.position, attackPos.position, 0.5f);
        Invoke("Idle", 0.4f);
    }

    /// <summary>
    /// 枪默认位置/回正，GunShifting里委托调用
    /// </summary>
    private void Idle()
    {
        transform.position = Vector3.Lerp(transform.position, idlePos.position, 0.2f);
    }

    /// <summary>
    /// 枪的旋转
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
        float z = transform.eulerAngles.z;
        if (z <= 35) z = 35;
        else if (z >= 150) z = 150;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
    }
}
