using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;
    private GameObject playerGameObject;
    private Animator anim;
    private bool impulse = true;
    Rigidbody2D rgb;
    void Start()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        player = playerGameObject.GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        rgb = GetComponent<Rigidbody2D>();


        if(player.getDirection() > 0) rgb.AddForce(new Vector2(1f,0.7f)*200*Time.fixedDeltaTime, ForceMode2D.Impulse);
            else if(player.getDirection() < 0) rgb.AddForce(new Vector2(-1f,0.7f)*200*Time.fixedDeltaTime, ForceMode2D.Impulse);
        
    }


    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "Player2" || other.gameObject.tag == "ground"){
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
