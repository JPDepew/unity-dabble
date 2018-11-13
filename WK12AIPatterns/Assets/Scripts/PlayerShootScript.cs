using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{


    public float speed = 5;
    private float actualSpeed;
    private Vector2 direction;
    public GameObject shootPoint;
    public GameObject bullet;
    //private AudioSource breakGlassSoundFX;

    public float coolDownPeriod = 0.5f;

    private float coolDownTimer = 0;
    private bool canShoot = true;
    // Use this for initialization
    void Start()
    {
        direction = Vector2.zero;
        //breakGlassSoundFX = GetComponent<AudioSource>();

    }

    void Update()
    {
        //coolDownTimer += Time.deltaTime;
        //if (coolDownTimer > coolDownPeriod)
        //{
        //    coolDownTimer = 0;
        //    canShoot = true;
        //}

        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            GameObject b = Instantiate(bullet);
            b.transform.position = shootPoint.transform.position;
            b.transform.rotation = shootPoint.transform.rotation;
            canShoot = false;
            StartCoroutine(CoolDown());
        }
        
    }

    IEnumerator CoolDown(){

        yield return new WaitForSeconds(1.5f);
        canShoot = true;
    }



}
