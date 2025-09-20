using UnityEngine;
using UnityEngine.UI;

public class Mylight : MonoBehaviour
{
    public Sprite[] lights;
    private Image img;
    private int i;
    private float timeVal;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        ImgCarousel();
    }

    public void ImgCarousel()
    {
        if (timeVal >= 0.08f)
        {
            img.sprite = lights[i];
            i++;
            if (i == lights.Length) i = 0;
            timeVal = 0;
        }
        else timeVal += Time.deltaTime;
    }
}
