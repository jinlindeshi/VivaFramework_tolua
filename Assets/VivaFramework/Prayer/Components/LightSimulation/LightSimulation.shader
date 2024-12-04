// Upgrade NOTE: replaced '_LightMatrix0' with 'unity_WorldToLight'
// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Prayer/Light Simulation/LightSimulation" {
	Properties {
		_Gloss ("Gloss", Range(8.0, 256)) = 20
		_DirectionForward ("DirectionForward", Vector) = (0, 0, 0, 0)
		_SpotForward ("SpotForward", Vector) = (0, 0, 0, 0)
		
		_DirectionCol ("DirectionCol", Color) = (0, 0, 0, 0)
		_PointCol ("PointCol", Color) = (0, 0, 0, 0)
		_SpotCol ("SpotCol", Color) = (0, 0, 0, 0)
		
		_DirectionIntensity ("DirectionIntensity", float) = 1
		_PointIntensity ("PointIntensity", float) = 1
		_SpotIntensity ("SpotIntensity", float) = 1
		
		_PointWorldPos ("PointWorldPos", Vector) = (0, 0, 0, 0)
		_SpotWorldPos ("SpotWorldPos", Vector) = (0, 0, 0, 0)
		
		_SpotLightAngle ("SpotLightAngle", Int) = 0
		_SpotLightRange ("SpotLightRange", Int) = 0
		_PointLightRadius ("PointLightRadius", Int) = 0
		
		_AttenFactor1 ("AttenFactor1", float) = 0
		_AttenFactor2 ("AttenFactor2", float) = 0
		_AttenFactor3 ("AttenFactor3", float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
	
		Pass {
			// Pass for other pixel lights
			Tags { "LightMode"="ForwardBase" }
			
		    
			CGPROGRAM
			
			// Apparently need to add this declaration
			#pragma multi_compile_fwdadd
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			#include "LightSimulation.cginc"
			
			float _Gloss;
			fixed4 _DirectionForward;
			fixed4 _SpotForward;
		
			fixed4 _DirectionCol;
			fixed4 _PointCol;
			fixed4 _SpotCol;
			
			float _DirectionIntensity;
			float _PointIntensity;
			float _SpotIntensity;
			
			float4 _PointWorldPos;
			float4 _SpotWorldPos;
			
			int _SpotLightAngle;
			int _SpotLightRange;
			int _PointLightRadius;
			
			float _AttenFactor1;
			float _AttenFactor2;
			float _AttenFactor3;
			
			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			
			struct v2f {
				float4 pos : SV_POSITION;
				float3 normal : NORMAL;
				float3 worldNormal : TEXCOORD0;
				float3 worldPos : TEXCOORD1;
				float3 viewDir:TEXCOORD2;	
			};
			
			v2f vert(a2v v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.viewDir = normalize(_WorldSpaceCameraPos.xyz - o.worldPos.xyz);
				
				return o;
			}
			
			fixed4 frag(v2f i) : SV_Target { 
				fixed3 worldNormal = normalize(i.worldNormal);
                fixed3 worldLightDir = normalize(-_DirectionForward.xyz);
                
				 
				///方向光
			 	fixed3 diffuse = _DirectionCol.xyz * _DirectionIntensity * max(0, dot(worldNormal, worldLightDir));
				
			 	fixed3 halfDir = normalize(worldLightDir + i.viewDir);
			 	fixed3 specular = _DirectionCol.xyz * _DirectionIntensity * pow(max(0, dot(worldNormal, halfDir)), _Gloss);				
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
				
				
				///点光源
				fixed3 pointLight = _PointCol.xyz * _PointIntensity * globalFakePointLightAttenuation(i.worldPos,
				 _PointWorldPos.xyz, _PointLightRadius, 2);
				 
				///投影光源
				
                float3 L = normalize(-_SpotForward.xyz);
                float diffuseHehe = saturate(dot(worldNormal, L));
				float2 atten = globalFakeProjLightAttenuation(i.worldPos, 
				_SpotWorldPos.xyz, -_SpotForward.xyz,
				 worldNormal, _SpotLightAngle, _SpotLightRange, _AttenFactor1, _AttenFactor3);
				 
				fixed3 spotLight = _SpotCol.xyz * _SpotIntensity * atten.x;
//				if(atten.x > _AttenFactor2)
//				{
//				    spotLight *= atten.y * diffuseHehe;
//				}
				
//				fixed3 spotLight = _SpotCol.xyz * _SpotIntensity * atten.x;
				  
//                fixed3 spotLight = (0,0,0);
				return fixed4(ambient + specular + diffuse + pointLight + spotLight, 1.0);
			}
			
			ENDCG
		}
	}
	FallBack "Specular"
}
