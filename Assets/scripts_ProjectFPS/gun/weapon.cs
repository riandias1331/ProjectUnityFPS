using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    // Gun config
    public TextMeshProUGUI TMPROgun;
    public string namegun;
    public int damage;
    public float range;
    public int bullets = 30;
    public int mag = 3;
    private int startBullets;

    // Components
    public Camera mainCamera;
    public Animator animator;

    // HUD
    public GameObject miraCrossHair;
    public TextMeshProUGUI textoReload;

    // Timing
    public float fireRate;
    private float currentRateToFire;
    public float timeToReload;
    private float currentTimeToReload;

    // States
    public bool drawnstartbool;
    public bool canShoot = false;
    public bool walking = true;
    public bool running = false;

    private bool gamerunning;

    // Audio
    public AudioSource gunAudio;
    public AudioClip fireAudio;
    public AudioClip reloadAudio;

    // Effects
    public ParticleSystem muzzleFlashFogo;
    public ParticleSystem cartridgeBala;
    public GameObject blood;

    // Gun Switch
    public float timeDrawn;
    public GameObject gun1;
    public Animator gunAnim;
    public ParticleSystem muzzleFlashFogo1;

    // Recoil
    public float recoilForce = 10f;
    public float recoilDuration = 0.5f;
    private float currentRecoilTime = 0f;
    private Vector3 initialCameraPosition;

    void Awake()
    {
        gamerunning = false;
    }

    void Start()
    {
        currentRateToFire = fireRate;
        currentTimeToReload = timeToReload;
        startBullets = bullets;
        animator = GetComponent<Animator>();
        gunAudio = GetComponent<AudioSource>();
        initialCameraPosition = mainCamera.transform.position;

        StartCoroutine(StartDrawn1());
    }

    void Update()
    {
        if (gamerunning)
        {
            textoReload.text = bullets + "/" + mag;
            TMPROgun.text = namegun;
            currentRateToFire += Time.deltaTime;
            currentTimeToReload += Time.deltaTime;
            miraCrossHair.SetActive(true);

            if (mag <= 0)
            {
                mag = 1;
            }

            if (currentTimeToReload >= timeToReload && !drawnstartbool)
            {
                canShoot = true;
            }
            if (!canShoot)
            {
                miraCrossHair.SetActive(false);
            }
        }

        // Reset animator parameters
        animator.SetBool("idle", true);
        animator.SetBool("fire", false);
        animator.SetBool("reload", false);
        animator.SetBool("reloadplus", false);
        animator.SetBool("mira", false);
        animator.SetBool("mirafire", false);
        animator.SetBool("run", false);
        animator.SetBool("walk", false);

        // Shooting
        if (Input.GetButton("Fire1") && currentRateToFire >= fireRate && canShoot && bullets > 0)
        {
            Shoot();
        }

        // Reloading
        if (Input.GetKeyDown(KeyCode.R) && mag > 0 && bullets < 30)
        {
            Reload();
        }

        // Aiming
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Mira();
        }

        // fire with mira
        if (Input.GetKey(KeyCode.Mouse1) && Input.GetButton("Fire1")  && canShoot && currentRateToFire >= fireRate && bullets > 0)
        {
            // MiraFire();
            Shoot();
        }

        // Running
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && running)
        {
            Run();
        }

        // Walking
        if (Input.GetKey(KeyCode.W) && walking || Input.GetKey(KeyCode.A) && walking || Input.GetKey(KeyCode.S) && walking || Input.GetKey(KeyCode.D) && walking)
        {
            Walk();
        }

    }

    IEnumerator StartDrawn1()
    {
        gunAudio.enabled = false;
        canShoot = false;
        drawnstartbool = true;
        miraCrossHair.SetActive(false);
        muzzleFlashFogo.Stop();

        // Espera até que a animação de "drawn" esteja completa
        yield return new WaitForSeconds(timeDrawn);

        drawnstartbool = false;
        gunAudio.enabled = true;
        canShoot = true;
        gamerunning = true;
    }

     void Shoot()
    {
        bullets--;
        muzzleFlashFogo.Play();
        miraCrossHair.SetActive(true);
        cartridgeBala.Play();
        currentRateToFire = 0;
        gunAudio.clip = fireAudio;
        gunAudio.Play();

        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "enemy")
            {
                hit.transform.GetComponent<enemy>().lifezombie -= damage;
            }
        }

        animator.SetBool("walk", false);
        animator.SetBool("idle", false);
        animator.SetBool("fire", true);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Mira();
            animator.SetBool("fire", false);
        }
        if (Input.GetKey(KeyCode.Mouse1) && Input.GetButton("Fire1"))
        {
            MiraFire();
            animator.SetBool("fire", false);
        }

        // Adicione o recoil aqui
        currentRecoilTime = 0f;
        StartCoroutine(Recoil());
    }

    IEnumerator Recoil()
    {
        while (currentRecoilTime < recoilDuration)
        {
            currentRecoilTime += Time.deltaTime;
            float t = currentRecoilTime / recoilDuration;
            t = t * t * (3f - 2f * t); // Curva de Hermite para suavizar o movimento
            mainCamera.transform.position = initialCameraPosition + new Vector3(Random.Range(-recoilForce, recoilForce) * t, Random.Range(-recoilForce, recoilForce) * t, 0f);
            yield return null;
        }
        mainCamera.transform.position = initialCameraPosition;
    }

    void Reload()
    {
        gunAudio.clip = reloadAudio;
        gunAudio.Play();
        currentTimeToReload = 0;
        canShoot = false;
        animator.SetBool("reloadplus", true);
        mag--;
        bullets = startBullets;
        miraCrossHair.SetActive(false);
    }

    void Mira()
    {
        miraCrossHair.SetActive(false);
        animator.SetBool("mira", true);
        animator.SetBool("idle", false);
    }

    void MiraFire()
    {
        if (Input.GetKey(KeyCode.Mouse1) && Input.GetButton("Fire1"))
        {
            animator.SetBool("mirafire", true);
        }
        else
        {
            animator.SetBool("mirafire", false);
        }
    }

    void Run()
    {
        running = true;
        animator.SetBool("run", true);
        animator.SetBool("idle", false);
        miraCrossHair.SetActive(false);
        canShoot = false;
    }

    void Walk()
    {
        walking = true;
        canShoot = false;
        animator.SetBool("walk", true);
        animator.SetBool("idle", false);

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && running)
        {
            Run();
        }

        if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) && canShoot && currentRateToFire >= fireRate &&  bullets > 0)
        {
            Shoot();
            animator.SetBool("walk", false);
        }

        if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.W))
        {
            Mira();
        }
    }
}