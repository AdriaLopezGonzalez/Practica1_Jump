using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public float MovementHorizontal { get; private set; }
    public float MovementVertical { get; private set; }


    public static Action<PlayerInput> OnJump;
    // Update is called once per frame
    void Update()
    {
        MovementHorizontal = Input.GetAxis("Horizontal");
        MovementVertical = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Space) && CollisionDetected.IsGrounded)
        {
           OnJump?.Invoke(this);
        }
    }
}
