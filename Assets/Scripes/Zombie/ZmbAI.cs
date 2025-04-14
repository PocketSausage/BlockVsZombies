using UnityEngine;

public class ZombieClimber : MonoBehaviour
{
    public enum State { Stop, Run, Climb }
    public State currentState = State.Stop;

    public float forwardForce = 10f;
    public float upwardForce = 8f;
    public float velocityThreshold = 1f;
    public float frontCheckDistance = 0.2f;
    public State CurrentState => currentState; // 添加这一行


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 初始给予一个向前的力（面对墙）
        rb.AddForce(Vector3.forward * forwardForce, ForceMode.Force);

        RemoveClimbable(); // 初始不可攀爬
    }

    void FixedUpdate()
    {
        float forwardVelocity = Vector3.Dot(rb.velocity, Vector3.forward);

        bool frontHasClimbable = CheckFrontClimbable();

        switch (currentState)
        {
            case State.Stop:
                if (forwardVelocity > velocityThreshold)
                {
                    currentState = State.Run;
                    RemoveClimbable();
                    rb.AddForce(Vector3.forward * forwardForce, ForceMode.Force);
                }else if (frontHasClimbable)
                {
                    currentState = State.Climb;
                    RemoveClimbable();
                    //AddClimbable();
                    //rb.AddForce(Vector3.forward * upwardForce*0.4f, ForceMode.Impulse);
                    rb.AddForce(Vector3.up * upwardForce, ForceMode.Force);
                    rb.AddForce(Vector3.forward * forwardForce, ForceMode.Force);
                }
                    
                break;

            case State.Run:
                if (forwardVelocity < velocityThreshold)
                {
                    if (frontHasClimbable)
                    {
                        currentState = State.Climb;
                        RemoveClimbable();
                        //AddClimbable();
                        //rb.AddForce(Vector3.forward * upwardForce*0.4f, ForceMode.Impulse);
                        rb.AddForce(Vector3.up * upwardForce, ForceMode.Force);
                        rb.AddForce(Vector3.forward * forwardForce, ForceMode.Force);
                    }
                    else
                    {
                        currentState = State.Stop;
                        AddClimbable();
                        rb.AddForce(Vector3.forward * forwardForce, ForceMode.Force);
                    }
                }
                break;

            case State.Climb:
                if (!frontHasClimbable)
                {
                    if (forwardVelocity < velocityThreshold)
                    {
                        currentState = State.Stop;
                        AddClimbable();
                        rb.AddForce(Vector3.forward * forwardForce, ForceMode.Force);
                    }
                    else if (forwardVelocity > velocityThreshold)
                    {
                        currentState = State.Run;
                        RemoveClimbable();
                        rb.AddForce(Vector3.forward * forwardForce, ForceMode.Force);
                    }
                }
                break;
        }
    }

    bool CheckFrontClimbable()
    {
        Ray ray = new Ray(transform.position, Vector3.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, frontCheckDistance))
        {
            return hit.collider.GetComponent<ClimbableSurface>() != null;
        }
        return false;
    }

    void AddClimbable()
    {
        if (GetComponent<ClimbableSurface>() == null)
            gameObject.AddComponent<ClimbableSurface>();
    }

    void RemoveClimbable()
    {
        ClimbableSurface cs = GetComponent<ClimbableSurface>();
        if (cs != null)
            Destroy(cs);
    }
}


