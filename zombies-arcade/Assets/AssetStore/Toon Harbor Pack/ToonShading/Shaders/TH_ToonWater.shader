// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Toon/TH_ToonWater"
{
	Properties
	{
		_MainColor("Main Color", Color) = (0,0,0,0)
		_NormalMap("Normal Map", 2D) = "bump" {}
		_DistortionAmount1("Distortion Amount 1", Float) = 1
		_DistortionAmount2("Distortion Amount 2", Float) = 1
		_AnimateUV1XYUV2ZW("Animate UV1 (XY) UV2 (ZW)", Vector) = (0,0,0,0)
		_LerpStrenght("Lerp Strenght", Range( 0 , 2)) = 1
		_UV1TilingXYScaleZW("UV1 Tiling (XY) Scale (ZW)", Vector) = (1,1,1,1)
		_UV2TilingXYScaleZW("UV2 Tiling (XY) Scale (ZW)", Vector) = (1,1,1,1)
		_FresnelPower("Fresnel Power", Range( 0 , 24)) = 1
		_DepthFadeDistance("Depth Fade Distance", Range( 1 , 20)) = 1.5
		_CameraDepthFadeLenght("Camera Depth Fade Lenght", Range( 0 , 16)) = 1
		_CameraDepthFadeOffset("Camera Depth Fade Offset", Range( 0 , 6)) = 0.5
		_EdgeDistance("Edge Distance", Range( 0 , 12)) = 1
		_EdgePower("Edge Power", Range( 0 , 1)) = 1
		_FoamMap("Foam Map", 2D) = "white" {}
		_FoamColor("Foam Color", Color) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#include "UnityPBSLighting.cginc"
		#include "UnityCG.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf StandardCustomLighting alpha:fade keepalpha novertexlights nolightmap  nodynlightmap nodirlightmap nometa noforwardadd vertex:vertexDataFunc 
		struct Input
		{
			float4 screenPos;
			float3 worldPos;
			half3 worldNormal;
			INTERNAL_DATA
			float eyeDepth;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform half _EdgePower;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform half _EdgeDistance;
		uniform sampler2D _FoamMap;
		uniform half4 _AnimateUV1XYUV2ZW;
		uniform half4 _UV2TilingXYScaleZW;
		uniform half4 _FoamColor;
		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform sampler2D _NormalMap;
		uniform half4 _UV1TilingXYScaleZW;
		uniform half _DistortionAmount1;
		uniform half _DepthFadeDistance;
		uniform half _DistortionAmount2;
		uniform half _LerpStrenght;
		uniform half4 _MainColor;
		uniform half _FresnelPower;
		uniform half _CameraDepthFadeLenght;
		uniform half _CameraDepthFadeOffset;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.eyeDepth = -UnityObjectToViewPos( v.vertex.xyz ).z;
		}

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			half4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			half2 appendResult5 = (half2(ase_grabScreenPosNorm.r , ase_grabScreenPosNorm.g));
			float3 ase_worldPos = i.worldPos;
			half2 temp_output_20_0 = (ase_worldPos).xz;
			half2 appendResult25 = (half2(( _Time.x * _AnimateUV1XYUV2ZW.x ) , ( _Time.x * _AnimateUV1XYUV2ZW.y )));
			half2 appendResult31 = (half2(_UV1TilingXYScaleZW.x , _UV1TilingXYScaleZW.y));
			half2 appendResult30 = (half2(_UV1TilingXYScaleZW.z , _UV1TilingXYScaleZW.w));
			half2 UV132 = ( ( ( temp_output_20_0 + appendResult25 ) * appendResult31 ) / appendResult30 );
			half4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth70 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			half distanceDepth70 = saturate( abs( ( screenDepth70 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _DepthFadeDistance ) ) );
			half depthFade71 = distanceDepth70;
			half2 appendResult35 = (half2(( _Time.x * _AnimateUV1XYUV2ZW.z ) , ( _Time.x * _AnimateUV1XYUV2ZW.w )));
			half2 appendResult41 = (half2(_UV2TilingXYScaleZW.x , _UV2TilingXYScaleZW.y));
			half2 appendResult42 = (half2(_UV2TilingXYScaleZW.z , _UV2TilingXYScaleZW.w));
			half2 UV239 = ( ( ( temp_output_20_0 + appendResult35 ) * appendResult41 ) / appendResult42 );
			half3 lerpResult7 = lerp( UnpackScaleNormal( tex2D( _NormalMap, UV132 ), ( _DistortionAmount1 * depthFade71 ) ) , UnpackScaleNormal( tex2D( _NormalMap, UV239 ), ( _DistortionAmount2 * depthFade71 ) ) , _LerpStrenght);
			half3 normalMapping9 = lerpResult7;
			half2 screenUV11 = ( appendResult5 - ( (normalMapping9).xy * 0.1 ) );
			half4 screenColor1 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,screenUV11);
			float3 indirectNormal67 = WorldNormalVector( i , normalMapping9 );
			Unity_GlossyEnvironmentData g67 = UnityGlossyEnvironmentSetup( 1.0, data.worldViewDir, indirectNormal67, float3(0,0,0));
			half3 indirectSpecular67 = UnityGI_IndirectSpecular( data, 1.0, indirectNormal67, g67 );
			half3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			half3 ase_worldNormal = WorldNormalVector( i, half3( 0, 0, 1 ) );
			half3 ase_vertexNormal = mul( unity_WorldToObject, float4( ase_worldNormal, 0 ) );
			half2 appendResult60 = (half2(ase_vertexNormal.x , ase_vertexNormal.y));
			half3 appendResult64 = (half3(( appendResult60 - (normalMapping9).xy ) , ase_vertexNormal.z));
			half dotResult51 = dot( ase_worldViewDir , appendResult64 );
			half fresnel57 = pow( ( 1.0 - saturate( abs( dotResult51 ) ) ) , _FresnelPower );
			half cameraDepthFade76 = (( i.eyeDepth -_ProjectionParams.y - _CameraDepthFadeOffset ) / _CameraDepthFadeLenght);
			half cameraDepthFade79 = saturate( cameraDepthFade76 );
			half4 lerpResult47 = lerp( screenColor1 , ( half4( indirectSpecular67 , 0.0 ) + _MainColor ) , ( fresnel57 * depthFade71 * cameraDepthFade79 ));
			c.rgb = lerpResult47.rgb;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			o.Normal = float3(0,0,1);
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			half4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth112 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			half distanceDepth112 = abs( ( screenDepth112 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _EdgeDistance ) );
			float3 ase_worldPos = i.worldPos;
			half2 temp_output_20_0 = (ase_worldPos).xz;
			half2 appendResult35 = (half2(( _Time.x * _AnimateUV1XYUV2ZW.z ) , ( _Time.x * _AnimateUV1XYUV2ZW.w )));
			half2 appendResult41 = (half2(_UV2TilingXYScaleZW.x , _UV2TilingXYScaleZW.y));
			half2 appendResult42 = (half2(_UV2TilingXYScaleZW.z , _UV2TilingXYScaleZW.w));
			half2 UV239 = ( ( ( temp_output_20_0 + appendResult35 ) * appendResult41 ) / appendResult42 );
			half4 clampResult120 = clamp( ( ( _EdgePower * ( ( 1.0 - distanceDepth112 ) + tex2D( _FoamMap, ( 1.0 * ( UV239 / 2.0 ) ) ) ) ) * _FoamColor ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			half4 Edge117 = clampResult120;
			o.Emission = Edge117.rgb;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=18800
2068;67;1696;857;149.073;800.9463;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;43;-1808,-1024;Inherit;False;1749;1057;Animated UVs;24;19;22;23;24;25;21;26;27;20;30;31;29;28;33;34;35;38;37;36;32;39;41;40;42;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;26;-1760,-512;Inherit;False;Property;_AnimateUV1XYUV2ZW;Animate UV1 (XY) UV2 (ZW);4;0;Create;True;0;0;0;False;0;False;0,0,0,0;-12,0,8,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;22;-1664,-656;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1360,-544;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-1360,-288;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-1360,-416;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-1360,-656;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;19;-1648,-816;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;25;-1120,-608;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;40;-1424,-176;Inherit;False;Property;_UV2TilingXYScaleZW;UV2 Tiling (XY) Scale (ZW);7;0;Create;True;0;0;0;False;0;False;1,1,1,1;0.5,0.5,4,4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;20;-1392,-800;Inherit;False;True;False;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;73;1566.886,-876.7434;Inherit;False;902.656;187.6821;Depth Fade;3;70;71;72;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;29;-1424,-976;Inherit;False;Property;_UV1TilingXYScaleZW;UV1 Tiling (XY) Scale (ZW);6;0;Create;True;0;0;0;False;0;False;1,1,1,1;0.25,0.25,8,8;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;35;-1120,-368;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;31;-1088,-976;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;41;-912,-320;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-912,-448;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;72;1616.886,-826.7434;Inherit;False;Property;_DepthFadeDistance;Depth Fade Distance;9;0;Create;True;0;0;0;False;0;False;1.5;12;1;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-912,-640;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DepthFade;70;1892.598,-822.0613;Inherit;False;True;True;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;30;-1088,-880;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-672,-656;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;42;-912,-192;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-704,-448;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;10;-16.38965,-544;Inherit;False;1556.876;561.5722;Normal Mapping;12;83;9;7;3;8;2;44;45;46;84;85;87;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;38;-528,-448;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;71;2226.542,-818.8347;Inherit;False;depthFade;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;28;-496,-656;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;87;46.48376,-453.2938;Inherit;False;Property;_DistortionAmount1;Distortion Amount 1;2;0;Create;True;0;0;0;False;0;False;1;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;39;-304,-448;Inherit;False;UV2;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;46;16,-221;Inherit;False;Property;_DistortionAmount2;Distortion Amount 2;3;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;32;-304,-656;Inherit;False;UV1;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;85;62.57764,-331.5433;Inherit;False;71;depthFade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;44;448,-432;Inherit;False;32;UV1;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;427.5776,-350.5433;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;456.3583,-130.8085;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;45;448,-240;Inherit;False;39;UV2;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;8;784,-80;Inherit;False;Property;_LerpStrenght;Lerp Strenght;5;0;Create;True;0;0;0;False;0;False;1;2;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;768,-496;Inherit;True;Property;_NormalMap;Normal Map;1;0;Create;True;0;0;0;False;0;False;3;None;c5ea55b0cded9244581ba0c08a9e6d50;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;773.2517,-288;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;0;False;0;False;2;None;None;True;0;False;white;Auto;True;Instance;2;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;7;1120,-416;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;58;-397.1119,-1760;Inherit;False;1954.112;680.0377;Fresnel;14;57;55;56;54;53;52;51;49;50;60;61;62;64;66;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;9;1296,-416;Inherit;False;normalMapping;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;61;-368,-1376;Inherit;False;9;normalMapping;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalVertexDataNode;49;-368,-1552;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;62;-112,-1376;Inherit;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;60;-144,-1520;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;66;104.0616,-1423.281;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;118;1732.665,-2141.998;Inherit;False;2103.649;842.4781;Edge Foam;16;128;121;124;123;117;120;115;116;114;112;113;125;126;127;122;130;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;127;1792,-1456;Inherit;False;Constant;_Float1;Float 1;20;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;50;112,-1712;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;64;260.6975,-1376.74;Inherit;False;FLOAT3;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;122;1776,-1744;Inherit;True;39;UV2;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;113;1792,-1952;Inherit;False;Property;_EdgeDistance;Edge Distance;12;0;Create;True;0;0;0;False;0;False;1;0.35;0;12;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;51;368,-1616;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;125;1808,-1840;Inherit;False;Constant;_Float0;Float 0;20;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;126;2016,-1744;Inherit;True;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;12;384,-1024;Inherit;False;1157.783;432.0106;Screen UVs;8;16;14;15;11;6;5;4;18;;1,1,1,1;0;0
Node;AmplifyShaderEditor.AbsOpNode;52;560,-1616;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;14;416,-800;Inherit;False;9;normalMapping;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DepthFade;112;2096,-1968;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;80;1724.39,-1259.855;Inherit;False;1027.7;253.9;Camera Depth Fade;5;76;78;77;79;82;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;124;2224,-1792;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;78;1739.09,-1119.655;Inherit;False;Property;_CameraDepthFadeOffset;Camera Depth Fade Offset;11;0;Create;True;0;0;0;False;0;False;0.5;1;0;6;0;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;4;656,-976;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;53;720,-1616;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;114;2620.272,-1965.998;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;704,-720;Inherit;False;Constant;_constant01;constant 0.1;1;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;77;1734.09,-1204.655;Inherit;False;Property;_CameraDepthFadeLenght;Camera Depth Fade Lenght;10;0;Create;True;0;0;0;False;0;False;1;1;0;16;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;121;2421.081,-1818.447;Inherit;True;Property;_FoamMap;Foam Map;14;0;Create;True;0;0;0;False;0;False;-1;None;073159d1880dbde4d9e83cd2a0cab53c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;18;672,-816;Inherit;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;944,-800;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;5;944,-944;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;116;2626.275,-2077.998;Inherit;False;Property;_EdgePower;Edge Power;13;0;Create;True;0;0;0;False;0;False;1;0.65;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;123;2871.749,-1947.732;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;54;912,-1616;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;800,-1520;Inherit;False;Property;_FresnelPower;Fresnel Power;8;0;Create;True;0;0;0;False;0;False;1;2.6;0;24;0;1;FLOAT;0
Node;AmplifyShaderEditor.CameraDepthFade;76;2048.633,-1166.952;Inherit;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;6;1120,-944;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;55;1120,-1600;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;130;2835.792,-1771.536;Inherit;False;Property;_FoamColor;Foam Color;15;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.5408953,0.5845691,0.6132076,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;82;2338.656,-1144.043;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;115;3055.635,-2058.604;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;68;1584,-432;Inherit;False;9;normalMapping;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;11;1312,-944;Inherit;False;screenUV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;57;1312,-1600;Inherit;False;fresnel;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;128;3248.672,-2044.729;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;79;2508.692,-1160.955;Inherit;False;cameraDepthFade;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;120;3453.273,-2045.998;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;48;1856,-272;Inherit;False;Property;_MainColor;Main Color;0;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.3444286,0.5023155,0.5660378,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;81;1714.927,192.8201;Inherit;False;79;cameraDepthFade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;75;1760,96;Inherit;False;71;depthFade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.IndirectSpecularLight;67;1824,-432;Inherit;False;Tangent;3;0;FLOAT3;0,0,1;False;1;FLOAT;1;False;2;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;59;1776,16;Inherit;False;57;fresnel;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;17;1744,-608;Inherit;False;11;screenUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScreenColorNode;1;1940,-609;Inherit;False;Global;_GrabScreen0;Grab Screen 0;0;0;Create;True;0;0;0;False;0;False;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;117;3629.273,-2045.998;Inherit;False;Edge;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;1984,32;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;69;2112,-352;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;119;2560,-656;Inherit;False;117;Edge;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;47;2432,-464;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2816,-688;Half;False;True;-1;2;;0;0;CustomLighting;Toon/TH_ToonWater;False;False;False;False;False;True;True;True;True;False;True;True;False;False;True;False;False;False;False;False;False;Back;0;False;-1;3;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;1;False;-1;1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;24;0;22;1
WireConnection;24;1;26;2
WireConnection;34;0;22;1
WireConnection;34;1;26;4
WireConnection;33;0;22;1
WireConnection;33;1;26;3
WireConnection;23;0;22;1
WireConnection;23;1;26;1
WireConnection;25;0;23;0
WireConnection;25;1;24;0
WireConnection;20;0;19;0
WireConnection;35;0;33;0
WireConnection;35;1;34;0
WireConnection;31;0;29;1
WireConnection;31;1;29;2
WireConnection;41;0;40;1
WireConnection;41;1;40;2
WireConnection;36;0;20;0
WireConnection;36;1;35;0
WireConnection;21;0;20;0
WireConnection;21;1;25;0
WireConnection;70;0;72;0
WireConnection;30;0;29;3
WireConnection;30;1;29;4
WireConnection;27;0;21;0
WireConnection;27;1;31;0
WireConnection;42;0;40;3
WireConnection;42;1;40;4
WireConnection;37;0;36;0
WireConnection;37;1;41;0
WireConnection;38;0;37;0
WireConnection;38;1;42;0
WireConnection;71;0;70;0
WireConnection;28;0;27;0
WireConnection;28;1;30;0
WireConnection;39;0;38;0
WireConnection;32;0;28;0
WireConnection;84;0;87;0
WireConnection;84;1;85;0
WireConnection;83;0;46;0
WireConnection;83;1;85;0
WireConnection;2;1;44;0
WireConnection;2;5;84;0
WireConnection;3;1;45;0
WireConnection;3;5;83;0
WireConnection;7;0;2;0
WireConnection;7;1;3;0
WireConnection;7;2;8;0
WireConnection;9;0;7;0
WireConnection;62;0;61;0
WireConnection;60;0;49;1
WireConnection;60;1;49;2
WireConnection;66;0;60;0
WireConnection;66;1;62;0
WireConnection;64;0;66;0
WireConnection;64;2;49;3
WireConnection;51;0;50;0
WireConnection;51;1;64;0
WireConnection;126;0;122;0
WireConnection;126;1;127;0
WireConnection;52;0;51;0
WireConnection;112;0;113;0
WireConnection;124;0;125;0
WireConnection;124;1;126;0
WireConnection;53;0;52;0
WireConnection;114;0;112;0
WireConnection;121;1;124;0
WireConnection;18;0;14;0
WireConnection;15;0;18;0
WireConnection;15;1;16;0
WireConnection;5;0;4;1
WireConnection;5;1;4;2
WireConnection;123;0;114;0
WireConnection;123;1;121;0
WireConnection;54;0;53;0
WireConnection;76;0;77;0
WireConnection;76;1;78;0
WireConnection;6;0;5;0
WireConnection;6;1;15;0
WireConnection;55;0;54;0
WireConnection;55;1;56;0
WireConnection;82;0;76;0
WireConnection;115;0;116;0
WireConnection;115;1;123;0
WireConnection;11;0;6;0
WireConnection;57;0;55;0
WireConnection;128;0;115;0
WireConnection;128;1;130;0
WireConnection;79;0;82;0
WireConnection;120;0;128;0
WireConnection;67;0;68;0
WireConnection;1;0;17;0
WireConnection;117;0;120;0
WireConnection;74;0;59;0
WireConnection;74;1;75;0
WireConnection;74;2;81;0
WireConnection;69;0;67;0
WireConnection;69;1;48;0
WireConnection;47;0;1;0
WireConnection;47;1;69;0
WireConnection;47;2;74;0
WireConnection;0;2;119;0
WireConnection;0;13;47;0
ASEEND*/
//CHKSM=073F4A3ACFA0DD9D57EF948B1A57D9A3E6F6EB77