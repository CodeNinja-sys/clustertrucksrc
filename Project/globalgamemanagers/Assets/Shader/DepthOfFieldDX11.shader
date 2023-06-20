Shader "Hidden/Dof/DX11Dof" {
Properties {
 _MainTex ("", 2D) = "white" { }
 _BlurredColor ("", 2D) = "white" { }
 _FgCocMask ("", 2D) = "white" { }
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 13752
Program "vp" {
SubProgram "d3d11 " {
"vs_5_0
					
					#version 430
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
					precise vec4 u_xlat_precise_vec4;
					precise ivec4 u_xlat_precise_ivec4;
					precise bvec4 u_xlat_precise_bvec4;
					precise uvec4 u_xlat_precise_uvec4;
					UNITY_BINDING(0) uniform VGlobals {
						vec4 unused_0_0;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_2[7];
					};
					UNITY_BINDING(1) uniform UnityPerDraw {
						mat4x4 glstate_matrix_mvp;
						vec4 unused_1_1[18];
					};
					layout(location = 0) in  vec4 in_POSITION0;
					layout(location = 1) in  vec2 in_TEXCOORD0;
					layout(location = 0) out vec2 vs_TEXCOORD0;
					 vec4 phase0_Output0_1;
					layout(location = 1) out vec2 vs_TEXCOORD1;
					vec4 u_xlat0;
					bool u_xlatb0;
					float u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    gl_Position.xzw = u_xlat0.xzw;
					    u_xlatb0 = _MainTex_TexelSize.y<0.0;
					    gl_Position.y = (u_xlatb0) ? (-u_xlat0.y) : u_xlat0.y;
					    u_xlat1 = (-in_TEXCOORD0.y) + 1.0;
					    phase0_Output0_1.y = (u_xlatb0) ? u_xlat1 : in_TEXCOORD0.y;
					    phase0_Output0_1.xzw = in_TEXCOORD0.xxy;
					vs_TEXCOORD0 = phase0_Output0_1.xy;
					vs_TEXCOORD1 = phase0_Output0_1.zw;
					    return;
					}"
}
}
Program "fp" {
SubProgram "d3d11 " {
"ps_5_0
					
					//
					// Generated by Microsoft (R) D3D Shader Disassembler
					//
					//
					// Input signature:
					//
					// Name                 Index   Mask Register SysValue  Format   Used
					// -------------------- ----- ------ -------- -------- ------- ------
					// SV_POSITION              0   xyzw        0      POS   float       
					// TEXCOORD                 0   xy          1     NONE   float   xy  
					// TEXCOORD                 1     zw        1     NONE   float     zw
					//
					//
					// Output signature:
					//
					// Name                 Index   Mask Register SysValue  Format   Used
					// -------------------- ----- ------ -------- -------- ------- ------
					// SV_Target                0   xyzw        0   TARGET   float   xyzw
					//
					ps_5_0
					dcl_globalFlags refactoringAllowed
					dcl_constantbuffer CB0[7], immediateIndexed
					dcl_sampler s0, mode_default
					dcl_sampler s1, mode_default
					dcl_sampler s2, mode_default
					dcl_resource_texture2d (float,float,float,float) t0
					dcl_resource_texture2d (float,float,float,float) t1
					dcl_resource_texture2d (float,float,float,float) t2
					dcl_uav_structured u1, 28
					dcl_input_ps linear v1.xy
					dcl_input_ps linear v1.zw
					dcl_output o0.xyzw
					dcl_temps 4
					sample_indexable(texture2d)(float,float,float,float) r0.xyzw, v1.xyxx, t0.xyzw, s1
					mul r1.xyz, r0.xyzx, cb0[6].xyzx
					add r1.xz, r1.yyzy, r1.xxxx
					mad r1.x, r0.z, cb0[6].z, r1.x
					mul r1.y, r1.z, r1.y
					sqrt r1.y, r1.y
					dp2 r1.y, cb0[6].wwww, r1.yyyy
					add r1.x, r1.y, r1.x
					sample_indexable(texture2d)(float,float,float,float) r2.xyzw, v1.zwzz, t1.xyzw, s0
					mul r1.yzw, r2.xxyz, cb0[6].xxyz
					add r1.yw, r1.zzzw, r1.yyyy
					mad r1.y, r2.z, cb0[6].z, r1.y
					mul r1.z, r1.w, r1.z
					sqrt r1.z, r1.z
					dp2 r1.z, cb0[6].wwww, r1.zzzz
					add r1.y, r1.z, r1.y
					mul r1.z, r0.w, cb0[0].w
					lt r1.z, l(1.000000), r1.z
					lt r1.w, l(0.100000), r2.w
					and r1.z, r1.w, r1.z
					lt r1.w, cb0[0].z, r1.x
					and r1.z, r1.w, r1.z
					add r1.x, -r1.y, r1.x
					lt r1.x, cb0[2].w, |r1.x|
					and r1.x, r1.x, r1.z
					if_nz r1.x
					  sample_indexable(texture2d)(float,float,float,float) r1.z, v1.zwzz, t2.xywz, s2
					  mul r2.x, r0.w, l(4.000000)
					  mov_sat r2.x, r2.x
					  mul r2.xyz, r0.xyzx, r2.xxxx
					  imm_atomic_alloc r3.x, u1
					  mov r1.xy, v1.zwzz
					  mov r1.w, r2.x
					  store_structured u1.xyzw, r3.x, l(0), r1.xyzw
					  mov r2.w, r0.w
					  store_structured u1.xyz, r3.x, l(16), r2.yzwy
					  mad_sat r1.x, -r0.w, l(4.000000), l(1.000000)
					  mul o0.xyz, r0.xyzx, r1.xxxx
					  mov o0.w, r2.w
					  ret 
					endif 
					mov o0.xyzw, r0.xyzw
					ret 
					// Approximately 0 instruction slots used"
}
}
 }
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  Blend One One
  GpuProgramID 93346
