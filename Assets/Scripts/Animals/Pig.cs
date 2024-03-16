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
        
    }
}
