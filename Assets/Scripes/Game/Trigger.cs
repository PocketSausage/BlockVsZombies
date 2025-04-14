using UnityEngine;

public class TopTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Destroy(other.gameObject); // 销毁僵尸
            HealthManager.Instance.TakeDamage(1); // 扣血
        }
    }
}