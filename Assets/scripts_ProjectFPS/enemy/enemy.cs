using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
public int lifezombie = 100;
public Animator enemyAnim;
private NavMeshAgent nav;
private GameObject player;
public float velocidadeInimigo;
private GameObject maoinimigo;
public AudioSource enemyaudio;



private int Nmortos = 0;
//public Text contadordemortos;




    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindWithTag("player");
        maoinimigo = GameObject.FindWithTag("maodoinimigo");
        //maoinimigo.SetActive(false);
        enemyaudio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        nav.destination = player.transform.position;
        enemyAnim.SetBool("walk", true);
        enemyAnim.SetBool("atack", false);

       // contadordemortos.text = Nmortos.ToString();

        
        
        if (Vector3.Distance(transform.position, player.transform.position) < 2.1f) {
             Debug.Log("");
            maoinimigo.SetActive(true);
            nav.speed = 0;
            //enemyAnim.SetBool("atack", true);
            StartCoroutine("ataque");
        }

        if(lifezombie <= 0){
            enemyAnim.SetBool("deadzombie", true);
            
            nav.speed = 0;
            StartCoroutine("morte");
            Nmortos ++;
            
        }

    }
    IEnumerator ataque ()
    {
        
        enemyAnim.SetBool("atack", true);
        yield return new WaitForSeconds(0.2f);
        enemyAnim.SetBool("atack", false);
        yield return new WaitForSeconds(0.2f);
        nav.speed = velocidadeInimigo;   
        enemyAnim.SetBool("atack", true);
        //maoinimigo.SetActive(false);   
        
    }
    
    IEnumerator morte()
    {
       yield return new WaitForSeconds(0.7f);
       Destroy(this.gameObject);
       enemyaudio.Stop();
       
       
    }
    
}
