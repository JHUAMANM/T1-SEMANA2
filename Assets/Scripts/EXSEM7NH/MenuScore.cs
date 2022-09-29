using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScore : MonoBehaviour
{


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGameJugar()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGameVolver()
    {
        SceneManager.LoadScene(0);
    }
}
