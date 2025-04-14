using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // 返回开始界面
    public void BackToStart()
    {
        SceneManager.LoadScene("GameS"); // 替换成你开始界面的场景名
    }

    // 重新开始游戏
    public void RetryGame()
    {
        SceneManager.LoadScene("SmplScn"); // 替换成你的游戏场景名
    }
}

