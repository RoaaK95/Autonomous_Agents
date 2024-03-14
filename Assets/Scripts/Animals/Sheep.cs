using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.AI;
public class Sheep : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animate _animate;
    private float _playerSpeed;
    private float _stoppingDistance;
    private float _speed;
    [SerializeField] private GameObject _player;
    private int _invokeX = 2;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animate = GetComponent<Animate>();

    }

    void Start()
    {
        _playerSpeed = Player.Speed;
        _stoppingDistance = 3.0f;
        _speed = 2.0f;
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
        float wanderRadius = 5.0f;
        float wanderDistance = 15.0f;
        float wanderJitter = 6.0f;

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

    private void Pursue()
    {
        Vector3 targetDir = _player.transform.position - transform.position;
        float relativeHeading = Vector3.Angle(transform.forward, transform.TransformVector(_player.transform.forward));
        float toTarget = Vector3.Angle(transform.forward, transform.TransformVector(targetDir));
        if (toTarget > 90 && relativeHeading < 20 || _playerSpeed < 0.1f)
        {
            Seek(_player.transform.position);
            return;
        }
        float lookAhead = targetDir.magnitude / (_agent.speed + _playerSpeed);
        Seek(_player.transform.position + _player.transform.forward * lookAhead);


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
            if (!TargetInRange(_player) && Random.Range(1, 10) <= _invokeX)
            {
                Wander();
                coolDown = true;
                Debug.Log("Wander invoked");
                Invoke("BehaviourCoolDown", 15);
            }
            else
            {
                Pursue();
                coolDown = true;
                Debug.Log("Pursue invoked");
                Invoke("BehaviourCoolDown", 1f);

            }

        }
        Animation(_player);


    }
}
