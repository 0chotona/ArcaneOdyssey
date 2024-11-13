Shader "Custom/ToonShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}
        _BumpRate("Normal Rate", Range(0,10)) = 0.5

        _BrightRate("Bright", Range(-1,1)) = 0.5

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        cull front

        CGPROGRAM

        #pragma surface surf NoLight vertex:vert noshadow noambient

        
        sampler2D _MainTex;

        struct Input
        {
            float4 color:COLOR;
        };
        void vert(inout appdata_full v)
        {
            v.vertex.xyz += v.normal.xyz;
        }
        void surf(Input IN, inout SurfaceOutput o)
        {
        
        }
        float4 LightingNoLight(SurfaceOutput s, float3 lightDir, float atten)
        {
            return _OutLineColor;
        }

        ENDCG

        cull back


        CGPROGRAM

        #pragma surface surf Toon noambient

        sampler2D _MainTex;
        sampler2D _BumpMap;

        float _BumpRate;

        float _BrightRate;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };

        

        

        
        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb + _BrightRate;
            

            fixed4 n = tex2D(_BumpMap, IN.uv_BumpMap);
            fixed3 Normal = UnpackNormal(n);

            if(_BumpRate >= 0.01)
                o.Normal = float3(Normal.x * _BumpRate, Normal.y * _BumpRate, Normal.z);

            o.Alpha = c.a;
        }

        float4 LightingToon(SurfaceOutput s, float3 lightDir, float atten)
        {
            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;

            ndotl *= 5;
            ndotl = ceil(ndotl);
            ndotl *= 0.2;

            float4 final;
            final.rgb = ndotl * s.Albedo * _LightColor0.rgb;
            final.a = s.Alpha;

            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
