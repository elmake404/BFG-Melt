using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DestructionPointID
{
    head,
    hip,
    left_ankle,
    right_ankle,
    left_knee,
    right_knee,
    left_upperArm,
    right_upperArm,
    left_wrist,
    right_wrist
}

public class RobotDestructionPointMap
{
    private SerializeStructRobotBreakPoints _serializeStructRobotBreakPoints;
    private SerializeStructBreakPoint[] _activeBrakPoints;
    private int numOfAlreadyDestructionPoints = 0;

    public RobotDestructionPointMap(SerializeStructRobotBreakPoints structRobotBreakPoints)
    {
        _serializeStructRobotBreakPoints = structRobotBreakPoints;
        _activeBrakPoints = GetCollectedDefineNumberOfRandomActivePoints(structRobotBreakPoints.points, 3);
        InstantiateBreakPointsToActiveBreakPoints();
    }

    public void AddNumOfAlreadyDestructPoints()
    {
        numOfAlreadyDestructionPoints += 1;
        if (numOfAlreadyDestructionPoints == _activeBrakPoints.Length)
        {
            Debug.Log("Enemy Destroyed");
        }
    }

    private void InstantiateBreakPointsToActiveBreakPoints()
    {
        for (int i = 0; i < _activeBrakPoints.Length; i++)
        {
            GameObject instance = GameObject.Instantiate(_serializeStructRobotBreakPoints.destructPointPrefab, _activeBrakPoints[i].pointTransform);
            BreakPointReceiver breakPointReceiver = instance.transform.GetChild(0).GetComponent<BreakPointReceiver>();
            breakPointReceiver.SetRobotDestructionPointMap(this);
            _activeBrakPoints[i].instancedBreakPoint = instance;
            _activeBrakPoints[i].breakPointReceiver = breakPointReceiver;
        }
    }

    private SerializeStructBreakPoint[] GetCollectedDefineNumberOfRandomActivePoints(SerializeStructBreakPoint[] serializeStructBreakPoints, int numOfDefinePoints)
    {
        SerializeStructBreakPoint[] mass = new SerializeStructBreakPoint[numOfDefinePoints];
        int numOfTotalElements = serializeStructBreakPoints.Length;

        string indexes = "";

        for (int i = 0; i < numOfTotalElements; i++)
        {
            indexes += i;
        }

        indexes = GetRecursiveDefineRandomNumbers(indexes, numOfDefinePoints);
        Debug.Log(indexes[2]);

        for (int i = 0; i < numOfDefinePoints; i++)
        {
            mass[i] = (serializeStructBreakPoints[indexes[i] - '0']);
        }

        return mass;
    }


    private string GetRecursiveDefineRandomNumbers(string numbers, int targetNum)
    {
        if (numbers.Length - 1 < targetNum)
        {
            return numbers;
        }
        else
        {
            return GetRecursiveDefineRandomNumbers(numbers.Remove(Random.Range(0, numbers.Length - 1), 1), targetNum);
        }

        
    }
}

[System.Serializable]
public struct SerializeStructRobotBreakPoints
{
    public GameObject destructPointPrefab;
    public SerializeStructBreakPoint[] points;
}

[System.Serializable]
public struct SerializeStructBreakPoint
{
    public string pointName;
    public DestructionPointID destructionPointID;
    public Transform pointTransform;
    [HideInInspector] public GameObject instancedBreakPoint;
    [HideInInspector] public BreakPointReceiver breakPointReceiver;
}