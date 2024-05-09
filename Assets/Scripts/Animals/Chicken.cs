using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour
{
    private NavMeshAgent _agent;
    private float _playerSpeed;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _food;
    [SerializeField] GameObject _FrontLegL, _FrontLegR;
    private float _stoppingDistance;
    private int _invokeX = 2;
    private bool canPeck = true;
    private bool _isWalking;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _playerSpeed = Player.Speed;
        _agent.speed = 2.0f;
        _isWalking = false;
        _stoppingDistance = 3.0f;
    }

    private void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - transform.position;
        _agent.SetDestination(transform.position - fleeVector);
        _isWalking = true;

    }
    private void Evade()
    {
        Vector3 targetDir = _player.transform.position - transform.position;
        float lookAhead = targetDir.magnitude / (_playerSpeed + _agent.speed);
        Flee(_player.transform.position + _player.transform.forward * lookAhead);

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

    private void GoToFood()
    {
        _isWalking = true;
        _agent.SetDestination(_food.transform.position);
        if (_agent.remainingDistance <= _stoppingDistance)
        {
            StartCoroutine(Peck());
        }
    }
    private bool TargetInRange(GameObject target)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 8f)
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
            if (TargetInRange(_player))
            {
                Evade();
                coolDown = true;
                Debug.Log(name + " Evade invoked");
                Invoke("BehaviourCoolDown", 8);
            }
            else
            {
                GoToFood();
                Debug.Log(name + " GoToFood invoked");

            }
        }


        Walk();

    }

    private IEnumerator Peck()
    {
        _isWalking = false;
        canPeck = false;
        transform.eulerAngles = new Vector3(45f, transform.eulerAngles.y, transform.eulerAngles.z);

        yield return new WaitForSeconds(4f);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);

        yield return new WaitForSeconds(4f);
        canPeck = true;
    }
}

