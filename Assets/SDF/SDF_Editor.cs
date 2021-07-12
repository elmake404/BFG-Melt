using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//structure : position // color // typeOfObject // parametersOfObject

public enum SDF_Type_Object
{
    sphere,
    cube
}


public enum SDF_Operation
{
    union,
    smoothUnion,
    smoothSubstract
}

public class SDF_Editor 
{
    public List<MeshFilter> boundsMeshFilters;


    // Smooth Union Spheres SDF

    private List<SDF_Behaviour> sDF_SU_0_behaviour;
    public SDF_SU_0_FILLED[] sDF_SU_0_FILLEDs { get; private set; }

    public int numOfActiveSU_0 = 0;



    // Smooth Substract Spheres SDF

    private List<SDF_Behaviour> sDF_SS_0_behaviour;
    public SDF_SS_0_FILLED[] sDF_SS_0_FILLEDs { get; private set; }

    public int numOfActiveSS_0 = 0;


    private Serialize_Structure_SDF_Storage _storage_SDF;
    private Transform _cameraTransform;

    public SDF_Editor() 
    {
        boundsMeshFilters = new List<MeshFilter>();
        sDF_SU_0_behaviour = new List<SDF_Behaviour>();
        sDF_SS_0_behaviour = new List<SDF_Behaviour>();
        sDF_SU_0_FILLEDs = new SDF_SU_0_FILLED[SDF_Propereties.numOf_SU_0];
        sDF_SS_0_FILLEDs = new SDF_SS_0_FILLED[SDF_Propereties.numOf_SS_0];
        
    }

    public SDF_Editor(Transform sdfContainer, Serialize_Structure_SDF_Storage storage_SDF, Transform transformCamera) : this()
    {
        _cameraTransform = transformCamera;
        _storage_SDF = storage_SDF;

        SDF_Behaviour[] sDF_Behaviours = sdfContainer.GetComponentsInChildren<SDF_Behaviour>();

        SetCameraTransformTo_SDFs(new List<SDF_Behaviour>(sDF_Behaviours));
        SortSDF_BehavioursByType(new List<SDF_Behaviour>(sDF_Behaviours));
        Add_SDF_Behaviour_BoundsMeshes(new List<SDF_Behaviour>(sDF_Behaviours));

        RecalculateActives_SDF_Behaviours();
    }

    public void Add_SDF_Behaviour(SDF_Behaviour behaviour)
    {
        SetCameraTransformTo_SDF(behaviour);
        SortSDF_BehaviourByType(behaviour);
        Add_SDF_Behaviour_BoundMesh(behaviour);
        RecalculateActives_SDF_Behaviours();
    }

    public void Add_SDF_Behaviour(List<SDF_Behaviour> behaviours)
    {
        for (int i = 0; i < behaviours.Count; i++)
        {
            Add_SDF_Behaviour(behaviours[i]);
        }
    }

    private void SetCameraTransformTo_SDF(SDF_Behaviour behaviour)
    {
        behaviour.SetTransformCamera(_cameraTransform);
    }

    private void SetCameraTransformTo_SDFs(List<SDF_Behaviour> behaviours)
    {
        for (int i = 0; i < behaviours.Count; i++)
        {
            SetCameraTransformTo_SDF(behaviours[i]);
        }
    }

    private void SortSDF_BehaviourByType(SDF_Behaviour behaviour)
    {
        SDF_Type_Object type = behaviour.typeObject;
        SDF_Operation operation = behaviour.operation;

        switch (type)
        {
            case SDF_Type_Object.sphere:
                switch (operation)
                {
                    case SDF_Operation.union:
                        break;
                    case SDF_Operation.smoothUnion:
                        sDF_SU_0_behaviour.Add(behaviour);
                        break;
                    case SDF_Operation.smoothSubstract:
                        sDF_SS_0_behaviour.Add(behaviour);
                        break;
                }
                ;
                break;
        }
    }

