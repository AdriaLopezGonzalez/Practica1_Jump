using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    //public float JumpForce;
    private Rigidbody2D _rigidbody;

    public float JumpHeight;
    // public float TimeToMaxHeight;

    public float DistanceToMaxHeight;
    public float SpeedHorizontal; //=> movemenetController.MaxSpeed

    private CollisionDetected collisionDetection;

    private float _lastVelocity_Y;

    public float PressTimeToMaxJump;

    private float _jumpStartedTime;

    public ContactFilter2D filter;


    private void OnEnable()
    {
        PlayerInput.OnJump += OnJump;
    }

    private void OnDisable()
    {
        PlayerInput.OnJump -= OnJump;
    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        collisionDetection = GetComponent<CollisionDetected>();
    }

    void FixedUpdate()
    {
        //if (PeakReached())
        //    TweakGravity();
    }

    //private void TweakGravity()
    //{
    //    _rigidbody.gravityScale *= 1.2f;
    //}

    private bool PeakReached()
    {
        bool reached = ((_lastVelocity_Y * _rigidbody.velocity.y) < 0);
        _lastVelocity_Y = _rigidbody.velocity.y;
        return reached;
    }

    public void OnJump(PlayerInput playerInput)
    {
        SetGravity();
        var vel = new Vector2(_rigidbody.velocity.x, GetJumpForce());
        _rigidbody.velocity = vel;
    }

    private float GetJumpForce()
    {
        return 2 * JumpHeight * SpeedHorizontal / DistanceToMaxHeight;
    }

    private void SetGravity()
    {
        var grav = 2 * JumpHeight * (SpeedHorizontal * SpeedHorizontal)
            / (DistanceToMaxHeight * DistanceToMaxHeight);
        _rigidbody.gravityScale = grav / 9.81f;
    }

    public void OnJumpStarted()
    {
        SetGravity();
        var vel = new Vector2(_rigidbody.velocity.x, GetJumpForce());
        _rigidbody.velocity = vel;
        _jumpStartedTime = Time.time;
    }
    public void OnJumpFinished()
    {
        float fractionOfTimePressed = 1 /
            Mathf.Clamp01((Time.time - _jumpStartedTime) /
            PressTimeToMaxJump);
        _rigidbody.gravityScale *= fractionOfTimePressed;
    }

    private float DistanceToGround()
    {

        RaycastHit2D[] hit = new RaycastHit2D[3];
        Physics2D.Raycast(transform.position, Vector2.down, filter, hit, 10);
        return hit[0].distance;
    }
}
