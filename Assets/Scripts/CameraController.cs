using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Vector3 _offset;
    private float _smoothTransition = 0.5f;
    void Start()
    {
        _offset = transform.position - _player.transform.position;

    }

    void LateUpdate()
    {
        Vector3 newPosition = _player.transform.position + _offset;
        transform.position = Vector3.Slerp(transform.position, newPosition, _smoothTransition);
        transform.LookAt(_player.transform);
        Quaternion.LookRotation(newPosition);

    }
}
