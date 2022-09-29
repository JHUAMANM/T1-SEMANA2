using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerNHSem7 : MonoBehaviour
{
   public SpriteRenderer srCharacter;
   public Sprite[] sprites;

   public int next = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

     public void ScoreGame()
    {
        SceneManager.LoadScene(2);
    }

      public void StartGameJugar()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGameVolver()
    {
        SceneManager.LoadScene(0);
    }


    //PARA CAMBIAR EL SPRITES
    public void ChangeCharacter(){
        srCharacter.sprite = sprites[next];
        next++;
        if(next == sprites.Length){
            next = 0;
        }
    }

    public void Salir(){
        Debug.Log("Salir del Juego...");
        Application.Quit();
    }
}
