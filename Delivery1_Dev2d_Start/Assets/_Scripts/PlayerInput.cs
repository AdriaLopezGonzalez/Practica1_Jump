using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public float MovementHorizontal { get; private set; }
    public float MovementVertical { get; private set; }


    public static Action<PlayerInput> OnJumpStarted;
    public static Action<PlayerInput> OnJumpFinished;
    // Update is called once per frame
    void Update()
    {
        MovementHorizontal = Input.GetAxis("Horizontal");
        MovementVertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && (CollisionDetected.IsGrounded || CollisionDetected.IsTouchingRoof))
        {
           OnJumpStarted?.Invoke(this);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpFinished?.Invoke(this);
        }
    }
}
