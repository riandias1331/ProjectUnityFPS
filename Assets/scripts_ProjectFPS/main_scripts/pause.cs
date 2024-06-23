using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pause : MonoBehaviour
{
    public Transform pauseMenu;
    
    
    public bool  isPaused = true;


    private bool audioON = true; 


    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape) && isPaused) {
            if(pauseMenu.gameObject.activeSelf) {
               pauseMenu.gameObject.SetActive(false);

             unlockmouse();
               Time.timeScale = 1;
               AudioListener.volume = 1;
            }
            else
            {
                pauseMenu.gameObject.SetActive(true);
               Cursor.visible = false;
                Time.timeScale = 0;
                AudioListener.volume = 0;
            }

            
        }
    }
    public void resumegame(){
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }


    public void unlockmouse()
    {
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    }

    //public void volumeGame(){
         //audioON =! audioON;
         //if(audioON==true)
       // {
           // AudioListener.volume = 1;
        //}
        //else
        //{
          //  AudioListener.volume = 0;
        //}
    //} 
}
