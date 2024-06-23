using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaArmas : MonoBehaviour 
{
	public GameObject gun1;
	public GameObject gun2;
	public GameObject gun3;


	public Animator gunAnim;
	public Animator gun2Anim;
	public Animator gun3Anim;



    public ParticleSystem muzzleflashfogo1;
	public ParticleSystem muzzleflashfogo2;
	public ParticleSystem muzzleflashfogo3;

    public AudioSource gunAudio;
	//public AudioSource akAudio;
	//public AudioSource glockAudio;
	//public AudioSource shotgunAudio;
    public float timedrawn;


	// Use this for initialization
	void Start () {
		gunAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        
       


		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			Drawn ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			Drawn2 ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			Drawn3 ();
		}
	}
	void Drawn ()
	{
		gun1.SetActive (true);
		gun2.SetActive (false);
		gun3.SetActive(false);
		
        
     // StartCoroutine("drawn1");
		
		muzzleflashfogo1.Stop ();
	}
	void Drawn2 ()
	{
	    gun1.SetActive(false);
        gun2.SetActive(true);
        gun3.SetActive(false);
		
		
		gunAudio.enabled = false;
        //StartCoroutine("drawn1");
		
		muzzleflashfogo2.Stop ();
	}
	void Drawn3 ()
	{
		gun1.SetActive (false);
		gun2.SetActive (false);
        gun3.SetActive(true);
        
		//("drawn1");
		//
		muzzleflashfogo3.Stop ();
	}






	
    IEnumerator drawn1(){
		gunAudio.enabled = false;
        yield return new WaitForSeconds(timedrawn);
		gunAudio.enabled = true;
	}  

}
  
