


using UnityEngine;

public class Controller : MonoBehaviour
{
    public float normalFallTime = 1f;   // 正常下落时间
    public float fastFallTime = 0.2f;   // 快速下落时间
    private float currentFallTime;
    private float fallTimer = 0f;
    public float fallTime = 1f;
    private Generator generator;
    private bool rotateClockwiseNext;
    private BlockInfo blockInfo;
    private Transform ghost;
    

    void Start()
    {
        generator = GameObject.FindObjectOfType<Generator>();
        rotateClockwiseNext = true;
        blockInfo = GetComponent<BlockInfo>();
        ghost = blockInfo.ghostPrefab.transform;
    }

    void Update()
    {
        LowDown();
        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentFallTime = fastFallTime; // 快速下落
        }
        else
        {
            currentFallTime = normalFallTime; // 恢复正常
        }
        fallTimer += Time.deltaTime;
        if (fallTimer >= currentFallTime)
        {
            TryMove(Vector3.down);
            ghost.position=transform.position;
            LowDown();
            fallTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TryMove(Vector3.left);
            ghost.position=transform.position;
            LowDown();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TryMove(Vector3.right);
            ghost.position=transform.position;
            LowDown();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryRotate();
            LowDown();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position=ghost.position;
            SitDown();
        }
    }

    void TryRotate()
    {
        //string blockName = gameObject.name;

        // 禁止 O 方块旋转
        if (blockInfo.type == BlockType.O) return;

        float angle;

        // 特殊规则：T、J、L 总是顺时针旋转（-90 度）
        if (blockInfo.type == BlockType.T || blockInfo.type == BlockType.J || blockInfo.type == BlockType.L) 
        {
            angle = -90f;
        }
        else
        {
            // 其他类型（如 I, S, Z）：交替旋转
            angle = rotateClockwiseNext ? -90f : 90f;
            rotateClockwiseNext = !rotateClockwiseNext; // 下次切换方向
        }

        // 尝试旋转
        transform.Rotate(0, 0, angle);
        ghost.Rotate(0, 0, angle);
        

        // 若旋转后不合法，则撤销
        if (!IsValidPosition(transform))
        {
            transform.Rotate(0, 0, -angle);
            ghost.Rotate(0, 0, -angle);
        }
    }

    void LowDown()
    {
        while(IsValidPosition(ghost))
        {ghost.position += Vector3.down;}
        ghost.position -= Vector3.down;

        
    }

    void TryMove(Vector3 direction)
    {
        transform.position += direction;

        if (!IsValidPosition(transform))
        {
            transform.position -= direction;

            if (direction == Vector3.down)
            {
                SitDown();
            }
        }
    }

    void SitDown()
    {
        SetBlocksToGrid();
        this.enabled = false;
        
        GameObject.FindObjectOfType<ClearManager>().CheckAndClearLines();
        rotateClockwiseNext = true;
        Destroy(ghost.gameObject);
    }

    bool IsValidPosition(Transform goal)
    {
        foreach (Transform block in goal)
        {
            if (GridManager.IsCellOccupied(block.position))
                return false;
        }
        return true;
    }

    void SetBlocksToGrid()
    {
        foreach (Transform block in transform)
        {
            GridManager.SetCellOccupied(block);
        }
    }
}


//using UnityEngine;

//public class Controller : MonoBehaviour
//{
//    public float fallTime = 1f; // 下落时间间隔
//    private float fallTimer = 0f;
//    private int[][] wall = new int[10][];

//    private Generator generator; // 引用生成器

//    void Start()
//    {
//        generator = GameObject.FindObjectOfType<Generator>();
//    }

//    void Update()
//    {
//        // 处理下落
//        fallTimer += Time.deltaTime;
//        if (fallTimer >= fallTime)
//        {
//            Move(Vector3.down); // 每秒向下移动一格
//            fallTimer = 0f;
//        }

//        // 左右移动（按一次移动一格）
//        if (Input.GetKeyDown(KeyCode.LeftArrow))
//        {
//            if (CanMove(Vector3.left)) Move(Vector3.left);
//        }

//        if (Input.GetKeyDown(KeyCode.RightArrow))
//        {
//            if (CanMove(Vector3.right)) Move(Vector3.right);
//        }
//    }

//    void Move(Vector3 direction)
//    {
//        transform.position += direction;
//        if (!IsValidPosition())
//        {
//            transform.position -= direction;

//            // 到底后不能再下落，则固定（下落失败）
//            if (direction == Vector3.down)
//            {
//                this.enabled = false; // 禁用控制器
//                generator.SpawnRandomTetromino(); // 生成下一个
//            }
//        }
//    }

//    bool CanMove(Vector3 direction)
//    {
//        transform.position += direction;
//        bool valid = IsValidPosition();
//        transform.position -= direction;
//        return valid;
//    }

//    bool IsValidPosition()
//    {
//        // 检测是否在合法区域（简单墙体检测）
//        foreach (Transform block in transform)
//        {
//            Vector2 pos = Round(block.position);

//            if (pos.x < 0 || pos.x > 9 || pos.y < 0)
//                return false;
//        }
//        return true;
//    }

//    Vector2 Round(Vector3 pos)
//    {
//        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
//    }
//}
