using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProp", menuName = "Prop")]
public class PropScriptableObject : ScriptableObject
{
    public int id;
    public int value;

    public string propName;
}
