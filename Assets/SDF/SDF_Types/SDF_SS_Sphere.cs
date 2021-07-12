using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDF_SS_Sphere : SDF_Behaviour
{
    public float radius = 1f;

    public void SetParentHolder(Transform holder)
    {
        transform.SetParent(holder);
    }
}
