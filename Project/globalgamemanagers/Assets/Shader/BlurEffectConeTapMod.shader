Shader "Petter/BlurEffectConeTap" {
Properties {
 _MainTex ("", any) = "" { }
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 55283
Program "vp" {
SubProgram "d3d9 " {
"vs_2_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 6.3.9600.16384
					//
					// Parameters:
					//
					//   float4 _BlurOffsets;
					//   float4 _MainTex_TexelSize;
					//   row_major float4x4 glstate_matrix_mvp;
					//
					//
					// Registers:
					//
					//   Name               Reg   Size
					//   ------------------ ----- ----
					//   glstate_matrix_mvp c0       4
					//   _MainTex_TexelSize c4       1
					//   _BlurOffsets       c5       1
					//
					
					    vs_2_0
					    def c6, 1, -1, 0, 0
					    dcl_position v0
					    dcl_texcoord v1
					    dp4 oPos.x, c0, v0
					    dp4 oPos.y, c1, v0
					    dp4 oPos.z, c2, v0
					    dp4 oPos.w, c3, v0
					    mov r0.xy, c5
					    mad r0.zw, r0.xyxy, -c4.xyxy, v1.xyxy
					    mad oT2.xy, r0, -c4, r0.zwzw
					    mul r0.xy, r0, c4
					    mad oT3.xy, r0, c6, r0.zwzw
					    mad oT4.xy, r0, -c6, r0.zwzw
					    mov oT0.xy, r0.zwzw
					    mov oT1.xy, v1
					
					// approximately 12 instruction slots used"
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
					layout(std140) uniform VGlobals {
						vec4 unused_0_0[6];
						vec4 _MainTex_TexelSize;
						vec4 _BlurOffsets;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 glstate_matrix_mvp;
						vec4 unused_1_1[18];
					};
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					vec2 u_xlat2;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat0.xy = (-_BlurOffsets.xy) * _MainTex_TexelSize.xy + in_TEXCOORD0.xy;
					    vs_TEXCOORD0.xy = u_xlat0.xy;
					    vs_TEXCOORD1.xy = in_TEXCOORD0.xy;
					    vs_TEXCOORD2.xy = (-_BlurOffsets.xy) * _MainTex_TexelSize.xy + u_xlat0.xy;
					    u_xlat2.xy = _MainTex_TexelSize.xy * _BlurOffsets.xy;
					    vs_TEXCOORD3.xy = u_xlat2.xy * vec2(1.0, -1.0) + u_xlat0.xy;
					    vs_TEXCOORD4.xy = (-u_xlat2.xy) * vec2(1.0, -1.0) + u_xlat0.xy;
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
					layout(std140) uniform VGlobals {
						vec4 unused_0_0[6];
						vec4 _MainTex_TexelSize;
						vec4 _BlurOffsets;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 glstate_matrix_mvp;
						vec4 unused_1_1[18];
					};
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					out vec2 vs_TEXCOORD2;
					out vec2 vs_TEXCOORD3;
					out vec2 vs_TEXCOORD4;
					vec4 u_xlat0;
					vec2 u_xlat2;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat0.xy = (-_BlurOffsets.xy) * _MainTex_TexelSize.xy + in_TEXCOORD0.xy;
					    vs_TEXCOORD0.xy = u_xlat0.xy;
					    vs_TEXCOORD1.xy = in_TEXCOORD0.xy;
					    vs_TEXCOORD2.xy = (-_BlurOffsets.xy) * _MainTex_TexelSize.xy + u_xlat0.xy;
					    u_xlat2.xy = _MainTex_TexelSize.xy * _BlurOffsets.xy;
					    vs_TEXCOORD3.xy = u_xlat2.xy * vec2(1.0, -1.0) + u_xlat0.xy;
					    vs_TEXCOORD4.xy = (-u_xlat2.xy) * vec2(1.0, -1.0) + u_xlat0.xy;
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
					//   sampler2D _MainTex;
					//
					//
					// Registers:
					//
					//   Name         Reg   Size
					//   ------------ ----- ----
					//   _MainTex     s0       1
					//
					
					    ps_2_0
					    def c0, 0.25, 0, 0, 0
					    dcl_pp t1.xy
					    dcl_pp t2.xy
					    dcl_pp t3.xy
					    dcl_pp t4.xy
					    dcl_2d s0
					    texld_pp r0, t1, s0
					    texld r1, t2, s0
					    texld r2, t3, s0
					    texld r3, t4, s0
					    add_pp r0, r0, r1
					    add_pp r0, r2, r0
					    add_pp r0, r3, r0
					    mul_pp r0, r0, c0.x
					    mov_pp oC0, r0
					
					// approximately 9 instruction slots used (4 texture, 5 arithmetic)"
}
SubProgram "d3d11 " {
"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD1.xy);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD2.xy);
					    u_xlat0 = u_xlat0 + u_xlat1;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD3.xy);
					    u_xlat0 = u_xlat0 + u_xlat1;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD4.xy);
					    u_xlat0 = u_xlat0 + u_xlat1;
					    SV_Target0 = u_xlat0 * vec4(0.25, 0.25, 0.25, 0.25);
					    return;
					}"
}
SubProgram "d3d11_9x " {
"ps_4_0_level_9_1
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD1.xy);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD2.xy);
					    u_xlat0 = u_xlat0 + u_xlat1;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD3.xy);
					    u_xlat0 = u_xlat0 + u_xlat1;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD4.xy);
					    u_xlat0 = u_xlat0 + u_xlat1;
					    SV_Target0 = u_xlat0 * vec4(0.25, 0.25, 0.25, 0.25);
					    return;
					}"
}
}
 }
}
Fallback Off
}