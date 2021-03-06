Shader "AlphaTransition"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            uniform sampler2D _Source;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 maskCol = tex2D(_MainTex, i.uv);
                
                
                /*
                {
                    fixed4 otherCol = tex2D(_Source, i.uv);
                    return otherCol;
                }
                */
                
                return lerp(maskCol, tex2D(_Source, i.uv), step(maskCol.w, 0));
            }
            ENDCG
        }
    }
}