Program "vp" {
SubProgram "d3d11 " {
"vs_5_0
					
					#version 430
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
					precise vec4 u_xlat_precise_vec4;
					precise ivec4 u_xlat_precise_ivec4;
					precise bvec4 u_xlat_precise_bvec4;
					precise uvec4 u_xlat_precise_uvec4;
					 struct pointBuffer_type {
						uint[1] value;
					};
					
					layout(std430, binding = 0) readonly buffer pointBuffer {
						pointBuffer_type pointBuffer_buf[];
					};
					layout(location = 0) out float vs_TEXCOORD2;
					layout(location = 1) out vec4 vs_TEXCOORD1;
					vec3 u_xlat0;
					void main()
					{
					    gl_Position.zw = vec2(0.0, 1.0);
					    u_xlat0.xyz = vec3(uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(0 >> 2) + 0]), uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(0 >> 2) + 1]), uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(0 >> 2) + 2]));
					    u_xlat0.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
					    vs_TEXCOORD2 = u_xlat0.z;
					    gl_Position.y = (-u_xlat0.y);
					    gl_Position.x = u_xlat0.x;
					    vs_TEXCOORD1 = vec4(uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(12 >> 2) + 0]), uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(12 >> 2) + 1]), uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(12 >> 2) + 2]), uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(12 >> 2) + 3]));
					    return;
					}"
}
}
Program "fp" {
SubProgram "d3d11 " {
"ps_5_0
					
					#version 430
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
					precise vec4 u_xlat_precise_vec4;
					precise ivec4 u_xlat_precise_ivec4;
					precise bvec4 u_xlat_precise_bvec4;
					precise uvec4 u_xlat_precise_uvec4;
					UNITY_LOCATION(0) uniform  sampler2D _MainTex;
					layout(location = 0) in  vec3 vs_TEXCOORD0;
					layout(location = 1) in  vec4 vs_TEXCOORD1;
					layout(location = 2) in  vec4 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec2 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					bvec2 u_xlatb1;
					vec4 u_xlat2;
					bvec2 u_xlatb6;
					void main()
					{
					    u_xlat0.xy = (-vs_TEXCOORD2.xy) + vec2(1.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * vec2(0.5, 0.5);
					    u_xlat0.xy = vs_TEXCOORD0.xy * vs_TEXCOORD2.xy + u_xlat0.xy;
					    u_xlatb6.xy = lessThan(vec4(0.0, 0.0, 0.0, 0.0), u_xlat0.xyxy).xy;
					    u_xlatb1.xy = lessThan(u_xlat0.xyxx, vec4(1.0, 1.0, 0.0, 0.0)).xy;
					    u_xlat2.xyz = texture(_MainTex, u_xlat0.xy).xyz;
					    u_xlatb0 = u_xlatb6.y && u_xlatb1.y;
					    u_xlatb0 = u_xlatb0 && u_xlatb1.x;
					    u_xlatb0 = u_xlatb0 && u_xlatb6.x;
					    u_xlat0.x = u_xlatb0 ? 1.0 : float(0.0);
					    u_xlat1.xyz = vs_TEXCOORD1.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat2.w = vs_TEXCOORD0.z;
					    u_xlat1 = u_xlat1 * u_xlat2;
					    SV_Target0 = u_xlat0.xxxx * u_xlat1;
					    return;
					}"
}
}
Program "gp" {
SubProgram "d3d11 " {
"gs_5_0
					
					#version 430
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
					precise vec4 u_xlat_precise_vec4;
					precise ivec4 u_xlat_precise_ivec4;
					precise bvec4 u_xlat_precise_bvec4;
					precise uvec4 u_xlat_precise_uvec4;
					UNITY_BINDING(0) uniform GGlobals {
						vec4 _BokehParams;
						vec4 unused_0_1;
						vec3 _Screen;
						vec4 unused_0_3[6];
					};
					layout(location = 0) in  vec2 vs_TEXCOORD0 [1];
					layout(location = 1) in  float vs_TEXCOORD2 [1];
					layout(location = 2) in  vec4 vs_TEXCOORD1 [1];
					vec4 u_xlat0;
					float u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					float u_xlat5;
					layout(points) in;
					layout(triangle_strip) out;
					layout(location = 0) out vec3 gs_TEXCOORD0;
					layout(location = 1) out vec4 gs_TEXCOORD1;
					layout(location = 2) out vec4 gs_TEXCOORD2;
					layout(max_vertices = 4) out;
					void main()
					{
					    u_xlat0.z = float(0.0);
					    u_xlat0.w = float(0.0);
					    u_xlat1 = _BokehParams.w * vs_TEXCOORD1[0].w;
					    u_xlat1 = u_xlat1 * _BokehParams.x + 0.5;
					    u_xlat5 = floor(u_xlat1);
					    u_xlat1 = u_xlat1 * 2.0 + 1.0;
					    u_xlat5 = u_xlat5 * 2.0 + 3.0;
					    u_xlat2.xy = vec2(u_xlat5) * _Screen.xy;
					    u_xlat5 = u_xlat5 / u_xlat1;
					    u_xlat1 = u_xlat1 * u_xlat1;
					    u_xlat1 = _BokehParams.y / u_xlat1;
					    u_xlat3 = vec4(u_xlat1) * vs_TEXCOORD1[0];
					    u_xlat0.xy = u_xlat2.xy * vec2(-1.0, 1.0);
					    u_xlat0 = u_xlat0 + gl_in[0].gl_Position;
					    gl_Position = u_xlat0;
					    gs_TEXCOORD0.xy = vec2(0.0, 1.0);
					    gs_TEXCOORD0.z = vs_TEXCOORD2[0];
					    gs_TEXCOORD1 = u_xlat3;
					    gs_TEXCOORD2.xy = vec2(u_xlat5);
					    gs_TEXCOORD2.zw = vec2(0.0, 0.0);
					    EmitVertex();
					    u_xlat2.z = 0.0;
					    u_xlat0 = u_xlat2.xyzz + gl_in[0].gl_Position;
					    gl_Position = u_xlat0;
					    gs_TEXCOORD0.xy = vec2(1.0, 1.0);
					    gs_TEXCOORD0.z = vs_TEXCOORD2[0];
					    gs_TEXCOORD1 = u_xlat3;
					    gs_TEXCOORD2.xy = vec2(u_xlat5);
					    gs_TEXCOORD2.zw = vec2(0.0, 0.0);
					    EmitVertex();
					    u_xlat0 = u_xlat2.xyzz * vec4(-1.0, -1.0, 1.0, 1.0) + gl_in[0].gl_Position;
					    gl_Position = u_xlat0;
					    gs_TEXCOORD0.xy = vec2(0.0, 0.0);
					    gs_TEXCOORD0.z = vs_TEXCOORD2[0];
					    gs_TEXCOORD1 = u_xlat3;
					    gs_TEXCOORD2.xy = vec2(u_xlat5);
					    gs_TEXCOORD2.zw = vec2(0.0, 0.0);
					    EmitVertex();
					    u_xlat2.w = (-u_xlat2.y);
					    u_xlat0 = u_xlat2.xwzz + gl_in[0].gl_Position;
					    gl_Position = u_xlat0;
					    gs_TEXCOORD0.xy = vec2(1.0, 0.0);
					    gs_TEXCOORD0.z = vs_TEXCOORD2[0];
					    gs_TEXCOORD1 = u_xlat3;
					    gs_TEXCOORD2.xy = vec2(u_xlat5);
					    gs_TEXCOORD2.zw = vec2(0.0, 0.0);
					    EmitVertex();
					    EndPrimitive();
					    return;
					}"
}
}
 }
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  Blend DstAlpha One, Zero One
  GpuProgramID 144434
