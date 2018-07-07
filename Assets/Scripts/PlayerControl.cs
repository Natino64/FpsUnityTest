using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public float MovmentSpeed;
    public float LookSpeed;
    public bool AbleToMove;

    void Start()
    {
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

            if (Input.GetAxis("Horizontal") > 0)
                moveVector += transform.right;
            if (Input.GetAxis("Horizontal") < 0)
                moveVector -= transform.right;
            if (Input.GetAxis("Vertical") > 0)
                moveVector += transform.forward;
            if (Input.GetAxis("Vertical") < 0)
                moveVector -= transform.forward;

            //if (Input.GetAxis("Mouse Y") > 0)
            //    rotateVector -= this.transform.right;
            //if (Input.GetAxis("Mouse Y") < 0)
            //    rotateVector += this.transform.right;
            if (Input.GetAxis("Mouse X") > 0)
                rotateVector += transform.up;
            if (Input.GetAxis("Mouse X") < 0)
                rotateVector -= transform.up;


            transform.position += moveVector * MovmentSpeed;
            transform.Rotate(rotateVector * LookSpeed);

            yield return new WaitForEndOfFrame();
        }
    }
}
