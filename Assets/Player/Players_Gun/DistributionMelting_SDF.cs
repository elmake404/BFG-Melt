using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReportMeltingAction
{
    addedMeltObject,
    continueMelting
}

public class DistributionMelting_SDF
{
    private SDF_Editor _sDF_Editor;
    private List<MeltObjectStruct> _meltAndMeltingObjects;

    public DistributionMelting_SDF(SDF_Editor sDF_Editor)
    {
        _sDF_Editor = sDF_Editor;
        _meltAndMeltingObjects = new List<MeltObjectStruct>();
    }

    public void Melt_SDF_Object(MeltObjectStruct meltAndMelting)
    {
        SDF_Behaviour meltingObject = meltAndMelting.meltingObject;
        
    }

    public ReportMeltingAction MeltingAction(SDF_Behaviour meltingObject)
    {
        ReportMeltingAction report = ReportMeltingAction.addedMeltObject;

        if (Is_SDF_ObjecAlreadyMelted(meltingObject))
        {
            if (Is_SDF_ObjectFullyHasMelt(meltingObject))
            {
                report = ReportMeltingAction.continueMelting;
                meltingObject.StepMelting();
            }
            else
            {
                //For Complex SDF_Object
                report = ReportMeltingAction.addedMeltObject;
            }
        }
        else
        {
            report = ReportMeltingAction.addedMeltObject;
            CreateAndAssignMeltStruct(meltingObject);
        }

        return report;
    }

    private bool Is_SDF_ObjecAlreadyMelted(SDF_Behaviour meltingObject)
    {
        int hashObject = meltingObject.GetHashCode();
        bool result = false;

        for (int i = 0; i < _meltAndMeltingObjects.Count; i++)
        {
            if (_meltAndMeltingObjects[i].hashMeltingObject == hashObject)
            {
                result = true;
                break;
            }
        }

        return result;
    }

    private bool Is_SDF_ObjectFullyHasMelt(SDF_Behaviour meltingObject)
    {
        SDF_Type_Object type = SDF_Type_Object.sphere;
        bool result = false;

        switch (type)
        {
            case SDF_Type_Object.sphere:
                SDF_SU_Sphere sphere = meltingObject as SDF_SU_Sphere;
                result = sphere.IsMeltObjectAlwaysSet();
                break;
        }

        return result;
    }

    private void CreateAndAssignMeltStruct(SDF_Behaviour meltingObject)
    {
        MeltObjectStruct meltObjectStruct;
        meltObjectStruct.hashMeltingObject = meltingObject.GetHashCode();
        meltObjectStruct.meltingObject = meltingObject;
        meltObjectStruct.meltObject = _sDF_Editor.ManuallyCreate_SDF_Behaviour(SDF_Type_Object.sphere, SDF_Operation.smoothSubstract) as SDF_SS_Sphere;

        SDF_Type_Object type = meltingObject.typeObject;

        switch (type)
        {
            case SDF_Type_Object.sphere:
                SDF_SU_Sphere sphere = meltingObject as SDF_SU_Sphere;
                sphere.SetMelting_SDF_Object(meltObjectStruct.meltObject);
                break;
        }

        _meltAndMeltingObjects.Add(meltObjectStruct);
    }
}

public struct MeltObjectStruct
{
    public SDF_SS_Sphere meltObject;
    public SDF_Behaviour meltingObject;
    public int hashMeltingObject;
}