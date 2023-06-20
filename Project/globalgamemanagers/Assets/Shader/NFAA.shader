Shader "Hidden/NFAA" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" { }
 _BlurTex ("Base (RGB)", 2D) = "white" { }
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 19850
Program "vp" {
SubProgram "d3d9 " {
"vs_3_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 6.3.9600.16384
					//
					// Parameters:
					//
					//   float4 _MainTex_TexelSize;
					//   float _OffsetScale;
					//   row_major float4x4 glstate_matrix_mvp;
					//
					//
					// Registers:
					//
					//   Name               Reg   Size
					//   ------------------ ----- ----
					//   glstate_matrix_mvp c0       4
					//   _MainTex_TexelSize c4       1
					//   _OffsetScale       c5       1
					//
					
					    vs_3_0
					    def c6, 0, 0, 0, 0
					    dcl_position v0
					    dcl_texcoord v1
					    dcl_position o0
					    dcl_texcoord o1.xy
					    dcl_texcoord1 o2.xy
					    dcl_texcoord2 o3.xy
					    dcl_texcoord3 o4.xy
					    dcl_texcoord4 o5.xy
					    dcl_texcoord5 o6.xy
					    dcl_texcoord6 o7.xy
					    dcl_texcoord7 o8.xy
					    dp4 o0.x, c0, v0
					    dp4 o0.y, c1, v0
					    dp4 o0.z, c2, v0
					    dp4 o0.w, c3, v0
					    mov r0.xy, c4
					    mul r0.yz, r0.xyxw, c5.x
					    mov r0.xw, c6.x
					    add o1.xy, r0, v1
					    add o2.xy, -r0, v1
					    add r1.xy, -r0.zwzw, v1
					    add o5.xy, r0, r1
					    add o6.xy, -r0, r1
					    mov o4.xy, r1
					    add r0.zw, r0, v1.xyxy
					    add o7.xy, r0, r0.zwzw
					    add o8.xy, -r0, r0.zwzw
					    mov o3.xy, r0.zwzw
					
					// approximately 17 instruction slots used"
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
						float _OffsetScale;
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
					out vec2 vs_TEXCOORD5;
					out vec2 vs_TEXCOORD6;
					out vec2 vs_TEXCOORD7;
					vec4 u_xlat0;
					vec2 u_xlat1;
					vec2 u_xlat4;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat0.yz = _MainTex_TexelSize.yx * vec2(_OffsetScale);
					    u_xlat0.x = float(0.0);
					    u_xlat0.w = float(0.0);
					    vs_TEXCOORD0.xy = u_xlat0.xy + in_TEXCOORD0.xy;
					    vs_TEXCOORD1.xy = (-u_xlat0.xy) + in_TEXCOORD0.xy;
					    u_xlat1.xy = u_xlat0.zw + in_TEXCOORD0.xy;
					    vs_TEXCOORD2.xy = u_xlat1.xy;
					    u_xlat4.xy = (-u_xlat0.zw) + in_TEXCOORD0.xy;
					    vs_TEXCOORD3.xy = u_xlat4.xy;
					    vs_TEXCOORD4.xy = u_xlat0.xy + u_xlat4.xy;
					    vs_TEXCOORD5.xy = (-u_xlat0.xy) + u_xlat4.xy;
					    vs_TEXCOORD6.xy = u_xlat0.xy + u_xlat1.xy;
					    vs_TEXCOORD7.xy = (-u_xlat0.xy) + u_xlat1.xy;
					    return;
					}"
}
}
Program "fp" {
SubProgram "d3d9 " {
"ps_3_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 6.3.9600.16384
					//
					// Parameters:
					//
					//   float _BlurRadius;
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
					//   _BlurRadius               c2       1
					//   _MainTex                  s0       1
					//
					
					    ps_3_0
					    def c3, 1, 0.5, 0.200000003, 0
					    dcl_texcoord v0.xy
					    dcl_texcoord1 v1.xy
					    dcl_texcoord2 v2.xy
					    dcl_texcoord3 v3.xy
					    dcl_texcoord4 v4.xy
					    dcl_texcoord5 v5.xy
					    dcl_texcoord6 v6.xy
					    dcl_texcoord7 v7.xy
					    dcl_2d s0
					    texld_pp r0, v2, s0
					    mul_pp r0.xyw, r0.xyzz, c0.xyzz
					    add_pp r0.xw, r0.yyzw, r0.x
					    mul_pp r0.y, r0.w, r0.y
					    mad_pp r0.x, r0.z, c0.z, r0.x
					    rsq_pp r0.y, r0.y
					    rcp_pp r0.y, r0.y
					    dp2add_pp r0.y, c0.w, r0.y, r0.x
					    texld_pp r1, v4, s0
					    mul_pp r1.xyw, r1.xyzz, c0.xyzz
					    add_pp r1.xw, r1.yyzw, r1.x
					    mul_pp r0.w, r1.w, r1.y
					    mad_pp r1.x, r1.z, c0.z, r1.x
					    rsq_pp r0.w, r0.w
					    rcp_pp r0.w, r0.w
					    dp2add_pp r1.z, c0.w, r0.w, r1.x
					    mov_pp r0.x, r1.z
					    texld_pp r2, v5, s0
					    mul_pp r2.xyw, r2.xyzz, c0.xyzz
					    add_pp r2.xw, r2.yyzw, r2.x
					    mul_pp r0.w, r2.w, r2.y
					    mad_pp r1.w, r2.z, c0.z, r2.x
					    rsq_pp r0.w, r0.w
					    rcp_pp r0.w, r0.w
					    dp2add_pp r0.z, c0.w, r0.w, r1.w
					    dp3 r0.w, c3.x, r0
					    texld_pp r2, v3, s0
					    mul_pp r2.xyw, r2.xyzz, c0.xyzz
					    add_pp r2.xw, r2.yyzw, r2.x
					    mul_pp r1.w, r2.w, r2.y
					    mad_pp r2.x, r2.z, c0.z, r2.x
					    rsq_pp r1.w, r1.w
					    rcp_pp r1.w, r1.w
					    dp2add_pp r2.y, c0.w, r1.w, r2.x
					    texld_pp r3, v6, s0
					    mul_pp r3.xyw, r3.xyzz, c0.xyzz
					    add_pp r3.xw, r3.yyzw, r3.x
					    mul_pp r1.w, r3.w, r3.y
					    mad_pp r2.w, r3.z, c0.z, r3.x
					    rsq_pp r1.w, r1.w
					    rcp_pp r1.w, r1.w
					    dp2add_pp r0.y, c0.w, r1.w, r2.w
					    mov_pp r2.x, r0.y
					    texld_pp r3, v7, s0
					    mul_pp r3.xyw, r3.xyzz, c0.xyzz
					    add_pp r3.xw, r3.yyzw, r3.x
					    mul_pp r1.w, r3.w, r3.y
					    mad_pp r2.w, r3.z, c0.z, r3.x
					    rsq_pp r1.w, r1.w
					    rcp_pp r1.w, r1.w
					    dp2add_pp r1.x, c0.w, r1.w, r2.w
					    mov_pp r2.z, r1.x
					    dp3 r1.w, c3.x, r2
					    add r2.y, -r0.w, r1.w
					    texld_pp r3, v1, s0
					    mul_pp r3.xyw, r3.xyzz, c0.xyzz
					    add_pp r2.zw, r3.xyyw, r3.x
					    mul_pp r0.w, r2.w, r3.y
					    mad_pp r1.w, r3.z, c0.z, r2.z
					    rsq_pp r0.w, r0.w
					    rcp_pp r0.w, r0.w
					    dp2add_pp r1.y, c0.w, r0.w, r1.w
					    dp3 r0.w, c3.x, r1
					    texld_pp r1, v0, s0
					    mul_pp r1.xyw, r1.xyzz, c0.xyzz
					    add_pp r1.xw, r1.yyzw, r1.x
					    mul_pp r1.y, r1.w, r1.y
					    mad_pp r1.x, r1.z, c0.z, r1.x
					    rsq_pp r1.y, r1.y
					    rcp_pp r1.y, r1.y
					    dp2add_pp r0.x, c0.w, r1.y, r1.x
					    dp3 r0.x, c3.x, r0
					    add r2.x, -r0.x, r0.w
					    mov r0.xy, c1
					    mul r0.xy, r0, c2.x
					    mul r0.xy, r0, r2
					    mov r1.xy, v0
					    add r1.xy, r1, v1
					    mad r1.zw, r1.xyxy, c3.y, r0.xyxy
					    texld r2, r1.zwzw, s0
					    mul r1.zw, r1.xyxy, c3.y
					    texld r3, r1.zwzw, s0
					    add r2, r2, r3
					    mad r1.zw, r1.xyxy, c3.y, -r0.xyxy
					    texld r3, r1.zwzw, s0
					    add r2, r2, r3
					    mov r0.z, -r0.y
					    mad r0.yw, r1.xxzy, c3.y, r0.xxzz
					    mad r0.xz, r1.xyyw, c3.y, -r0
					    texld r1, r0.xzzw, s0
					    texld r0, r0.ywzw, s0
					    add r0, r0, r2
					    add r0, r1, r0
					    mul_pp oC0, r0, c3.z
					
					// approximately 102 instruction slots used (13 texture, 89 arithmetic)"
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
						vec4 _MainTex_TexelSize;
						float _BlurRadius;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					in  vec2 vs_TEXCOORD5;
					in  vec2 vs_TEXCOORD6;
					in  vec2 vs_TEXCOORD7;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec3 u_xlat4;
					float u_xlat5;
					vec2 u_xlat9;
					vec2 u_xlat10;
					float u_xlat12;
					float u_xlat13;
					float u_xlat14;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD2.xy);
					    u_xlat0.xyw = u_xlat0.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat0.xw = u_xlat0.yw + u_xlat0.xx;
					    u_xlat4.x = u_xlat0.w * u_xlat0.y;
					    u_xlat0.x = u_xlat0.z * unity_ColorSpaceLuminance.z + u_xlat0.x;
					    u_xlat4.x = sqrt(u_xlat4.x);
					    u_xlat4.x = dot(unity_ColorSpaceLuminance.ww, u_xlat4.xx);
					    u_xlat0.y = u_xlat4.x + u_xlat0.x;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD4.xy);
					    u_xlat1.xyw = u_xlat1.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat1.xw = u_xlat1.yw + u_xlat1.xx;
					    u_xlat12 = u_xlat1.w * u_xlat1.y;
					    u_xlat1.x = u_xlat1.z * unity_ColorSpaceLuminance.z + u_xlat1.x;
					    u_xlat12 = sqrt(u_xlat12);
					    u_xlat12 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat12));
					    u_xlat1.z = u_xlat12 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD5.xy);
					    u_xlat2.xyw = u_xlat2.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat2.xw = u_xlat2.yw + u_xlat2.xx;
					    u_xlat12 = u_xlat2.w * u_xlat2.y;
					    u_xlat13 = u_xlat2.z * unity_ColorSpaceLuminance.z + u_xlat2.x;
					    u_xlat12 = sqrt(u_xlat12);
					    u_xlat12 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat12));
					    u_xlat0.z = u_xlat12 + u_xlat13;
					    u_xlat12 = dot(vec3(1.0, 1.0, 1.0), u_xlat0.xyz);
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD3.xy);
					    u_xlat2.xyw = u_xlat2.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat2.xw = u_xlat2.yw + u_xlat2.xx;
					    u_xlat13 = u_xlat2.w * u_xlat2.y;
					    u_xlat2.x = u_xlat2.z * unity_ColorSpaceLuminance.z + u_xlat2.x;
					    u_xlat13 = sqrt(u_xlat13);
					    u_xlat13 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat13));
					    u_xlat2.y = u_xlat13 + u_xlat2.x;
					    u_xlat3 = texture(_MainTex, vs_TEXCOORD6.xy);
					    u_xlat3.xyw = u_xlat3.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat3.xw = u_xlat3.yw + u_xlat3.xx;
					    u_xlat13 = u_xlat3.w * u_xlat3.y;
					    u_xlat14 = u_xlat3.z * unity_ColorSpaceLuminance.z + u_xlat3.x;
					    u_xlat13 = sqrt(u_xlat13);
					    u_xlat13 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat13));
					    u_xlat0.y = u_xlat13 + u_xlat14;
					    u_xlat2.x = u_xlat0.y;
					    u_xlat3 = texture(_MainTex, vs_TEXCOORD7.xy);
					    u_xlat3.xyw = u_xlat3.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat3.xw = u_xlat3.yw + u_xlat3.xx;
					    u_xlat13 = u_xlat3.w * u_xlat3.y;
					    u_xlat14 = u_xlat3.z * unity_ColorSpaceLuminance.z + u_xlat3.x;
					    u_xlat13 = sqrt(u_xlat13);
					    u_xlat13 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat13));
					    u_xlat1.x = u_xlat13 + u_xlat14;
					    u_xlat2.z = u_xlat1.x;
					    u_xlat13 = dot(vec3(1.0, 1.0, 1.0), u_xlat2.xyz);
					    u_xlat2.y = (-u_xlat12) + u_xlat13;
					    u_xlat3 = texture(_MainTex, vs_TEXCOORD1.xy);
					    u_xlat3.xyw = u_xlat3.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat10.xy = u_xlat3.yw + u_xlat3.xx;
					    u_xlat12 = u_xlat10.y * u_xlat3.y;
					    u_xlat13 = u_xlat3.z * unity_ColorSpaceLuminance.z + u_xlat10.x;
					    u_xlat12 = sqrt(u_xlat12);
					    u_xlat12 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat12));
					    u_xlat1.y = u_xlat12 + u_xlat13;
					    u_xlat12 = dot(vec3(1.0, 1.0, 1.0), u_xlat1.xyz);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xyw = u_xlat1.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat1.xw = u_xlat1.yw + u_xlat1.xx;
					    u_xlat5 = u_xlat1.w * u_xlat1.y;
					    u_xlat1.x = u_xlat1.z * unity_ColorSpaceLuminance.z + u_xlat1.x;
					    u_xlat5 = sqrt(u_xlat5);
					    u_xlat5 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat5));
					    u_xlat0.x = u_xlat5 + u_xlat1.x;
					    u_xlat0.x = dot(vec3(1.0, 1.0, 1.0), u_xlat0.xyz);
					    u_xlat2.x = (-u_xlat0.x) + u_xlat12;
					    u_xlat0.xy = _MainTex_TexelSize.xy * vec2(vec2(_BlurRadius, _BlurRadius));
					    u_xlat0.xy = u_xlat0.xy * u_xlat2.xy;
					    u_xlat1.xy = vs_TEXCOORD0.xy + vs_TEXCOORD1.xy;
					    u_xlat9.xy = u_xlat1.xy * vec2(0.5, 0.5) + u_xlat0.xy;
					    u_xlat2 = texture(_MainTex, u_xlat9.xy);
					    u_xlat9.xy = u_xlat1.xy * vec2(0.5, 0.5);
					    u_xlat3 = texture(_MainTex, u_xlat9.xy);
					    u_xlat2 = u_xlat2 + u_xlat3;
					    u_xlat9.xy = u_xlat1.xy * vec2(0.5, 0.5) + (-u_xlat0.xy);
					    u_xlat3 = texture(_MainTex, u_xlat9.xy);
					    u_xlat2 = u_xlat2 + u_xlat3;
					    u_xlat0.z = (-u_xlat0.y);
					    u_xlat4.xz = u_xlat1.xy * vec2(0.5, 0.5) + u_xlat0.xz;
					    u_xlat0.xz = u_xlat1.xy * vec2(0.5, 0.5) + (-u_xlat0.xz);
					    u_xlat1 = texture(_MainTex, u_xlat0.xz);
					    u_xlat0 = texture(_MainTex, u_xlat4.xz);
					    u_xlat0 = u_xlat0 + u_xlat2;
					    u_xlat0 = u_xlat1 + u_xlat0;
					    SV_Target0 = u_xlat0 * vec4(0.200000003, 0.200000003, 0.200000003, 0.200000003);
					    return;
					}"
}
}
 }
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 74856
Program "vp" {
SubProgram "d3d9 " {
"vs_3_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 6.3.9600.16384
					//
					// Parameters:
					//
					//   float4 _MainTex_TexelSize;
					//   float _OffsetScale;
					//   row_major float4x4 glstate_matrix_mvp;
					//
					//
					// Registers:
					//
					//   Name               Reg   Size
					//   ------------------ ----- ----
					//   glstate_matrix_mvp c0       4
					//   _MainTex_TexelSize c4       1
					//   _OffsetScale       c5       1
					//
					
					    vs_3_0
					    def c6, 0, 0, 0, 0
					    dcl_position v0
					    dcl_texcoord v1
					    dcl_position o0
					    dcl_texcoord o1.xy
					    dcl_texcoord1 o2.xy
					    dcl_texcoord2 o3.xy
					    dcl_texcoord3 o4.xy
					    dcl_texcoord4 o5.xy
					    dcl_texcoord5 o6.xy
					    dcl_texcoord6 o7.xy
					    dcl_texcoord7 o8.xy
					    dp4 o0.x, c0, v0
					    dp4 o0.y, c1, v0
					    dp4 o0.z, c2, v0
					    dp4 o0.w, c3, v0
					    mov r0.xy, c4
					    mul r0.yz, r0.xyxw, c5.x
					    mov r0.xw, c6.x
					    add o1.xy, r0, v1
					    add o2.xy, -r0, v1
					    add r1.xy, -r0.zwzw, v1
					    add o5.xy, r0, r1
					    add o6.xy, -r0, r1
					    mov o4.xy, r1
					    add r0.zw, r0, v1.xyxy
					    add o7.xy, r0, r0.zwzw
					    add o8.xy, -r0, r0.zwzw
					    mov o3.xy, r0.zwzw
					
					// approximately 17 instruction slots used"
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
						float _OffsetScale;
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
					out vec2 vs_TEXCOORD5;
					out vec2 vs_TEXCOORD6;
					out vec2 vs_TEXCOORD7;
					vec4 u_xlat0;
					vec2 u_xlat1;
					vec2 u_xlat4;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlat0.yz = _MainTex_TexelSize.yx * vec2(_OffsetScale);
					    u_xlat0.x = float(0.0);
					    u_xlat0.w = float(0.0);
					    vs_TEXCOORD0.xy = u_xlat0.xy + in_TEXCOORD0.xy;
					    vs_TEXCOORD1.xy = (-u_xlat0.xy) + in_TEXCOORD0.xy;
					    u_xlat1.xy = u_xlat0.zw + in_TEXCOORD0.xy;
					    vs_TEXCOORD2.xy = u_xlat1.xy;
					    u_xlat4.xy = (-u_xlat0.zw) + in_TEXCOORD0.xy;
					    vs_TEXCOORD3.xy = u_xlat4.xy;
					    vs_TEXCOORD4.xy = u_xlat0.xy + u_xlat4.xy;
					    vs_TEXCOORD5.xy = (-u_xlat0.xy) + u_xlat4.xy;
					    vs_TEXCOORD6.xy = u_xlat0.xy + u_xlat1.xy;
					    vs_TEXCOORD7.xy = (-u_xlat0.xy) + u_xlat1.xy;
					    return;
					}"
}
}
Program "fp" {
SubProgram "d3d9 " {
"ps_3_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 6.3.9600.16384
					//
					// Parameters:
					//
					//   float _BlurRadius;
					//   sampler2D _MainTex;
					//   float4 unity_ColorSpaceLuminance;
					//
					//
					// Registers:
					//
					//   Name                      Reg   Size
					//   ------------------------- ----- ----
					//   unity_ColorSpaceLuminance c0       1
					//   _BlurRadius               c1       1
					//   _MainTex                  s0       1
					//
					
					    ps_3_0
					    def c2, 1, 0.5, 0, 0
					    dcl_texcoord v0.xy
					    dcl_texcoord1 v1.xy
					    dcl_texcoord2 v2.xy
					    dcl_texcoord3 v3.xy
					    dcl_texcoord4 v4.xy
					    dcl_texcoord5 v5.xy
					    dcl_texcoord6 v6.xy
					    dcl_texcoord7 v7.xy
					    dcl_2d s0
					    texld_pp r0, v2, s0
					    mul_pp r0.xyw, r0.xyzz, c0.xyzz
					    add_pp r0.xw, r0.yyzw, r0.x
					    mul_pp r0.y, r0.w, r0.y
					    mad_pp r0.x, r0.z, c0.z, r0.x
					    rsq_pp r0.y, r0.y
					    rcp_pp r0.y, r0.y
					    dp2add_pp r0.y, c0.w, r0.y, r0.x
					    texld_pp r1, v4, s0
					    mul_pp r1.xyw, r1.xyzz, c0.xyzz
					    add_pp r1.xw, r1.yyzw, r1.x
					    mul_pp r0.w, r1.w, r1.y
					    mad_pp r1.x, r1.z, c0.z, r1.x
					    rsq_pp r0.w, r0.w
					    rcp_pp r0.w, r0.w
					    dp2add_pp r1.z, c0.w, r0.w, r1.x
					    mov_pp r0.x, r1.z
					    texld_pp r2, v5, s0
					    mul_pp r2.xyw, r2.xyzz, c0.xyzz
					    add_pp r2.xw, r2.yyzw, r2.x
					    mul_pp r0.w, r2.w, r2.y
					    mad_pp r1.w, r2.z, c0.z, r2.x
					    rsq_pp r0.w, r0.w
					    rcp_pp r0.w, r0.w
					    dp2add_pp r0.z, c0.w, r0.w, r1.w
					    dp3 r0.w, c2.x, r0
					    texld_pp r2, v3, s0
					    mul_pp r2.xyw, r2.xyzz, c0.xyzz
					    add_pp r2.xw, r2.yyzw, r2.x
					    mul_pp r1.w, r2.w, r2.y
					    mad_pp r2.x, r2.z, c0.z, r2.x
					    rsq_pp r1.w, r1.w
					    rcp_pp r1.w, r1.w
					    dp2add_pp r2.y, c0.w, r1.w, r2.x
					    texld_pp r3, v6, s0
					    mul_pp r3.xyw, r3.xyzz, c0.xyzz
					    add_pp r3.xw, r3.yyzw, r3.x
					    mul_pp r1.w, r3.w, r3.y
					    mad_pp r2.w, r3.z, c0.z, r3.x
					    rsq_pp r1.w, r1.w
					    rcp_pp r1.w, r1.w
					    dp2add_pp r0.y, c0.w, r1.w, r2.w
					    mov_pp r2.x, r0.y
					    texld_pp r3, v7, s0
					    mul_pp r3.xyw, r3.xyzz, c0.xyzz
					    add_pp r3.xw, r3.yyzw, r3.x
					    mul_pp r1.w, r3.w, r3.y
					    mad_pp r2.w, r3.z, c0.z, r3.x
					    rsq_pp r1.w, r1.w
					    rcp_pp r1.w, r1.w
					    dp2add_pp r1.x, c0.w, r1.w, r2.w
					    mov_pp r2.z, r1.x
					    dp3 r1.w, c2.x, r2
					    add r2.y, -r0.w, r1.w
					    texld_pp r3, v1, s0
					    mul_pp r3.xyw, r3.xyzz, c0.xyzz
					    add_pp r2.zw, r3.xyyw, r3.x
					    mul_pp r0.w, r2.w, r3.y
					    mad_pp r1.w, r3.z, c0.z, r2.z
					    rsq_pp r0.w, r0.w
					    rcp_pp r0.w, r0.w
					    dp2add_pp r1.y, c0.w, r0.w, r1.w
					    dp3 r0.w, c2.x, r1
					    texld_pp r1, v0, s0
					    mul_pp r1.xyw, r1.xyzz, c0.xyzz
					    add_pp r1.xw, r1.yyzw, r1.x
					    mul_pp r1.y, r1.w, r1.y
					    mad_pp r1.x, r1.z, c0.z, r1.x
					    rsq_pp r1.y, r1.y
					    rcp_pp r1.y, r1.y
					    dp2add_pp r0.x, c0.w, r1.y, r1.x
					    dp3 r0.x, c2.x, r0
					    add r2.x, -r0.x, r0.w
					    mul_pp r0.xy, r2, c1.x
					    mul_pp r0.xy, r0, c2.y
					    mov_pp r0.z, c2.y
					    add_pp r0.xyz, r0, c2.y
					    dp3_pp r0.w, r0, r0
					    rsq_pp r0.w, r0.w
					    mul_pp oC0.xyz, r0.w, r0
					    mov_pp oC0.w, c2.x
					
					// approximately 89 instruction slots used (8 texture, 81 arithmetic)"
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
						vec4 unused_0_2[3];
						float _BlurRadius;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					in  vec2 vs_TEXCOORD2;
					in  vec2 vs_TEXCOORD3;
					in  vec2 vs_TEXCOORD4;
					in  vec2 vs_TEXCOORD5;
					in  vec2 vs_TEXCOORD6;
					in  vec2 vs_TEXCOORD7;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					float u_xlat4;
					float u_xlat5;
					vec2 u_xlat10;
					float u_xlat12;
					float u_xlat13;
					float u_xlat14;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD2.xy);
					    u_xlat0.xyw = u_xlat0.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat0.xw = u_xlat0.yw + u_xlat0.xx;
					    u_xlat4 = u_xlat0.w * u_xlat0.y;
					    u_xlat0.x = u_xlat0.z * unity_ColorSpaceLuminance.z + u_xlat0.x;
					    u_xlat4 = sqrt(u_xlat4);
					    u_xlat4 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat4));
					    u_xlat0.y = u_xlat4 + u_xlat0.x;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD4.xy);
					    u_xlat1.xyw = u_xlat1.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat1.xw = u_xlat1.yw + u_xlat1.xx;
					    u_xlat12 = u_xlat1.w * u_xlat1.y;
					    u_xlat1.x = u_xlat1.z * unity_ColorSpaceLuminance.z + u_xlat1.x;
					    u_xlat12 = sqrt(u_xlat12);
					    u_xlat12 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat12));
					    u_xlat1.z = u_xlat12 + u_xlat1.x;
					    u_xlat0.x = u_xlat1.z;
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD5.xy);
					    u_xlat2.xyw = u_xlat2.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat2.xw = u_xlat2.yw + u_xlat2.xx;
					    u_xlat12 = u_xlat2.w * u_xlat2.y;
					    u_xlat13 = u_xlat2.z * unity_ColorSpaceLuminance.z + u_xlat2.x;
					    u_xlat12 = sqrt(u_xlat12);
					    u_xlat12 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat12));
					    u_xlat0.z = u_xlat12 + u_xlat13;
					    u_xlat12 = dot(vec3(1.0, 1.0, 1.0), u_xlat0.xyz);
					    u_xlat2 = texture(_MainTex, vs_TEXCOORD3.xy);
					    u_xlat2.xyw = u_xlat2.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat2.xw = u_xlat2.yw + u_xlat2.xx;
					    u_xlat13 = u_xlat2.w * u_xlat2.y;
					    u_xlat2.x = u_xlat2.z * unity_ColorSpaceLuminance.z + u_xlat2.x;
					    u_xlat13 = sqrt(u_xlat13);
					    u_xlat13 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat13));
					    u_xlat2.y = u_xlat13 + u_xlat2.x;
					    u_xlat3 = texture(_MainTex, vs_TEXCOORD6.xy);
					    u_xlat3.xyw = u_xlat3.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat3.xw = u_xlat3.yw + u_xlat3.xx;
					    u_xlat13 = u_xlat3.w * u_xlat3.y;
					    u_xlat14 = u_xlat3.z * unity_ColorSpaceLuminance.z + u_xlat3.x;
					    u_xlat13 = sqrt(u_xlat13);
					    u_xlat13 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat13));
					    u_xlat0.y = u_xlat13 + u_xlat14;
					    u_xlat2.x = u_xlat0.y;
					    u_xlat3 = texture(_MainTex, vs_TEXCOORD7.xy);
					    u_xlat3.xyw = u_xlat3.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat3.xw = u_xlat3.yw + u_xlat3.xx;
					    u_xlat13 = u_xlat3.w * u_xlat3.y;
					    u_xlat14 = u_xlat3.z * unity_ColorSpaceLuminance.z + u_xlat3.x;
					    u_xlat13 = sqrt(u_xlat13);
					    u_xlat13 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat13));
					    u_xlat1.x = u_xlat13 + u_xlat14;
					    u_xlat2.z = u_xlat1.x;
					    u_xlat13 = dot(vec3(1.0, 1.0, 1.0), u_xlat2.xyz);
					    u_xlat2.y = (-u_xlat12) + u_xlat13;
					    u_xlat3 = texture(_MainTex, vs_TEXCOORD1.xy);
					    u_xlat3.xyw = u_xlat3.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat10.xy = u_xlat3.yw + u_xlat3.xx;
					    u_xlat12 = u_xlat10.y * u_xlat3.y;
					    u_xlat13 = u_xlat3.z * unity_ColorSpaceLuminance.z + u_xlat10.x;
					    u_xlat12 = sqrt(u_xlat12);
					    u_xlat12 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat12));
					    u_xlat1.y = u_xlat12 + u_xlat13;
					    u_xlat12 = dot(vec3(1.0, 1.0, 1.0), u_xlat1.xyz);
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.xyw = u_xlat1.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat1.xw = u_xlat1.yw + u_xlat1.xx;
					    u_xlat5 = u_xlat1.w * u_xlat1.y;
					    u_xlat1.x = u_xlat1.z * unity_ColorSpaceLuminance.z + u_xlat1.x;
					    u_xlat5 = sqrt(u_xlat5);
					    u_xlat5 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat5));
					    u_xlat0.x = u_xlat5 + u_xlat1.x;
					    u_xlat0.x = dot(vec3(1.0, 1.0, 1.0), u_xlat0.xyz);
					    u_xlat2.x = (-u_xlat0.x) + u_xlat12;
					    u_xlat0.xy = u_xlat2.xy * vec2(vec2(_BlurRadius, _BlurRadius));
					    u_xlat0.xy = u_xlat0.xy * vec2(0.5, 0.5);
					    u_xlat0.z = 0.5;
					    u_xlat0.xyz = u_xlat0.xyz + vec3(0.5, 0.5, 0.5);
					    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
					    u_xlat12 = inversesqrt(u_xlat12);
					    SV_Target0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
					    SV_Target0.w = 1.0;
					    return;
					}"
}
}
 }
}
Fallback Off
}