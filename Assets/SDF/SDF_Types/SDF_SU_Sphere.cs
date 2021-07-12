using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDF_SU_Sphere : SDF_Behaviour
{
    public float radius = 1f;

    private SDF_SS_Sphere _meltingObject;
    private bool _isMeltObjectSet;
    private Vector3 _startPosMeltObject;
    private Quaternion _lerpedRotationToCamera = Quaternion.identity;

    public void SetMelting_SDF_Object(SDF_SS_Sphere meltingObject)
    {
        if (_isMeltObjectSet == true)
        {
            Debug.LogWarning("Melt Object Is Always Set!");
        }
        _isMeltObjectSet = true;
        _meltingObject = meltingObject;
        meltingObject.transform.SetParent(transform);
        Vector3 normalVectorTOCamera = transform.InverseTransformDirection(Vector3.Normalize(_transformCamera.position - transform.position));
        meltingObject.transform.position = transform.TransformPoint(normalVectorTOCamera * radius);
        meltingObject.radius = 0f;
        _startPosMeltObject = meltingObject.transform.position;
    }

    public override void StepMelting()
    {
        Vector3 directionToMeltSDF = _meltingObject.transform.position - transform.position;
        Vector3 newTargetPos = directionToMeltSDF.magnitude * Vector3.Normalize(_transformCamera.position - transform.position);
        directionToMeltSDF = Vector3.Slerp(directionToMeltSDF, newTargetPos, 4f * Time.deltaTime);
        Vector3 newPosMelt = Vector3.Lerp(transform.position + directionToMeltSDF, transform.position, Time.deltaTime);
        _meltingObject.radius = Mathf.Lerp(_meltingObject.radius, radius, Time.deltaTime);
        _meltingObject.transform.position = newPosMelt;
    }

    public bool IsMeltObjectAlwaysSet()
    {
        return _isMeltObjectSet;
    }
}
