Shader "Projector/Light" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _ShadowTex ("Cookie", 2D) = "" { }
 _FalloffTex ("FallOff", 2D) = "" { }
}
SubShader { 
 Tags { "QUEUE"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" }
  ZWrite Off
  Blend DstColor One
  ColorMask RGB
  Offset -1, -1
  GpuProgramID 5630
Program "vp" {
SubProgram "d3d9 " {
"vs_2_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 6.3.9600.16384
					//
					// Parameters:
					//
					//   row_major float4x4 _Projector;
					//   row_major float4x4 _ProjectorClip;
					//   row_major float4x4 glstate_matrix_mvp;
					//
					//
					// Registers:
					//
					//   Name               Reg   Size
					//   ------------------ ----- ----
					//   glstate_matrix_mvp c0       4
					//   _Projector         c4       4
					//   _ProjectorClip     c8       4
					//
					
					    vs_2_0
					    dcl_position v0
					    dp4 oPos.x, c0, v0
					    dp4 oPos.y, c1, v0
					    dp4 oPos.z, c2, v0
					    dp4 oPos.w, c3, v0
					    dp4 oT0.x, c4, v0
					    dp4 oT0.y, c5, v0
					    dp4 oT0.z, c6, v0
					    dp4 oT0.w, c7, v0
					    dp4 oT1.x, c8, v0
					    dp4 oT1.y, c9, v0
					    dp4 oT1.z, c10, v0
					    dp4 oT1.w, c11, v0
					
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
						mat4x4 _Projector;
						mat4x4 _ProjectorClip;
						vec4 unused_0_3;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 glstate_matrix_mvp;
						vec4 unused_1_1[18];
					};
					in  vec4 in_POSITION0;
					out vec4 vs_TEXCOORD0;
					out vec4 vs_TEXCOORD1;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * _Projector[1];
					    u_xlat0 = _Projector[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = _Projector[2] * in_POSITION0.zzzz + u_xlat0;
					    vs_TEXCOORD0 = _Projector[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat0 = in_POSITION0.yyyy * _ProjectorClip[1];
					    u_xlat0 = _ProjectorClip[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = _ProjectorClip[2] * in_POSITION0.zzzz + u_xlat0;
					    vs_TEXCOORD1 = _ProjectorClip[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
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
						mat4x4 _Projector;
						mat4x4 _ProjectorClip;
						vec4 unused_0_3;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 glstate_matrix_mvp;
						vec4 unused_1_1[18];
					};
					in  vec4 in_POSITION0;
					out vec4 vs_TEXCOORD0;
					out vec4 vs_TEXCOORD1;
					vec4 u_xlat0;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * _Projector[1];
					    u_xlat0 = _Projector[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = _Projector[2] * in_POSITION0.zzzz + u_xlat0;
					    vs_TEXCOORD0 = _Projector[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat0 = in_POSITION0.yyyy * _ProjectorClip[1];
					    u_xlat0 = _ProjectorClip[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = _ProjectorClip[2] * in_POSITION0.zzzz + u_xlat0;
					    vs_TEXCOORD1 = _ProjectorClip[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    return;
					}"
}
SubProgram "d3d9 " {
Keywords { "FOG_EXP2" }
					"vs_2_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 6.3.9600.16384
					//
					// Parameters:
					//
					//   row_major float4x4 _Projector;
					//   row_major float4x4 _ProjectorClip;
					//   row_major float4x4 glstate_matrix_mvp;
					//   float4 unity_FogParams;
					//
					//
					// Registers:
					//
					//   Name               Reg   Size
					//   ------------------ ----- ----
					//   glstate_matrix_mvp c0       4
					//   _Projector         c4       4
					//   _ProjectorClip     c8       4
					//   unity_FogParams    c12      1
					//
					
					    vs_2_0
					    dcl_position v0
					    dp4 oPos.x, c0, v0
					    dp4 oPos.y, c1, v0
					    dp4 oPos.w, c3, v0
					    dp4 oT0.x, c4, v0
					    dp4 oT0.y, c5, v0
					    dp4 oT0.z, c6, v0
					    dp4 oT0.w, c7, v0
					    dp4 oT1.x, c8, v0
					    dp4 oT1.y, c9, v0
					    dp4 oT1.z, c10, v0
					    dp4 oT1.w, c11, v0
					    dp4 r0.x, c2, v0
					    mul r0.y, r0.x, c12.x
					    mov oPos.z, r0.x
					    mul r0.x, r0.y, -r0.y
					    exp oT2.x, r0.x
					
					// approximately 16 instruction slots used"
}
SubProgram "d3d11 " {
Keywords { "FOG_EXP2" }
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
						mat4x4 _Projector;
						mat4x4 _ProjectorClip;
						vec4 unused_0_3;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 glstate_matrix_mvp;
						vec4 unused_1_1[18];
					};
					layout(std140) uniform UnityFog {
						vec4 unused_2_0;
						vec4 unity_FogParams;
					};
					in  vec4 in_POSITION0;
					out vec4 vs_TEXCOORD0;
					out vec4 vs_TEXCOORD1;
					out float vs_TEXCOORD2;
					vec4 u_xlat0;
					float u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * _Projector[1];
					    u_xlat0 = _Projector[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = _Projector[2] * in_POSITION0.zzzz + u_xlat0;
					    vs_TEXCOORD0 = _Projector[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat0 = in_POSITION0.yyyy * _ProjectorClip[1];
					    u_xlat0 = _ProjectorClip[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = _ProjectorClip[2] * in_POSITION0.zzzz + u_xlat0;
					    vs_TEXCOORD1 = _ProjectorClip[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat1 = u_xlat0.z * unity_FogParams.x;
					    gl_Position = u_xlat0;
					    u_xlat0.x = u_xlat1 * (-u_xlat1);
					    vs_TEXCOORD2 = exp2(u_xlat0.x);
					    return;
					}"
}
SubProgram "d3d11_9x " {
Keywords { "FOG_EXP2" }
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
						mat4x4 _Projector;
						mat4x4 _ProjectorClip;
						vec4 unused_0_3;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 glstate_matrix_mvp;
						vec4 unused_1_1[18];
					};
					layout(std140) uniform UnityFog {
						vec4 unused_2_0;
						vec4 unity_FogParams;
					};
					in  vec4 in_POSITION0;
					out vec4 vs_TEXCOORD0;
					out vec4 vs_TEXCOORD1;
					out float vs_TEXCOORD2;
					vec4 u_xlat0;
					float u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * _Projector[1];
					    u_xlat0 = _Projector[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = _Projector[2] * in_POSITION0.zzzz + u_xlat0;
					    vs_TEXCOORD0 = _Projector[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat0 = in_POSITION0.yyyy * _ProjectorClip[1];
					    u_xlat0 = _ProjectorClip[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = _ProjectorClip[2] * in_POSITION0.zzzz + u_xlat0;
					    vs_TEXCOORD1 = _ProjectorClip[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat1 = u_xlat0.z * unity_FogParams.x;
					    gl_Position = u_xlat0;
					    u_xlat0.x = u_xlat1 * (-u_xlat1);
					    vs_TEXCOORD2 = exp2(u_xlat0.x);
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
					//   float4 _Color;
					//   sampler2D _FalloffTex;
					//   sampler2D _ShadowTex;
					//
					//
					// Registers:
					//
					//   Name         Reg   Size
					//   ------------ ----- ----
					//   _Color       c0       1
					//   _ShadowTex   s0       1
					//   _FalloffTex  s1       1
					//
					
					    ps_2_0
					    def c1, 1, 0, 0, 0
					    dcl t0
					    dcl t1
					    dcl_2d s0
					    dcl_2d s1
					    texldp_pp r0, t1, s1
					    texldp_pp r1, t0, s0
					    mul_pp r2.xyz, r1, c0
					    add_pp r2.w, -r1.w, c1.x
					    mul_pp r0, r0.w, r2
					    mov_pp oC0, r0
					
					// approximately 6 instruction slots used (2 texture, 4 arithmetic)"
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
						vec4 unused_0_0[14];
						vec4 _Color;
					};
					uniform  sampler2D _ShadowTex;
					uniform  sampler2D _FalloffTex;
					in  vec4 vs_TEXCOORD0;
					in  vec4 vs_TEXCOORD1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD1.xy / vs_TEXCOORD1.ww;
					    u_xlat0 = texture(_FalloffTex, u_xlat0.xy);
					    u_xlat0.xy = vs_TEXCOORD0.xy / vs_TEXCOORD0.ww;
					    u_xlat1 = texture(_ShadowTex, u_xlat0.xy);
					    u_xlat2.xyz = u_xlat1.xyz * _Color.xyz;
					    u_xlat2.w = (-u_xlat1.w) + 1.0;
					    SV_Target0 = u_xlat0.wwww * u_xlat2;
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
						vec4 unused_0_0[14];
						vec4 _Color;
					};
					uniform  sampler2D _ShadowTex;
					uniform  sampler2D _FalloffTex;
					in  vec4 vs_TEXCOORD0;
					in  vec4 vs_TEXCOORD1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD1.xy / vs_TEXCOORD1.ww;
					    u_xlat0 = texture(_FalloffTex, u_xlat0.xy);
					    u_xlat0.xy = vs_TEXCOORD0.xy / vs_TEXCOORD0.ww;
					    u_xlat1 = texture(_ShadowTex, u_xlat0.xy);
					    u_xlat2.xyz = u_xlat1.xyz * _Color.xyz;
					    u_xlat2.w = (-u_xlat1.w) + 1.0;
					    SV_Target0 = u_xlat0.wwww * u_xlat2;
					    return;
					}"
}
SubProgram "d3d9 " {
Keywords { "FOG_EXP2" }
					"ps_2_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 6.3.9600.16384
					//
					// Parameters:
					//
					//   float4 _Color;
					//   sampler2D _FalloffTex;
					//   sampler2D _ShadowTex;
					//
					//
					// Registers:
					//
					//   Name         Reg   Size
					//   ------------ ----- ----
					//   _Color       c0       1
					//   _ShadowTex   s0       1
					//   _FalloffTex  s1       1
					//
					
					    ps_2_0
					    def c1, 1, 0, 0, 0
					    dcl t0
					    dcl t1
					    dcl t2.x
					    dcl_2d s0
					    dcl_2d s1
					    texldp_pp r0, t1, s1
					    texldp_pp r1, t0, s0
					    mul_pp r2.xyz, r1, c0
					    add_pp r2.w, -r1.w, c1.x
					    mul_pp r0, r0.w, r2
					    mov_sat r1.x, t2.x
					    mul_pp r0.xyz, r0, r1.x
					    mov_pp oC0, r0
					
					// approximately 8 instruction slots used (2 texture, 6 arithmetic)"
}
SubProgram "d3d11 " {
Keywords { "FOG_EXP2" }
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
						vec4 unused_0_0[14];
						vec4 _Color;
					};
					uniform  sampler2D _ShadowTex;
					uniform  sampler2D _FalloffTex;
					in  vec4 vs_TEXCOORD0;
					in  vec4 vs_TEXCOORD1;
					in  float vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD1.xy / vs_TEXCOORD1.ww;
					    u_xlat0 = texture(_FalloffTex, u_xlat0.xy);
					    u_xlat0.xy = vs_TEXCOORD0.xy / vs_TEXCOORD0.ww;
					    u_xlat1 = texture(_ShadowTex, u_xlat0.xy);
					    u_xlat2.xyz = u_xlat1.xyz * _Color.xyz;
					    u_xlat2.w = (-u_xlat1.w) + 1.0;
					    u_xlat0 = u_xlat0.wwww * u_xlat2;
					    u_xlat1.x = vs_TEXCOORD2;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    SV_Target0.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    SV_Target0.w = u_xlat0.w;
					    return;
					}"
}
SubProgram "d3d11_9x " {
Keywords { "FOG_EXP2" }
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
						vec4 unused_0_0[14];
						vec4 _Color;
					};
					uniform  sampler2D _ShadowTex;
					uniform  sampler2D _FalloffTex;
					in  vec4 vs_TEXCOORD0;
					in  vec4 vs_TEXCOORD1;
					in  float vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD1.xy / vs_TEXCOORD1.ww;
					    u_xlat0 = texture(_FalloffTex, u_xlat0.xy);
					    u_xlat0.xy = vs_TEXCOORD0.xy / vs_TEXCOORD0.ww;
					    u_xlat1 = texture(_ShadowTex, u_xlat0.xy);
					    u_xlat2.xyz = u_xlat1.xyz * _Color.xyz;
					    u_xlat2.w = (-u_xlat1.w) + 1.0;
					    u_xlat0 = u_xlat0.wwww * u_xlat2;
					    u_xlat1.x = vs_TEXCOORD2;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    SV_Target0.xyz = u_xlat0.xyz * u_xlat1.xxx;
					    SV_Target0.w = u_xlat0.w;
					    return;
					}"
}
}
 }
}
}