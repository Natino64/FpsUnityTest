using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour {
    public float MovmentSpeed;
    public float LookSpeed;
    public bool AbleToMove;

    Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        StartCoroutine(checkForMove());
    }

    private IEnumerator checkForMove()
    {
        while (true)
        {
            if (!AbleToMove)
            {
                yield return new WaitUntil(() => AbleToMove);
            }

            Vector3 moveVector = Vector3.zero;
            Vector3 rotateVector = Vector3.zero;
            Vector3 camRotate = Vector3.zero;

            if (Input.GetAxis("Horizontal") > 0)
                moveVector += transform.right;
            if (Input.GetAxis("Horizontal") < 0)
                moveVector -= transform.right;
            if (Input.GetAxis("Vertical") > 0)
                moveVector += transform.forward;
            if (Input.GetAxis("Vertical") < 0)
                moveVector -= transform.forward;

            if (Input.GetAxis("Mouse Y") > 0)
                camRotate -= Vector3.right;
            if (Input.GetAxis("Mouse Y") < 0)
                camRotate += Vector3.right;
            if (Input.GetAxis("Mouse X") > 0)
                rotateVector += transform.up;
            if (Input.GetAxis("Mouse X") < 0)
                rotateVector -= transform.up;


            
            rb.AddForce(Vector3.ClampMagnitude(moveVector, 1f) * MovmentSpeed);
            //transform.position += moveVector * MovmentSpeed;
            transform.Rotate(rotateVector * LookSpeed);
            Camera.main.transform.Rotate(camRotate * (LookSpeed/2));

            yield return new WaitForEndOfFrame();
        }
    }
}
