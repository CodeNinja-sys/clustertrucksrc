Shader "Skybox/Gradient Skybox" {
Properties {
 _Color1 ("Color 1", Color) = (1,1,1,0)
 _Color2 ("Color 2", Color) = (1,1,1,0)
 _UpVector ("Up Vector", Vector) = (0,1,0,0)
 _Intensity ("Intensity", Float) = 1
 _Exponent ("Exponent", Float) = 1
 _UpVectorPitch ("Up Vector Pitch", Float) = 0
 _UpVectorYaw ("Up Vector Yaw", Float) = 0
}
SubShader { 
 Tags { "QUEUE"="Background" "RenderType"="Background" }
 Pass {
  Tags { "QUEUE"="Background" "RenderType"="Background" }
  ZWrite Off
  Cull Off
  GpuProgramID 31251
Program "vp" {
SubProgram "d3d9 " {
"vs_2_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 6.3.9600.16384
					//
					// Parameters:
					//
					//   row_major float4x4 glstate_matrix_mvp;
					//
					//
					// Registers:
					//
					//   Name               Reg   Size
					//   ------------------ ----- ----
					//   glstate_matrix_mvp c0       4
					//
					
					    vs_2_0
					    dcl_position v0
					    dcl_texcoord v1
					    dp4 oPos.x, c0, v0
					    dp4 oPos.y, c1, v0
					    dp4 oPos.z, c2, v0
					    dp4 oPos.w, c3, v0
					    mov oT0.xyz, v1
					
					// approximately 5 instruction slots used"
}
SubProgram "d3d11 " {
"vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform UnityPerDraw {
						mat4x4 glstate_matrix_mvp;
						vec4 unused_0_1[18];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_TEXCOORD0;
					out vec3 vs_TEXCOORD0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    vs_TEXCOORD0.xyz = in_TEXCOORD0.xyz;
					    return;
					}"
}
SubProgram "d3d11_9x " {
"vs_4_0_level_9_1
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform UnityPerDraw {
						mat4x4 glstate_matrix_mvp;
						vec4 unused_0_1[18];
					};
					in  vec4 in_POSITION0;
					in  vec3 in_TEXCOORD0;
					out vec3 vs_TEXCOORD0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    vs_TEXCOORD0.xyz = in_TEXCOORD0.xyz;
					    return;
					}"
}
}
Program "fp" {
SubProgram "d3d9 " {
"ps_2_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 6.3.9600.16384
					//
					// Parameters:
					//
					//   float4 _Color1;
					//   float4 _Color2;
					//   float _Exponent;
					//   float _Intensity;
					//   float4 _UpVector;
					//
					//
					// Registers:
					//
					//   Name         Reg   Size
					//   ------------ ----- ----
					//   _Color1      c0       1
					//   _Color2      c1       1
					//   _UpVector    c2       1
					//   _Intensity   c3       1
					//   _Exponent    c4       1
					//
					
					    ps_2_0
					    def c5, 0.5, 0, 0, 0
					    dcl t0.xyz
					    nrm r0.xyz, t0
					    dp3 r0.x, r0, c2
					    mad_pp r0.x, r0.x, c5.x, c5.x
					    pow_pp r1.w, r0.x, c4.x
					    mov r0, c0
					    add_pp r0, -r0, c1
					    mad_pp r0, r1.w, r0, c0
					    mul_pp r0, r0, c3.x
					    mov_pp oC0, r0
					
					// approximately 13 instruction slots used"
}
SubProgram "d3d11 " {
"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform PGlobals {
						vec4 unused_0_0[6];
						vec4 _Color1;
						vec4 _Color2;
						vec4 _UpVector;
						float _Intensity;
						float _Exponent;
					};
					in  vec3 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0.x = dot(vs_TEXCOORD0.xyz, vs_TEXCOORD0.xyz);
					    u_xlat0.x = inversesqrt(u_xlat0.x);
					    u_xlat0.xyz = u_xlat0.xxx * vs_TEXCOORD0.xyz;
					    u_xlat0.x = dot(u_xlat0.xyz, _UpVector.xyz);
					    u_xlat0.x = u_xlat0.x * 0.5 + 0.5;
					    u_xlat0.x = log2(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * _Exponent;
					    u_xlat0.x = exp2(u_xlat0.x);
					    u_xlat1 = (-_Color1) + _Color2;
					    u_xlat0 = u_xlat0.xxxx * u_xlat1 + _Color1;
					    SV_Target0 = u_xlat0 * vec4(_Intensity);
					    return;
					}"
}
SubProgram "d3d11_9x " {
"ps_4_0_level_9_1
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform PGlobals {
						vec4 unused_0_0[6];
						vec4 _Color1;
						vec4 _Color2;
						vec4 _UpVector;
						float _Intensity;
						float _Exponent;
					};
					in  vec3 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0.x = dot(vs_TEXCOORD0.xyz, vs_TEXCOORD0.xyz);
					    u_xlat0.x = inversesqrt(u_xlat0.x);
					    u_xlat0.xyz = u_xlat0.xxx * vs_TEXCOORD0.xyz;
					    u_xlat0.x = dot(u_xlat0.xyz, _UpVector.xyz);
					    u_xlat0.x = u_xlat0.x * 0.5 + 0.5;
					    u_xlat0.x = log2(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * _Exponent;
					    u_xlat0.x = exp2(u_xlat0.x);
					    u_xlat1 = (-_Color1) + _Color2;
					    u_xlat0 = u_xlat0.xxxx * u_xlat1 + _Color1;
					    SV_Target0 = u_xlat0 * vec4(_Intensity);
					    return;
					}"
}
}
 }
}
}