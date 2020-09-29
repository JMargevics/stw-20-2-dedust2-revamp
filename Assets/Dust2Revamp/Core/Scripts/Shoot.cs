using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class Shoot : MonoBehaviour
{
    PlayerManager playerManager;

    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    public Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    //private AudioSource gunAudio;
    private float nextFire;

    public AudioClip shoot;
    public AudioClip reload;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            playerManager.audioSource.clip = shoot;
            playerManager.audioSource.Play();
            playerManager.animator.SetTrigger("Shoot");

            nextFire = Time.time + fireRate;


            StartCoroutine(ShotEffect());


            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));


            RaycastHit hit;


            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                //ShootableBox health = hit.collider.GetComponent<ShootableBox>();


                //if (health != null)
                //{
                //    health.Damage(gunDamage);
                //}

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
                if(hit.transform.gameObject)
                {

                }

                Debug.DrawRay(rayOrigin, fpsCam.transform.forward);
            }
        }
    }


    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();

        yield return shotDuration;
    }
}
