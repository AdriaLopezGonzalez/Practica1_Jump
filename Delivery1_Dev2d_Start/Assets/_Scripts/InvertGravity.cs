using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertGravity : MonoBehaviour
{
    private bool isCollidingPlayer;

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

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerJump>();
        if (player)
        {
            isCollidingPlayer = false;
        }
    }

    private void CheckCollision(Rigidbody2D playerBody2D)
    {
        Debug.Log("entro a checkcol");
        if (isCollidingPlayer)
        {
            Debug.Log("canvi");
            TweakGravity(playerBody2D);
        }
    }

    private void TweakGravity(Rigidbody2D playerBody2D)
    {
        playerBody2D.gravityScale = -playerBody2D.gravityScale;
    }
}
