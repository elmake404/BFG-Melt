using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InstantiateUtilities
{
    public static void InstantiateAndDestroydAfterTime(GameObject gameObject, Vector3 instantiatedPos, float timeToDestroy, System.Type monoBehaviourFromRunCoroutine)
    {
        GameObject instance = GameObject.Instantiate(gameObject);
        instance.transform.position = instantiatedPos;
        Component addedComponent = instance.AddComponent(monoBehaviourFromRunCoroutine);
        (addedComponent as MonoBehaviour).StartCoroutine(DestroyEplasedTime(instance, timeToDestroy));
    }

    private static IEnumerator DestroyEplasedTime(GameObject gameObject, float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);
        GameObject.Destroy(gameObject);
        yield return null;
    }
}
