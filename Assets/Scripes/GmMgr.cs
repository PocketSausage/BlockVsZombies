using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void GameOver()
    {
        Debug.Log("Game Over");
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        // SceneManager.LoadScene("GameOverScene"); // 或加载失败场景
    }
}

