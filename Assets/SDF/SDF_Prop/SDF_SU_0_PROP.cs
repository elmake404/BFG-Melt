using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct SDF_SU_0_PROP
{
    public int[] p;
    public int[] c;
    public int[] v;

    public SDF_SU_0_PROP(int numOf_p, int numOf_c, int num_Of_v)
    {
        p = new int[numOf_p];
        c = new int[numOf_c];
        v = new int[num_Of_v];
    }
}
