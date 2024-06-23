using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class player : MonoBehaviour

{


  
    //public Animator Anim;
    public int lifeplayer = 100;
    public int danoEnemy = 40;
   
   
    public TextMeshProUGUI lifetext;
    public GameObject sanguetela;
    public string gameover;
    
    
    // public GameObject Gun;

    //private GameObject CameraIsPaused;
    
     /*
     */


    void Start() 
    {
        //Anim = GetComponent<Animator>();
        sanguetela.SetActive(false);   
        // Gun.SetActive(false);
       // CameraIsPaused = GetComponentInParent<FirstPersonLook>();
       

    }   


    void update ()
    {
       lifetext.text = lifeplayer.ToString();


       //morte do player
       if (lifeplayer <= 0){
        danoEnemy = 0;
        SceneManager.LoadScene("GameOver");
          StartCoroutine ("Morte");
        }

        /*
        if (Input.GetKeyDown (KeyCode.Escape)){
            CameraIsPaused.SetActive(false);
        }else{
            CameraIsPaused.SetActive(true);
        }
        */
        
        
    
 
        
    } 

    void OnTriggerEnter(Collider collider) 
    {
        //colisao do player com o enemy (sua mao)
        if (collider.gameObject.tag == "maodoinimigo"){
        lifeplayer -= danoEnemy;
        //lifeplayer -= danoEnemy;
        sanguetela.SetActive(true);
        Debug.Log("colider");
        } 

        if (lifeplayer <= 0){
        danoEnemy = 0;
        SceneManager.LoadScene("GameOver");
          StartCoroutine ("Morte");
        }

        // if (collider.gameObject.tag == "ak"){
        //     Gun.SetActive(true);
        // }              

     
       
        
    }
    IEnumerator Morte(){
       
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(3);
    }

   
}
