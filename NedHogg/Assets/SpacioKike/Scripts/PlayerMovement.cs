using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerMovement : MonoBehaviour
{

    private float move;
    [SerializeField]
    private float speed = 3;

    [SerializeField]
    private float forceJump = 450;

    private Animator anim;
    private Rigidbody2D rb;
    private bool ground = true;

    private float timepoLanzamiento = 1f;
    private float tiempo = 0;
    [SerializeField]
    private GameObject axe;

    
    [SerializeField]
    private GameObject player2;

    private int direction = 1;

    private bool dead;
    private Vector2 zonaDescanso = new Vector2(5, 5.5f);
    [SerializeField]
    private int playerId = 0;

    private Rewired.Player player { get { return ReInput.isReady ? ReInput.players.GetPlayer(playerId) : null; } }

    private bool aaa = false;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dead = false;
    }

    void Update()
    {
        if(!dead){
            
            move = player.GetAxis("Movement");

            if(move < 0) transform.localRotation = Quaternion.Euler(0, 180, 0); else if(move > 0) transform.localRotation = Quaternion.Euler(0, 0, 0); 

            if(move < 0) direction = -1; else if(move > 0) direction = 1; 


Debug.Log(player.GetButtonDown("Jump"));
            if(move != 0 && !player.GetButtonDown("Attack1")) {
                anim.SetBool("Attack2", false);
                anim.SetBool("Walk", true); 
                
                transform.Translate(Mathf.Abs(move)*speed*Time.deltaTime,0,0);

            }else {
                anim.SetBool("Walk", false);

                if(player.GetButtonDown("Attack1") && ground) anim.SetBool("Attack2", true); else anim.SetBool("Attack2", false);
                
            }        

            if(player.GetButtonDown("Lanzar") && (Time.time > timepoLanzamiento+tiempo)){
                tiempo = Time.time;
                
                GameObject g = Instantiate(axe);
                if(direction < 0) g.transform.position = new Vector2(this.transform.position.x-0.3f, this.transform.position.y+0.3f); 
                    else if(direction > 0) g.transform.position = new Vector2(this.transform.position.x+0.3f, this.transform.position.y+0.3f); 
                
            }

            if(player.GetButtonDown("Jump") && ground){
                ground = false;
                anim.SetBool("Jump", true);
                aaa = true;
            }
        }
    }

    void FixedUpdate(){
        if(aaa){
            aaa = false;
            rb.AddForce(Vector2.up*forceJump*Time.fixedDeltaTime, ForceMode2D.Impulse);
        }  
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "ground"){
        ground = false;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.tag == "ground"){
            ground = true;
            anim.SetBool("Jump", false);
        }


        if(other.gameObject.tag == "Sword"){
            
            anim.SetBool("Reborn", false);
            // animacion die
            dead = true;
            anim.SetBool("Dead", true);
            // eseramos un poco y lo transladamos fuera de la pantlla
            Invoke("goZonaDescanso", 1f);
            // lo recolocamos en la direccion "X" de su defensa con respecto al otro jugador
            Invoke("Reborn", 4f);
        }
    }

    public int getDirection(){
        return direction;
    }

    private void goZonaDescanso(){
        transform.position = zonaDescanso;
        anim.SetBool("Dead", false);

    }

    private void Reborn(){
        dead = false;
        anim.SetBool("Reborn", true);

        transform.position = new Vector2(player2.transform.position.x - 2f, player2.transform.position.y);
    }


}
