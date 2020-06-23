using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GrabPropClass
{
    public bool freezeRotation;
    public bool shouldUnfreezeRotations;
    public float pickupRange;
    public float distance;
    public float maxDistance;
}
[System.Serializable]
public class GrabDrawerClass
{
    public bool freezeRotation;
    public bool shouldUnfreezeRotations;
    public float pickupRange;
    public float distance;
    public float maxDistance;
}
[System.Serializable]
public class GrabDoorClass
{
    public bool freezeRotation;
    public bool shouldUnfreezeRotations;
    public float pickupRange;
    public float distance;
    public float maxDistance;
}

[System.Serializable]
public class TagsClass
{
    public string grabPropTag = "CanBeGrabbed";
    public string drawerTag = "Drawer";
    public string doorTag = "Door";
}

public class DragObject : MonoBehaviour
{
    public GameObject p_Cam;

    public string grabKey = "Grab";

    public GrabPropClass GrabProps = new GrabPropClass();
    public GrabDrawerClass GrabDrawer = new GrabDrawerClass();
    public GrabDoorClass GrabDoor = new GrabDoorClass();
    public TagsClass Tags = new TagsClass();

    private float pickupRange = 3f;
    private float distance;
    private float maxDistanceGrab;

    private Ray playerAim;
    private GameObject objectHeld;
    private bool isObjectHeld;
    private bool tryPickupObject;

    private bool shouldUnfreezeRotations;

    public bool isHoveringAnObject = false;

    void Start()
    {
        isObjectHeld = false;
        tryPickupObject = false;
        objectHeld = null;
        isHoveringAnObject = false;
    }

    private void Update()
    {
        analyseProp();
    }
    private void FixedUpdate()
    {
        if (Input.GetButton(grabKey))
        {
            if (!isObjectHeld)
            {
                tryPickObject();
                tryPickupObject = true;
            }
            else
            {
                holdObject();
            }
        }
        else if (isObjectHeld)
        {
            dropObject();
        }
    }

    void tryPickObject()
    {        
        Ray playerAim = p_Cam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(playerAim, out hit, pickupRange))
        {
            print(hit.collider.name);
            objectHeld = hit.collider.gameObject;
            if(hit.collider.tag == Tags.grabPropTag && tryPickupObject)
            {
                isObjectHeld = true;
                objectHeld.GetComponent<Rigidbody>().useGravity = false;
                objectHeld.GetComponent<Rigidbody>().freezeRotation = GrabProps.freezeRotation;
                //
                pickupRange = GrabProps.pickupRange;
                distance = GrabProps.distance;
                maxDistanceGrab = GrabProps.maxDistance;
                shouldUnfreezeRotations = GrabProps.shouldUnfreezeRotations;
            }
            if (hit.collider.tag == Tags.drawerTag && tryPickupObject)
            {
                isObjectHeld = true;
                objectHeld.GetComponent<Rigidbody>().useGravity = true;
                objectHeld.GetComponent<Rigidbody>().freezeRotation = GrabDrawer.freezeRotation;
                //
                pickupRange = GrabDrawer.pickupRange;
                distance = GrabDrawer.distance;
                maxDistanceGrab = GrabDrawer.maxDistance;
                shouldUnfreezeRotations = GrabDrawer.shouldUnfreezeRotations;
            }
            if (hit.collider.tag == Tags.doorTag && tryPickupObject)
            {
                isObjectHeld = true;
                objectHeld.GetComponent<Rigidbody>().useGravity = true;
                objectHeld.GetComponent<Rigidbody>().freezeRotation = GrabDoor.freezeRotation;
                //
                pickupRange = GrabDoor.pickupRange;
                distance = GrabDoor.distance;
                maxDistanceGrab = GrabDoor.maxDistance;
                shouldUnfreezeRotations = GrabDoor.shouldUnfreezeRotations;
            }
        }
    }
    void holdObject()
    {
        //print("Holding object");
        Ray playerAim = p_Cam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 nextPos = p_Cam.transform.position + playerAim.direction * distance;
        Vector3 currPos = objectHeld.transform.position;

        objectHeld.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * 10;

        //print(Vector3.Distance(objectHeld.transform.position, p_Cam.gameObject.transform.position));

        if(Vector3.Distance(objectHeld.transform.position, p_Cam.gameObject.transform.position) > maxDistanceGrab)
        {
            dropObject();
        }
    }

    void dropObject()
    {
        isObjectHeld = false;
        tryPickupObject = false;
        objectHeld.GetComponent<Rigidbody>().useGravity = true;
        objectHeld.GetComponent<Rigidbody>().freezeRotation = !shouldUnfreezeRotations;
        objectHeld = null;
    }

    void analyseProp()
    {
        Ray playerAimForOutline = p_Cam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        {
            if (Physics.Raycast(playerAimForOutline, out hit, pickupRange))
            {
                if ((hit.collider.tag == Tags.grabPropTag || hit.collider.tag == Tags.doorTag || hit.collider.tag == Tags.drawerTag))
                {
                    if(hit.collider.GetComponent<EnableOutline>() != null)
                    {
                        isHoveringAnObject = true;
                        hit.collider.GetComponent<EnableOutline>().startCustomCoroutine(this.GetComponent<DragObject>());
                    }
                    else
                    {
                        Debug.LogWarning("The object you are trying to interact with does not have and EnabledOutline script attached.");
                    }
                    
                }
                else
                {
                    // Hitting something else.
                    isHoveringAnObject = false;
                }
            }
            else if (isHoveringAnObject == true)
            {
                // not anymore.
                isHoveringAnObject = false;
            }
        }
    }
}
