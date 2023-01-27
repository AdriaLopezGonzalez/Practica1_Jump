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

    private static bool _wasTouchingRoof;
    public static bool WasTouchingRoof { get { return _wasTouchingRoof; } }

    [SerializeField]
    private static bool _isGrounded;
    public static bool IsGrounded { get { return _isGrounded || _isPlatformGround; } }

    [SerializeField]
    private static bool _isPlatformGround;
    public bool IsPlatForm { get { return _isPlatformGround; } }

    public Transform CurrentPlatform;

    [SerializeField]
    private static bool _isTouchingRoof;
    public static bool IsTouchingRoof { get { return _isTouchingRoof; } }

    [SerializeField]
    private float _distanceToGround;
    public float DistanceToGround { get { return _distanceToGround; } }

    [SerializeField]
    private float _groundAngle;
    public float GroundAngle { get { return _groundAngle; } }

    void FixedUpdate()
    {
        CheckCollisions();
        CheckDistanceToGround();
    }

    private void CheckCollisions()
    {
        CheckGrounded();
        //CheckPlatformed();
        CheckRoof();
    }

    private void CheckRoof()
    {
        var colliders = Physics2D.OverlapCircleAll(RoofCheckPoint.position,
          checkRadius, WhatIsRoof);
        _isTouchingRoof = colliders.Length > 0;

        if (!_wasTouchingRoof && _isTouchingRoof)
            //SendMessage("OnLanding");
        _wasTouchingRoof = _isTouchingRoof;
    }

    private void CheckGrounded()
    {
        var colliders = Physics2D.OverlapCircleAll(GroundCheckPoint.position,
           checkRadius, WhatIsGround);
        _isGrounded = colliders.Length > 0;

        if (!_wasGrounded && _isGrounded)
            //SendMessage("OnLanding");
        _wasGrounded = _isGrounded;
    }

    //private void CheckPlatformed()
    //{
    //    var colliders = Physics2D.OverlapCircleAll(GroundCheckPoint.position,
    //       checkRadius, WhatIsPlatform);
    //    _isPlatformGround = colliders.Length > 0;
    //    if (_isPlatformGround)
    //        CurrentPlatform = colliders[0].transform;

    //    if (!_wasGrounded && _isGrounded)
    //        //SendMessage("OnLanding");
    //    _wasGrounded = _isGrounded;
    //}

    private void CheckDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(GroundCheckPoint.position,
            Vector2.down, 100, WhatIsGround);

        _distanceToGround = hit.distance;
        _groundAngle = Vector2.Angle(hit.normal, new Vector2(1, 0));
    }
}

