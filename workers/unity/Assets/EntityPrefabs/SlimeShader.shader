Shader "Slime" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Speed("Speed", Range(0,10)) = 0.5
		_Amount("Extrusion Amount", Range(-1,1)) = 0.5
		_Tint("Tint", Color) = (1.0,1.0,1.0,1.0)
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert
		struct Input {
			float2 uv_MainTex;
		};

		float _Amount;
		float _Speed;
		float4 _Tint;
		
		void vert(inout appdata_full v) {
			float3 world = mul(_Object2World, v.vertex).xyz;
			float modX = asin(sin(world.x / 30.0)) * 30.0;
			float modY = asin(sin(world.y / 10.0)) * 10.0;
			v.vertex.y += _Amount * sin(_Time.x * _Speed * modX) * cos(_Time.x * _Speed * modY);
			v.vertex.x += _Amount * sin(_Time.x * _Speed * modX) * cos(_Time.x * _Speed * modY);
			v.vertex.z += _Amount * cos(_Time.x * _Speed * modX) * sin(_Time.x * _Speed * modY);
		}

		sampler2D _MainTex;
		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Tint;
		}
		ENDCG
	}
	Fallback "Diffuse"
}