using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _rotationSpeed = 100.0f;
    private float _currentSpeed = 0.0f;
   
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float translation = Input.GetAxis("Vertical") * _speed;
        float rotation = Input.GetAxis("Horizontal") * _rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation);
        _currentSpeed = translation;
        transform.Rotate(0, rotation, 0);
    }
}
