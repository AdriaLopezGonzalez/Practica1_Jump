using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionDetected : MonoBehaviour
{
    [SerializeField]
    private LayerMask WhatIsGround;
    [SerializeField]
    private LayerMask WhatIsRoof;

    [SerializeField]
    private Transform GroundCheckPoint;
    [SerializeField]
    private Transform RoofCheckPoint;

    private float checkRadius = 0.15f;
    private bool _wasGrounded;

    private bool _wasTouchingRoof;
    public bool WasTouchingRoof { get { return _wasTouchingRoof; } }

    [SerializeField]
    private bool _isGrounded;
    public bool IsGrounded { get { return _isGrounded || _isPlatformGround; } }

    [SerializeField]
    private bool _isPlatformGround;
    public bool IsPlatForm { get { return _isPlatformGround; } }

    public Transform CurrentPlatform;

    [SerializeField]
    private bool _isTouchingRoof;
    public bool IsTouchingRoof { get { return _isTouchingRoof; } }

    [SerializeField]
    private float _distanceToGround;
    public float DistanceToGround { get { return _distanceToGround; } }

    [SerializeField]
    private float _groundAngle;
    public float GroundAngle { get { return _groundAngle; } }

    PlayerJump playerJump;

    private void Start()
    {
        playerJump = gameObject.GetComponent<PlayerJump>();
    }

    void FixedUpdate()
    {
        CheckCollisions();
        CheckDistanceToGround();
    }

    private void CheckCollisions()
    {
        CheckGrounded();
        CheckRoof();
    }

    private void CheckRoof()
    {
        var colliders = Physics2D.OverlapCircleAll(RoofCheckPoint.position,
          checkRadius, WhatIsRoof);
        _isTouchingRoof = colliders.Length > 0;

        if (_wasTouchingRoof && !_isTouchingRoof)
            playerJump.canChangeGravity = true;
        _wasTouchingRoof = _isTouchingRoof;
    }

    private void CheckGrounded()
    {
        var colliders = Physics2D.OverlapCircleAll(GroundCheckPoint.position,
           checkRadius, WhatIsGround);
        _isGrounded = colliders.Length > 0;

        if (_wasGrounded && !_isGrounded)
            playerJump.canChangeGravity = true;
        _wasGrounded = _isGrounded;
    }

    private void CheckDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(GroundCheckPoint.position,
            Vector2.down, 100, WhatIsGround);

        _distanceToGround = hit.distance;
        _groundAngle = Vector2.Angle(hit.normal, new Vector2(1, 0));
    }
}

