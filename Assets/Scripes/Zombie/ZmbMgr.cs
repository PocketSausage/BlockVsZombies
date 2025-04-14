using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance;

    private List<GameObject> zombies = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterZombie(GameObject zombie)
    {
        if (!zombies.Contains(zombie))
            zombies.Add(zombie);
    }

    public void UnregisterZombie(GameObject zombie)
    {
        if (zombies.Contains(zombie))
            zombies.Remove(zombie);
    }

    public List<GameObject> GetZombies()
    {
        return zombies;
    }
}