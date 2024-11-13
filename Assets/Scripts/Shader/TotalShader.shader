Shader "Custom/TotalShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)

        [Space(20)]
        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor("SrcBlend Mode", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor("DstBlend Mode", Float) = 10

        [Space(20)]
        [Toggle(PANNING_TEX)]
        _PanningToggle("Use Panning", float) = 0

        _PanningTex("Panning Texture", 2D) = "white" {}
        _PanningSpeed("Panning speed (XY main texture - ZW displacement texture)", Vector) = (0,0,0,0)

        [Space(20)]
        _NoiseTex("NoiseTex", 2D) = "white" {}
        _Cut("Alpha cut", Range(0, 1)) = 0
        [HDR]_OutColor("OutColor", Color) = (1, 1, 1, 1)
        _OutThickness("OutThickness", Range(1, 5)) = 1.15

        [Space(20)]
        [Toggle(_RimLightToggle)]
        _RimLightToggle("Enable RimLight", Float) = 0
        _RimRate("Rim Rate", Range(0, 10)) = 3
        _RimColor("Rim Color", Color) = (1,1,1,1)

        [Space(20)]
        [Toggle(_ZWriteToggle)]
        _ZWriteToggle("Enable ZWrite", Float) = 0
    }

        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            Blend[_SrcFactor][_DstFactor]
            LOD 100

            Pass
            {
                ZWrite[_ZWriteToggle]

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float2 uv2 : TEXCOORD1;
                    float3 worldPos : TEXCOORD2;
                    float3 viewDir : TEXCOORD3;
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                sampler2D _PanningTex;
                sampler2D _NoiseTex;
                float4 _MainTex_ST;
                float4 _PanningTex_ST;
                float4 _PanningSpeed;
                float _PanningToggle;

                float _Cut;
                float4 _OutColor;
                float _OutThickness;

                float _RimRate;
                float4 _RimColor;
                float _RimLightToggle;

                fixed4 _Color;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.uv2 = TRANSFORM_TEX(v.uv, _PanningTex);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                    o.viewDir = normalize(UnityWorldSpaceViewDir(o.worldPos));
                    UNITY_TRANSFER_FOG(o, o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Main and panning texture sampling
                    float2 mainTexUV = i.uv + _PanningSpeed.xy * _Time.y;
                    fixed4 c = tex2D(_MainTex, mainTexUV);

                    fixed4 d;
                    if (_PanningToggle > 0)
                    {
                        float2 panningTexUV = i.uv2 + _PanningSpeed.zw * _Time.y;
                        d = tex2D(_PanningTex, panningTexUV);
                    }
                    else
                    {
                        d = fixed4(1, 1, 1, 1);
                    }

                    // Dissolve and outline effect
                    fixed4 noise = tex2D(_NoiseTex, i.uv);
                    float alpha = noise.r >= _Cut ? 1 : 0;
                    float outline = noise.r >= _Cut * _OutThickness ? 0 : 1;
                    fixed4 outlineColor = outline * _OutColor;

                    // Rim lighting
                    float rim = 0;
                    if (_RimLightToggle > 0)
                    {
                        rim = 1.0 - saturate(dot(normalize(i.viewDir), normalize(i.worldPos)));
                        rim = pow(rim, _RimRate);
                    }

                    // Final color calculation
                    fixed4 finalColor = c * d * _Color;
                    finalColor.rgb += rim * _RimColor.rgb + outlineColor.rgb;
                    finalColor.a *= alpha;

                    UNITY_APPLY_FOG(i.fogCoord, finalColor);
                    return finalColor;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}
