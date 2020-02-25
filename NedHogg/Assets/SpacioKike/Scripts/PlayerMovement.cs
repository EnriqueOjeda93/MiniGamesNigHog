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

    
   // private GameObject player2;

    private int direction = 1;

    private bool dead;
    private Vector2 zonaDescanso = new Vector2(27.23f, 20f);
    [SerializeField]
    private int playerId = 0;

    private Rewired.Player player { get { return ReInput.isReady ? ReInput.players.GetPlayer(playerId) : null; } }


    [SerializeField]
    private CameraScript cameraState;

    [SerializeField]
    private GameObject paredIzq;
    [SerializeField]
    private GameObject paredDer;

    private bool initPaded = false;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        dead = false;
    }

    void FixedUpdate(){

        
        if(!dead){
            Debug.Log(initPaded);
            move = player.GetAxis("Movement");

            if(move < 0) transform.localRotation = Quaternion.Euler(0, 180, 0); else if(move > 0) transform.localRotation = Quaternion.Euler(0, 0, 0); 

            if(move < 0) direction = -1; else if(move > 0) direction = 1; 


            if(move != 0 && !player.GetButtonDown("Attack1")) {
                anim.SetBool("Attack2", false);
                anim.SetBool("Walk", true); 
                
                //transform.Translate(Mathf.Abs(move)*speed*Time.deltaTime,0,0);
                rb.AddForce(new Vector2(move*speed*Time.deltaTime, 0), ForceMode2D.Impulse);

                if(player.GetButton("Attack1") && ground) {
                    anim.SetBool("Attack3", true); 
                } else {
                    anim.SetBool("Attack3", false);
                }

                 if (rb.velocity.x > 5f || rb.velocity.x < (5f * -1))
                {
                    if (rb.velocity.x > 0)
                    {
                        rb.velocity = new Vector2(5f, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-5f, rb.velocity.y);
                    }
                }


            }else {
                anim.SetBool("Walk", false);

                if(player.GetButton("Attack1") && ground) {
                    anim.SetBool("Attack2", true); 
                    } else {
                        anim.SetBool("Attack2", false);
                    }
            }        

            if(!ground && anim.GetBool("Jump") == true && player.GetButton("Attack1")){
                anim.SetBool("Attack1", true);
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
                rb.AddForce(Vector2.up*forceJump*Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
            
            if(rb.velocity.y < -5f){
                rb.velocity = new Vector2(rb.velocity.x, -5f);
            }
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
            anim.SetBool("Attack1", false);
            anim.SetBool("Jump", false);
        }


        if(other.gameObject.tag == "Sword" && playerId == 0){
            
            paredIzq.SetActive(false);
            paredDer.SetActive(true);
            initPaded = true;
            configureDead(false, true);
        }

        if(other.gameObject.tag == "Axe" && playerId == 1){
            paredDer.SetActive(false);
            paredIzq.SetActive(true);
            initPaded = true;
            configureDead(true, false);
        }

        if(other.gameObject.tag == "Empty"){
            
            if(playerId == 1 && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().getDead()){
                initPaded = true;
                paredDer.SetActive(false);
                paredIzq.SetActive(true);
                configureDead(true, false);

            } else if(playerId == 0 && !GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerMovement>().getDead()){
                initPaded = true;
                paredDer.SetActive(true);
                paredIzq.SetActive(false);
               configureDead(false, true);

            } else {
                initPaded = false;
                configureDead(false, false);
                paredDer.SetActive(true);
                paredIzq.SetActive(true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.tag == "PaderDer" && playerId == 0 && initPaded){
            initPaded = true;
            configureDead(false, true);
        }

        if(other.gameObject.tag == "ParedIzq" && playerId == 1 && initPaded){
            configureDead(true, false);
        }
    }

    public int getDirection(){
        return direction;
    }

    public bool getDead(){
        return dead;
    }

    private void goZonaDescanso(){
        transform.position = zonaDescanso;
        anim.SetBool("Dead", false);

    }

    private void Reborn(){
        dead = false;
        anim.SetBool("Reborn", true);
        if(playerId == 0){
            transform.position = new Vector2(GameObject.FindGameObjectWithTag("Player2").GetComponent<Transform>().position.x - 7f, 0);
        } else if(playerId == 1){
            transform.position = new Vector2(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position.x + 7f, 0);
        } else {
            transform.position = this.transform.position;
        }
    }

    private void configureDead(bool winP1, bool winP2){
        cameraState.setPly1Winning(winP1);
        cameraState.setPly2Winning(winP2);

        anim.SetBool("Reborn", false);
        dead = true;
        anim.SetBool("Dead", true);
        Invoke("goZonaDescanso", 1f);
        
        Invoke("Reborn", 4f);

    }


}
