using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public const KeyCode SHOOT_INPUT = KeyCode.Mouse0;
    public float ShootDelay;
    public float x = 1;
    public GameObject BulletPrefab;

    Animator anim;

    private Coroutine fire;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(SHOOT_INPUT) )
        {
            fire = StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        
        //anim.Play("GunShot");
        RaycastHit hit;
        Vector3 hitDir = Vector3.zero ;
        if (Physics.Raycast(transform.parent.GetChild(1).position, transform.parent.GetChild(1).forward, out hit, 100f))
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
