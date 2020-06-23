using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class EnableOutline : MonoBehaviour
{
    private Outline outline;
    private DragObject dragObject;
    bool coroutineHasStarted = false;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void startCustomCoroutine(DragObject drag)
    {
        if (!coroutineHasStarted)
        {
            dragObject = drag;
            print(dragObject.isHoveringAnObject);
            StartCoroutine(ChangeOutlineStatus());
            coroutineHasStarted = true;
        }
    }

    public IEnumerator ChangeOutlineStatus()
    {
        while (true)
        {
            if (dragObject != null && dragObject.isHoveringAnObject)
            {
                outline.enabled = true;
            }
            else
            {
                outline.enabled = false;
                dragObject = null;
                coroutineHasStarted = false;
                yield return null;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
