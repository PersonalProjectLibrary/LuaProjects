using UnityEngine;

/// <summary>
/// 有护盾的boss
/// </summary>
public class DeffendBoss : Boss
{
    public GameObject deffend;
    private bool isDeffend = false;
    private float deffendTime = 0;

    void Start()
    {
        InitializeBoss();
    }

    public override void BossAction()
    {
        base.BossAction();
        if (deffendTime >= 10)
        {
            deffendTime = 0;
            DeffenMe();
        }
        else deffendTime += Time.deltaTime;
    }

    void DeffenMe()
    {
        isDeffend = true;
        deffend.SetActive(true);
        Invoke("CloseDeffendMe", 3);
    }
    private void CloseDeffendMe()
    {
        deffend.SetActive(false);
        isDeffend = false;
    }
    public override void TakeDamage(int attackValue)
    {
        if (isDeffend) return;
        //Debug.Log("DeffbossDie");
        base.TakeDamage(attackValue);
    }
}
