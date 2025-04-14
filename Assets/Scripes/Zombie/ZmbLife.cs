using UnityEngine;

public class Zombie : MonoBehaviour
{
    void Start()
    {
        // 僵尸生成时注册到管理器
        if (ZombieManager.Instance != null)
        {
            ZombieManager.Instance.RegisterZombie(gameObject);
        }
    }

    void OnDestroy()
    {
        // 僵尸销毁时从管理器注销
        if (ZombieManager.Instance != null)
        {
            ZombieManager.Instance.UnregisterZombie(gameObject);
        }
    }
}