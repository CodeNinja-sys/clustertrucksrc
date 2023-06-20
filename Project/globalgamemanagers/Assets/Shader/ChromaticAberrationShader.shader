Shader "Hidden/ChromaticAberration" {
Properties {
 _MainTex ("Base", 2D) = "" { }
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 47138
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
					    mov oT0.xy, v1
					
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
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
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
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
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
					//   float4 _MainTex_TexelSize;
					//
					//
					// Registers:
					//
					//   Name               Reg   Size
					//   ------------------ ----- ----
					//   _MainTex_TexelSize c0       1
					//   _MainTex           s0       1
					//
					
					    ps_2_0
					    def c1, 0.5, -0.5, 0.25, 0
					    dcl t0.xy
					    dcl_2d s0
					    mov r0.xy, c1
					    mad r1.xy, c0, r0.x, t0
					    mad r2.xy, c0, -r0.x, t0
					    mad r3.xy, c0, r0, t0
					    mad r0.xy, c0, -r0, t0
					    texld_pp r1, r1, s0
					    texld r2, r2, s0
					    texld r3, r3, s0
					    texld r0, r0, s0
					    add_pp r1, r1, r2
					    add_pp r1, r3, r1
					    add_pp r0, r0, r1
					    mul_pp r0, r0, c1.z
					    mov_pp oC0, r0
					
					// approximately 14 instruction slots used (4 texture, 10 arithmetic)"
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
						vec4 _MainTex_TexelSize;
						vec4 unused_0_2[2];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					void main()
					{
					    u_xlat0 = _MainTex_TexelSize.xyxy * vec4(0.5, 0.5, 0.5, -0.5) + vs_TEXCOORD0.xyxy;
					    u_xlat1 = texture(_MainTex, u_xlat0.xy);
					    u_xlat0 = texture(_MainTex, u_xlat0.zw);
					    u_xlat2 = (-_MainTex_TexelSize.xyxy) * vec4(0.5, 0.5, 0.5, -0.5) + vs_TEXCOORD0.xyxy;
					    u_xlat3 = texture(_MainTex, u_xlat2.xy);
					    u_xlat2 = texture(_MainTex, u_xlat2.zw);
					    u_xlat1 = u_xlat1 + u_xlat3;
					    u_xlat0 = u_xlat0 + u_xlat1;
					    u_xlat0 = u_xlat2 + u_xlat0;
					    SV_Target0 = u_xlat0 * vec4(0.25, 0.25, 0.25, 0.25);
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
						vec4 _MainTex_TexelSize;
						vec4 unused_0_2[2];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					void main()
					{
					    u_xlat0 = _MainTex_TexelSize.xyxy * vec4(0.5, 0.5, 0.5, -0.5) + vs_TEXCOORD0.xyxy;
					    u_xlat1 = texture(_MainTex, u_xlat0.xy);
					    u_xlat0 = texture(_MainTex, u_xlat0.zw);
					    u_xlat2 = (-_MainTex_TexelSize.xyxy) * vec4(0.5, 0.5, 0.5, -0.5) + vs_TEXCOORD0.xyxy;
					    u_xlat3 = texture(_MainTex, u_xlat2.xy);
					    u_xlat2 = texture(_MainTex, u_xlat2.zw);
					    u_xlat1 = u_xlat1 + u_xlat3;
					    u_xlat0 = u_xlat0 + u_xlat1;
					    u_xlat0 = u_xlat2 + u_xlat0;
					    SV_Target0 = u_xlat0 * vec4(0.25, 0.25, 0.25, 0.25);
					    return;
					}"
}
}
 }
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 124008
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
					    mov oT0.xy, v1
					
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
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
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
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
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
					//   float _ChromaticAberration;
					//   sampler2D _MainTex;
					//   float4 _MainTex_TexelSize;
					//
					//
					// Registers:
					//
					//   Name                 Reg   Size
					//   -------------------- ----- ----
					//   _MainTex_TexelSize   c0       1
					//   _ChromaticAberration c1       1
					//   _MainTex             s0       1
					//
					
					    ps_2_0
					    def c2, -0.5, 0, 9.99999975e-005, 0
					    dcl_pp t0.xy
					    dcl_2d s0
					    mov r0.xy, c0
					    mul r0.xy, r0, c1.x
					    add_pp r0.zw, t0.wzyx, c2.x
					    add_pp r1.xy, r0.wzyx, r0.wzyx
					    mul r0.xy, r0, r1
					    dp2add_pp r0.z, r1, r1, c2.y
					    mad_pp r0.xy, r0, -r0.z, t0
					    texld r0, r0, s0
					    texld_pp r1, t0, s0
					    mad_pp r1.y, r1.y, c2.z, r0.y
					    mov_pp oC0, r1
					
					// approximately 12 instruction slots used (2 texture, 10 arithmetic)"
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
						vec4 _MainTex_TexelSize;
						float _ChromaticAberration;
						vec4 unused_0_3;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec2 u_xlat2;
					void main()
					{
					    u_xlat0.xy = _MainTex_TexelSize.xy * vec2(_ChromaticAberration);
					    u_xlat2.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
					    u_xlat2.xy = u_xlat2.xy + u_xlat2.xy;
					    u_xlat0.xy = u_xlat2.xy * u_xlat0.xy;
					    u_xlat2.x = dot(u_xlat2.xy, u_xlat2.xy);
					    u_xlat0.xy = (-u_xlat0.xy) * u_xlat2.xx + vs_TEXCOORD0.xy;
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    SV_Target0.y = u_xlat0.y;
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    SV_Target0.xzw = u_xlat0.xzw;
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
						vec4 _MainTex_TexelSize;
						float _ChromaticAberration;
						vec4 unused_0_3;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec2 u_xlat2;
					void main()
					{
					    u_xlat0.xy = _MainTex_TexelSize.xy * vec2(_ChromaticAberration);
					    u_xlat2.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
					    u_xlat2.xy = u_xlat2.xy + u_xlat2.xy;
					    u_xlat0.xy = u_xlat2.xy * u_xlat0.xy;
					    u_xlat2.x = dot(u_xlat2.xy, u_xlat2.xy);
					    u_xlat0.xy = (-u_xlat0.xy) * u_xlat2.xx + vs_TEXCOORD0.xy;
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    SV_Target0.y = u_xlat0.y;
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    SV_Target0.xzw = u_xlat0.xzw;
					    return;
					}"
}
}
 }
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 134533
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
					    mov oT0.xy, v1
					
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
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
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
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
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
					//   float _AxialAberration;
					//   float2 _BlurDistance;
					//   float _ChromaticAberration;
					//   float _Luminance;
					//   sampler2D _MainTex;
					//   float4 _MainTex_TexelSize;
					//   float4 unity_ColorSpaceLuminance;
					//
					//
					// Registers:
					//
					//   Name                      Reg   Size
					//   ------------------------- ----- ----
					//   unity_ColorSpaceLuminance c0       1
					//   _MainTex_TexelSize        c1       1
					//   _ChromaticAberration      c2       1
					//   _AxialAberration          c3       1
					//   _Luminance                c4       1
					//   _BlurDistance             c5       1
					//   _MainTex                  s0       1
					//
					
					    ps_2_0
					    def c6, -0.5, 0, -0.405809999, -0.926212013
					    def c7, 0.100000001, -0.69591397, 0.457136989, 0.108695656
					    def c8, -0.321940005, -0.932614982, 2, 0
					    def c9, 0.185461, -0.893123984, 0.412458003, 0.896420002
					    def c10, 0.473434001, -0.480026007, 0.767022014, 0.519456029
					    def c11, -0.203345001, 0.820716023, -0.194983006, 0.962339997
					    dcl_pp t0.xy
					    dcl_2d s0
					    mov r0.xy, c1
					    mul r1.xy, r0, c7.yzxw
					    add_pp r0.zw, t0.wzyx, c6.x
					    add_pp r2.xy, r0.wzyx, r0.wzyx
					    dp2add_pp r0.z, r2, r2, c6.y
					    mul_pp r0.z, r0.z, r0.z
					    mul_pp r0.z, r0.z, c2.x
					    max_pp r1.z, c3.x, r0.z
					    max_pp r0.z, r1.z, c5.x
					    min_pp r1.z, c5.y, r0.z
					    mad_pp r1.xy, r1, r1.z, t0
					    mul r0.zw, r0.wzyx, c6
					    mad_pp r2.xy, r0.wzyx, r1.z, t0
					    mul r0.zw, r0.wzyx, c11.wzyx
					    mad_pp r3.xy, r0.wzyx, r1.z, t0
					    mul r0.zw, r0.wzyx, c11
					    mad_pp r4.xy, r0.wzyx, r1.z, t0
					    mul r0.zw, r0.wzyx, c10.wzyx
					    mad_pp r5.xy, r0.wzyx, r1.z, t0
					    mul r0.zw, r0.wzyx, c10
					    mad_pp r6.xy, r0.wzyx, r1.z, t0
					    mul r0.zw, r0.wzyx, c9.wzyx
					    mad_pp r7.xy, r0.wzyx, r1.z, t0
					    mul r0.zw, r0.wzyx, c9
					    mad_pp r8.xy, r0.wzyx, r1.z, t0
					    mul r0.xy, r0, c8
					    mad_pp r0.xy, r0, r1.z, t0
					    texld_pp r1, r1, s0
					    texld_pp r2, r2, s0
					    texld_pp r9, t0, s0
					    texld_pp r3, r3, s0
					    texld_pp r4, r4, s0
					    texld_pp r5, r5, s0
					    texld_pp r6, r6, s0
					    texld_pp r7, r7, s0
					    texld_pp r8, r8, s0
					    texld_pp r0, r0, s0
					    mad_pp r2.xyz, r9, c7.x, r2
					    add_pp r1.xyz, r1, r2
					    add_pp r1.xyz, r3, r1
					    add_pp r1.xyz, r4, r1
					    add_pp r1.xyz, r5, r1
					    add_pp r1.xyz, r6, r1
					    add_pp r1.xyz, r7, r1
					    add_pp r1.xyz, r8, r1
					    add_pp r0.xyz, r0, r1
					    mad_pp r0.xyz, r0, c7.w, -r9
					    abs_pp r1.xyz, r0
					    mul_pp r2.xyz, r1, c0
					    add_pp r0.y, r2.z, r2.x
					    mul_pp r0.y, r0.y, r2.y
					    add_pp r0.w, r2.y, r2.x
					    mad_pp r0.w, r1.z, c0.z, r0.w
					    rsq_pp r0.y, r0.y
					    rcp_pp r0.y, r0.y
					    mul_pp r0.y, r0.y, c0.w
					    mad_pp r0.y, r0.y, c8.z, r0.w
					    mul_sat_pp r0.y, r0.y, c4.x
					    mad_pp r9.xz, r0.y, r0, r9
					    mov_pp oC0, r9
					
					// approximately 61 instruction slots used (10 texture, 51 arithmetic)"
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
					vec2 ImmCB_0_0_0[9];
					layout(std140) uniform PGlobals {
						vec4 unused_0_0[3];
						vec4 unity_ColorSpaceLuminance;
						vec4 unused_0_2[2];
						vec4 _MainTex_TexelSize;
						float _ChromaticAberration;
						float _AxialAberration;
						float _Luminance;
						vec2 _BlurDistance;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec4 u_xlat3;
					bool u_xlatb3;
					vec3 u_xlat4;
					float u_xlat12;
					int u_xlati14;
					void main()
					{
						ImmCB_0_0_0[0] = vec2(-0.926212013, -0.405809999);
						ImmCB_0_0_0[1] = vec2(-0.69591397, 0.457136989);
						ImmCB_0_0_0[2] = vec2(-0.203345001, 0.820716023);
						ImmCB_0_0_0[3] = vec2(0.962339997, -0.194983006);
						ImmCB_0_0_0[4] = vec2(0.473434001, -0.480026007);
						ImmCB_0_0_0[5] = vec2(0.519456029, 0.767022014);
						ImmCB_0_0_0[6] = vec2(0.185461, -0.893123984);
						ImmCB_0_0_0[7] = vec2(0.896420002, 0.412458003);
						ImmCB_0_0_0[8] = vec2(-0.321940005, -0.932614982);
					    u_xlat0.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
					    u_xlat0.xy = u_xlat0.xy + u_xlat0.xy;
					    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _ChromaticAberration;
					    u_xlat0.x = max(u_xlat0.x, _AxialAberration);
					    u_xlat0.x = max(u_xlat0.x, _BlurDistance.x);
					    u_xlat0.x = min(u_xlat0.x, _BlurDistance.y);
					    u_xlat4.xyz = u_xlat1.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat2.xyz = u_xlat4.xyz;
					    for(int u_xlati_loop_1 = 0 ; u_xlati_loop_1<9 ; u_xlati_loop_1++)
					    {
					        u_xlat3.xy = _MainTex_TexelSize.xy * ImmCB_0_0_0[u_xlati_loop_1].xy;
					        u_xlat3.xy = u_xlat3.xy * u_xlat0.xx + vs_TEXCOORD0.xy;
					        u_xlat3 = texture(_MainTex, u_xlat3.xy);
					        u_xlat2.xyz = u_xlat2.xyz + u_xlat3.xyz;
					    }
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.108695656, 0.108695656, 0.108695656) + (-u_xlat1.xyz);
					    u_xlat2.xyz = abs(u_xlat0.xyz) * unity_ColorSpaceLuminance.xyz;
					    u_xlat4.xz = u_xlat2.yz + u_xlat2.xx;
					    u_xlat4.x = abs(u_xlat0.z) * unity_ColorSpaceLuminance.z + u_xlat4.x;
					    u_xlat12 = u_xlat4.z * u_xlat2.y;
					    u_xlat12 = sqrt(u_xlat12);
					    u_xlat12 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat12));
					    u_xlat4.x = u_xlat12 + u_xlat4.x;
					    u_xlat4.x = u_xlat4.x * _Luminance;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    SV_Target0.xz = u_xlat4.xx * u_xlat0.xz + u_xlat1.xz;
					    SV_Target0.yw = u_xlat1.yw;
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
					vec2 ImmCB_0_0_0[9];
					layout(std140) uniform PGlobals {
						vec4 unused_0_0[3];
						vec4 unity_ColorSpaceLuminance;
						vec4 unused_0_2[2];
						vec4 _MainTex_TexelSize;
						float _ChromaticAberration;
						float _AxialAberration;
						float _Luminance;
						vec2 _BlurDistance;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec3 u_xlat0;
					vec4 u_xlat1;
					vec3 u_xlat2;
					vec4 u_xlat3;
					bool u_xlatb3;
					vec3 u_xlat4;
					int u_xlati14;
					void main()
					{
						ImmCB_0_0_0[0] = vec2(-0.926212013, -0.405809999);
						ImmCB_0_0_0[1] = vec2(-0.69591397, 0.457136989);
						ImmCB_0_0_0[2] = vec2(-0.203345001, 0.820716023);
						ImmCB_0_0_0[3] = vec2(0.962339997, -0.194983006);
						ImmCB_0_0_0[4] = vec2(0.473434001, -0.480026007);
						ImmCB_0_0_0[5] = vec2(0.519456029, 0.767022014);
						ImmCB_0_0_0[6] = vec2(0.185461, -0.893123984);
						ImmCB_0_0_0[7] = vec2(0.896420002, 0.412458003);
						ImmCB_0_0_0[8] = vec2(-0.321940005, -0.932614982);
					    u_xlat0.xy = vs_TEXCOORD0.xy + vec2(-0.5, -0.5);
					    u_xlat0.xy = u_xlat0.xy + u_xlat0.xy;
					    u_xlat0.x = dot(u_xlat0.xy, u_xlat0.xy);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    u_xlat0.x = u_xlat0.x * _ChromaticAberration;
					    u_xlat0.x = max(u_xlat0.x, _AxialAberration);
					    u_xlat0.x = max(u_xlat0.x, _BlurDistance.x);
					    u_xlat0.x = min(u_xlat0.x, _BlurDistance.y);
					    u_xlat4.xyz = u_xlat1.xyz * vec3(0.100000001, 0.100000001, 0.100000001);
					    u_xlat2.xyz = u_xlat4.xyz;
					    for(int u_xlati_loop_1 = 0 ; u_xlati_loop_1<9 ; u_xlati_loop_1++)
					    {
					        u_xlat3.xy = _MainTex_TexelSize.xy * ImmCB_0_0_0[u_xlati_loop_1].xy;
					        u_xlat3.xy = u_xlat3.xy * u_xlat0.xx + vs_TEXCOORD0.xy;
					        u_xlat3 = texture(_MainTex, u_xlat3.xy);
					        u_xlat2.xyz = u_xlat2.xyz + u_xlat3.xyz;
					    }
					    u_xlat0.xyz = u_xlat2.xyz * vec3(0.108695656, 0.108695656, 0.108695656) + (-u_xlat1.xyz);
					    u_xlat4.x = dot(abs(u_xlat0.xyz), unity_ColorSpaceLuminance.xyz);
					    u_xlat4.x = u_xlat4.x * _Luminance;
					    u_xlat4.x = clamp(u_xlat4.x, 0.0, 1.0);
					    SV_Target0.xz = u_xlat4.xx * u_xlat0.xz + u_xlat1.xz;
					    SV_Target0.yw = u_xlat1.yw;
					    return;
					}"
}
}
 }
}
Fallback Off
}