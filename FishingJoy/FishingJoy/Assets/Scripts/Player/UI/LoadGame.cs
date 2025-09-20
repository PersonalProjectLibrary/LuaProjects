using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public Slider processBar;

    void Start()
    {
        StartCoroutine(StartLoading_4(2));
    }

    private IEnumerator StartLoading_4(int scene)
    {
        int displayBar = 0;
        int targetBar;
        AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            targetBar = (int)op.progress * 100;
            while (displayBar < targetBar)
            {
                ++displayBar;
                SetBarValue(displayBar);
                yield return new WaitForEndOfFrame();
            }
        }

        targetBar = 100;
        while (displayBar < targetBar)
        {
            ++displayBar;
            SetBarValue(displayBar);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }

    /// <summary>
    /// 主线程修改进度条，直接协程里修改会显示卡顿或过快
    /// </summary>
    /// <param name="v"></param>
    private void SetBarValue(float v)
    {
        processBar.value = v / 100;
    }
}
