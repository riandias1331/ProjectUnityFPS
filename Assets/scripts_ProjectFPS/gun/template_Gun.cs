using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class template_Gun : MonoBehaviour
{
//Gun config
		public int damage;
		public float range;
		public int bullets = 30;
		public  int mag = 3;
		private int startBullets;

	// 	
		public Camera maincamera;
		public Animator animator;
	

	// hud
		public GameObject MiraCrossHair;
		public TextMeshProUGUI textoReload;   


	//Enemy
	
		//public  int vidaenemy;
		//private int totalvidaenemy;
	
	
	//
		public float firerate;
		private float currentRateToFire;
		public float timeToReload;
		private float currentTimeToReload;

	// bool
		public bool canShoot = true;
		// public bool atirar = true;
		public bool walking = true;
		public bool running = true;
			
		
			
	
	//Audio
		public AudioSource gunAudio;
		public AudioClip fireAudio;
		public AudioClip reloadAudio;

	//Effects
		public ParticleSystem muzzleflashfogo;
		public ParticleSystem cartridgebala;
		public GameObject blood;
	

	//public recoil recoil;
 
      
   //Trocararma
		public float timedrawn;
		public GameObject gun1;
		public Animator gunAnim;
		public ParticleSystem muzzleflashfogo1;
	

	

	// Use this for initialization
	void Start () {
		currentRateToFire = firerate;
		currentTimeToReload = timeToReload;
		startBullets = bullets;
		animator = GetComponent<Animator>();
		gunAudio = GetComponent<AudioSource>();
		//totalvidaenemy = vidaenemy;
	    
       StartCoroutine("startdrawn1");
		
	}
	
	// Update is called once per frame
	void Update () {
       
	  
		// walking = true;
		// running = true;
		
	    	
	   MiraCrossHair.SetActive(true);
       textoReload.text = bullets + "/" + mag;
       muzzleflashfogo.Stop ();
       
      if (mag <= 0){
		mag =1;
	  }

		currentRateToFire += Time.deltaTime;
		currentTimeToReload += Time.deltaTime;

       
         
		if (currentTimeToReload >= timeToReload ) {
			canShoot = true;
		} 
		//if (currentTimeToReload >= timeToReload) {
		//	atirar = true;
		//}



	
	//Animations setBool
	  	// animator.SetBool("drawn", false);
		animator.SetBool("idle", true);
		animator.SetBool("fire", false);
		animator.SetBool("reload", false);
		animator.SetBool("reload++", false);
		animator.SetBool("mira", false);
		animator.SetBool("mirafire", false);
		animator.SetBool("run", false);
		animator.SetBool("walk", false);
	   



		if (Input.GetButton ("Fire1") && currentRateToFire >= firerate && canShoot  && bullets > 0 ) {
			shoot ();
		} 
		//else if (Input.GetButton ("Fire1") && bullets <= 0){
            // reload();
		//}
		if (Input.GetKeyDown (KeyCode.R) && mag > 0 && bullets < 30) {
			reload ();
		}
		if (Input.GetKey (KeyCode.Mouse1)) {
			mira ();
		}
		if (Input.GetKey (KeyCode.Mouse1) && Input.GetButton ("Fire1") && bullets > 0 ) {
			mirafiree ();
		}

		if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.W)) {
			run ();
		}
		
		if (Input.GetKey(KeyCode.W) && walking ){//!canShoot
       		andar();
        } else if(Input.GetKey(KeyCode.A) && walking){
			andar();
		} else if(Input.GetKey(KeyCode.S)&& walking ){
			andar();
		} else if(Input.GetKey(KeyCode.D) && walking ){
			andar();
		} else{
			// canShoot = true;
			// atirar = true;
		}

	}

    
	IEnumerator startdrawn1(){
		gunAudio.enabled = false;
        canShoot = false;

       yield return new WaitForSeconds(timedrawn);
	    gunAudio.enabled = true;
		canShoot = true;
    }


	void Drawn ()
	{
		gun1.SetActive (true);		
        
        StartCoroutine("startdrawn1");
		
		muzzleflashfogo1.Stop ();
	}




	void shoot (){
		bullets--;
		muzzleflashfogo.Play ();
		MiraCrossHair.SetActive (true);
		cartridgebala.Play ();
		currentRateToFire = 0;
		gunAudio.clip = fireAudio;
		gunAudio.Play ();
		
		//atirar = true;
		//canShoot = true;
			

		RaycastHit hit;
		if (Physics.Raycast (maincamera.transform.position, maincamera.transform.forward, out hit, range)) {
			//if (hit.transform.tag == "enemy") {
			//	Instantiate (blood, hit.point, Quaternion.LookRotation (hit.normal));	
			//}
			
		   if (hit.transform.tag == "enemy"){
			hit.transform.GetComponent<enemy>().lifezombie -= damage;
		   }   		
		}

        animator.SetBool("walk", false);
        animator.SetBool("idle", false);
		if (Input.GetButton("Fire1")) {
       		 animator.SetBool("fire", true);
        }

	}




	void reload(){
        gunAudio.clip = reloadAudio;
		gunAudio.Play ();
		currentTimeToReload = 0;
		// MiraCrossHair.SetActive (true);
		canShoot = false;
		//atirar = false;
		animator.SetBool("reloadplus", true);
		mag--;
		bullets = startBullets;
		MiraCrossHair.SetActive(false);


	}




	void mira (){

		MiraCrossHair.SetActive (false);
        
		if (Input.GetKey(KeyCode.Mouse1)) {
        animator.SetBool("mira", true);
        }
        else
        {
          animator.SetBool("mira", false);
        }
        animator.SetBool("fire", false);	
		animator.SetBool("idle", false);
	}




	void mirafiree () {

		currentRateToFire = 0;		
		muzzleflashfogo.Play ();

		if (Input.GetKey(KeyCode.Mouse1) && Input.GetButton("Fire1")) {
        animator.SetBool("mirafire", true);
        }
        else
        {
          animator.SetBool("mirafire", false);
        }
        animator.SetBool("mira", true);
		animator.SetBool("idle", false);

       
		gunAudio.Play ();
        
        MiraCrossHair.SetActive (false);
		animator.SetBool("idle", false);

        RaycastHit hit;

		if (Physics.Raycast (maincamera.transform.position, maincamera.transform.forward, out hit, range)) {
			
			//if (hit.transform.tag == "enemy") {
			//	Instantiate (blood, hit.point, Quaternion.LookRotation (hit.normal));
				
			//}
			
			
		   if (hit.transform.tag == "enemy"){
			hit.transform.GetComponent<enemy>().lifezombie -= damage/2;
		   }
		}
	}




	void run (){

		animator.SetBool("run", true);
		animator.SetBool("idle", false);
        currentTimeToReload = 0;
		// canShoot = false;
	    
		
		
        MiraCrossHair.SetActive (false);
		if (Input.GetButton ("Fire1") && bullets <= 0){
             reload();
			 animator.SetBool("reloadplus", true);
		}
		if (Input.GetKeyDown(KeyCode.R) && mag > 0 && bullets < 30) {
            reload();
			animator.SetBool("run", false);
		}
		
    }




	void andar (){
	
		animator.SetBool("walk", true);
		animator.SetBool("idle", false);

		if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.W)) {
			animator.SetBool("run", true);
		}
		if (Input.GetButton ("Fire1") && Input.GetKey(KeyCode.W) && currentRateToFire >= firerate && canShoot && bullets > 0 ){
			//canShoot = true;
			shoot();
			animator.SetBool("walk", false);
		}
		if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.W)){
			mira();
		}
	}
    
}	
 
