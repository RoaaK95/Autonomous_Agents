using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chicken : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animate _animate;
    private float _playerSpeed;
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
        _playerSpeed = Player.Speed;
        _stoppingDistance = 3.0f;
        _agent.speed = 2.0f;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
