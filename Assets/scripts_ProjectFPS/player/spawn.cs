using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public float tempo;
    public GameObject inimigo;
    public GameObject inimigoRun;


    //local exato do spawn
    public Transform localEnemy;


    public GameObject horda;

    float conta;
    float contaRun;

   
   void NovoInimigo(){
       
     if(!GameObject.Find("Orc"))
     {
        conta += Time.deltaTime;
        contaRun += Time.deltaTime * 2;
        if(conta > tempo ){
            Instantiate(inimigo, localEnemy.position, localEnemy.rotation);
            conta = 0;
        }
        if(contaRun > tempo ){
            Instantiate(inimigoRun, localEnemy.position, localEnemy.rotation);
            contaRun = 0;
        }

     }
   }

    void Update()
    {
        NovoInimigo();
       // Novahorda();
    }




// void Novahorda(){
    //horda.SetActive (true);
       
    // if(!GameObject.Find("Orc"))
   //  {
    //    conta += Time.deltaTime;
    ///    if(conta > tempo ){
     //       Instantiate(horda);
     //       conta = 0;
      //  }
//
    // }
   //}
}   

    
