using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RayMarchMaster : MonoBehaviour
{
    public Material raymarchMat;
    public Material alphaTransitionMat;
    public Material drawMesh;
    public Camera renderCamera;
    public Light directionalLight;
    public Transform sDFContainer;
    public Serialize_Structure_SDF_Storage SDF_Storage;
    private SDF_Editor sDF_Editor;
    private DistributionMelting_SDF _distributionMelting;
    public MeshFilter boundsMeshFilter;

    private void Start()
    {
        sDF_Editor = new SDF_Editor(sDFContainer, SDF_Storage, renderCamera.transform);
        _distributionMelting = new DistributionMelting_SDF(sDF_Editor);
        SDF_Propereties.InitPropereties();
        renderCamera.depthTextureMode = renderCamera.depthTextureMode | DepthTextureMode.Depth;
        
        Debug.Log(SystemInfo.graphicsDeviceType);
    }

    private void FixedUpdate()
    {
        sDF_Editor.CombineBoundsInMeshFilter(boundsMeshFilter);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        var commandBuffer = new CommandBuffer();
        
        sDF_Editor.RecalculateStructures();
        SetProperetiesInShader();
        SetDirectionalLightPosToShader();
        raymarchMat.SetMatrix("_FrustumCorners", GetFrustumCamera());
        raymarchMat.SetMatrix("_CameraInvViewMatrix", Matrix4x4.Inverse(renderCamera.projectionMatrix));
        raymarchMat.SetMatrix("_ClipToWorld", GetClipToWorld());
        raymarchMat.SetVector("_CameraWS", renderCamera.transform.position);

        RenderTexture tempRenderTexture = RenderTexture.GetTemporary(source.width/4, source.height/4, 0, source.format);
        tempRenderTexture.filterMode = FilterMode.Point;

        commandBuffer.GetTemporaryRT(SDF_Propereties.PROP_BoundsTex, tempRenderTexture.descriptor);
        commandBuffer.SetRenderTarget(SDF_Propereties.PROP_BoundsTex);
        commandBuffer.ClearRenderTarget(true, true, Color.clear);
        commandBuffer.DrawMesh(boundsMeshFilter.mesh, Matrix4x4.identity, drawMesh);


        Graphics.Blit(source, tempRenderTexture, raymarchMat);
        Shader.SetGlobalTexture("_Source", source);
        Graphics.Blit(tempRenderTexture, source, alphaTransitionMat);

        commandBuffer.ReleaseTemporaryRT(SDF_Propereties.PROP_BoundsTex);
        Graphics.ExecuteCommandBuffer(commandBuffer);
        commandBuffer.Dispose();
        
        Graphics.Blit(source, destination);
        RenderTexture.ReleaseTemporary(tempRenderTexture);
    }

    private Matrix4x4 GetFrustumCamera()
    {
        Vector3[] frustumCorners = new Vector3[4];
        renderCamera.CalculateFrustumCorners(new Rect(0,0,1,1), renderCamera.farClipPlane, renderCamera.stereoActiveEye, frustumCorners);
        
        var bottomLeft = renderCamera.transform.TransformVector(frustumCorners[0]);
        var topLeft = renderCamera.transform.TransformVector(frustumCorners[1]);
        var topRight = renderCamera.transform.TransformVector(frustumCorners[2]);
        var bottomRight = renderCamera.transform.TransformVector(frustumCorners[3]);

        Debug.DrawRay(renderCamera.transform.position, bottomLeft, Color.blue);
        Debug.DrawRay(renderCamera.transform.position, topLeft, Color.blue);
        Debug.DrawRay(renderCamera.transform.position, topRight, Color.blue);
        Debug.DrawRay(renderCamera.transform.position, bottomRight, Color.blue);


        Matrix4x4 frustumCornersArray = Matrix4x4.identity;
        frustumCornersArray.SetRow(0, bottomLeft);
        frustumCornersArray.SetRow(1, bottomRight);
        frustumCornersArray.SetRow(2, topLeft);
        frustumCornersArray.SetRow(3, topRight);

        return frustumCornersArray;
    }

    private Matrix4x4 GetClipToWorld()
    {
        Matrix4x4 p = renderCamera.projectionMatrix;

        p[2, 3] = p[3, 2] = 0.0f;
        p[3, 3] = 1.0f;

        p = Matrix4x4.Inverse(p * renderCamera.worldToCameraMatrix)
           * Matrix4x4.TRS(new Vector3(0, 0, -p[2, 2]), Quaternion.identity, Vector3.one);

        return p;
    }

    /*private Matrix4x4 FrustumCorners(Camera cam)
    {
        Transform camtr = cam.transform;

        Vector3[] frustumCorners = new Vector3[4];
        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1),
        cam.farClipPlane, cam.stereoActiveEye, frustumCorners);

        Vector3 bottomLeft = camtr.TransformVector(frustumCorners[1]);
        Vector3 topLeft = camtr.TransformVector(frustumCorners[0]);
        Vector3 bottomRight = camtr.TransformVector(frustumCorners[2]);

        Matrix4x4 frustumVectorsArray = Matrix4x4.identity;
        frustumVectorsArray.SetRow(0, bottomLeft);
        frustumVectorsArray.SetRow(1, bottomLeft + (bottomRight - bottomLeft) * 2);
        frustumVectorsArray.SetRow(2, bottomLeft + (topLeft - bottomLeft) * 2);

        return frustumVectorsArray;
    }*/

    /*private Matrix4x4 calculateFrustumCornersPerspective(Vector3 position, Quaternion orientation, float fov, float nearClipPlane, float farClipPlane, float aspect)
    {
        Vector3[] _corners = new Vector3[8];

        float fovWHalf = fov * 0.5f;

        Vector3 toRight = Vector3.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * aspect;
        Vector3 toTop = Vector3.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);
        var forward = Vector3.forward;

        Vector3 topLeft = (forward - toRight + toTop);
        float camScale = topLeft.magnitude * farClipPlane;

        topLeft.Normalize();
        topLeft *= camScale;

        Vector3 topRight = (forward + toRight + toTop);
        topRight.Normalize();
        topRight *= camScale;

        Vector3 bottomRight = (forward + toRight - toTop);
        bottomRight.Normalize();
        bottomRight *= camScale;

        Vector3 bottomLeft = (forward - toRight - toTop);
        bottomLeft.Normalize();
        bottomLeft *= camScale;

        _corners[0] = position + orientation * bottomLeft;
        _corners[1] = position + orientation * topLeft;
        _corners[2] = position + orientation * topRight;
        _corners[3] = position + orientation * bottomRight;

        topLeft = (forward - toRight + toTop);
        camScale = topLeft.magnitude * nearClipPlane;

        topLeft.Normalize();
        topLeft *= camScale;

        topRight = (forward + toRight + toTop);
        topRight.Normalize();
        topRight *= camScale;

        bottomRight = (forward + toRight - toTop);
        bottomRight.Normalize();
        bottomRight *= camScale;

        bottomLeft = (forward - toRight - toTop);
        bottomLeft.Normalize();
        bottomLeft *= camScale;

        _corners[4] = position + orientation * bottomLeft;
        _corners[5] = position + orientation * topLeft;
        _corners[6] = position + orientation * topRight;
        _corners[7] = position + orientation * bottomRight;

        Matrix4x4 frustumCornersArray = Matrix4x4.identity;
        frustumCornersArray.SetRow(0, _corners[0]);
        frustumCornersArray.SetRow(1, _corners[3]);
        frustumCornersArray.SetRow(2, _corners[1]);
        frustumCornersArray.SetRow(3, _corners[2]);

        Debug.DrawRay(renderCamera.transform.position, _corners[0], Color.blue);
        Debug.DrawRay(renderCamera.transform.position, _corners[1], Color.blue);
        Debug.DrawRay(renderCamera.transform.position, _corners[2], Color.blue);
        Debug.DrawRay(renderCamera.transform.position, _corners[3], Color.blue);

        return frustumCornersArray;
    }*/

    private void SetProperetiesInShader()
    {
        int numSU_0_prop = SDF_Propereties.SU_0_prop.p.Length;
        int numSS_0_prop = SDF_Propereties.SS_0_prop.p.Length;

        int maxNumOfProps = Mathf.Max(numSU_0_prop, numSS_0_prop);

        for (int i = 0; i < maxNumOfProps; i++)
        {
            if (i < sDF_Editor.numOfActiveSU_0)
            {
                Shader.SetGlobalVector(SDF_Propereties.SU_0_prop.p[i], sDF_Editor.sDF_SU_0_FILLEDs[i].worldPos);
                Shader.SetGlobalVector(SDF_Propereties.SU_0_prop.c[i], sDF_Editor.sDF_SU_0_FILLEDs[i].color);
                Shader.SetGlobalFloat(SDF_Propereties.SU_0_prop.v[i], sDF_Editor.sDF_SU_0_FILLEDs[i].parameter);
            }
            else
            {
                Shader.SetGlobalVector(SDF_Propereties.SU_0_prop.p[i], Vector3.one * 10);
                Shader.SetGlobalFloat(SDF_Propereties.SU_0_prop.v[i], -1f);
            }

            if (i < sDF_Editor.numOfActiveSS_0)
            {
                Shader.SetGlobalVector(SDF_Propereties.SS_0_prop.p[i], sDF_Editor.sDF_SS_0_FILLEDs[i].worldPos);
                Shader.SetGlobalFloat(SDF_Propereties.SS_0_prop.v[i], sDF_Editor.sDF_SS_0_FILLEDs[i].parameter);
            }
            else
            {
                Shader.SetGlobalVector(SDF_Propereties.SS_0_prop.p[i], Vector3.one * 10);
                Shader.SetGlobalFloat(SDF_Propereties.SS_0_prop.v[i], 0f);
            }

        }
    }

    private void SetDirectionalLightPosToShader()
    {
        Vector3 pos = directionalLight.transform.position;
        Shader.SetGlobalVector(SDF_Propereties.PROP_WorldLightPos, new Vector4(pos.x, pos.y, pos.z, 0));
    }

    public ReportMeltingAction Melt_SDF_Object(SDF_Behaviour objectToMelt)
    {
        ReportMeltingAction report = _distributionMelting.MeltingAction(objectToMelt);

        switch (report)
        {
            case ReportMeltingAction.addedMeltObject:
                
                break;
            case ReportMeltingAction.continueMelting:
                
                break;
        }

        return report;
    }

}
