using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SDF_SS_0_PROP
{
    public int[] p;
    public int[] v;

    public SDF_SS_0_PROP(int numOf_p, int numOf_v)
    {
        p = new int[numOf_p];
        v = new int[numOf_v];
    }
}
