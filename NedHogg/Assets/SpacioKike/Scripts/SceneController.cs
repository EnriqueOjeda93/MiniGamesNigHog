using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    [SerializeField]
    private GameObject pointSpawnDer;

    [SerializeField]
    private GameObject pointSpawnIzq;
    
    private float initialPosYPointDer;
    private float distance = 4f;

    void Start()
    {
        initialPosYPointDer = pointSpawnDer.transform.position.y;
    }

    void OnCollisionEnter2D(Collision2D other) {
        
        if(other.contacts[0].normal.y > 0.7f && other.gameObject.tag == "ground"){
            if(distance != (other.contacts[0].point.y-pointSpawnDer.transform.position.y)){
                pointSpawnDer.transform.position = new Vector2(pointSpawnDer.transform.position.x, other.contacts[0].point.y+initialPosYPointDer);
            }
        }
    }
}
    