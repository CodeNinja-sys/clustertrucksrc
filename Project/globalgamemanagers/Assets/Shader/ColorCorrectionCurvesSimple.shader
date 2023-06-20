Shader "Hidden/ColorCorrectionCurvesSimple" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "" { }
 _RgbTex ("_RgbTex (RGB)", 2D) = "" { }
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 49602
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
					//   sampler2D _RgbTex;
					//   float _Saturation;
					//   float4 unity_ColorSpaceLuminance;
					//
					//
					// Registers:
					//
					//   Name                      Reg   Size
					//   ------------------------- ----- ----
					//   unity_ColorSpaceLuminance c0       1
					//   _Saturation               c1       1
					//   _MainTex                  s0       1
					//   _RgbTex                   s1       1
					//
					
					    ps_2_0
					    def c2, 0.125, 0.375, 0.625, 2
					    def c3, 0, 1, 0, 0
					    dcl_pp t0.xy
					    dcl_2d s0
					    dcl_2d s1
					    texld_pp r0, t0, s0
					    mov_pp r1.y, c2.y
					    mov_pp r1.x, r0.y
					    mov_pp r0.y, c2.x
					    mov_pp r2.x, r0.z
					    mov_pp r2.y, c2.z
					    texld r1, r1, s1
					    texld r3, r0, s1
					    texld r2, r2, s1
					    mul_pp r1.xyz, r1, c3
					    mad_pp r1.xyz, r3, c3.yzxw, r1
					    mad_pp r1.xyz, r2, c3.zxyw, r1
					    mul_pp r2.xyz, r1, c0
					    add_pp r1.w, r2.z, r2.x
					    mul_pp r1.w, r1.w, r2.y
					    add_pp r2.x, r2.y, r2.x
					    mad_pp r2.x, r1.z, c0.z, r2.x
					    rsq_pp r1.w, r1.w
					    rcp_pp r1.w, r1.w
					    mul_pp r1.w, r1.w, c0.w
					    mad_pp r1.w, r1.w, c2.w, r2.x
					    lrp_pp r0.xyz, c1.x, r1, r1.w
					    mov_pp oC0, r0
					
					// approximately 23 instruction slots used (4 texture, 19 arithmetic)"
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
						vec4 unused_0_0[3];
						vec4 unity_ColorSpaceLuminance;
						vec4 unused_0_2[2];
						float _Saturation;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _RgbTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					float u_xlat9;
					void main()
					{
					    u_xlat0.y = float(0.125);
					    u_xlat0.w = float(0.375);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy).zxyw;
					    u_xlat0.xz = u_xlat1.yz;
					    u_xlat2 = texture(_RgbTex, u_xlat0.zw);
					    u_xlat0 = texture(_RgbTex, u_xlat0.xy);
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.0, 1.0, 0.0);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(1.0, 0.0, 0.0) + u_xlat2.xyz;
					    SV_Target0.w = u_xlat1.w;
					    u_xlat1.y = 0.625;
					    u_xlat1 = texture(_RgbTex, u_xlat1.xy);
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.0, 0.0, 1.0) + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat1.xz = u_xlat1.yz + u_xlat1.xx;
					    u_xlat9 = u_xlat1.z * u_xlat1.y;
					    u_xlat1.x = u_xlat0.z * unity_ColorSpaceLuminance.z + u_xlat1.x;
					    u_xlat9 = sqrt(u_xlat9);
					    u_xlat9 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat9));
					    u_xlat9 = u_xlat9 + u_xlat1.x;
					    u_xlat0.xyz = (-vec3(u_xlat9)) + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Saturation) * u_xlat0.xyz + vec3(u_xlat9);
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
						vec4 unused_0_0[3];
						vec4 unity_ColorSpaceLuminance;
						vec4 unused_0_2[2];
						float _Saturation;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _RgbTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					float u_xlat9;
					void main()
					{
					    u_xlat0.y = float(0.125);
					    u_xlat0.w = float(0.375);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy).zxyw;
					    u_xlat0.xz = u_xlat1.yz;
					    u_xlat2 = texture(_RgbTex, u_xlat0.zw);
					    u_xlat0 = texture(_RgbTex, u_xlat0.xy);
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.0, 1.0, 0.0);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(1.0, 0.0, 0.0) + u_xlat2.xyz;
					    SV_Target0.w = u_xlat1.w;
					    u_xlat1.y = 0.625;
					    u_xlat1 = texture(_RgbTex, u_xlat1.xy);
					    u_xlat0.xyz = u_xlat1.xyz * vec3(0.0, 0.0, 1.0) + u_xlat0.xyz;
					    u_xlat9 = dot(u_xlat0.xyz, unity_ColorSpaceLuminance.xyz);
					    u_xlat0.xyz = (-vec3(u_xlat9)) + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Saturation) * u_xlat0.xyz + vec3(u_xlat9);
					    return;
					}"
}
}
 }
}
Fallback Off
}