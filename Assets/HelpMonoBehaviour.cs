using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMonoBehaviour : MonoBehaviour
{
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
