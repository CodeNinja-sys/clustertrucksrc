Shader "Hidden/ColorCorrectionCurves" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "" { }
 _RgbTex ("_RgbTex (RGB)", 2D) = "" { }
 _ZCurve ("_ZCurve (RGB)", 2D) = "" { }
 _RgbDepthTex ("_RgbDepthTex (RGB)", 2D) = "" { }
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 59989
Program "vp" {
SubProgram "d3d9 " {
"vs_2_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 6.3.9600.16384
					//
					// Parameters:
					//
					//   float4 _CameraDepthTexture_ST;
					//   float4 _MainTex_TexelSize;
					//   row_major float4x4 glstate_matrix_mvp;
					//
					//
					// Registers:
					//
					//   Name                   Reg   Size
					//   ---------------------- ----- ----
					//   glstate_matrix_mvp     c0       4
					//   _CameraDepthTexture_ST c4       1
					//   _MainTex_TexelSize     c5       1
					//
					
					    vs_2_0
					    def c6, 0, -2, 1, 0
					    dcl_position v0
					    dcl_texcoord v1
					    dp4 oPos.x, c0, v0
					    dp4 oPos.y, c1, v0
					    dp4 oPos.z, c2, v0
					    dp4 oPos.w, c3, v0
					    mov oT0.xy, v1
					    mov r0.x, c6.x
					    slt r0.x, c5.y, r0.x
					    mad r1.xy, v1, c4, c4.zwzw
					    mad r0.y, r1.y, c6.y, c6.z
					    mad r1.z, r0.x, r0.y, r1.y
					    mov oT1.xy, r1.xzzw
					
					// approximately 11 instruction slots used"
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
						vec4 _CameraDepthTexture_ST;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_3;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 glstate_matrix_mvp;
						vec4 unused_1_1[18];
					};
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec2 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlatb0 = _MainTex_TexelSize.y<0.0;
					    u_xlat1.xy = in_TEXCOORD0.xy * _CameraDepthTexture_ST.xy + _CameraDepthTexture_ST.zw;
					    u_xlat3 = (-u_xlat1.y) + 1.0;
					    vs_TEXCOORD1.y = (u_xlatb0) ? u_xlat3 : u_xlat1.y;
					    vs_TEXCOORD1.x = u_xlat1.x;
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
					layout(std140) uniform VGlobals {
						vec4 unused_0_0[6];
						vec4 _CameraDepthTexture_ST;
						vec4 _MainTex_TexelSize;
						vec4 unused_0_3;
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 glstate_matrix_mvp;
						vec4 unused_1_1[18];
					};
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					out vec2 vs_TEXCOORD1;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec2 u_xlat1;
					float u_xlat3;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
					    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
					    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
					    u_xlatb0 = _MainTex_TexelSize.y<0.0;
					    u_xlat1.xy = in_TEXCOORD0.xy * _CameraDepthTexture_ST.xy + _CameraDepthTexture_ST.zw;
					    u_xlat3 = (-u_xlat1.y) + 1.0;
					    vs_TEXCOORD1.y = (u_xlatb0) ? u_xlat3 : u_xlat1.y;
					    vs_TEXCOORD1.x = u_xlat1.x;
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
					//   sampler2D _CameraDepthTexture;
					//   sampler2D _MainTex;
					//   sampler2D _RgbDepthTex;
					//   sampler2D _RgbTex;
					//   float _Saturation;
					//   float4 _ZBufferParams;
					//   sampler2D _ZCurve;
					//   float4 unity_ColorSpaceLuminance;
					//
					//
					// Registers:
					//
					//   Name                      Reg   Size
					//   ------------------------- ----- ----
					//   _ZBufferParams            c0       1
					//   unity_ColorSpaceLuminance c1       1
					//   _Saturation               c2       1
					//   _MainTex                  s0       1
					//   _CameraDepthTexture       s1       1
					//   _RgbTex                   s2       1
					//   _ZCurve                   s3       1
					//   _RgbDepthTex              s4       1
					//
					
					    ps_2_0
					    def c3, 0.125, 0.375, 0.625, 0.5
					    def c4, 0, 1, 0, 2
					    dcl t0.xy
					    dcl t1.xy
					    dcl_2d s0
					    dcl_2d s1
					    dcl_2d s2
					    dcl_2d s3
					    dcl_2d s4
					    texld_pp r0, t1, s1
					    texld_pp r1, t0, s0
					    mad r0.x, c0.x, r0.x, c0.y
					    rcp_pp r0.x, r0.x
					    mov_pp r0.y, c3.w
					    mov_pp r2.y, c3.y
					    mov_pp r2.x, r1.y
					    mov_pp r1.y, c3.x
					    mov_pp r3.x, r1.z
					    mov_pp r3.y, c3.z
					    texld_pp r0, r0, s3
					    texld r4, r2, s2
					    texld r2, r2, s4
					    texld r5, r1, s2
					    texld r6, r1, s4
					    texld r7, r3, s2
					    texld r3, r3, s4
					    mul_pp r0.yzw, r4.wzyx, c4.wzyx
					    mad_pp r4.xyz, r5, c4.yzxw, r0.wzyx
					    mul_pp r3.xyz, r3, c4.zxyw
					    mad_pp r3.xyz, r6, c4.yzxw, r3
					    mad_pp r0.yzw, r2.wzyx, c4.wzyx, r3.wzyx
					    mad_pp r2.xyz, r7, c4.zxyw, r4
					    lrp_pp r3.xyz, r0.x, r0.wzyx, r2
					    mul_pp r0.xyz, r3, c1
					    add_pp r3.w, r0.z, r0.x
					    mul_pp r3.w, r0.y, r3.w
					    add_pp r0.x, r0.y, r0.x
					    mad_pp r0.x, r3.z, c1.z, r0.x
					    rsq_pp r3.w, r3.w
					    rcp_pp r3.w, r3.w
					    mul_pp r3.w, r3.w, c1.w
					    mad_pp r3.w, r3.w, c4.w, r0.x
					    lrp_pp r1.xyz, c2.x, r3, r3.w
					    mov_pp oC0, r1
					
					// approximately 35 instruction slots used (9 texture, 26 arithmetic)"
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
						vec4 unused_0_2[4];
						float _Saturation;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[7];
						vec4 _ZBufferParams;
						vec4 unused_1_2;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _RgbTex;
					uniform  sampler2D _CameraDepthTexture;
					uniform  sampler2D _ZCurve;
					uniform  sampler2D _RgbDepthTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec2 u_xlat11;
					float u_xlat15;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy).xzyw;
					    u_xlat1.x = u_xlat0.y;
					    u_xlat1.y = float(0.625);
					    u_xlat11.y = float(0.5);
					    u_xlat2 = texture(_RgbDepthTex, u_xlat1.xy);
					    u_xlat3 = texture(_RgbTex, u_xlat1.xy);
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.0, 0.0, 1.0);
					    SV_Target0.w = u_xlat0.w;
					    u_xlat0.y = float(0.125);
					    u_xlat0.w = float(0.375);
					    u_xlat4 = texture(_RgbDepthTex, u_xlat0.xy);
					    u_xlat2.xyz = u_xlat4.xyz * vec3(1.0, 0.0, 0.0) + u_xlat2.xyz;
					    u_xlat4 = texture(_RgbDepthTex, u_xlat0.zw);
					    u_xlat2.xyz = u_xlat4.xyz * vec3(0.0, 1.0, 0.0) + u_xlat2.xyz;
					    u_xlat4 = texture(_RgbTex, u_xlat0.zw);
					    u_xlat0 = texture(_RgbTex, u_xlat0.xy);
					    u_xlat4.xyz = u_xlat4.xyz * vec3(0.0, 1.0, 0.0);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(1.0, 0.0, 0.0) + u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat3.xyz * vec3(0.0, 0.0, 1.0) + u_xlat0.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat2.xyz;
					    u_xlat3 = texture(_CameraDepthTexture, vs_TEXCOORD1.xy);
					    u_xlat15 = _ZBufferParams.x * u_xlat3.x + _ZBufferParams.y;
					    u_xlat11.x = float(1.0) / u_xlat15;
					    u_xlat1 = texture(_ZCurve, u_xlat11.xy);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.xyz * unity_ColorSpaceLuminance.xyz;
					    u_xlat1.xz = u_xlat1.yz + u_xlat1.xx;
					    u_xlat15 = u_xlat1.z * u_xlat1.y;
					    u_xlat1.x = u_xlat0.z * unity_ColorSpaceLuminance.z + u_xlat1.x;
					    u_xlat15 = sqrt(u_xlat15);
					    u_xlat15 = dot(unity_ColorSpaceLuminance.ww, vec2(u_xlat15));
					    u_xlat15 = u_xlat15 + u_xlat1.x;
					    u_xlat0.xyz = (-vec3(u_xlat15)) + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Saturation) * u_xlat0.xyz + vec3(u_xlat15);
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
						vec4 unused_0_2[4];
						float _Saturation;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 unused_1_0[7];
						vec4 _ZBufferParams;
						vec4 unused_1_2;
					};
					uniform  sampler2D _MainTex;
					uniform  sampler2D _RgbTex;
					uniform  sampler2D _CameraDepthTexture;
					uniform  sampler2D _ZCurve;
					uniform  sampler2D _RgbDepthTex;
					in  vec2 vs_TEXCOORD0;
					in  vec2 vs_TEXCOORD1;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec2 u_xlat11;
					float u_xlat15;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy).xzyw;
					    u_xlat1.x = u_xlat0.y;
					    u_xlat1.y = float(0.625);
					    u_xlat11.y = float(0.5);
					    u_xlat2 = texture(_RgbDepthTex, u_xlat1.xy);
					    u_xlat3 = texture(_RgbTex, u_xlat1.xy);
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.0, 0.0, 1.0);
					    SV_Target0.w = u_xlat0.w;
					    u_xlat0.y = float(0.125);
					    u_xlat0.w = float(0.375);
					    u_xlat4 = texture(_RgbDepthTex, u_xlat0.xy);
					    u_xlat2.xyz = u_xlat4.xyz * vec3(1.0, 0.0, 0.0) + u_xlat2.xyz;
					    u_xlat4 = texture(_RgbDepthTex, u_xlat0.zw);
					    u_xlat2.xyz = u_xlat4.xyz * vec3(0.0, 1.0, 0.0) + u_xlat2.xyz;
					    u_xlat4 = texture(_RgbTex, u_xlat0.zw);
					    u_xlat0 = texture(_RgbTex, u_xlat0.xy);
					    u_xlat4.xyz = u_xlat4.xyz * vec3(0.0, 1.0, 0.0);
					    u_xlat0.xyz = u_xlat0.xyz * vec3(1.0, 0.0, 0.0) + u_xlat4.xyz;
					    u_xlat0.xyz = u_xlat3.xyz * vec3(0.0, 0.0, 1.0) + u_xlat0.xyz;
					    u_xlat2.xyz = (-u_xlat0.xyz) + u_xlat2.xyz;
					    u_xlat3 = texture(_CameraDepthTexture, vs_TEXCOORD1.xy);
					    u_xlat15 = _ZBufferParams.x * u_xlat3.x + _ZBufferParams.y;
					    u_xlat11.x = float(1.0) / u_xlat15;
					    u_xlat1 = texture(_ZCurve, u_xlat11.xy);
					    u_xlat0.xyz = u_xlat1.xxx * u_xlat2.xyz + u_xlat0.xyz;
					    u_xlat15 = dot(u_xlat0.xyz, unity_ColorSpaceLuminance.xyz);
					    u_xlat0.xyz = (-vec3(u_xlat15)) + u_xlat0.xyz;
					    SV_Target0.xyz = vec3(_Saturation) * u_xlat0.xyz + vec3(u_xlat15);
					    return;
					}"
}
}
 }
}
Fallback Off
}