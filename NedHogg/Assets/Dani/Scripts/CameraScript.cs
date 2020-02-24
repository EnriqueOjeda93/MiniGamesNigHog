using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform Player1;
    private Transform Player2;

    private Player1 PlayerScript1;
    private Player1 PlayerScript2;

    bool ply1Winning = false;
    bool ply2Winning = false;

    float smoothSpeed = 0.125f;

    
    void Start()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Transform>();

        PlayerScript1 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player1>();
        PlayerScript2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player1>();

        
    }

    void LateUpdate()
    { 
        if(ply1Winning){
            Vector3 desiredPosition = new Vector3(Player1.position.x, -2.4f, -8.88f);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothPosition;   
        }
            
        if(ply2Winning){
            Vector3 desiredPosition = new Vector3(Player2.position.x, -2.4f, -8.88f);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothPosition;
        }
            
    }

    public bool setPly1Winning(bool state) {
        ply1Winning = state;
    }
    public bool setPly2Winning(bool state) {
        ply2Winning = state;
    }
}
