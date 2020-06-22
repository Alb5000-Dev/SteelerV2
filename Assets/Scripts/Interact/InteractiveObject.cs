using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private Vector3 openPos, closePos;
    [SerializeField] private Vector3 desiredPos;
    [SerializeField] private bool isOpened;
    [SerializeField] private float speed;

    private Rigidbody myRb;

    private void Start()
    {
        myRb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isOpened)
        {
            //myRb.AddForce((closePos - transform.localPosition) * speed, ForceMode.VelocityChange);
            Vector3 moveDirection = new Vector3(closePos.x, closePos.y, closePos.z);
            moveDirection = transform.TransformDirection(moveDirection);
            myRb.MovePosition(transform.position + moveDirection);
        }
        else
        {
            //myRb.AddForce((openPos - transform.localPosition) * speed, ForceMode.VelocityChange);
            Vector3 moveDirection = new Vector3(openPos.x, openPos.y, openPos.z);
            moveDirection = transform.TransformDirection(moveDirection);
            myRb.MovePosition(transform.position - moveDirection);
        }                  
    }
}
