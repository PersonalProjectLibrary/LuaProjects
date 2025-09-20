using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 星星闪耀的特效
/// </summary>
public class Shine : MonoBehaviour
{

    private Image img;
    public float speed = 4;
    private bool add;

    public void Awake()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        ShineByA();
    }

    private void ShineByA()
    {
        transform.Rotate(Vector3.forward * 4, Space.World);
        if (!add)
        {
            img.color -= new Color(0, 0, 0, Time.deltaTime * speed);
            if (img.color.a <= 0.2f) add = true;
        }
        else
        {
            img.color += new Color(0, 0, 0, Time.deltaTime * speed);
            if (img.color.a >= 0.8f) add = false;
        }
    }
}
