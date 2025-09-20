using UnityEngine;

/// <summary>
/// 地图背景
/// </summary>
public class Ground : MonoBehaviour
{

    private MeshRenderer mr;

    public Material[] materialList;

    private AudioSource audioSource;
    public AudioClip[] audioClips;


    // Use this for initialization
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAudio();

        ChangeBgImage();
    }

    private void ChangeAudio()
    {
        if (Gun.Instance.changeAudio)
        {
            audioSource.clip = audioClips[Gun.Instance.level - 1];
            audioSource.Play();
            Gun.Instance.changeAudio = false;
        }
    }

    private void ChangeBgImage()
    {
        switch (Gun.Instance.level)
        {
            case 1:
                if (Gun.Instance.Fire) mr.material = materialList[1];
                else if (Gun.Instance.Ice) mr.material = materialList[2];
                else mr.material = materialList[0];
                break;
            case 2:
                if (Gun.Instance.Fire) mr.material = materialList[4];
                else if (Gun.Instance.Ice) mr.material = materialList[5];
                else mr.material = materialList[3];
                break;
            case 3:
                if (Gun.Instance.Fire) mr.material = materialList[7];
                else if (Gun.Instance.Ice) mr.material = materialList[8];
                else mr.material = materialList[6];
                break;
        }
    }
}
