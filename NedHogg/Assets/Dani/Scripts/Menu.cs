using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("Map_01");
    }

    public void Credits(){
        SceneManager.LoadScene("Credits");
    }

    public void Exit(){
        Application.Quit();
    }
}
