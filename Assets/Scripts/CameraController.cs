using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    public Vector3 _offset= new Vector3(0,0,0);
    private Vector3 _playerPos;
    private void Awake()
    {
        _playerPos = _player.transform.position;
        
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(_playerPos.x, _playerPos.y, _playerPos.z) + _offset;
        transform.rotation = Quaternion.identity;
    }
}
