Shader "Hidden/Deluxe/EyeAdaptationBright" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" { }
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 22823
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
					//   float _BrightnessMultiplier;
					//   sampler2D _BrightnessTex;
					//   sampler2D _ColorTex;
					//   float _ExposureOffset;
					//
					//
					// Registers:
					//
					//   Name                  Reg   Size
					//   --------------------- ----- ----
					//   _BrightnessMultiplier c0       1
					//   _ExposureOffset       c1       1
					//   _BrightnessTex        s0       1
					//   _ColorTex             s1       1
					//
					
					    ps_2_0
					    def c2, 0.5, 0.00999999978, 0, 0
					    dcl_pp t0.xy
					    dcl_2d s0
					    dcl_2d s1
					    mov r0.xy, c2.x
					    texld r0, r0, s0
					    texld r1, t0, s1
					    rcp r0.x, r0.x
					    add r0.x, r0.x, c1.x
					    max r2.w, r0.x, c2.y
					    mul r0, r1, r2.w
					    mul r0, r0, c0.x
					    mov oC0, r0
					
					// approximately 9 instruction slots used (2 texture, 7 arithmetic)"
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
						float _BrightnessMultiplier;
						float _ExposureOffset;
					};
					uniform  sampler2D _ColorTex;
					uniform  sampler2D _BrightnessTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = texture(_BrightnessTex, vec2(0.5, 0.5));
					    u_xlat0.x = float(1.0) / u_xlat0.x;
					    u_xlat0.x = u_xlat0.x + _ExposureOffset;
					    u_xlat0.x = max(u_xlat0.x, 0.00999999978);
					    u_xlat1 = texture(_ColorTex, vs_TEXCOORD0.xy);
					    u_xlat0 = u_xlat0.xxxx * u_xlat1;
					    SV_Target0 = u_xlat0 * vec4(_BrightnessMultiplier);
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
						float _BrightnessMultiplier;
						float _ExposureOffset;
					};
					uniform  sampler2D _ColorTex;
					uniform  sampler2D _BrightnessTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = texture(_BrightnessTex, vec2(0.5, 0.5));
					    u_xlat0.x = float(1.0) / u_xlat0.x;
					    u_xlat0.x = u_xlat0.x + _ExposureOffset;
					    u_xlat0.x = max(u_xlat0.x, 0.00999999978);
					    u_xlat1 = texture(_ColorTex, vs_TEXCOORD0.xy);
					    u_xlat0 = u_xlat0.xxxx * u_xlat1;
					    SV_Target0 = u_xlat0 * vec4(_BrightnessMultiplier);
					    return;
					}"
}
}
 }
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 122789
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
					//   float4 _PixelSize;
					//   sampler2D _UpTex;
					//
					//
					// Registers:
					//
					//   Name         Reg   Size
					//   ------------ ----- ----
					//   _PixelSize   c0       1
					//   _UpTex       s0       1
					//
					
					    ps_2_0
					    def c1, -1, 1, 0.25, 0
					    dcl t0.xy
					    dcl_2d s0
					    add r0.x, t0.x, c0.x
					    add r0.y, t0.y, -c0.y
					    add r1.xy, t0, -c0
					    mov r2.xy, c0
					    mad r2.xy, r2, c1, t0
					    add r3.xy, t0, c0
					    texld_pp r0, r0, s0
					    texld_pp r1, r1, s0
					    texld_pp r2, r2, s0
					    texld_pp r3, r3, s0
					    add_pp r0, r0, r1
					    add_pp r0, r2, r0
					    add_pp r0, r3, r0
					    mul_pp r0, r0, c1.z
					    mov_pp oC0, r0
					
					// approximately 15 instruction slots used (4 texture, 11 arithmetic)"
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
						vec4 _PixelSize;
					};
					uniform  sampler2D _UpTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD0.xy + (-_PixelSize.xy);
					    u_xlat0 = texture(_UpTex, u_xlat0.xy);
					    u_xlat1 = _PixelSize.xyxy * vec4(1.0, -1.0, -1.0, 1.0) + vs_TEXCOORD0.xyxy;
					    u_xlat2 = texture(_UpTex, u_xlat1.xy);
					    u_xlat1 = texture(_UpTex, u_xlat1.zw);
					    u_xlat0 = u_xlat0 + u_xlat2;
					    u_xlat0 = u_xlat1 + u_xlat0;
					    u_xlat1.xy = vs_TEXCOORD0.xy + _PixelSize.xy;
					    u_xlat1 = texture(_UpTex, u_xlat1.xy);
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
						vec4 _PixelSize;
					};
					uniform  sampler2D _UpTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD0.xy + (-_PixelSize.xy);
					    u_xlat0 = texture(_UpTex, u_xlat0.xy);
					    u_xlat1 = _PixelSize.xyxy * vec4(1.0, -1.0, -1.0, 1.0) + vs_TEXCOORD0.xyxy;
					    u_xlat2 = texture(_UpTex, u_xlat1.xy);
					    u_xlat1 = texture(_UpTex, u_xlat1.zw);
					    u_xlat0 = u_xlat0 + u_xlat2;
					    u_xlat0 = u_xlat1 + u_xlat0;
					    u_xlat1.xy = vs_TEXCOORD0.xy + _PixelSize.xy;
					    u_xlat1 = texture(_UpTex, u_xlat1.xy);
					    u_xlat0 = u_xlat0 + u_xlat1;
					    SV_Target0 = u_xlat0 * vec4(0.25, 0.25, 0.25, 0.25);
					    return;
					}"
}
}
 }
 Pass {
  GpuProgramID 141674
Program "vp" {
SubProgram "d3d9 " {
"vs_3_0
					
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
					
					    vs_3_0
					    dcl_position v0
					    dcl_texcoord v1
					    dcl_position o0
					    dcl_texcoord o1.xy
					    dp4 o0.x, c0, v0
					    dp4 o0.y, c1, v0
					    dp4 o0.z, c2, v0
					    dp4 o0.w, c3, v0
					    mov o1.xy, v1
					
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
}
Program "fp" {
SubProgram "d3d9 " {
"ps_3_0
					
					//
					// Generated by Microsoft (R) HLSL Shader Compiler 6.3.9600.16384
					//
					// Parameters:
					//
					//   sampler2D _BrightnessTex;
					//   sampler2D _ColorTex;
					//   sampler2D _Histogram;
					//   float4 _HistogramCoefs;
					//   float4 _MinMax;
					//   float4 _MinMaxSpeedDt;
					//   float _VisualizeExposure;
					//   float _VisualizeHistogram;
					//
					//
					// Registers:
					//
					//   Name                Reg   Size
					//   ------------------- ----- ----
					//   _HistogramCoefs     c0       1
					//   _MinMax             c1       1
					//   _VisualizeExposure  c2       1
					//   _VisualizeHistogram c3       1
					//   _MinMaxSpeedDt      c4       1
					//   _Histogram          s0       1
					//   _ColorTex           s1       1
					//   _BrightnessTex      s2       1
					//
					
					    ps_3_0
					    def c5, 0.949999988, 0.300000012, 0.560000002, 3
					    def c6, 0, 0.5, 9.99999975e-005, 1
					    def c7, 0.999899983, -0.0500000007, -0.00999999978, -0.0149999997
					    def c8, -0.00150000001, 0.00150000001, -0.00300000003, -0.00200000009
					    dcl_texcoord_pp v0.xy
					    dcl_2d s0
					    dcl_2d s1
					    dcl_2d s2
					    add r0.x, -c1.x, c1.y
					    add r0.y, -c1.x, v0.x
					    max r1.x, r0.y, c6.x
					    min r2.x, r0.x, r1.x
					    rcp r0.x, r0.x
					    mul r1.x, r0.x, r2.x
					    mov r1.y, c6.y
					    texld r3, r1, s0
					    texld_pp r4, v0, s1
					    add r0.y, -r2.x, c6.z
					    mad r0.z, r2.x, -r0.x, c7.x
					    cmp r0.yz, r0, c6.xwxw, c6.xxww
					    add r0.y, r0.z, r0.y
					    if_lt -r0.y, c6.x
					      mov_pp oC0.xyz, r4
					      mov_pp oC0.w, c6.w
					    else
					      texld r5, c6.y, s2
					      add r0.y, -c1.z, c1.w
					      add r0.z, -c1.z, v0.y
					      max r1.y, r0.z, c6.x
					      min r2.y, r0.y, r1.y
					      rcp r0.y, r0.y
					      mul r0.z, r0.y, r2.y
					      mad r0.w, r2.y, -r0.y, c6.z
					      mad r1.y, r2.y, r0.y, c7.y
					      mov r6.xw, c6
					      add r1.z, -r6.w, c2.x
					      cmp r1.z, -r1_abs.z, c6.w, c6.x
					      cmp r1.y, r1.y, c6.x, r1.z
					      cmp r0.w, r0.w, c6.x, r1.y
					      if_ne r0.w, -r0.w
					        rcp r0.w, c4.x
					        mul r0.w, r0.w, r5.x
					        rcp r0.w, r0.w
					        mad r0.w, r2.x, r0.x, -r0.w
					        add r0.w, r0_abs.w, c7.z
					        rcp r1.y, c4.y
					        mad r1.y, r1.y, -c4.x, r1.x
					        add r1.y, r1_abs.y, c7.w
					        cmp_pp r7.xw, r1.y, c6.x, c6.w
					        mov_pp r7.yz, c6.x
					        cmp_pp oC0, r0.w, r7, c6.w
					      else
					        add r0.w, -r2.y, c6.z
					        mad r0.y, r2.y, -r0.y, c7.x
					        cmp r0.yw, r0, c6.xxzw, c6.xwzx
					        add r0.y, r0.y, r0.w
					        if_lt -r0.y, c6.x
					          mov_pp oC0.xyz, r4
					          mov_pp oC0.w, c6.w
					        else
					          if_eq c3.x, r6.x
					            mov_pp oC0.xyz, r4
					            mov_pp oC0.w, c6.w
					          else
					            log r0.y, r5.x
					            mad r0.y, r0.y, c0.x, c0.y
					            mad r0.y, r2.x, r0.x, -r0.y
					            add r0.w, r0_abs.y, c8.x
					            if_lt r0_abs.y, c8.y
					              cmp_pp oC0.xyz, r0.w, r4, c6.xwxw
					              mov_pp oC0.w, c6.w
					            else
					              rcp r0.y, r5.w
					              mul r0.y, r0.y, r3.x
					              log r0.w, c4.x
					              mad r0.w, r0.w, c0.x, c0.y
					              mad r0.w, r2.x, r0.x, -r0.w
					              log r1.y, c4.y
					              mad r1.y, r1.y, c0.x, c0.y
					              mad r1.y, r2.x, r0.x, -r1.y
					              log r1.z, r5.y
					              mad r1.z, r1.z, c0.x, c0.y
					              mad r1.z, r2.x, r0.x, -r1.z
					              add r1.yz, r1_abs, c8.xzww
					              log r1.w, r5.z
					              mad r1.w, r1.w, c0.x, c0.y
					              mad r0.x, r2.x, r0.x, -r1.w
					              add r0.xw, r0_abs, c8.wyzz
					              mad r0.y, r0.y, c5.x, -r0.z
					              mad_pp r2.xyz, r4, c5.y, c5.z
					              mul r0.z, r1.x, c6.y
					              mul_pp r0.z, r0.z, r0.z
					              mul_pp r1.x, r0.z, c5.w
					              add_pp r1.w, r0.z, r0.z
					              cmp_pp r3.xy, r0.y, r1.xwzw, r2
					              cmp_pp r3.z, r0.y, r0.z, r2.z
					              mov_pp r3.w, c6.w
					              cmp_pp r2, r0.x, r3, c6.xxww
					              cmp_pp r2, r1.z, r2, c6.xxww
					              cmp_pp r1, r1.y, r2, c6.wxxw
					              cmp_pp oC0, r0.w, r1, c6.wxxw
					            endif
					          endif
					        endif
					      endif
					    endif
					
					// approximately 108 instruction slots used (3 texture, 105 arithmetic)"
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
						vec4 unused_0_0[7];
						vec4 _HistogramCoefs;
						vec4 _MinMax;
						float _VisualizeExposure;
						float _VisualizeHistogram;
						vec4 _MinMaxSpeedDt;
					};
					uniform  sampler2D _BrightnessTex;
					uniform  sampler2D _Histogram;
					uniform  sampler2D _ColorTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec2 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					float u_xlat5;
					bool u_xlatb5;
					float u_xlat6;
					vec2 u_xlat7;
					bool u_xlatb7;
					float u_xlat11;
					bool u_xlatb11;
					float u_xlat16;
					bool u_xlatb16;
					void main()
					{
					    u_xlat0.x = (-_MinMax.x) + _MinMax.y;
					    u_xlat5 = vs_TEXCOORD0.x + (-_MinMax.x);
					    u_xlat5 = max(u_xlat5, 0.0);
					    u_xlat5 = min(u_xlat0.x, u_xlat5);
					    u_xlat1.x = u_xlat5 / u_xlat0.x;
					    u_xlat1.y = 0.5;
					    u_xlat2 = texture(_Histogram, u_xlat1.xy);
					    u_xlat3 = texture(_ColorTex, vs_TEXCOORD0.xy);
					    u_xlatb0 = 9.99999975e-05>=u_xlat5;
					    u_xlatb5 = 0.999899983<u_xlat1.x;
					    u_xlatb0 = u_xlatb5 || u_xlatb0;
					    if(u_xlatb0){
					        SV_Target0.xyz = u_xlat3.xyz;
					        SV_Target0.w = 1.0;
					        return;
					    }
					    u_xlat0 = texture(_BrightnessTex, vec2(0.5, 0.5));
					    u_xlat6 = (-_MinMax.z) + _MinMax.w;
					    u_xlat11 = vs_TEXCOORD0.y + (-_MinMax.z);
					    u_xlat11 = max(u_xlat11, 0.0);
					    u_xlat11 = min(u_xlat6, u_xlat11);
					    u_xlat6 = u_xlat11 / u_xlat6;
					    u_xlatb16 = 9.99999975e-05<u_xlat6;
					    u_xlatb7 = u_xlat6<0.0500000007;
					    u_xlatb16 = u_xlatb16 && u_xlatb7;
					    u_xlatb7 = _VisualizeExposure==1.0;
					    u_xlatb16 = u_xlatb16 && u_xlatb7;
					    if(u_xlatb16){
					        u_xlat16 = u_xlat0.x / _MinMaxSpeedDt.x;
					        u_xlat16 = float(1.0) / u_xlat16;
					        u_xlat16 = (-u_xlat16) + u_xlat1.x;
					        u_xlatb16 = abs(u_xlat16)>=0.00999999978;
					        u_xlat7.xy = vec2(1.0, 1.0) / _MinMaxSpeedDt.yx;
					        u_xlat7.x = u_xlat7.x / u_xlat7.y;
					        u_xlat7.x = u_xlat1.x + (-u_xlat7.x);
					        u_xlatb7 = abs(u_xlat7.x)<0.0149999997;
					        u_xlat4 = bool(u_xlatb7) ? vec4(1.0, 0.0, 0.0, 1.0) : vec4(0.0, 0.0, 0.0, 0.0);
					        SV_Target0 = (bool(u_xlatb16)) ? u_xlat4 : vec4(1.0, 1.0, 1.0, 1.0);
					        return;
					    }
					    u_xlatb11 = 9.99999975e-05>=u_xlat11;
					    u_xlatb16 = 0.999899983<u_xlat6;
					    u_xlatb11 = u_xlatb16 || u_xlatb11;
					    if(u_xlatb11){
					        SV_Target0.xyz = u_xlat3.xyz;
					        SV_Target0.w = 1.0;
					        return;
					    }
					    u_xlatb11 = _VisualizeHistogram==0.0;
					    if(u_xlatb11){
					        SV_Target0.xyz = u_xlat3.xyz;
					        SV_Target0.w = 1.0;
					        return;
					    }
					    u_xlat0.x = log2(u_xlat0.x);
					    u_xlat0.x = u_xlat0.x * _HistogramCoefs.x + _HistogramCoefs.y;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat1.x;
					    u_xlatb0 = abs(u_xlat0.x)<0.00150000001;
					    if(u_xlatb0){
					        SV_Target0 = vec4(0.0, 1.0, 0.0, 1.0);
					        return;
					    }
					    u_xlat0.x = log2(_MinMaxSpeedDt.x);
					    u_xlat0.x = u_xlat0.x * _HistogramCoefs.x + _HistogramCoefs.y;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat1.x;
					    u_xlatb0 = abs(u_xlat0.x)<0.00300000003;
					    if(u_xlatb0){
					        SV_Target0 = vec4(1.0, 0.0, 0.0, 1.0);
					        return;
					    }
					    u_xlat0.x = log2(_MinMaxSpeedDt.y);
					    u_xlat0.x = u_xlat0.x * _HistogramCoefs.x + _HistogramCoefs.y;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat1.x;
					    u_xlatb0 = abs(u_xlat0.x)<0.00300000003;
					    if(u_xlatb0){
					        SV_Target0 = vec4(1.0, 0.0, 0.0, 1.0);
					        return;
					    }
					    u_xlat0.x = log2(u_xlat0.y);
					    u_xlat0.x = u_xlat0.x * _HistogramCoefs.x + _HistogramCoefs.y;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat1.x;
					    u_xlatb0 = abs(u_xlat0.x)<0.00200000009;
					    if(u_xlatb0){
					        SV_Target0 = vec4(0.0, 0.0, 1.0, 1.0);
					        return;
					    }
					    u_xlat0.x = log2(u_xlat0.z);
					    u_xlat0.x = u_xlat0.x * _HistogramCoefs.x + _HistogramCoefs.y;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat1.x;
					    u_xlatb0 = abs(u_xlat0.x)<0.00200000009;
					    if(u_xlatb0){
					        SV_Target0 = vec4(0.0, 0.0, 1.0, 1.0);
					        return;
					    }
					    u_xlat0.x = u_xlat2.x / u_xlat0.w;
					    u_xlat0.x = u_xlat0.x * 0.949999988;
					    u_xlatb0 = u_xlat0.x<u_xlat6;
					    if(u_xlatb0){
					        u_xlat0.xyz = u_xlat3.xyz * vec3(0.300000012, 0.300000012, 0.300000012);
					        u_xlat0.w = 0.300000012;
					        SV_Target0 = u_xlat0 + vec4(0.560000002, 0.560000002, 0.560000002, 0.699999988);
					        return;
					    }
					    u_xlat0.x = u_xlat1.x * 0.5;
					    u_xlat0.x = u_xlat0.x * u_xlat0.x;
					    SV_Target0.x = u_xlat0.x * 3.0;
					    SV_Target0.y = u_xlat0.x + u_xlat0.x;
					    SV_Target0.z = u_xlat0.x;
					    SV_Target0.w = 1.0;
					    return;
					}"
}
}
 }
}
Fallback Off
}