using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCalculations
{

    [HideInInspector] public Vector2 dragDirection;
    [HideInInspector] public Vector2 deltaForceToRotateCam;

    private Vector2 prevMousePos;
    private float crossPixels;

    public InputCalculations()
    {
        crossPixels = (Screen.width + Screen.height) / 2;
    }

    public void UpdateCalculations()
    {
        if (Input.GetMouseButtonDown(0))
        {
            prevMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 mouseDelta = mousePos - prevMousePos;
            Vector2 moveDelta = mouseDelta * (360f / crossPixels);
            deltaForceToRotateCam.x = moveDelta.x;
            deltaForceToRotateCam.y = -moveDelta.y;
            prevMousePos = Input.mousePosition;
        }

        

    }
}
