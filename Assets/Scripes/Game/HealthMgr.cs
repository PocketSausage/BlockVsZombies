using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    public Slider healthBar;
    private int maxHealth = 10;
    private int currentHealth;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateUI();

        if (currentHealth <= 0)
        {
            Debug.Log("Health Depleted. Game Over.");
            SceneManager.LoadScene("GameO"); // 替换为你的结束场景
        }
    }

    void UpdateUI()
    {
        if (healthBar != null)
            healthBar.value = currentHealth;
    }
}

