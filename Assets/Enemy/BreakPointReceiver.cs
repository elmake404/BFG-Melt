using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPointReceiver : MonoBehaviour
{
    public GameObject particlesOndDestroy;
    public Color destruct_Color;
    public Color destruct_FresnelColor;
    public MeshRenderer meshRenderer;

    private RobotDestructionPointMap _robotDestructionPointMap;
    private MaterialPropertyBlock _materialPropertyBlock;
    private float mapToColors = 0f;

    private int _Color;
    private int _FresnelColor;
    private Color init_Color;
    private Color init_FresnelColor;

    private void Start()
    {
        _Color = Shader.PropertyToID("_Color");
        _FresnelColor = Shader.PropertyToID("_FresnelColor");
        init_Color = meshRenderer.material.GetColor(_Color);
        init_FresnelColor = meshRenderer.material.GetColor(_FresnelColor);
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void SetRobotDestructionPointMap(RobotDestructionPointMap pointMap)
    {
        _robotDestructionPointMap = pointMap;
        
    }

    public void StepBreaking()
    {
        mapToColors = Mathf.Clamp01(mapToColors + 0.4f * Time.deltaTime);
        _materialPropertyBlock.SetColor(_Color, Color.Lerp(init_Color, destruct_Color, mapToColors));
        _materialPropertyBlock.SetColor(_FresnelColor, Color.Lerp(init_FresnelColor, destruct_FresnelColor, mapToColors));
        meshRenderer.SetPropertyBlock(_materialPropertyBlock);

        if (mapToColors > 0.90f)
        {
            DestroyBreakPoint();
        }
    }

    private void DestroyBreakPoint()
    {
        _robotDestructionPointMap.AddNumOfAlreadyDestructPoints();
        InstantiateUtilities.InstantiateAndDestroydAfterTime(particlesOndDestroy, transform.position, 1.5f, typeof(HelpMonoBehaviour));
        Destroy(transform.parent.gameObject);
    }
}
