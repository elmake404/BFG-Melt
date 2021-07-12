using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SDF_Behaviour : MonoBehaviour
{
    public SDF_Type_Object typeObject;
    public SDF_Operation operation;
    public Color color;
    public MeshFilter bound;
    public int numOfRequiredMeltObjects;

    protected Transform _transformCamera;

    public void SetTransformCamera(Transform transformCamera)
    {
        _transformCamera = transformCamera;
    }

    public virtual void StepMelting()
    {
        
    }
}
