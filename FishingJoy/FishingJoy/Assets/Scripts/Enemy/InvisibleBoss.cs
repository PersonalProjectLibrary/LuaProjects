using UnityEngine;

/// <summary>
/// 会隐藏的boss
/// </summary>
public class InvisibleBoss : Boss
{
    private bool isInvisible = false;
    private float invisibleTime = 0;
    private float recoverTime = 0;

    private BoxCollider box;
    private SpriteRenderer sr;


    void Start()
    {
        InitializeBoss();

        box = GetComponent<BoxCollider>();
        sr = GetComponent<SpriteRenderer>();
    }

    public override void BossAction()
    {
        base.BossAction();

        if (invisibleTime >= 10)
        {
            invisibleTime = 0;
            Invisible();
        }
        else invisibleTime += Time.deltaTime;

        if (isInvisible)
        {
            sr.color -= new Color(0, 0, 0, Time.deltaTime);
            box.enabled = false;
        }
        else
        {
            sr.color += new Color(0, 0, 0, Time.deltaTime);
            if (recoverTime >= 3)
            {
                recoverTime = 0;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            }
            else recoverTime += Time.deltaTime;

            box.enabled = true;
        }
    }
    private void Invisible()
    {
        isInvisible = true;
        Invoke("CloseInvisible", 3);
    }
    private void CloseInvisible()
    {
        isInvisible = false;
    }
}
