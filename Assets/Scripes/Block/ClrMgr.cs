using UnityEngine;
using System.Collections.Generic;

public class ClearManager : MonoBehaviour
{
    public Generator generator;

    public void CheckAndClearLines()
    {
        List<int> fullLines = new List<int>();

        // 第一步：扫描所有行
        for (int y = 1; y < GridManager.height; y++)
        {
            if (IsLineFull(y))
            {
                fullLines.Add(y);
            }
        }

        if (fullLines.Count > 0)
        {
            // 第二步：统一清除
            foreach (int y in fullLines)
            {
                ClearLine(y);
            }

            // 第三步：统一掉落（从底到顶）
            
            
            MoveAllLinesDownAdvanced(fullLines); // 只移动当前清除行以上的内容
            
        }

        generator.SpawnRandomTetromino(); // 继续游戏
    }

    bool IsLineFull(int y)
    {
        for (int x = 1; x < GridManager.width + 1; x++)
        {
            if (GridManager.grid[x, y] == null)
                return false;
        }
        return true;
    }

    void ClearLine(int y)
    {
        for (int x = 1; x < GridManager.width + 1; x++)
        {
            Destroy(GridManager.grid[x, y].gameObject);
            GridManager.grid[x, y] = null;
        }

        ClearZombiesInArea(y); // 清除对应区域的僵尸
    }

    void MoveAllLinesDownAdvanced(List<int> clearedLines)
    {
        clearedLines.Sort(); // 确保从小到大排序

        int z = 1;
        int w = clearedLines[0] + 1;

        while (w < GridManager.height)
        {
            if (clearedLines.Contains(w))
            {
                z++;    // 如果当前行也是清除的，跳过并 z++
            }
            else
            {
                // 将 w 行下移 z 行
                for (int x = 1; x < GridManager.width + 1; x++)
                {
                    Transform block = GridManager.grid[x, w];
                    GridManager.grid[x, w] = null;

                    if (block != null)
                    {
                        GridManager.grid[x, w - z] = block;
                        block.position += Vector3.down * z;
                    }
                }
            }

            w++; // 继续向上处理
        }
    }
    /*void MoveAllLinesDown(int fromY)
    {
        for (int y = fromY; y < GridManager.height; y++)
        {
            for (int x = 1; x < GridManager.width + 1; x++)
            {
                Transform block = GridManager.grid[x, y];
                if (block != null)
                {
                    GridManager.grid[x, y - 1] = block;
                    GridManager.grid[x, y] = null;
                    block.position += Vector3.down;
                }
            }
        }
    }*/

    void ClearZombiesInArea(int clearedLineY)
    {
        float yMin = clearedLineY-1;
        float yMax = clearedLineY;

        float xMin = 11f;
        float xMax = 23f;

        float zMin = -17f;
        float zMax = 5f;

        foreach (GameObject zombie in ZombieManager.Instance.GetZombies().ToArray()) // 拷贝防止迭代中修改
        {
            if (zombie == null) continue;

            Vector3 pos = zombie.transform.position;

            if (pos.x >= xMin && pos.x <= xMax &&
                pos.y >= yMin && pos.y <= yMax &&
                pos.z >= zMin && pos.z <= zMax)
            {
                Destroy(zombie);
            }
        }
    }

    /*void ClearZombiesInArea(int clearedLineY)
    {
        float yMin = clearedLineY - 1;
        float yMax = clearedLineY + 1;

        float xMin = 11f;
        float xMax = 23f;

        float zMin = -17f;
        float zMax = 5f;

        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");

        foreach (GameObject zombie in zombies)
        {
            Vector3 pos = zombie.transform.position;

            if (pos.x >= xMin && pos.x <= xMax &&
                pos.y >= yMin && pos.y <= yMax &&
                pos.z >= zMin && pos.z <= zMax)
            {
                Destroy(zombie);
            }
        }
    }*/
}
/*using UnityEngine;

public class ClearManager : MonoBehaviour
{
    public Generator generator;

    public void CheckAndClearLines()
    {
        for (int y = 1; y < GridManager.height; y++)
        {
            if (IsLineFull(y))
            {
                ClearLine(y);
                MoveAllLinesDown(y + 1); // 从上面一行开始下坠
                y--; // 回退一行重新检查
            }
        }

        
        generator.SpawnRandomTetromino(); // 继续游戏
        
    }

    bool IsLineFull(int y)
    {
        for (int x = 1; x < GridManager.width+1; x++)
        {
            if (GridManager.grid[x, y] == null)
                return false;
        }
        return true;
    }

    void ClearLine(int y)
    {
        for (int x = 1; x < GridManager.width+1; x++)
        {
            Destroy(GridManager.grid[x, y].gameObject);
            GridManager.grid[x, y] = null;
        }
        ClearZombiesInArea(y);
    }

    void MoveAllLinesDown(int fromY)
    {
        for (int y = fromY; y < GridManager.height; y++)
        {
            for (int x = 1; x < GridManager.width + 1; x++)
            {
                Transform block = GridManager.grid[x, y];
                GridManager.grid[x, y - 1] = block;
                GridManager.grid[x, y] = null;
                if (block != null)
                {
                    block.position += Vector3.down;
                }
            }
        }
    }
    
    void ClearZombiesInArea(int clearedLineY)
    {
        float yMin = clearedLineY - 1;
        float yMax = clearedLineY+1;

        float xMin = 11f;
        float xMax = 23f;

        float zMin = -17f;
        float zMax = 5f;

        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");

        foreach (GameObject zombie in zombies)
        {
            Vector3 pos = zombie.transform.position;

            if (pos.x >= xMin && pos.x <= xMax &&
                pos.y >= yMin && pos.y <= yMax &&
                pos.z >= zMin && pos.z <= zMax)
            {
                Destroy(zombie);
            }
        }
    }


}*/


