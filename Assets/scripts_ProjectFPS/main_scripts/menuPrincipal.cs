using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPrincipal : MonoBehaviour
{
[SerializeField] private string startgame;
[SerializeField] private string menu;
[SerializeField] private GameObject painelMenuInicial;
[SerializeField] private GameObject painelPause;
[SerializeField] private GameObject Opções;


public bool ispaused = true;
private bool audioON = true; 
public bool enemyActive = true;

public GameObject menudinamic;
public GameObject playerOne;
public GameObject enemys;
public GameObject hud;


public void  Start() {
   unlockmouse();
}

public void  Update() {
    
    pause();
     
    unlockmouse();

}
public void unlockmouse()
{
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}

public void jogar()
{
    SceneManager.LoadScene(startgame);
    Time.timeScale = 1;
    AudioListener.volume = 1;
    //hud.SetActive (true);
    ispaused = true;
    //if(enemyActive){
     //  enemys.SetActive (true);
    //}
}


public void menudinamico(){
    menudinamic.SetActive (false);
    playerOne.SetActive (true);
    painelPause.SetActive(false);
    ispaused = true;  
    

    painelMenuInicial.SetActive (false);
    hud.SetActive (true);
    Destroy(menudinamic);
    
    if(enemyActive){
       enemys.SetActive (true);
    }
    

}


public void pause(){

    if (Input.GetKeyDown (KeyCode.Escape) && ispaused) {
            if(painelPause.gameObject.activeSelf) {
               painelPause.gameObject.SetActive(false);
               Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
               unlockmouse();
               Time.timeScale = 1;
               AudioListener.volume = 1;
              // hud.SetActive (true);
            }
            else
            {
                painelPause.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
                Time.timeScale = 0;
                AudioListener.volume = 0;
                //hud.SetActive (false);
            }

            
        }
}

public void menuprimario (){
    SceneManager.LoadScene("menu");
}
public void AbrirOpções(){
    painelMenuInicial.SetActive(false);
    Opções.SetActive(true);
}
public void FecharOpções(){
    //Opções.SetActive(false);
    //Opções.SetActive(false);
    painelMenuInicial.SetActive(true);
}
public void SairDoJogo(){
    Debug.Log("Saiu do jogo");
    Application.Quit();
}

public void resumegame(){
    painelPause.SetActive(false);
   
}




public void resoluçao1high(){
    Screen.SetResolution (1920, 1080, true);
}
public void resoluçao2medium(){
    Screen.SetResolution (1280, 960, true);
}
public void resoluçao3low(){
    Screen.SetResolution (640, 480, true);
}


public void low(){
    QualitySettings.SetQualityLevel (0);
}
public void medium(){
    QualitySettings.SetQualityLevel (1);
}
public void high(){
    QualitySettings.SetQualityLevel (2);
}

public void volumeGame(){
    audioON =! audioON;
    if(audioON==true)
    {
        AudioListener.volume = 1;
    }
    else
        {
            AudioListener.volume = 0;
        }
    } 


}