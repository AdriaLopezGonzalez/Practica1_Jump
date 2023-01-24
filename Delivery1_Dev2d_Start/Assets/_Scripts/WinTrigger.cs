using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject WinObject;

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
            ShowWin();
            Destroy(gameObject);
        }
    }

    private void ShowWin()
    {
        WinObject.SetActive(true);
    }
}
