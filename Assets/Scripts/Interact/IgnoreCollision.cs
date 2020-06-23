using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public string [] ignoreCollisionsFrom;
    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = this.GetComponent<Collider>();
        foreach (string tag in ignoreCollisionsFrom)
        {
            if (collision.transform.tag == tag)
            {
                Physics.IgnoreCollision(collision.collider, collider);
            }
        }        
    }
}
