using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour {
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
            Destroy(collision.gameObject);
        Destroy(this.gameObject);
    }
}
