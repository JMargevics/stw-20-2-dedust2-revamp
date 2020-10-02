using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;

[RequireComponent(typeof(PlayerManager))]
public class Shoot : MonoBehaviour
{
    PlayerManager playerManager;

    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 5000f;
    public float hitForce = 100f;
    public Transform gunEnd;
    private Light muzzleLight;

    public Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    //private AudioSource gunAudio;
    private float nextFire;

    public AudioClip shoot;
    public AudioClip reload;

    public VisualEffect muzzleFlash;
    public GameObject bulletImpactDecal;
    public Vector2Int impactDecalTiles = new Vector2Int(2, 2);
    private List<GameObject> impactDecals = new List<GameObject>();
    private int availableDecalIndex = 0;
    private int shotsFired = 0;
    private Transform impactDecalsParent;
    public int maxDecals = 100;

    public GameObject barrelVfx;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        muzzleLight = gunEnd.GetComponent<Light>();
        muzzleLight.enabled = false;
        impactDecalsParent = new GameObject("Impact Decals").transform;
    }


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {

            nextFire = Time.time + fireRate;


            StartCoroutine(ShotEffect());


            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));


            RaycastHit hit;
            LayerMask layerMask = 1 << 0;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, layerMask))
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
                if (hit.transform.gameObject)
                {
                    Debug.Log(hit.transform.gameObject);
                    BulletImpact(hit);
                }

                Debug.DrawRay(rayOrigin, fpsCam.transform.forward);
            }
        }
    }


    private IEnumerator ShotEffect()
    {
        
        muzzleFlash.Play();
        muzzleLight.enabled = true;
        playerManager.animator.SetTrigger("Shoot");
        playerManager.audioSource.clip = shoot;
        playerManager.audioSource.Play();

        yield return shotDuration;

        yield return new WaitForSeconds(0.05f);
        muzzleLight.enabled = false;
    }

    private void BulletImpact(RaycastHit hit)
    {

        if(hit.transform.gameObject.tag == "Barrel")
        {
            Debug.Log("barrel");
            Instantiate(barrelVfx, hit.point, Quaternion.FromToRotation(Vector3.forward * (-1), hit.normal));
        }

        if (impactDecals.Count < maxDecals)
            impactDecals.Add(Instantiate(bulletImpactDecal));

        var impactDecal = impactDecals[availableDecalIndex];

        impactDecal.transform.position = hit.point;
        impactDecal.transform.rotation = Quaternion.FromToRotation(Vector3.forward * (-1), hit.normal);
        impactDecal.transform.parent = impactDecalsParent;
        DecalProjector impactProjector = impactDecal.GetComponentInChildren<DecalProjector>();

        impactProjector.uvBias = new Vector2(
            Random.Range(0, impactDecalTiles.x) * 1.0f / impactDecalTiles.x,
            Random.Range(0, impactDecalTiles.y) * 1.0f / impactDecalTiles.y
            );

        availableDecalIndex = (availableDecalIndex + 1) % maxDecals;
    }
}
