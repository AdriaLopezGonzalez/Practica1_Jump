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
    public float SpeedVertical; //=> movemenetController.MaxSpeed

    private CollisionDetected collisionDetection;

    private float _lastVelocity_Y;

    public float PressTimeToMaxJump;

    private float _jumpStartedTime;

    public ContactFilter2D filter;

    public static Action<Rigidbody2D> CheckCollision;

    CollisionDetected collisionDetected;

    public bool canChangeGravity;

    private void OnEnable()
    {
        PlayerInput.OnJumpStarted += OnJumpStarted;
        PlayerInput.OnJumpFinished += OnJumpFinished;
    }

    private void OnDisable()
    {
        PlayerInput.OnJumpStarted -= OnJumpStarted;
        PlayerInput.OnJumpFinished -= OnJumpFinished;
    }
    void Start()
    {
        collisionDetected = gameObject.GetComponent<CollisionDetected>();
        _rigidbody = GetComponent<Rigidbody2D>();
        collisionDetection = GetComponent<CollisionDetected>();
    }

    void FixedUpdate()
    {
        if (PeakReached() && canChangeGravity)
        {
            Debug.Log("pik");

            CheckCollision?.Invoke(_rigidbody);
            canChangeGravity = false;
        }
    }

    private void TweakGravity(InvertGravity v)
    {
        _rigidbody.gravityScale = -_rigidbody.gravityScale;
    }

    private bool PeakReached()
    {
        bool reached = ((_lastVelocity_Y * _rigidbody.velocity.y) < 0);
        _lastVelocity_Y = _rigidbody.velocity.y;
        return reached;
    }

    public void OnJump(PlayerInput v)
    {
        SetGravity();
        var vel = new Vector2(_rigidbody.velocity.x, GetJumpForce());
        _rigidbody.velocity = vel;
    }

    private float GetJumpForce()
    {
        return 2 * JumpHeight * SpeedVertical / DistanceToMaxHeight;
    }

    private void SetGravity()
    {
        var grav = 2 * JumpHeight * (SpeedVertical * SpeedVertical)
            / (DistanceToMaxHeight * DistanceToMaxHeight);
        if (collisionDetected.IsTouchingRoof)
            grav = -grav;
        _rigidbody.gravityScale = grav / 9.81f;
    }

    public void OnJumpStarted(PlayerInput v)
    {
        SetGravity();
        var vel = new Vector2(_rigidbody.velocity.x, GetJumpForce());
        _rigidbody.velocity = vel;
        _jumpStartedTime = Time.time;
    }
    public void OnJumpFinished(PlayerInput v)
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
