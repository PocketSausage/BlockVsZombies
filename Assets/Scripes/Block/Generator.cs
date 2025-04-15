using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] tetrominoPrefabs; // 拖入预制体
    public Vector3 spawnPosition = new Vector3(5, 21, 0); // 出现位置（居中顶部）
    public GameManager gameManager;

    void Start()
    {
        SpawnRandomTetromino();

    }
    public void SpawnRandomTetromino()
    {
        int index = Random.Range(0, tetrominoPrefabs.Length); // 随机下标
        /*GameObject newTetromino = Instantiate(tetrominoPrefabs[index], spawnPosition, Quaternion.identity);
        newTetromino.AddComponent<Controller>();*/

        GameObject next = Instantiate(tetrominoPrefabs[index], spawnPosition, Quaternion.identity);

        BlockInfo bif = next.GetComponent<BlockInfo>();
        GameObject ghost = Instantiate(bif.ghostPrefab, spawnPosition, Quaternion.identity);
            //ghost.GetComponent<Generator>().Init(ghost.transform); // 传入实际方块的 Transform
        
        
        if (IsOccupied(next))
        {
            Destroy(next);
            Destroy(ghost);// 不要让出界方块残留
            gameManager.GameOver(); // 立即游戏结束
        }
        next.AddComponent<Controller>();
        next.GetComponent<BlockInfo>().ghostPrefab = ghost;
    }

    bool IsOccupied(GameObject tetromino)
    {
        foreach (Transform block in tetromino.transform)
        {
            Vector2 pos = block.position;
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);

            if (GridManager.grid[x, y] != null)
            {
                return true;
            }
        }
        return false;
    }
}
