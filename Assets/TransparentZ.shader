// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

 Shader "Custom/Gradient" {
     Properties {
         _Color_1("Color_1", Color) = (1,1,1,1)
         _Color_2("Color_2", Color) = (1,1,1,1)
         _ColorBoost("Color Boost", Float) = 1
         _MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
     }
 
     SubShader {
         
 
         Pass {
             // Now render color channel
             Tags{ "Queue" = "Opaque" "IgnoreProjector" = "False" "RenderType" = "Opaque" }
 
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
 
             sampler2D _MainTex;
             fixed4 _Color_1;
             fixed4 _Color_2;
             float _ColorBoost;
 
             struct appdata {
                 float4 vertex : POSITION;
                 float2 uv : TEXCOORD0;
             };
 
             struct v2f {
                 float2 uv : TEXCOORD0;
                 float4 vertex : SV_POSITION;
             };
 
             v2f vert(appdata v) {
                 v2f o;
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.uv = v.uv;
                 return o;
             }
 
             fixed4 frag(v2f i) : SV_Target{
                 //fixed4 col = _Color_1 * tex2D(_MainTex, i.uv);
                 float gray = dot(tex2D(_MainTex, i.uv).rgb, float3(0.299, 0.587, 0.114));
                 fixed4 col = lerp(_Color_1, _Color_2, gray) * _ColorBoost;
                 return col;
             }
             ENDCG
         }
     }
 
     Fallback "Diffuse"
 }
 
