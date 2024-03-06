using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _rotSpeed = 100.0f;
    private float _xBound = 17.0f;
    private float _zBound = 18.0f;

    void Update()
    {
        PlayerMovement();
        KeepPlayerInBounds();
    }

    void PlayerMovement()
    {
        float translation = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");
        float moveForward = translation * _speed ;
        float rotate = rotation * _rotSpeed ;
        transform.Translate(0, 0, moveForward* Time.deltaTime);
        transform.Rotate(0, rotate*Time.deltaTime, 0);
    }

    void KeepPlayerInBounds()
    {
        if (transform.position.x >= _xBound)
        {
            transform.position = new Vector3(_xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x <= -_xBound)
        {
            transform.position = new Vector3(-_xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.z >= _zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _zBound);
        }
        if (transform.position.z <= -_zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -_zBound);
        }
    }
}
