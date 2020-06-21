using System.Collections;
using UnityEngine;

[System.Serializable]
public class GrabObjectClass
{
    public bool carrying = false;
    public bool objectDroped = true;
    public GameObject carriedObject;
    public float distance, minDist, maxDist;
    public float smooth;
    public float rayDistance;
    public float wheelAmount;
    public float rotSpeed;
    public bool throwingObject;
    public float forceSpeed;
    public Rigidbody goRB;
    public Transform carriedObjectParent;
}

public class RaycastInteractionManager : MonoBehaviour
{

    public GrabObjectClass ObjectGrab = new GrabObjectClass();


    public Transform camPos;
    public float range;

    string objectTag = "Untagged";

    GameObject interactionGO;

    public PlayerLook playerLook;

    [SerializeField]
    private string interactKey;
    public RandomPropsList randPropList;

    private void Update()
    {
        //print(interactionGO);
        checkForDrop();
        ObjectGrab.distance = Mathf.Clamp(ObjectGrab.distance, ObjectGrab.minDist, ObjectGrab.maxDist);        

        RaycastHit hit;
        if (Physics.Raycast(camPos.position, transform.TransformDirection(Vector3.forward), out hit, range))
        {            
            objectTag = hit.transform.tag;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                interactionGO = hit.transform.gameObject;
                ActionDependingOnGOTag();
            }
            else if(Input.GetAxisRaw(interactKey) > 0)
            {
                if(hit.transform.gameObject.GetComponent<propID>() != null)
                {
                    //print("item has a prop id");
                    GameObject go = hit.transform.gameObject;
                    //print(go.name);
                    randPropList.propStolen(go);
                }
            }
        }
    }

    void ActionDependingOnGOTag()
    {
        switch (objectTag)
        {
            case "CanBeGrabbed":                
                if (!ObjectGrab.carrying && ObjectGrab.objectDroped)
                {
                    ObjectGrab.carriedObject = interactionGO.gameObject;
                    ObjectGrab.carriedObjectParent = ObjectGrab.carriedObject.GetComponentInParent<Transform>();
                    StartCoroutine(GrabORReleaseObject());
                    objectTag = "Untagged";
                }
                else
                {
                    objectTag = "Untagged";
                }
                break;
            default:
                break;
        }
    }
    //For items that can be grabbed
    IEnumerator GrabORReleaseObject()
    {
        pickup();
        while (true)
        {
            if (ObjectGrab.carrying)
            {               
                carry(ObjectGrab.carriedObject);            
            }
            else if (!ObjectGrab.carrying){
                //yield return new WaitForSeconds(0.1f);
                ObjectGrab.objectDroped = true;
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    void carry(GameObject go)
    {
        ObjectGrab.goRB = go.GetComponent<Rigidbody>();
        ObjectGrab.goRB.position = Vector3.Lerp(go.transform.position, 
        Camera.main.transform.position + Camera.main.transform.forward * ObjectGrab.distance, 
        ObjectGrab.smooth * Time.fixedDeltaTime);
        

        //

        float mouseWheel = Input.GetAxis("Mouse ScrollWheel") * ObjectGrab.wheelAmount;
        if(ObjectGrab.distance > ObjectGrab.minDist && ObjectGrab.distance < ObjectGrab.maxDist)
        {
            ObjectGrab.distance += mouseWheel;
        }
        else if(ObjectGrab.distance <= ObjectGrab.minDist && mouseWheel > 0f)
        {
            ObjectGrab.distance += mouseWheel;
        }
        else if (ObjectGrab.distance >= ObjectGrab.maxDist && mouseWheel < 0f)
        {
            ObjectGrab.distance += mouseWheel;
        }

        //

        if (Input.GetKey(KeyCode.Mouse1))
        {
            ObjectGrab.goRB.angularVelocity = Vector3.zero;
            ObjectGrab.goRB.constraints = RigidbodyConstraints.FreezeRotation;
            playerLook.mouseSensitivity = 0f;
            float rotX = Input.GetAxis("Mouse X") * ObjectGrab.rotSpeed * Mathf.Deg2Rad * Time.fixedDeltaTime * 40;
            float rotY = Input.GetAxis("Mouse Y") * ObjectGrab.rotSpeed * Mathf.Deg2Rad * Time.fixedDeltaTime * 40;

            ObjectGrab.carriedObject.transform.Rotate(Vector3.up, -rotX, Space.Self);
            ObjectGrab.carriedObjectParent.Rotate(Vector3.right, rotY, Space.World);
        }
        
        else
        {
            playerLook.resetSensitivity();
            ObjectGrab.goRB.constraints = RigidbodyConstraints.None;
        }
        
    }
    void pickup()
    {
        ObjectGrab.objectDroped = false;
        ObjectGrab.distance = ObjectGrab.minDist;
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        
        if (Physics.Raycast(ray,out hit, ObjectGrab.rayDistance) && ObjectGrab.carriedObject != null)
        {
            print("GrabbingObject");
            ObjectGrab.carrying = true;
            ObjectGrab.carriedObject.GetComponent<Rigidbody>().useGravity = false;        
        }
        else
        {
            Debug.LogError("Null error : object not detected by raycast");
        }
    }
    void checkForDrop()
    {
        if ((Input.GetKeyUp(KeyCode.Mouse0) && ObjectGrab.carrying))
        {
            dropObject();
        }
        if (Input.GetKeyDown(KeyCode.Mouse2) && ObjectGrab.carrying)
        {
            dropObject();
            ObjectGrab.goRB.AddForce(transform.forward * ObjectGrab.forceSpeed, ForceMode.Impulse);
        }
    }
    void dropObject()
    {
        ObjectGrab.carrying = false;
        ObjectGrab.carriedObject.GetComponent<Rigidbody>().useGravity = true;
        //carry(null);
        ObjectGrab.carriedObject = null;
        ObjectGrab.carriedObjectParent = null;

    }    

}
