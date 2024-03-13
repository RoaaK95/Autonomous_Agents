using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
public class Cow : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animate _animate;
    [SerializeField] private GameObject _waterTrough;
    private int _invokeX = 2;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animate = GetComponent<Animate>();
    }

    void Seek(Vector3 location)
    {
        _agent.stoppingDistance = 1.0f;
        _animate._isWalking = true;
        _agent.isStopped = false;
        _agent.SetDestination(location);
    }

    Vector3 _wanderTarget = Vector3.zero;
    private void Wander()
    {
        float wanderRadius = 6.0f;
        float wanderDistance = 12.0f;
        float wanderJitter = 5.0f;

        _wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);
        _wanderTarget.Normalize();
        _wanderTarget *= wanderRadius;

        Vector3 localTarget = _wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 worldTarget = gameObject.transform.InverseTransformVector(localTarget);
        Seek(worldTarget);

    }

    private void GoToWaterTrough()
    {
        Seek(_waterTrough.transform.position);
        if (Vector3.Distance(_waterTrough.transform.position, transform.position) <= 1f)
        {
            _animate._isWalking = false;
        }

    }
    private bool TargetInRange(GameObject target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 10)
        {
            return true;
        }
        return false;
    }
    bool coolDown = false;
    private void BehaviourCoolDown()
    {
        coolDown = false;
    }
    void Update()
    {
        if (!coolDown)
        {
            if (TargetInRange(_waterTrough) && Random.Range(1, 10) <= _invokeX)
            {
                GoToWaterTrough();
                coolDown = true;
                Debug.Log("go to water invoked");
                Invoke("BehaviourCoolDown", 10);

            }
            else
            {
                Wander();
                coolDown = true;
                Debug.Log("Wander invoked");
                Invoke("BehaviourCoolDown", 15);

            }

        }

        _animate.WalkAnimation();

    }

}
