using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform point;
    public float distance;
    private Vector3 rotatedVector = Vector3.up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector3.Project(rotatedVector, Vector3.forward);
        Quaternion rotatedOffset = Quaternion.AngleAxis(1f, point.forward);
        rotatedVector = rotatedOffset * rotatedVector;
        
        Debug.DrawLine(point.position, point.position + distance * rotatedVector, Color.red, Mathf.Infinity);
    }
}
