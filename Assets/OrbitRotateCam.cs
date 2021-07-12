using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitRotateCam
{
    private InputCalculations _inputCalculations;
    private Transform _cameraTransform;

    private float slowDownFactor = 2f;

    private Quaternion _yaw;
    private Quaternion _pitch;

    public OrbitRotateCam(Transform cameraTransform)
    {
        _cameraTransform = cameraTransform;
        _inputCalculations = new InputCalculations();
        _pitch = Quaternion.Euler(_cameraTransform.rotation.eulerAngles.x, 0, 0);
        _yaw = Quaternion.Euler(0, _cameraTransform.rotation.eulerAngles.y, 0);
    }


    public void UpdateOrbitRotateCamera()
    {
        _inputCalculations.UpdateCalculations();

        if (Input.GetMouseButton(0))
        {
            Vector2 deltaForce = _inputCalculations.deltaForceToRotateCam / slowDownFactor;
            _yaw = _yaw * Quaternion.Euler(0, deltaForce.x, 0);
            _pitch = _pitch * Quaternion.Euler(deltaForce.y, 0, 0);
            Quaternion targetRotation = _yaw * _pitch;

            _cameraTransform.localRotation = targetRotation;
        }
    }


    

}
