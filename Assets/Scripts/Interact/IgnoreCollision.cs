using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = this.GetComponent<Collider>();
        if(collision.transform.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, collider);
        }
    }
}
