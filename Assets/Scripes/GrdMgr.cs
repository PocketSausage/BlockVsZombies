using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static int width = 10;
    public static int height = 20;
    public GameObject blockPrefab;

    public static Transform[,] grid = new Transform[width+2, height+4];

    void Start()
    {
        InitGrid(); // 初始化墙体
    }

    void InitGrid()
    {
        for (int x = 0; x < width + 2; x++)
        {
            for (int y = 0; y < height + 2; y++)
            {
                if (x == 0 || x == width + 1 || y == 0) // 左、右、底部为墙
                {
                    Vector3 pos = new Vector3(x, y, 0);
                    GameObject wallBlock = Instantiate(blockPrefab, pos, Quaternion.identity, transform);
                    grid[x, y] = wallBlock.transform;
                }
                else
                {
                    grid[x, y] = null;
                }
            }
        }
    }

    public static Vector2Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector2Int(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));
    }

    public static bool IsCellOccupied(Vector3 worldPos)
    {
        Vector2Int cell = WorldToGrid(worldPos);
        return grid[cell.x, cell.y] != null;
    }

    public static void SetCellOccupied(Transform block)
    {
        Vector3 worldPos = block.position;
        Vector2Int cell = WorldToGrid(worldPos);
        grid[cell.x, cell.y] = block;
    }
}
