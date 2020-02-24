using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform player1;
    private Transform player2;

    private PlayerMovement playerScript1;
    private PlayerMovement playerScript2;

    bool ply1Winning = false;
    bool ply2Winning = false;
    [SerializeField]
    float smoothSpeed = 0.325f;

    
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Transform>();

        playerScript1 = player1.GetComponent<PlayerMovement>();
        playerScript2 = player2.GetComponent<PlayerMovement>();

        
    }

    void LateUpdate()
    { 
        if(ply1Winning){
            Vector3 desiredPosition = new Vector3(player1.position.x, -2.4f, -8.88f);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothPosition;   
        }
            
        if(ply2Winning){
            Vector3 desiredPosition = new Vector3(player2.position.x, -2.4f, -8.88f);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothPosition;
        }
            
    }

    public void setPly1Winning(bool state) {
        ply1Winning = state;
    }
    public void setPly2Winning(bool state) {
        ply2Winning = state;
    }

    public bool getPly1Winning() {
        return ply1Winning;
    }
    public bool getPly2Winning() {
        return ply2Winning;
    }
}
