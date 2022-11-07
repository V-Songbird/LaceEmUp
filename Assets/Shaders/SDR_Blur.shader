// Made with Amplify Shader Editor v1.9.0.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SDR_Blur"
{
	Properties
	{
		_BlurStrength("BlurStrength", Range( 0 , 1)) = 0

	}

	SubShader
	{
		LOD 0

		Cull Off
		ZWrite Off
		ZTest Always
		GrabPass{ }

		Pass
		{
			CGPROGRAM

			#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
			#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
			#else
			#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
			#endif


			#pragma vertex Vert
			#pragma fragment Frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			#define ASE_NEEDS_FRAG_SCREEN_POSITION_NORMALIZED

		
			struct ASEAttributesDefault
			{
				float3 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				
			};

			struct ASEVaryingsDefault
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float2 texcoordStereo : TEXCOORD1;
			#if STEREO_INSTANCING_ENABLED
				uint stereoTargetEyeIndex : SV_RenderTargetArrayIndex;
			#endif
				
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
			uniform float _BlurStrength;


			
			float2 TransformTriangleVertexToUV (float2 vertex)
			{
				float2 uv = (vertex + 1.0) * 0.5;
				return uv;
			}

			ASEVaryingsDefault Vert( ASEAttributesDefault v  )
			{
				ASEVaryingsDefault o;
				o.vertex = float4(v.vertex.xy, 0.0, 1.0);
				o.texcoord = TransformTriangleVertexToUV (v.vertex.xy);
#if UNITY_UV_STARTS_AT_TOP
				o.texcoord = o.texcoord * float2(1.0, -1.0) + float2(0.0, 1.0);
#endif
				o.texcoordStereo = TransformStereoScreenSpaceTex (o.texcoord, 1.0);

				v.texcoord = o.texcoordStereo;
				float4 ase_ppsScreenPosVertexNorm = float4(o.texcoordStereo,0,1);

				

				return o;
			}

			float4 Frag (ASEVaryingsDefault i  ) : SV_Target
			{
				float4 ase_ppsScreenPosFragNorm = float4(i.texcoordStereo,0,1);

				float2 appendResult7 = (float2(ase_ppsScreenPosFragNorm.x , ase_ppsScreenPosFragNorm.y));
				float4 screenColor10 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( ( _BlurStrength * float2( 1,0 ) ) + appendResult7 ));
				float4 screenColor11 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,appendResult7);
				float4 screenColor12 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( appendResult7 + ( _BlurStrength * float2( 0,1 ) ) ));
				

				float4 color = ( ( screenColor10 + screenColor11 + screenColor12 ) / 3.0 );
				
				return color;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19002
401;548;1640;768;1157.719;147.6659;1;True;False
Node;AmplifyShaderEditor.Vector2Node;1;-1223.207,-128.7468;Inherit;False;Constant;_Vector0;Vector 0;6;0;Create;True;0;0;0;False;0;False;1,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;2;-1476.623,56.47911;Inherit;False;Property;_BlurStrength;BlurStrength;1;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;3;-1223.419,255.1942;Inherit;False;Constant;_Vector1;Vector 1;5;0;Create;True;0;0;0;False;0;False;0,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ScreenPosInputsNode;4;-1073.903,51.8441;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-968.4401,-93.25283;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-966.5912,246.0881;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;7;-855.7859,82.36007;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-710.8106,-94.36391;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;9;-708.6107,253.3341;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScreenColorNode;10;-544.1381,-99.11782;Inherit;False;Global;_GrabScreen0;Grab Screen 0;5;0;Create;True;0;0;0;False;0;False;Object;-1;False;False;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;11;-546.4473,71.88216;Inherit;False;Global;_GrabScreen2;Grab Screen 0;5;0;Create;True;0;0;0;False;0;False;Object;-1;False;False;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;12;-546.4472,249.4623;Inherit;False;Global;_GrabScreen3;Grab Screen 0;5;0;Create;True;0;0;0;False;0;False;Object;-1;False;False;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-319.8334,211.2162;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-317.422,52.19615;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;15;-128.5023,53.20909;Inherit;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;22;9,54;Float;False;True;-1;2;ASEMaterialInspector;0;4;SDR_Blur;32139be9c1eb75640a847f011acf3bcf;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;True;7;False;;False;False;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;5;0;2;0
WireConnection;5;1;1;0
WireConnection;6;0;2;0
WireConnection;6;1;3;0
WireConnection;7;0;4;1
WireConnection;7;1;4;2
WireConnection;8;0;5;0
WireConnection;8;1;7;0
WireConnection;9;0;7;0
WireConnection;9;1;6;0
WireConnection;10;0;8;0
WireConnection;11;0;7;0
WireConnection;12;0;9;0
WireConnection;14;0;10;0
WireConnection;14;1;11;0
WireConnection;14;2;12;0
WireConnection;15;0;14;0
WireConnection;15;1;13;0
WireConnection;22;0;15;0
ASEEND*/
//CHKSM=89BA607448E54BF28764E471AF6A450AE911186A