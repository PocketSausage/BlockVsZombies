using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ZombieDebugColor : MonoBehaviour
{
    private ZombieClimber climber;
    private Renderer rend;
    private Color defaultColor;

    void Start()
    {
        climber = GetComponent<ZombieClimber>();
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color; // 记录默认颜色
    }

    void Update()
    {
        switch (climber.CurrentState) // 注意：ZombieClimber 需要提供 public CurrentState 属性
        {
            case ZombieClimber.State.Stop:
                rend.material.color = defaultColor;
                break;

            case ZombieClimber.State.Run:
                rend.material.color = Color.green;
                break;

            case ZombieClimber.State.Climb:
                rend.material.color = Color.blue;
                break;
        }
    }
}