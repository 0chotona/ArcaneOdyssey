Shader "Custom/EnemyRinLight"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}

        _RimColor("RimColor", Color) = (1,0,1,1)
        _RimRate("Rim Rate", Range(0,10)) = 6

    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }

            CGPROGRAM

            #pragma surface surf Lambert noambient


            sampler2D _MainTex;

            float4 _Color;
            float4 _RimColor;

            float _RimRate;

            struct Input
            {
                float2 uv_MainTex;
                float2 uv_NormalMap;

                float3 viewDir;
            };

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                //o.Albedo = 0;

                float actRim;
                float rim = dot(o.Normal, IN.viewDir);
                actRim = rim;

                actRim = 1 - rim;
                actRim = pow(actRim,_RimRate);

                o.Emission = actRim * _RimColor;
                o.Alpha = c.a;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
