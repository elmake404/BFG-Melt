using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class SDF_Propereties 
{
    public static int numOf_SU_0 { get => _numOf_SU_0; private set { } }
    private static int _numOf_SU_0 = 26;
    private static string _pSU_0_XXX = "_pSU_0_";
    private static string _cSU_0_XXX = "_cSU_0_";
    private static string _vSU_0_XXX = "_vSU_0_";
    public static SDF_SU_0_PROP SU_0_prop;

    public static int numOf_SS_0 { get => _numOf_SU_0; private set { } }
    private static int _numOf_SS_0 = 21;
    private static string _pSS_0_XXX = "_pSS_0_";
    private static string _vSS_0_XXX = "_vSS_0_";
    public static SDF_SS_0_PROP SS_0_prop;

    private static string _WorldLightPos = "_WorldLightPos";
    private static string _BoundsTex = "_BoundsTex";
    public static int PROP_WorldLightPos;
    public static int PROP_BoundsTex;

    public static void InitPropereties()
    {
        PROP_WorldLightPos = Shader.PropertyToID(_WorldLightPos);
        PROP_BoundsTex = Shader.PropertyToID(_BoundsTex);
        Set_SU_0_Parameters();
        Set_SS_0_Parameters();

    }

    public static int GetNumOfSelectedType_SDF(SDF_Type_Object type, SDF_Operation operation)
    {
        int result = 0;

        switch (type)
        {
            case SDF_Type_Object.sphere:
                switch (operation)
                {
                    case SDF_Operation.union:
                        break;
                    case SDF_Operation.smoothUnion:
                        result = numOf_SU_0;
                        break;
                    case SDF_Operation.smoothSubstract:
                        result = numOf_SS_0;
                        break;
                }
                break;
        }

        return result;
    }

    private static void Set_SU_0_Parameters()
    {
        SU_0_prop = new SDF_SU_0_PROP(_numOf_SU_0, _numOf_SU_0, _numOf_SU_0);

        for (int i = 0; i < _numOf_SU_0; i++)
        {
            string indexToString = i.ToString();
            SU_0_prop.p[i] = Shader.PropertyToID(_pSU_0_XXX + (indexToString.Length < 2 ? indexToString.Insert(0, "00") : indexToString.Insert(0, "0")));
            SU_0_prop.c[i] = Shader.PropertyToID(_cSU_0_XXX + (indexToString.Length < 2 ? indexToString.Insert(0, "00") : indexToString.Insert(0, "0")));
            SU_0_prop.v[i] = Shader.PropertyToID(_vSU_0_XXX + (indexToString.Length < 2 ? indexToString.Insert(0, "00") : indexToString.Insert(0, "0")));

        }
    }

    private static void Set_SS_0_Parameters()
    {
        SS_0_prop = new SDF_SS_0_PROP(numOf_SS_0, numOf_SS_0);

        for (int i = 0; i < _numOf_SS_0; i++)
        {
            string indexToString = i.ToString();
            SS_0_prop.p[i] = Shader.PropertyToID(_pSS_0_XXX + (indexToString.Length < 2 ? indexToString.Insert(0, "00") : indexToString.Insert(0, "0")));
            SS_0_prop.v[i] = Shader.PropertyToID(_vSS_0_XXX + (indexToString.Length < 2 ? indexToString.Insert(0, "00") : indexToString.Insert(0, "0")));
        }
    }
}
