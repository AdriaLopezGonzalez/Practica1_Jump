using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpPowerUp : MonoBehaviour
{

    [SerializeField]
    private GameObject PowerUpObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player)
        {
            HighJump(other);
            Destroy (gameObject);
        }
    }

    private void HighJump(Collider2D other)
    {
        var player = other.GetComponent<PlayerJump>();
        player.JumpHeight = player.JumpHeight * 2;
    }
}
