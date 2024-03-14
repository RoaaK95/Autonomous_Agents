using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Sheep : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animate _animate;
    [SerializeField] private GameObject _player;
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
        if(!coolDown)
        {
            Wander();
            coolDown = true;
            Invoke("BehaviourCoolDown", 15);
        }
        _animate.WalkAnimation();
    }
}
