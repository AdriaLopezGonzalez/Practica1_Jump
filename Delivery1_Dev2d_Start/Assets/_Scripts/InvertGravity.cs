using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertGravity : MonoBehaviour
{
    private bool isCollidingPlayer;

    public static Action<InvertGravity> TweakGravity;

    private void OnEnable()
    {
        PlayerJump.CheckCollision += CheckCollision;
    }

    private void OnDisable()
    {
        PlayerJump.CheckCollision -= CheckCollision;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerJump>();
        if (player)
        {
            isCollidingPlayer = true;
        }
    }

    private void CheckCollision(Rigidbody2D playerBody2D)
    {
        Debug.Log("entro a la coli");
        if (isCollidingPlayer)
        {
            Debug.Log("hola");
            playerBody2D.gravityScale = -playerBody2D.gravityScale;
        }
    }
}
