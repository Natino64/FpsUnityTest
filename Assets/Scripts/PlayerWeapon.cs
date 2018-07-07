using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public const KeyCode SHOOT = KeyCode.Mouse0;
    public const KeyCode ADS = KeyCode.Mouse1;
    public float ShootDelay;
    public float x = 1;
    public GameObject BulletPrefab;
    public LayerMask friendlyLayer;

    Animator anim;

    private Coroutine fire;
    private float startFov;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        startFov = Camera.main.fieldOfView;
    }

    private void Update()
    {
        if (Input.GetKeyDown(SHOOT) )
        {
            fire = StartCoroutine(shoot());
        }
        if (Input.GetKeyDown(ADS))
        {
            StartCoroutine(ads(true));
        }
        if (Input.GetKeyUp(ADS))
        {
            StartCoroutine(ads(false));
        }
    }

    IEnumerator ads(bool held)
    {
        float targetFov = held ? startFov - 20f : startFov;
        if(held)
            while(Camera.main.fieldOfView > targetFov)
            {
                Camera.main.fieldOfView -= Time.deltaTime * 90;
                yield return new WaitForEndOfFrame();
            }
        else
            while (Camera.main.fieldOfView < targetFov)
            {
                Camera.main.fieldOfView += Time.deltaTime*90;
                yield return new WaitForEndOfFrame();
            }
    }

    IEnumerator shoot()
    {
        
        //anim.Play("GunShot");
        RaycastHit hit;
        Vector3 hitDir = Vector3.zero ;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f, friendlyLayer))
        {
            hitDir = hit.point - transform.GetChild(0).position;
            GameObject newBullet = Instantiate(BulletPrefab);
            newBullet.transform.position = transform.GetChild(0).position;
            newBullet.GetComponent<Rigidbody>().AddForce(hitDir*x);
            
        }
        yield return new WaitForSeconds(ShootDelay);
        fire = null;
    }

}
