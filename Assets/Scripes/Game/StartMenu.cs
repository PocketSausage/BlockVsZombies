using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SmplScn"); // 替换为你的主游戏场景名
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // 在打包后退出游戏
    }
}