using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animate _animate;
    private float _playerSpeed;
    private float _stoppingDistance;
    [SerializeField] private GameObject _player;
    [SerializeField] GameObject _FrontLegL, _FrontLegR;
    private int _invokeX = 2;
    private bool canPeck = true;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animate = GetComponent<Animate>();
    }

    void Start()
    {
        _playerSpeed = Player.Speed;
        _stoppingDistance = 3.0f;
        _agent.speed = 2.0f;
    }

    private void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - transform.position;
        _agent.SetDestination(transform.position - fleeVector);
    }
    private void Walk()
    {
        float _legsRotSpeed = 4.0f;
        Vector3 legStartPosA = new Vector3(10.0f, 0f, 0f);
        Vector3 legEndPosA = new Vector3(-10.0f, 0f, 0f);
        Vector3 legStartPosB = new Vector3(-10.0f, 0f, 0f);
        Vector3 legEndPosB = new Vector3(10.0f, 0f, 0f);
        Quaternion legAngleFromA = Quaternion.Euler(legStartPosA.x, -90.0f, legStartPosA.z);
        Quaternion legAngleToA = Quaternion.Euler(legEndPosA.x, -90.0f, legEndPosA.z);
        Quaternion legAngleFromB = Quaternion.Euler(legStartPosB.x, -90.0f, legStartPosB.z);
        Quaternion legAngleToB = Quaternion.Euler(legEndPosB.x, -90.0f, legEndPosB.z);
        float lerp = 0.5f * (1.0f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * _legsRotSpeed));
        _FrontLegL.transform.localRotation = Quaternion.Lerp(legAngleFromA, legAngleToA, lerp);
        _FrontLegR.transform.localRotation = Quaternion.Lerp(legAngleFromB, legAngleToB, lerp);

    }

    private bool TargetInRange(GameObject target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 10)
        {
            return true;
        }
        return false;
    }

    void Update()
    {

       
        

    }

    private IEnumerator Peck()
    {
        canPeck = false;
        transform.eulerAngles = new Vector3(45f, transform.eulerAngles.y, transform.eulerAngles.z);

        yield return new WaitForSeconds(2f);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);

        yield return new WaitForSeconds(8);
        canPeck = true;
    }
}

