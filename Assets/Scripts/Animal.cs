using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Animal : MonoBehaviour
{
    private NavMeshAgent _agent;
    public GameObject _target;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        
    }
    void Update()
    {
        Seek(_target.transform.position);
    }

    private void Seek(Vector3 location)
    {
        _agent.stoppingDistance = 2f;
        _agent.SetDestination(location);
    }
}
