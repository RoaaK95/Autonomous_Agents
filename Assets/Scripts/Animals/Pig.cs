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
    GameObject chosenGO;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animate = GetComponent<Animate>();
        chosenGO = World.Instance.HideSpots()[0];
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

    private void Hide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;

        for (int i = 0; i < World.Instance.HideSpots().Length; i++)
        {
            Vector3 hideDir = World.Instance.HideSpots()[i].transform.position - _player.transform.position;
            Vector3 hidePos = World.Instance.HideSpots()[i].transform.position + hideDir.normalized * 10;

            if (Vector3.Distance(transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                chosenGO = World.Instance.HideSpots()[i];
                dist = Vector3.Distance(transform.position, hidePos);
            }
        }

        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backray = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit hitInfo;
        float distance = 60.0f;
        hideCol.Raycast(backray, out hitInfo, distance);

        Seek(hitInfo.point + chosenDir.normalized);

    }
    private bool CanSeeMe()
    {
        Vector3 rayFromTarget = transform.position - _player.transform.position;
        float lookAngle = Vector3.Angle(_player.transform.forward, rayFromTarget);
        if (lookAngle < 60)
        {
            return true;
        }
        return false;
    }

    private bool CanSeeTarget()
    {
        RaycastHit hitInfo;
        Vector3 rayToTarget = _player.transform.position - transform.position;
        float lookAngle = Vector3.Angle(transform.forward, rayToTarget);
        if (lookAngle < 60 && Physics.Raycast(transform.position, rayToTarget, out hitInfo))
        {
            if (hitInfo.transform.gameObject.CompareTag("Player"))
                return true;
        }
        return false;
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
            if (CanSeeMe() && CanSeeTarget())
            {
                Hide();
                coolDown = true;
                Debug.Log(name + " Hide invoked");
                Invoke("BehaviourCoolDown", 12);
                
            }
            else
            {
                Wander();
                Debug.Log(name + " Wander invoked");
            }

        }
        Animation(chosenGO);
    }
}
