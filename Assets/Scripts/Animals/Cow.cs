using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
public class Cow : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animate _animate;
    [SerializeField] private GameObject _waterTrough;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animate = GetComponent<Animate>();
    }
    private void Start()
    {
        GoToWaterTrough();
    }
    void Seek(Vector3 location)
    {
        _agent.stoppingDistance = 2.0f;
        _animate.WalkAnimation();
        _agent.SetDestination(location);

    }
    Vector3 _wanderTarget = Vector3.zero;
    private void Wander()
    {
        float wanderRadius = 10.0f;
        float wanderDistance = 20.0f;
        float wanderJitter = 1.0f;

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
        if (Vector3.Distance(transform.position, _waterTrough.transform.position) < 2f)
        {
            _agent.isStopped = true;
        }
    }

 
    void Update()
    {
        //Wander();
        // GoToWaterTrough();
    }

}