Program "vp" {
SubProgram "d3d11 " {
"vs_5_0
					
					#version 430
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
					precise vec4 u_xlat_precise_vec4;
					precise ivec4 u_xlat_precise_ivec4;
					precise bvec4 u_xlat_precise_bvec4;
					precise uvec4 u_xlat_precise_uvec4;
					 struct pointBuffer_type {
						uint[1] value;
					};
					
					layout(std430, binding = 0) readonly buffer pointBuffer {
						pointBuffer_type pointBuffer_buf[];
					};
					layout(location = 0) out float vs_TEXCOORD2;
					layout(location = 1) out vec4 vs_TEXCOORD1;
					vec3 u_xlat0;
					void main()
					{
					    gl_Position.zw = vec2(0.0, 1.0);
					    u_xlat0.xyz = vec3(uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(0 >> 2) + 0]), uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(0 >> 2) + 1]), uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(0 >> 2) + 2]));
					    u_xlat0.xy = u_xlat0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
					    vs_TEXCOORD2 = u_xlat0.z;
					    gl_Position.y = (-u_xlat0.y);
					    gl_Position.x = u_xlat0.x;
					    vs_TEXCOORD1 = vec4(uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(12 >> 2) + 0]), uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(12 >> 2) + 1]), uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(12 >> 2) + 2]), uintBitsToFloat(pointBuffer_buf[gl_VertexID].value[(12 >> 2) + 3]));
					    return;
					}"
}
}
Program "fp" {
SubProgram "d3d11 " {
"ps_5_0
					
					#version 430
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
					precise vec4 u_xlat_precise_vec4;
					precise ivec4 u_xlat_precise_ivec4;
					precise bvec4 u_xlat_precise_bvec4;
					precise uvec4 u_xlat_precise_uvec4;
					UNITY_LOCATION(0) uniform  sampler2D _MainTex;
					layout(location = 0) in  vec3 vs_TEXCOORD0;
					layout(location = 1) in  vec4 vs_TEXCOORD1;
					layout(location = 2) in  vec4 vs_TEXCOORD2;
					layout(location = 0) out vec4 SV_Target0;
					vec2 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					bvec2 u_xlatb1;
					vec4 u_xlat2;
					bvec2 u_xlatb6;
					void main()
					{
					    u_xlat0.xy = (-vs_TEXCOORD2.xy) + vec2(1.0, 1.0);
					    u_xlat0.xy = u_xlat0.xy * vec2(0.5, 0.5);
					    u_xlat0.xy = vs_TEXCOORD0.xy * vs_TEXCOORD2.xy + u_xlat0.xy;
					    u_xlatb6.xy = lessThan(vec4(0.0, 0.0, 0.0, 0.0), u_xlat0.xyxy).xy;
					    u_xlatb1.xy = lessThan(u_xlat0.xyxx, vec4(1.0, 1.0, 0.0, 0.0)).xy;
					    u_xlat2.xyz = texture(_MainTex, u_xlat0.xy).xyz;
					    u_xlatb0 = u_xlatb6.y && u_xlatb1.y;
					    u_xlatb0 = u_xlatb0 && u_xlatb1.x;
					    u_xlatb0 = u_xlatb0 && u_xlatb6.x;
					    u_xlat0.x = u_xlatb0 ? 1.0 : float(0.0);
					    u_xlat1.xyz = vs_TEXCOORD1.xyz;
					    u_xlat1.w = 1.0;
					    u_xlat2.w = vs_TEXCOORD0.z;
					    u_xlat1 = u_xlat1 * u_xlat2;
					    SV_Target0 = u_xlat0.xxxx * u_xlat1;
					    return;
					}"
}
}
Program "gp" {
SubProgram "d3d11 " {
"gs_5_0
					
					#version 430
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
					precise vec4 u_xlat_precise_vec4;
					precise ivec4 u_xlat_precise_ivec4;
					precise bvec4 u_xlat_precise_bvec4;
					precise uvec4 u_xlat_precise_uvec4;
					UNITY_BINDING(0) uniform GGlobals {
						vec4 _BokehParams;
						vec4 unused_0_1;
						vec3 _Screen;
						vec4 unused_0_3[6];
					};
					layout(location = 0) in  vec2 vs_TEXCOORD0 [1];
					layout(location = 1) in  float vs_TEXCOORD2 [1];
					layout(location = 2) in  vec4 vs_TEXCOORD1 [1];
					vec4 u_xlat0;
					float u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					float u_xlat5;
					layout(points) in;
					layout(triangle_strip) out;
					layout(location = 0) out vec3 gs_TEXCOORD0;
					layout(location = 1) out vec4 gs_TEXCOORD1;
					layout(location = 2) out vec4 gs_TEXCOORD2;
					layout(max_vertices = 4) out;
					void main()
					{
					    u_xlat0.z = float(0.0);
					    u_xlat0.w = float(0.0);
					    u_xlat1 = _BokehParams.w * vs_TEXCOORD1[0].w;
					    u_xlat1 = u_xlat1 * _BokehParams.x + 0.5;
					    u_xlat5 = floor(u_xlat1);
					    u_xlat1 = u_xlat1 * 2.0 + 1.0;
					    u_xlat5 = u_xlat5 * 2.0 + 3.0;
					    u_xlat2.xy = vec2(u_xlat5) * _Screen.xy;
					    u_xlat5 = u_xlat5 / u_xlat1;
					    u_xlat1 = u_xlat1 * u_xlat1;
					    u_xlat1 = _BokehParams.y / u_xlat1;
					    u_xlat3 = vec4(u_xlat1) * vs_TEXCOORD1[0];
					    u_xlat0.xy = u_xlat2.xy * vec2(-1.0, 1.0);
					    u_xlat0 = u_xlat0 + gl_in[0].gl_Position;
					    gl_Position = u_xlat0;
					    gs_TEXCOORD0.xy = vec2(0.0, 1.0);
					    gs_TEXCOORD0.z = vs_TEXCOORD2[0];
					    gs_TEXCOORD1 = u_xlat3;
					    gs_TEXCOORD2.xy = vec2(u_xlat5);
					    gs_TEXCOORD2.zw = vec2(0.0, 0.0);
					    EmitVertex();
					    u_xlat2.z = 0.0;
					    u_xlat0 = u_xlat2.xyzz + gl_in[0].gl_Position;
					    gl_Position = u_xlat0;
					    gs_TEXCOORD0.xy = vec2(1.0, 1.0);
					    gs_TEXCOORD0.z = vs_TEXCOORD2[0];
					    gs_TEXCOORD1 = u_xlat3;
					    gs_TEXCOORD2.xy = vec2(u_xlat5);
					    gs_TEXCOORD2.zw = vec2(0.0, 0.0);
					    EmitVertex();
					    u_xlat0 = u_xlat2.xyzz * vec4(-1.0, -1.0, 1.0, 1.0) + gl_in[0].gl_Position;
					    gl_Position = u_xlat0;
					    gs_TEXCOORD0.xy = vec2(0.0, 0.0);
					    gs_TEXCOORD0.z = vs_TEXCOORD2[0];
					    gs_TEXCOORD1 = u_xlat3;
					    gs_TEXCOORD2.xy = vec2(u_xlat5);
					    gs_TEXCOORD2.zw = vec2(0.0, 0.0);
					    EmitVertex();
					    u_xlat2.w = (-u_xlat2.y);
					    u_xlat0 = u_xlat2.xwzz + gl_in[0].gl_Position;
					    gl_Position = u_xlat0;
					    gs_TEXCOORD0.xy = vec2(1.0, 0.0);
					    gs_TEXCOORD0.z = vs_TEXCOORD2[0];
					    gs_TEXCOORD1 = u_xlat3;
					    gs_TEXCOORD2.xy = vec2(u_xlat5);
					    gs_TEXCOORD2.zw = vec2(0.0, 0.0);
					    EmitVertex();
					    EndPrimitive();
					    return;
					}"
}
}
 }
}
Fallback Off
}