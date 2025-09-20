using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 返回主菜单
/// </summary>
public class ReturnMain : MonoBehaviour
{
    private Button but;

    void Start()
    {
        but = GetComponent<Button>();
        but.onClick.AddListener(StartGames);
    }

    public void StartGames()
    {
        SceneManager.LoadScene(0);
    }
}

