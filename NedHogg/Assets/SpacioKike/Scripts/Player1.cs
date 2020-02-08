using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{

    private float move;
    [SerializeField]
    private float speed = 3;

    private Animator anim;
    private Rigidbody2D rb;
    private bool ground = true;

    
    private float timepoLanzamiento = 1f;
    private float tiempo = 0;
    [SerializeField]
    private GameObject axe;
    private int direction = 1;
    
    private bool dead;
    private Vector2 zonaDescanso = new Vector2(5, 5.5f);
    [SerializeField]
    private GameObject player;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dead = false;
    }

    void Update()
    {
        if(!dead){
            move = Input.GetAxis("Horizontal");
            
            if(move < 0) transform.localRotation = Quaternion.Euler(0, 180, 0); else if(move > 0) transform.localRotation = Quaternion.Euler(0, 0, 0); 
            
            if(move < 0) direction = -1; else if(move > 0) direction = 1; 

            if(move != 0 && !Input.GetKey("[6]")) {
                anim.SetBool("Attack2", false);
                anim.SetBool("Walk", true); 
                transform.Translate(Mathf.Abs(move)*speed*Time.deltaTime,0,0);


            }else {
            
                anim.SetBool("Walk", false);

                if(Input.GetKey("[6]") && ground) anim.SetBool("Attack2", true); else anim.SetBool("Attack2", false);
                
            }   

            if(Input.GetKey("[5]") && (Time.time > timepoLanzamiento+tiempo)){
                tiempo = Time.time;
                
                GameObject g = Instantiate(axe);
                if(direction < 0) g.transform.position = new Vector2(this.transform.position.x-0.3f, this.transform.position.y+0.3f); 
                    else if(direction > 0) g.transform.position = new Vector2(this.transform.position.x+0.3f, this.transform.position.y+0.3f); 
                
            } 
        }    
    }

    void FixedUpdate(){

        if(Input.GetKey("[4]") && ground){
            rb.AddForce(Vector2.up*200*Time.fixedDeltaTime, ForceMode2D.Impulse);
            ground = false;
            anim.SetBool("Jump", true);
        }  
    }


    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "ground"){
            ground = true;
            anim.SetBool("Jump", false);
        }
    }
    

    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "ground"){
        ground = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Axe"){
            
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

        transform.position = new Vector2(player.transform.position.x + 2f, player.transform.position.y);
    }

}
