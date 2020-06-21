using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupProp : MonoBehaviour
{
    public RandomPropsList randPropList;

    public void pickupItem(GameObject go)
    {
        randPropList.propStolen(go);
        Destroy(go);
    }
}
