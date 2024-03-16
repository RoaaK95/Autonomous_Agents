using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Pig : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animate _animate;
    private float _stoppingDistance;
    [SerializeField] private GameObject _player;
    private int _invokeX = 2;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animate = GetComponent<Animate>();
    }
    void Start()
    {
        _agent.speed = 2.6f;
        _stoppingDistance = 3.0f;
    }
    void Seek(Vector3 location)
    {
        _agent.stoppingDistance = _stoppingDistance;
        _animate._isWalking = true;
        _agent.isStopped = false;
        _agent.SetDestination(location);
    }
    Vector3 _wanderTarget = Vector3.zero;
    private void Wander()
    {
        float wanderRadius = 4.0f;
        float wanderDistance = 10.0f;
        float wanderJitter = 2.0f;

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

    private void Animation(GameObject target)
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= _stoppingDistance)
        {
            _animate._isWalking = false;
        }
        else
        {
            _animate._isWalking = true;
        }
        _animate.WalkAnimation();
    }
    void Update()
    {
        if (!coolDown)
        {
            if (!TargetInRange(_player))
            {
                Wander();
                coolDown = true;
                Invoke("BehaviourCoolDown", 6f);
            }

        }
        _animate.WalkAnimation();
    }
}
