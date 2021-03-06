﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player2;
    private GameObject playerGameObject;
    private Animator anim;
    private bool impulse = true;
    Rigidbody2D rgb;
    private int forzeArm = 300;
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player2");
        player2 = playerGameObject.GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        rgb = GetComponent<Rigidbody2D>();


        if(player2.getDirection() > 0) rgb.AddForce(new Vector2(1f,0.7f)*forzeArm*Time.fixedDeltaTime, ForceMode2D.Impulse);
            else if(player2.getDirection() < 0) rgb.AddForce(new Vector2(-1f,0.7f)*forzeArm*Time.fixedDeltaTime, ForceMode2D.Impulse);
        
    }


    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "Player" || other.gameObject.tag == "ground"){
            anim.SetBool("colision", true);
            rgb.gravityScale = 0;
            rgb.velocity = Vector2.zero;
            Invoke("Destruir", 0.2f);
        }
    }

    private void Destruir(){
        Destroy(this.gameObject);
    }

}
