using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Animal : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] GameObject _target;
    private Animate _animate;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animate = GetComponent<Animate>();
    }
    void Update()
    {
        Seek(_target.transform.position);
    }
    void Seek(Vector3 location)
    {
        _agent.stoppingDistance = 2.0f;
        _animate.WalkAnimation();
        _agent.SetDestination(location);
    }
   




}


