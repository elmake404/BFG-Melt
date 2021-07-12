Shader "Unlit/VolumeShader Test"
{
    Properties
    {
        _MainTex ("Texture", 3D) = "" {}
        _StepSize ("Step Size", float) = 0.01
        _OffsetSDFSphere ("Offset SDF Sphere", Vector) = (.0, .0, .0, 1)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Blend One OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            #define MAX_STEP_COUNT 128
            #define EPSILON 0.01f
            #define MIN_DIST 0.0 // Starting raymarching distance
			#define MAX_DIST 100.0 // The farthest raymarching distance


            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 objectVertex : TEXCOORD0;
                float3 vectorToSurface : TEXCOORD1;
            };


            sampler3D _MainTex;
            float4 _MainTex_ST;
            float4 _OffsetSDFSphere;
            float _Alpha;
            float _StepSize;

            v2f vert (appdata v)
            {
                v2f o;

                o.objectVertex = v.vertex;  
                float3 worldPosVertex = mul(unity_ObjectToWorld, v.vertex).xyz;  
                o.vectorToSurface = worldPosVertex - _WorldSpaceCameraPos; 

                o.vertex = UnityObjectToClipPos(v.vertex); 
                return o;
            }

            float sdSphere(float3 p)
			{
                float3 offset = _OffsetSDFSphere.xyz;
				return length(offset + p) - 1.0f;
			}

            float DistSphere(float3 globalVertexPos) {
                float3 marchingDirection = normalize(globalVertexPos - _WorldSpaceCameraPos);
				float depth = MIN_DIST;
				for (int i = 0; i < MAX_STEP_COUNT; i++) {
					float dist = sdSphere(_WorldSpaceCameraPos + depth * marchingDirection);
					if (dist < EPSILON) {
						return depth;
					}
					depth += dist;
					if (depth >= MAX_DIST) {
						return MAX_DIST;
					}
				}
				return MAX_DIST;
			}

            float DistSDFVolume(float3 globalVertexPos)
            {
                float distAtVertToCam = abs(distance(globalVertexPos, _WorldSpaceCameraPos));
                float3 rayDirAtCamToVert = normalize(globalVertexPos - _WorldSpaceCameraPos);
                float3 rayOrigin = _WorldSpaceCameraPos + (rayDirAtCamToVert * distAtVertToCam);
                float distAtVertToRayOrign = distance(globalVertexPos, rayOrigin);

                if(distAtVertToRayOrign > EPSILON)
                {
                    return MAX_DIST;
                }

                float3 sampledPos = rayOrigin;
                for(int i = 0; i < MAX_STEP_COUNT; i++)
                {
                    float sampledColor = tex3D(_MainTex, sampledPos + float3(0.5f, 0.5f, 0.5f));
                    if(sampledColor < 0.0f)
                    {
                        float dist = distance(sampledPos, _WorldSpaceCameraPos);
                        return dist;
                    }
                    sampledPos += rayDirAtCamToVert * _StepSize;
                }

                return MAX_DIST;
            }

            float sdfScene(float3 globalVertex)
            {
                float dSphere = DistSphere(globalVertex);
                float dVolume = DistSDFVolume(globalVertex);

                return max(-dSphere, dVolume);
            }
            
            

            fixed4 frag(v2f i) : SV_Target
            {
                float3 worldVertex = mul(unity_ObjectToWorld, i.objectVertex); 
                float dist = sdfScene(worldVertex);

                float4 color = float4(0, 0, 0, 0);

                if(dist > MAX_DIST - EPSILON)
                {
                    return color;
                }

                

                color.r = 1;
                return color;
            }
            ENDCG
        }
    }
}