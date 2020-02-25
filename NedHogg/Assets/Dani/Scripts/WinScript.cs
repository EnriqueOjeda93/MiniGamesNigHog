using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
   public void Win1() {
        SceneManager.LoadScene("Player1Win");
   }
   public void Win2() {
        SceneManager.LoadScene("Player2Win");   
   }

   void OnTriggerEnter2D(Collider2D other)
   {
       if(other.tag == "Player"){
           Win1();
       }
       /*if(other.tag == "Player2"){
           Win2();
       }*/
   }

   public void PlayAgain() {
        SceneManager.LoadScene("Map_01");
   }

   public void Menu() {
        SceneManager.LoadScene("Menu");
   }

}
