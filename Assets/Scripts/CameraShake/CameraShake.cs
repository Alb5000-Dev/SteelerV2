using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public ShakeTransform2 st;
    public ShakeTransformEventData2 dataPos, dataRot;
    public float speed;
    public float idlePosFrequency;
    public float idleRotFrequency;
    public float walkingPosFrequency;
    public float walkingRotFrequency;
    public float runningPosFrequency;
    public float runningRotFrequency;

    private float walkingSpeed;
    //private float runningSpeed;


    bool isMoving = false;

    public Rigidbody playerRB;

    public Camera pCam;


    public PlayerMove pMove;

    private void Start()
    {
        walkingSpeed = pMove.walkingSpeed;
        //runningSpeed = pMove.runningSpeed;

        st.AddShakeEvent(dataPos);
        st.AddShakeEvent(dataRot);
    }
    private void Update()
    {
        pCam.gameObject.transform.localPosition = new Vector3(pCam.gameObject.transform.localPosition.x,
            pCam.gameObject.transform.localPosition.y,
            Mathf.Clamp(pCam.gameObject.transform.localPosition.z, 0f, 0f));
        if (playerRB.velocity.magnitude >= 1)
        {

            //if the player is idle
            if (pMove.movementSpeed == 0f)
            {
                if (dataRot.frequency != idleRotFrequency && dataPos.frequency != idlePosFrequency)
                {
                    dataRot.frequency = Mathf.Lerp(dataRot.frequency, idlePosFrequency, speed);
                    dataPos.frequency = Mathf.Lerp(dataPos.frequency, idleRotFrequency, speed);
                }
                else
                {
                    dataRot.frequency = idleRotFrequency;
                    dataPos.frequency = idlePosFrequency;
                }
            }
            //if the player is walking
            if (pMove.movementSpeed == walkingSpeed)
            {
                if (dataRot.frequency != walkingRotFrequency && dataPos.frequency != walkingPosFrequency)
                {
                    dataRot.frequency = Mathf.Lerp(dataRot.frequency, walkingPosFrequency, speed);
                    dataPos.frequency = Mathf.Lerp(dataPos.frequency, walkingRotFrequency, speed);
                }
                else
                {
                    dataRot.frequency = walkingRotFrequency;
                    dataPos.frequency = walkingPosFrequency;
                }
            }

            //if the player is running
            /*if (pMove.movementSpeed == runningSpeed)
            {
                if(pCam.fieldOfView != desiredFOV)
                {
                pCam.fieldOfView = Mathf.Lerp(pCam.fieldOfView, desiredFOV, speed * Time.deltaTime);
                }
                if (dataRot.frequency != runningRotFrequency && dataPos.frequency != runningPosFrequency)
                {
                    dataRot.frequency = Mathf.Lerp(dataRot.frequency, runningPosFrequency, speed);
                    dataPos.frequency = Mathf.Lerp(dataPos.frequency, runningRotFrequency, speed);
                }
                else
                {
                    dataRot.frequency = runningRotFrequency;
                    dataPos.frequency = runningPosFrequency;
                }
            }*/
        }
        else
        {
            dataRot.frequency = Mathf.Lerp(dataRot.frequency, idlePosFrequency, speed);
            dataPos.frequency = Mathf.Lerp(dataPos.frequency, idleRotFrequency, speed);
        }
    }
}