// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


//	_pXX_X_XXX  world position object
//	_pXX_X_XXX.w  is active object?
//	_cXX_X_XXX  color of object
//	_vXX_X_XXX  SDF object parameters

//	_XU_X_XXX  SDF union pointer 
//	_XSU_X_XXX  SDF smooth union pointer 
//	_XSS_X_XXX  SDF smooth substract pointer 

//	_XXX_0_XXX  ID SDF Sphere
//	_XXX_1_XXX  ID SDF Cube

//	_XXX_X_000  index

Shader "Custom/SDF"
{
	Properties
	{
		[HideInInspector] _MainTex ("Texture", 2D) = "black" {}
		_FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
		_FresnelBias ("Fresnel Bias", Float) = 0
		_FresnelScale ("Fresnel Scale", Float) = 1
		_FresnelPower ("Fresnel Power", Float) = 1
		_EnemyColor ("Enemy Color", Color) = (1,1,1,1)
		_HeatColor ("Heat Color", Color) = (1,0,0,1)
		_DiffuseShadeColor("Diffuse Shade Color", Color) = (0,0,0,1)
		_DepthInterpolate ("Depth Interpolate", Float) = 100
    }

	SubShader{

		Cull Off ZWrite Off ZTest Always
		//Tags { "Queue" = "Opaque" "RenderType" = "Opaque" "DisableBatching" = "True"}
		//Tags { "LightMode"="ForwardBase"}
		//Blend One OneMinusSrcAlpha


		Pass {
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			//#pragma target 3.0

			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"

			

			uniform float4 _pSU_0_000;
			uniform float4 _pSU_0_001;
			uniform float4 _pSU_0_002;
			uniform float4 _pSU_0_003;
			uniform float4 _pSU_0_004;
			uniform float4 _pSU_0_005;
			uniform float4 _pSU_0_006;
			uniform float4 _pSU_0_007;
			uniform float4 _pSU_0_008;
			uniform float4 _pSU_0_009;
			uniform float4 _pSU_0_010;
			uniform float4 _pSU_0_011;
			uniform float4 _pSU_0_012;
			uniform float4 _pSU_0_013;
			uniform float4 _pSU_0_014;
			uniform float4 _pSU_0_015;
			uniform float4 _pSU_0_016;
			uniform float4 _pSU_0_017;
			uniform float4 _pSU_0_018;
			uniform float4 _pSU_0_019;
			uniform float4 _pSU_0_020;
			uniform float4 _pSU_0_021;
			uniform float4 _pSU_0_022;
			uniform float4 _pSU_0_023;
			uniform float4 _pSU_0_024;
			uniform float4 _pSU_0_025;
			
			uniform float4 _cSU_0_000;
			uniform float4 _cSU_0_001;
			uniform float4 _cSU_0_002;
			uniform float4 _cSU_0_003;
			uniform float4 _cSU_0_004;
			uniform float4 _cSU_0_005;
			uniform float4 _cSU_0_006;
			uniform float4 _cSU_0_007;
			uniform float4 _cSU_0_008;
			uniform float4 _cSU_0_009;
			uniform float4 _cSU_0_010;
			uniform float4 _cSU_0_011;
			uniform float4 _cSU_0_012;
			uniform float4 _cSU_0_013;
			uniform float4 _cSU_0_014;
			uniform float4 _cSU_0_015;
			uniform float4 _cSU_0_016;
			uniform float4 _cSU_0_017;
			uniform float4 _cSU_0_018;
			uniform float4 _cSU_0_019;
			uniform float4 _cSU_0_020;
			uniform float4 _cSU_0_021;
			uniform float4 _cSU_0_022;
			uniform float4 _cSU_0_023;
			uniform float4 _cSU_0_024;
			uniform float4 _cSU_0_025;

			uniform float _vSU_0_000;
			uniform float _vSU_0_001;
			uniform float _vSU_0_002;
			uniform float _vSU_0_003;
			uniform float _vSU_0_004;
			uniform float _vSU_0_005;
			uniform float _vSU_0_006;
			uniform float _vSU_0_007;
			uniform float _vSU_0_008;
			uniform float _vSU_0_009;
			uniform float _vSU_0_010;
			uniform float _vSU_0_011;
			uniform float _vSU_0_012;
			uniform float _vSU_0_013;
			uniform float _vSU_0_014;
			uniform float _vSU_0_015;
			uniform float _vSU_0_016;
			uniform float _vSU_0_017;
			uniform float _vSU_0_018;
			uniform float _vSU_0_019;
			uniform float _vSU_0_020;
			uniform float _vSU_0_021;
			uniform float _vSU_0_022;
			uniform float _vSU_0_023;
			uniform float _vSU_0_024;
			uniform float _vSU_0_025;

			uniform float4 _pSS_0_000;
			uniform float4 _pSS_0_001;
			uniform float4 _pSS_0_002;
			uniform float4 _pSS_0_003;
			uniform float4 _pSS_0_004;
			uniform float4 _pSS_0_005;
			uniform float4 _pSS_0_006;
			uniform float4 _pSS_0_007;
			uniform float4 _pSS_0_008;
			uniform float4 _pSS_0_009;
			uniform float4 _pSS_0_010;
			uniform float4 _pSS_0_011;
			uniform float4 _pSS_0_012;
			uniform float4 _pSS_0_013;
			uniform float4 _pSS_0_014;
			uniform float4 _pSS_0_015;
			uniform float4 _pSS_0_016;
			uniform float4 _pSS_0_017;
			uniform float4 _pSS_0_018;
			uniform float4 _pSS_0_019;
			uniform float4 _pSS_0_020;

			uniform float _vSS_0_000;
			uniform float _vSS_0_001;
			uniform float _vSS_0_002;
			uniform float _vSS_0_003;
			uniform float _vSS_0_004;
			uniform float _vSS_0_005;
			uniform float _vSS_0_006;
			uniform float _vSS_0_007;
			uniform float _vSS_0_008;
			uniform float _vSS_0_009;
			uniform float _vSS_0_010;
			uniform float _vSS_0_011;
			uniform float _vSS_0_012;
			uniform float _vSS_0_013;
			uniform float _vSS_0_014;
			uniform float _vSS_0_015;
			uniform float _vSS_0_016;
			uniform float _vSS_0_017;
			uniform float _vSS_0_018;
			uniform float _vSS_0_019;
			uniform float _vSS_0_020;


			uniform float4x4 _FrustumCorners;
			uniform float4x4 _CameraInvViewMatrix;
			uniform float4x4 _ClipToWorld;
			uniform float4 _WorldLightPos;
			uniform float4 _CameraWS;
			uniform float _DepthInterpolate;


			sampler1D _FrustumCornersTex;
			sampler2D _MainTex;
			uniform float4 _MainTex_TexelSize;
			sampler2D _BoundsTex;
			uniform sampler2D _CameraDepthTexture;

			float _MinDist;
			float3 _PContactSDF;
			float4 _FresnelColor;
			float _FresnelBias;
			float _FresnelScale;
			float _FresnelPower;
			float4 _EnemyColor;
			
			float4 _DiffuseShadeColor;
			float4 _HeatColor;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;

			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				float4 scrPos : TEXCOORD2;
				float4 worldVertex : TEXCOORD3;

			};

			float2 TransformTriangleVertexToUV(float2 vertex)
            {
                float2 uv = (vertex + 1.0) * 0.5;
                return uv;
            }


			v2f vert(appdata v) {
				v2f o;

				//o.vertex = UnityObjectToClipPos(v.vertex);
				o.vertex = v.vertex * float4(2, 2, 1, 1) - float4(1, 1, 0, 0);
				float4 clip = float4((v.uv.xy * 2.0f - 1.0f) * float2(1, -1), 0.0f, 1.0f);
				o.viewDir = mul(_ClipToWorld, clip) - _WorldSpaceCameraPos;

				o.uv = v.uv;
				o.uv.y = 1.0f - o.uv.y;

				/*o.worldVertex = o.vertex;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv.xy;
				o.scrPos = ComputeScreenPos(o.vertex);

				#if UNITY_UV_STARTS_AT_TOP
					if (_MainTex_TexelSize.y < 0)
					o.uv.y = 1-o.uv.y;
				#endif


				//int frustumIndex = o.uv.x + (2 * v.uv.y);
				int frustumIndex = o.uv.x + (2 * v.uv.y);


				o.viewDir = _FrustumCorners[frustumIndex].xyz;
				*/


				return o;
			}


			// constant definition
			#define MAX_MARCHING_STEPS 32// Maximum number of raymarching
			#define MIN_DIST 0.0// Starting raymarching distance
			#define MAX_DIST 100.0// The farthest raymarching distance
			#define EPSILON 0.0001// very small amount
			#define SMOOTH_FACTOR 0.4// SDF smooth union factor
			#define SMOOTH_FACTOR_SS 0.5
			#define LIGHT_INTENSITY (1,1,1)// SDF smooth union factor

			float invLerp(float from, float to, float value)
			{
				return (value - from) / (to - from);
			}

			float opUnion(float d1, float d2)
			{
				return min(d1, d2);
			}

			float opSubtraction(float d1, float d2)
			{
				return max(-d1, d2);
			}

			float opIntersection(float d1, float d2)
			{
				return max(d1, d2);
			}

			float sminCubic( float a, float b, float k )
			{
				float h = max( k-abs(a-b), 0.0 )/k;
				float m = h*h*h*0.5;
				//float s = m*k*(1.0/3.0); 
				return (a<b) ? m : (1.0-m);
			}

			inline float opSmoothUnion(float d1, float d2, float k)
			{
				/*float h = max(k - abs(d1 - d2), 0.0);
				return min(d1, d2) - h * h * 0.25 / k;*/
				return min(d1, d2);
			}

			float opSmoothSubtraction(float d1, float d2, float k)
			{
				float h = max(k - abs(-d1 - d2), 0.0);
				return max(-d1, d2) + h * h * 0.25 / k;
				//float h = clamp( 0.5 - 0.5*(d2+d1)/k, 0.0, 1.0 );
				//return mix( d2, -d1, h ) + k*h*(1.0-h);
			}

			float opSmoothIntersection(float d1, float d2, float k)
			{
				float h = max(k - abs(d1 - d2), 0.0);
				return max(d1, d2) + h * h * 0.25 / k;
				//float h = clamp( 0.5 - 0.5*(d2-d1)/k, 0.0, 1.0 );
				//return mix( d2, d1, h ) + k*h*(1.0-h);
			}
				
			/**
			 * Rotation matrix around the X axis.
			 */
			fixed3x3 rotateX(float theta) {
				fixed c = cos(theta);
				fixed s = sin(theta);
				return fixed3x3(
					fixed3(1, 0, 0),
					fixed3(0, c, s),
					fixed3(0, -s, c)
				);
			}

			/**
			 * Rotation matrix around the Y axis.
			 */
			fixed3x3 rotateY(float theta) {
				fixed c = cos(theta);
				fixed s = sin(theta);
				return fixed3x3(
					fixed3(c, 0, -s),
					fixed3(0, 1, 0),
					fixed3(s, 0, c)
				);
			}

			/**
			 * Rotation matrix around the Z axis.
			 */
			fixed3x3 rotateZ(float theta) {
				fixed c = cos(theta);
				fixed s = sin(theta);
				return fixed3x3(
					fixed3(c, s, 0),
					fixed3(-s, c, 0),
					fixed3(0, 0, 1)
				);
			}

			// Rounded Cuboid SDF
			float sdRoundBox(float3 p, float3 b, float r)
			{
				float3 q = abs(p) - b;
				return length(max(q, 0.0)) + min(max(q.x, max(q.y, q.z)), 0.0) - r;
			}

			// Sphere SDF
			inline float sdSphere(float4 worldPos, float3 p, float r)
			{
				//return length(worldPos.xyz - p) - r;
				return length(worldPos.xyz - p) - r;
			}



			inline float GetMinDistToSS_Sphere_SDF(float3 p)
			{
				float minDist = MAX_DIST;
				float curDist;

				curDist = sdSphere(_pSS_0_000, p, _vSS_0_000);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_001, p, _vSS_0_001);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_002, p, _vSS_0_002);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_003, p, _vSS_0_003);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_004, p, _vSS_0_004);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_005, p, _vSS_0_005);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_006, p, _vSS_0_006);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_007, p, _vSS_0_007);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_008, p, _vSS_0_008);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_009, p, _vSS_0_009);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_010, p, _vSS_0_010);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_011, p, _vSS_0_011);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_012, p, _vSS_0_012);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_013, p, _vSS_0_013);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_014, p, _vSS_0_014);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_015, p, _vSS_0_015);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_016, p, _vSS_0_016);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_017, p, _vSS_0_017);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_018, p, _vSS_0_018);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_019, p, _vSS_0_019);
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSS_0_020, p, _vSS_0_020);
				minDist = min(curDist, minDist);
				
				return minDist;
			}

			inline float4 GetMinDistToSU_Sphere_SDF(float3 p)
			{
				float minDist = MAX_DIST;
				float3 pos = _pSU_0_000.xyz;
				float curDist;

				curDist = sdSphere(_pSU_0_000, p, _vSU_0_000);
				pos = lerp(_pSU_0_000.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);
				
				curDist = sdSphere(_pSU_0_001, p, _vSU_0_001);
				pos = lerp(_pSU_0_001.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_002, p, _vSU_0_002);
				pos = lerp(_pSU_0_002.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_003, p, _vSU_0_003);
				pos = lerp(_pSU_0_003.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_004, p, _vSU_0_004);
				pos = lerp(_pSU_0_004.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_005, p, _vSU_0_005);
				pos = lerp(_pSU_0_005.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_006, p, _vSU_0_006);
				pos = lerp(_pSU_0_006.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_007, p, _vSU_0_007);
				pos = lerp(_pSU_0_007.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_008, p, _vSU_0_008);
				pos = lerp(_pSU_0_008.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_009, p, _vSU_0_009);
				pos = lerp(_pSU_0_009.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_010, p, _vSU_0_010);
				pos = lerp(_pSU_0_010.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_011, p, _vSU_0_011);
				pos = lerp(_pSU_0_011.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_012, p, _vSU_0_012);
				pos = lerp(_pSU_0_012.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_013, p, _vSU_0_013);
				pos = lerp(_pSU_0_013.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_014, p, _vSU_0_014);
				pos = lerp(_pSU_0_014.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_015, p, _vSU_0_015);
				pos = lerp(_pSU_0_015.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_016, p, _vSU_0_016);
				pos = lerp(_pSU_0_016.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_017, p, _vSU_0_017);
				pos = lerp(_pSU_0_017.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_018, p, _vSU_0_018);
				pos = lerp(_pSU_0_018.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_019, p, _vSU_0_019);
				pos = lerp(_pSU_0_019.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_020, p, _vSU_0_020);
				pos = lerp(_pSU_0_020.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_021, p, _vSU_0_021);
				pos = lerp(_pSU_0_021.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_022, p, _vSU_0_022);
				pos = lerp(_pSU_0_022.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_023, p, _vSU_0_023);
				pos = lerp(_pSU_0_023.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_024, p, _vSU_0_024);
				pos = lerp(_pSU_0_024.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				curDist = sdSphere(_pSU_0_025, p, _vSU_0_025);
				pos = lerp(_pSU_0_025.xyz, pos, step(minDist, curDist));
				minDist = min(curDist, minDist);

				_MinDist = minDist;
				return float4(pos, minDist);
			}
			
			// Scene SDF
			float4x4 sdScene(float3 p) 
			{	
				float4 dist_Sphere_SU = GetMinDistToSU_Sphere_SDF(p);
				float dist_Sphere_SS = GetMinDistToSS_Sphere_SDF(p);

				float SS = opSmoothSubtraction(dist_Sphere_SS, dist_Sphere_SU.w, 0.1);

				float warmBlend = sminCubic(dist_Sphere_SS, dist_Sphere_SU.w, 0.4);

				float4x4 result = { dist_Sphere_SU.xyz, SS,
				0, 0, 0, warmBlend,
				0, 0, 0, 0,
				0, 0, 0, 0
				};
  
				//result._m13 = warmBlend;

				return result;
			}



			inline float4x4 shortestDistanceToSurface(float3 eye, float3 marchingDirection, float maxDepth) {
				float depth = 0;
				float4x4 sdResult;
				
				for (int i = 0; i < MAX_MARCHING_STEPS; i++) {
					sdResult = sdScene(eye + (depth * marchingDirection));
					float dist = sdResult._m03;

					if (dist < EPSILON * i) 
					{
						sdResult._m03 = depth;
						return sdResult;
					}

					

					if (depth >= maxDepth) {
						sdResult._m03 = MAX_DIST;
						return sdResult;
					}
					depth += dist;
				}

				sdResult._m03 = MAX_DIST;
				return sdResult;
				//return float4(sdResult.xyz, MAX_DIST);
			}


			fixed4 frag(v2f i) : SV_Target{

				fixed4 boundsMask = tex2D(_BoundsTex, i.uv.xy);

				float2 duv = i.uv;
				#if UNITY_UV_STARTS_AT_TOP
				if (_MainTex_TexelSize.y < 0)
					duv.y = 1 - duv.y;
				#endif


				float depth = LinearEyeDepth(tex2D(_CameraDepthTexture, duv).r);;
				float3 worldspace = i.viewDir * depth + _WorldSpaceCameraPos;
				depth = distance(_WorldSpaceCameraPos.xyz, worldspace);

				i.viewDir = normalize(i.viewDir);

				fixed4 texCol = fixed4(0,0,0,0);

				if(boundsMask.x < 1)
				{
					return texCol;
				}


				float4x4 result = shortestDistanceToSurface(_CameraWS.xyz, i.viewDir.xyz, min(depth, MAX_DIST));
				float dist = result._m03;
				

				if(depth < dist)
				{
					return texCol;
				}

				if (dist > MAX_DIST - EPSILON) {

					return texCol;
				}



				float3 p = _CameraWS.xyz + dist * i.viewDir.xyz;// Calculate the coordinates of the point where raymarching intersects the scene
				//_PContactSDF = p;

				float3 worldNormal = p - result._m00_m01_m02;

				float dot_norma_and_viewDir = dot(worldNormal, i.viewDir.xyz);
				float fresnel = lerp(1, _FresnelBias + _FresnelScale * pow(1 + (dot_norma_and_viewDir), _FresnelPower), step(dot_norma_and_viewDir, 0.0));

				float nl = dot(worldNormal, _WorldSpaceLightPos0.xyz);

				fixed4 surfCol = fixed4((lerp(_DiffuseShadeColor, _EnemyColor, pow(nl, 1))).rgb, 1);
				fixed4 fresnelCol = fixed4(_FresnelColor.xyz, 1);
				//fixed4 surf_and_fresnelCol = lerp(surfCol, fresnelCol, saturate(1 - fresnel));

				fixed4 surf_and_fresnelCol = lerp(surfCol, fresnelCol, saturate(1 - fresnel));
				fixed4 blendCol = lerp(_HeatColor * 3, surf_and_fresnelCol, result._m13);
				
				return blendCol;
				//return fixed4(1 - (1/depth),1 - (1 / depth),1 - (1 / depth),1);

			}
			ENDCG
		}
	}
}