    private void SortSDF_BehavioursByType(List <SDF_Behaviour> behaviours)
    {
        for (int i = 0; i < behaviours.Count; i++)
        {
            SortSDF_BehaviourByType(behaviours[i]);
        }
    }

    private void Add_SDF_Behaviour_BoundMesh(SDF_Behaviour behaviour)
    {
        boundsMeshFilters.Add(behaviour.bound);
    }

    private void Add_SDF_Behaviour_BoundsMeshes(List<SDF_Behaviour> behaviours)
    {
        for (int i = 0; i < behaviours.Count; i++)
        {
            Add_SDF_Behaviour_BoundMesh(behaviours[i]);
        }
    }

    private void RecalculateActives_SDF_Behaviours()
    {
        numOfActiveSU_0 = sDF_SU_0_behaviour.Count;
        numOfActiveSS_0 = sDF_SS_0_behaviour.Count;
    }

    private void FillObjectStructures(List<SDF_Behaviour> behaviours)
    {
        if (behaviours.Count == 0) { return; }
        SDF_Type_Object type = behaviours[0].typeObject;
        SDF_Operation operation = behaviours[0].operation;
        
        int numIteration = SDF_Propereties.GetNumOfSelectedType_SDF(type, operation);
        int numOfBehaviours = behaviours.Count;

        for (int i = 0; i < numIteration; i++)
        {
            if (i < numOfBehaviours)
            {
                Vector3 worldPos = behaviours[i].transform.position;

                switch (type)
                {
                    case SDF_Type_Object.sphere:

                        switch (operation)
                        {
                            case SDF_Operation.union:
                                break;

                            case SDF_Operation.smoothUnion:
                                
                                sDF_SU_0_FILLEDs[i].worldPos = new Vector4(worldPos.x, worldPos.y, worldPos.z, 1);
                                sDF_SU_0_FILLEDs[i].color = behaviours[i].color;
                                SDF_SU_Sphere sphere = behaviours[i] as SDF_SU_Sphere;
                                sDF_SU_0_FILLEDs[i].parameter = sphere.radius;
                                break;

                            case SDF_Operation.smoothSubstract:
                                sDF_SS_0_FILLEDs[i].worldPos = new Vector4(worldPos.x, worldPos.y, worldPos.z, 1);
                                sDF_SS_0_FILLEDs[i].parameter = (behaviours[i] as SDF_SS_Sphere).radius;
                                break;
                        }

                        break;
                }
            }

            /*else
            {
                sDF_SU_0_FILLEDs[i].worldPos.w = -1;
            }*/
        }
    }

    public void RecalculateStructures()
    {
        FillObjectStructures(sDF_SU_0_behaviour);
        FillObjectStructures(sDF_SS_0_behaviour);
    }

    public void CombineBoundsInMeshFilter(MeshFilter meshFilter)
    {
        CombineInstance[] combineInstances = new CombineInstance[boundsMeshFilters.Count];

        for (int i = 0; i < combineInstances.Length; i++)
        {
            combineInstances[i].mesh = boundsMeshFilters[i].sharedMesh;
            combineInstances[i].transform = boundsMeshFilters[i].transform.localToWorldMatrix;
        }
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combineInstances);
    }

    public SDF_Behaviour ManuallyCreate_SDF_Behaviour(SDF_Type_Object type, SDF_Operation operation)
    {
        SDF_Behaviour instance = null;

        switch (type)
        {
            case SDF_Type_Object.sphere:
                switch (operation)
                {
                    case SDF_Operation.union:
                        break;
                    case SDF_Operation.smoothUnion:
                        instance = GameObject.Instantiate(_storage_SDF.SDF_SU_0).GetComponent<SDF_Behaviour>();
                        break;
                    case SDF_Operation.smoothSubstract:
                        instance = GameObject.Instantiate(_storage_SDF.SDF_SS_0).GetComponent<SDF_Behaviour>();
                        break;
                }
                break;

            case SDF_Type_Object.cube:
                break;
        }

        Add_SDF_Behaviour(instance);
        return instance;
    }
}
