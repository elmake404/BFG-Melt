using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GunSolver gunSolver;
    private OrbitRotateCam _orbitRotateCam;

    private void Start()
    {
        _orbitRotateCam = new OrbitRotateCam(transform);
    }

    private void Update()
    {
        _orbitRotateCam.UpdateOrbitRotateCamera();
        gunSolver.UpdateSolver();

    }
}
