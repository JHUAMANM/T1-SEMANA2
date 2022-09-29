using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ManagerControllerNHSem7 : MonoBehaviour
{
   public const int goku = 0;
    public const int evaluacion = 1;
    public const int sample = 2;
    public const int caballero = 3;
    public Text scoreText;
    public Text livesText;
    public Text balaText;
    public Text monedaText;
    public Text moneda1Text;
    public Text moneda2Text;
    private int score;
    private int lives;
    private int bala;
    private int monedas;
    private int monedas1;
    private int monedas2;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 3;
        bala = 100;
        PrintScoreInScreen();
        PrintLivesInScreen();
        PrintBalaInScreen();
        PrintMonedasInScreen();
        PrintMonedas1InScreen();
        PrintMonedas2InScreen();
        LoadGame();
    }
    public void SaveGame()
    {
        var filePath=Application.persistentDataPath + "/Save.dat";
        FileStream file;

        if (File.Exists(filePath))
            file=File.OpenWrite(filePath);

        else 
            file=File.Create(filePath);

        GameData data =new GameData();
        data.Score = score;
        data.Coins = monedas;
        data.Coins1 = monedas1;
        data.Coins2 = monedas2;

        BinaryFormatter bf =new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();

    }

    public void LoadGame()
    {
        var filePath=Application.persistentDataPath + "/Save.dat";
        FileStream file;

        if (File.Exists(filePath))
            file=File.OpenRead(filePath);

        else {
            Debug.LogError("No se encuentra el archivo");            
            return;
        
        }

        BinaryFormatter bf =new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        score = data.Score;
        monedas = data.Coins;
        monedas1 = data.Coins1;
        monedas2 = data.Coins2;
        PrintScoreInScreen();
        PrintMonedasInScreen();
        PrintMonedas1InScreen();
        PrintMonedas2InScreen();

    }

    public int Score(){
        return score;
    }

    public int Lives(){
        return lives;
    }
    public int Bala(){
        return bala;
    }
    public int Monedas(){
        return monedas;
    }
    public int Monedas1(){
        return monedas1;
    }
    public int Monedas2(){
        return monedas2;
    }

    public void GanarPuntos(int puntos){
        score += puntos;
        PrintScoreInScreen();
    }
    public void PerderVida(){
        lives -= 1;
        PrintLivesInScreen();
        if (lives == 2)
        {
            livesText.text = "Fin de Juego";
        }
    }
    public void PerderBalas(){
        bala -= 1;
        PrintBalaInScreen();
    }
    public void GanarMonedas(int puntos){
        monedas += puntos;
        PrintMonedasInScreen();
    }

    public void GanarMonedas1(int puntos){
        monedas1 += puntos;
        PrintMonedas1InScreen();
    }

    public void GanarMonedas2(int puntos){
        monedas2 += puntos;
        PrintMonedas2InScreen();
    }

    private void PrintScoreInScreen(){
        scoreText.text = "Puntaje: " + score;
    }

    
    private void PrintLivesInScreen(){
        livesText.text = "Vidas: " + lives;
    }
    private void PrintBalaInScreen(){
        balaText.text = "Bala: " + bala;
    }
    private void PrintMonedasInScreen(){
       monedaText.text = "Monedas: " + monedas; 
    }
    private void PrintMonedas1InScreen(){
       moneda1Text.text = "Monedas1: " + monedas1; 
    }
    private void PrintMonedas2InScreen(){
       moneda2Text.text = "Monedas2: " + monedas2; 
    }
}

