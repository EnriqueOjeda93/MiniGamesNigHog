using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float move;
    [SerializeField]
    private float speed = 3;

    private Animator anim;
    private Rigidbody2D rb;
    private bool ground = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");

        if(move < 0) transform.localRotation = Quaternion.Euler(0, 180, 0); else if(move > 0) transform.localRotation = Quaternion.Euler(0, 0, 0); 
        if(move != 0 && Input.GetAxis("Fire2") == 0) {
            anim.SetBool("Attack2", false);
            anim.SetBool("Walk", true); 
            transform.Translate(Mathf.Abs(move)*speed*Time.deltaTime,0,0);
            if(Input.GetAxis("Jump") != 0){
                transform.Translate(Mathf.Abs(move)*speed*2*Time.deltaTime,0,0);
                anim.SetBool("Run", true);
            }else{
                anim.SetBool("Run", false);
            }
        }else {
            anim.SetBool("Run", false);
            anim.SetBool("Walk", false);

            if(Input.GetAxis("Fire2") != 0 && ground) anim.SetBool("Attack2", true); else anim.SetBool("Attack2", false);
        }        
    }

    void FixedUpdate(){

        if(Input.GetAxis("Fire1") != 0 && ground){
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
        ground = false;
    }

}
