Shader "Hidden/DLAA" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" { }
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 45679
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
					    def c1, 0.330000013, 0, 0, 0
					    def c2, 1, -1, 1, 4
					    dcl t0.xy
					    dcl_2d s0
					    add r0.xy, t0, -c0
					    mov r1.xyz, c2
					    mad r2.xy, c0, r1, t0
					    mad r1.xy, c0, r1.yzxw, t0
					    add r3.xy, t0, c0
					    texld r0, r0, s0
					    texld r2, r2, s0
					    texld r1, r1, s0
					    texld r3, r3, s0
					    texld_pp r4, t0, s0
					    add r0.xyz, r0, r2
					    add r0.xyz, r1, r0
					    add r0.xyz, r3, r0
					    mad r0.xyz, r4, -c2.w, r0
					    abs r0.xyz, r0
					    mul r0.xyz, r0, c2.w
					    dp3_pp r4.w, r0, c1.x
					    mov_pp oC0, r4
					
					// approximately 18 instruction slots used (5 texture, 13 arithmetic)"
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
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					void main()
					{
					    u_xlat0.xy = vs_TEXCOORD0.xy + (-_MainTex_TexelSize.xy);
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat1 = _MainTex_TexelSize.xyxy * vec4(1.0, -1.0, -1.0, 1.0) + vs_TEXCOORD0.xyxy;
					    u_xlat2 = texture(_MainTex, u_xlat1.xy);
					    u_xlat1 = texture(_MainTex, u_xlat1.zw);
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat2.xyz;
					    u_xlat0.xyz = u_xlat1.xyz + u_xlat0.xyz;
					    u_xlat1.xy = vs_TEXCOORD0.xy + _MainTex_TexelSize.xy;
					    u_xlat1 = texture(_MainTex, u_xlat1.xy);
					    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat0.xyz = (-u_xlat1.xyz) * vec3(4.0, 4.0, 4.0) + u_xlat0.xyz;
					    SV_Target0.xyz = u_xlat1.xyz;
					    u_xlat0.xyz = abs(u_xlat0.xyz) * vec3(4.0, 4.0, 4.0);
					    SV_Target0.w = dot(u_xlat0.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    return;
					}"
}
}
 }
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 68659
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
					
					    ps_3_0
					    def c1, 0.25, 2, 0.166666672, 0.330000013
					    def c2, 3, -0.100000001, 0.125, 1
					    def c3, 0.25, -1, 0, 1
					    def c4, -5.5, 0, -7.5, 0
					    def c5, 7.5, 0, -3.5, 0
					    def c6, 3.5, 0, 5.5, 1
					    def c7, -1.5, 0, 1.5, 4
					    dcl_texcoord v0.xy
					    dcl_2d s0
					    mov r0.xy, c0
					    mad r1, r0.xyxy, c6.yxyz, v0.xyxy
					    texld r2, r1.zwzw, s0
					    texld r1, r1, s0
					    mad r3, r0.xyxy, c7.yxyz, v0.xyxy
					    texld r4, r3.zwzw, s0
					    texld r3, r3, s0
					    add r1, r1.wxyz, r4.wxyz
					    add r4, r4, r3
					    add r1, r2.wxyz, r1
					    mad r2, r0.xyxy, c5.yxyz, v0.xyxy
					    texld r5, r2, s0
					    texld r2, r2.zwzw, s0
					    add r1, r1, r5.wxyz
					    add r1, r3.wxyz, r1
					    add r1, r2.wxyz, r1
					    mad r2, r0.xyxy, c4.yxyz, v0.xyxy
					    texld r3, r2, s0
					    texld r2, r2.zwzw, s0
					    add r1, r1, r3.wxyz
					    add r1, r2.wxyz, r1
					    mad_sat r0.z, r1.x, c3.x, c3.y
					    mul r1.xyz, r1.yzww, c2.z
					    dp3 r0.w, r1, c1.w
					    add r1.xyz, r4, r4
					    texld r2, v0, s0
					    mad r1.xyz, r2, -c7.w, r1
					    mul r1.xyz, r1_abs, c1.x
					    dp3 r1.x, r1, c1.w
					    mad r1.x, r1.x, c2.x, c2.y
					    mad r3, r0.xyxy, c7.xyzy, v0.xyxy
					    texld r5, r3, s0
					    texld r3, r3.zwzw, s0
					    add r6, r3, r5
					    add r7, r2, r2
					    mad r8, r6, c1.y, r7
					    add r1.yzw, r6.xxyz, r6.xxyz
					    mad r1.yzw, r2.xxyz, -c7.w, r1
					    mul r1.yzw, r1_abs, c1.x
					    dp3 r1.y, r1.yzww, c1.w
					    mad r1.y, r1.y, c2.x, c2.y
					    mad r4, r4, c1.y, r7
					    mul r6.xyz, r8, c1.z
					    mad r7, r8, c1.z, -r2
					    dp3 r1.z, r6, c1.w
					    rcp r1.z, r1.z
					    mul_sat r1.x, r1.z, r1.x
					    mad r6, r1.x, r7, r2
					    mad r7, r4, c1.z, -r6
					    mul r1.xzw, r4.xyyz, c1.z
					    dp3 r1.x, r1.xzww, c1.w
					    rcp r1.x, r1.x
					    mul_sat r1.x, r1.x, r1.y
					    mad r1, r1.x, r7, r6
					    mad r4, r0.xyxy, c3.yzwz, v0.xyxy
					    texld r6, r4, s0
					    texld r4, r4.zwzw, s0
					    dp3 r7.x, r6, c1.w
					    add r7.y, r0.w, -r7.x
					    dp3 r7.z, r2, c1.w
					    add r7.x, -r7.x, r7.z
					    rcp r7.w, r7.x
					    mul_sat r7.y, r7.w, r7.y
					    cmp r7.x, -r7_abs.x, c7.y, r7.y
					    lrp r8, r7.x, r2, r6
					    add r0.w, r0.w, -r7.z
					    dp3 r6.x, r4, c1.w
					    add r6.x, -r6.x, r7.z
					    rcp r6.y, r6.x
					    mad_sat r0.w, r0.w, r6.y, c2.w
					    cmp r0.w, -r6_abs.x, c7.y, r0.w
					    lrp r6, r0.w, r8, r4
					    lrp r4, r0.z, r6, r1
					    mad r6, r0.xyxy, c6.xyzy, v0.xyxy
					    texld r8, r6, s0
					    texld r6, r6.zwzw, s0
					    add r3, r3.wxyz, r8.wxyz
					    add r3, r6.wxyz, r3
					    mad r6, r0.xyxy, c5.xyzy, v0.xyxy
					    texld r8, r6, s0
					    texld r6, r6.zwzw, s0
					    add r3, r3, r8.wxyz
					    add r3, r5.wxyz, r3
					    add r3, r6.wxyz, r3
					    mad r5, r0.xyxy, c4.xyzy, v0.xyxy
					    texld r6, r5, s0
					    texld r5, r5.zwzw, s0
					    add r3, r3, r6.wxyz
					    add r3, r5.wxyz, r3
					    mad_sat r0.w, r3.x, c3.x, c3.y
					    mul r3.xyz, r3.yzww, c2.z
					    dp3 r3.x, r3, c1.w
					    mad r5, r0.xyxy, c3.zyzw, v0.xyxy
					    texld r6, r5, s0
					    texld r5, r5.zwzw, s0
					    dp3 r0.x, r6, c1.w
					    add r0.y, -r0.x, r3.x
					    add r3.x, -r7.z, r3.x
					    add r0.x, -r0.x, r7.z
					    rcp r3.y, r0.x
					    mul_sat r0.y, r0.y, r3.y
					    cmp r0.x, -r0_abs.x, c7.y, r0.y
					    lrp r8, r0.x, r2, r6
					    dp3 r0.x, r5, c1.w
					    add r0.x, -r0.x, r7.z
					    rcp r0.y, r0.x
					    mad_sat r0.y, r3.x, r0.y, c2.w
					    cmp r0.x, -r0_abs.x, c7.y, r0.y
					    lrp r2, r0.x, r8, r5
					    lrp_pp r3, r0.w, r2, r4
					    cmp r0.xz, -r0.wyzw, c6.y, c6.w
					    add r0.x, r0.z, r0.x
					    cmp_pp oC0, -r0.x, r1, r3
					
					// approximately 113 instruction slots used (21 texture, 92 arithmetic)"
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
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec3 u_xlat10;
					bool u_xlatb10;
					vec3 u_xlat13;
					float u_xlat17;
					float u_xlat20;
					bool u_xlatb20;
					float u_xlat23;
					float u_xlat30;
					void main()
					{
					    u_xlat0 = _MainTex_TexelSize.xyxy * vec4(0.0, -1.5, 0.0, 1.5) + vs_TEXCOORD0.xyxy;
					    u_xlat1 = texture(_MainTex, u_xlat0.xy);
					    u_xlat0 = texture(_MainTex, u_xlat0.zw);
					    u_xlat2 = u_xlat0 + u_xlat1;
					    u_xlat3.xyz = u_xlat2.xyz + u_xlat2.xyz;
					    u_xlat4 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat3.xyz = (-u_xlat4.xyz) * vec3(4.0, 4.0, 4.0) + u_xlat3.xyz;
					    u_xlat3.xyz = abs(u_xlat3.xyz) * vec3(0.25, 0.25, 0.25);
					    u_xlat3.x = dot(u_xlat3.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat3.x = u_xlat3.x * 3.0 + -0.100000001;
					    u_xlat5 = _MainTex_TexelSize.xyxy * vec4(-1.5, 0.0, 1.5, 0.0) + vs_TEXCOORD0.xyxy;
					    u_xlat6 = texture(_MainTex, u_xlat5.xy);
					    u_xlat5 = texture(_MainTex, u_xlat5.zw);
					    u_xlat7 = u_xlat5 + u_xlat6;
					    u_xlat8 = u_xlat4 + u_xlat4;
					    u_xlat9 = u_xlat7 * vec4(2.0, 2.0, 2.0, 2.0) + u_xlat8;
					    u_xlat13.xyz = u_xlat7.xyz + u_xlat7.xyz;
					    u_xlat13.xyz = (-u_xlat4.xyz) * vec3(4.0, 4.0, 4.0) + u_xlat13.xyz;
					    u_xlat13.xyz = abs(u_xlat13.xyz) * vec3(0.25, 0.25, 0.25);
					    u_xlat13.x = dot(u_xlat13.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat13.x = u_xlat13.x * 3.0 + -0.100000001;
					    u_xlat2 = u_xlat2 * vec4(2.0, 2.0, 2.0, 2.0) + u_xlat8;
					    u_xlat7.xyz = u_xlat9.xyz * vec3(0.166666672, 0.166666672, 0.166666672);
					    u_xlat8 = u_xlat9 * vec4(0.166666672, 0.166666672, 0.166666672, 0.166666672) + (-u_xlat4);
					    u_xlat23 = dot(u_xlat7.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat3.x = u_xlat3.x / u_xlat23;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat7 = u_xlat3.xxxx * u_xlat8 + u_xlat4;
					    u_xlat8 = u_xlat2 * vec4(0.166666672, 0.166666672, 0.166666672, 0.166666672) + (-u_xlat7);
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.166666672, 0.166666672, 0.166666672);
					    u_xlat2.x = dot(u_xlat2.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat2.x = u_xlat13.x / u_xlat2.x;
					    u_xlat2.x = clamp(u_xlat2.x, 0.0, 1.0);
					    u_xlat2 = u_xlat2.xxxx * u_xlat8 + u_xlat7;
					    u_xlat3 = _MainTex_TexelSize.xyxy * vec4(0.0, 3.5, 0.0, 5.5) + vs_TEXCOORD0.xyxy;
					    u_xlat7 = texture(_MainTex, u_xlat3.xy);
					    u_xlat3 = texture(_MainTex, u_xlat3.zw);
					    u_xlat0 = u_xlat0.wxyz + u_xlat7.wxyz;
					    u_xlat0 = u_xlat3.wxyz + u_xlat0;
					    u_xlat3 = _MainTex_TexelSize.xyxy * vec4(0.0, 7.5, 0.0, -3.5) + vs_TEXCOORD0.xyxy;
					    u_xlat7 = texture(_MainTex, u_xlat3.xy);
					    u_xlat3 = texture(_MainTex, u_xlat3.zw);
					    u_xlat0 = u_xlat0 + u_xlat7.wxyz;
					    u_xlat0 = u_xlat1.wxyz + u_xlat0;
					    u_xlat0 = u_xlat3.wxyz + u_xlat0;
					    u_xlat1 = _MainTex_TexelSize.xyxy * vec4(0.0, -5.5, 0.0, -7.5) + vs_TEXCOORD0.xyxy;
					    u_xlat3 = texture(_MainTex, u_xlat1.xy);
					    u_xlat1 = texture(_MainTex, u_xlat1.zw);
					    u_xlat0 = u_xlat0 + u_xlat3.wxyz;
					    u_xlat0 = u_xlat1.wxyz + u_xlat0;
					    u_xlat10.xyz = u_xlat0.yzw * vec3(0.125, 0.125, 0.125);
					    u_xlat0.x = u_xlat0.x * 0.25 + -1.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat10.x = dot(u_xlat10.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat1 = _MainTex_TexelSize.xyxy * vec4(-1.0, 0.0, 1.0, 0.0) + vs_TEXCOORD0.xyxy;
					    u_xlat3 = texture(_MainTex, u_xlat1.xy);
					    u_xlat1 = texture(_MainTex, u_xlat1.zw);
					    u_xlat20 = dot(u_xlat3.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat30 = (-u_xlat20) + u_xlat10.x;
					    u_xlat7.x = dot(u_xlat4.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat17 = (-u_xlat20) + u_xlat7.x;
					    u_xlatb20 = u_xlat20==u_xlat7.x;
					    u_xlat30 = u_xlat30 / u_xlat17;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat20 = (u_xlatb20) ? 0.0 : u_xlat30;
					    u_xlat8 = (-u_xlat3) + u_xlat4;
					    u_xlat3 = vec4(u_xlat20) * u_xlat8 + u_xlat3;
					    u_xlat3 = (-u_xlat1) + u_xlat3;
					    u_xlat10.x = u_xlat10.x + (-u_xlat7.x);
					    u_xlat20 = dot(u_xlat1.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat30 = (-u_xlat20) + u_xlat7.x;
					    u_xlatb20 = u_xlat20==u_xlat7.x;
					    u_xlat10.x = u_xlat10.x / u_xlat30;
					    u_xlat10.x = u_xlat10.x + 1.0;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat10.x = (u_xlatb20) ? 0.0 : u_xlat10.x;
					    u_xlat1 = u_xlat10.xxxx * u_xlat3 + u_xlat1;
					    u_xlat1 = (-u_xlat2) + u_xlat1;
					    u_xlat1 = u_xlat0.xxxx * u_xlat1 + u_xlat2;
					    u_xlatb0 = 0.0<u_xlat0.x;
					    u_xlat3 = _MainTex_TexelSize.xyxy * vec4(3.5, 0.0, 5.5, 0.0) + vs_TEXCOORD0.xyxy;
					    u_xlat8 = texture(_MainTex, u_xlat3.xy);
					    u_xlat3 = texture(_MainTex, u_xlat3.zw);
					    u_xlat5 = u_xlat5.wxyz + u_xlat8.wxyz;
					    u_xlat3 = u_xlat3.wxyz + u_xlat5;
					    u_xlat5 = _MainTex_TexelSize.xyxy * vec4(7.5, 0.0, -3.5, 0.0) + vs_TEXCOORD0.xyxy;
					    u_xlat8 = texture(_MainTex, u_xlat5.xy);
					    u_xlat5 = texture(_MainTex, u_xlat5.zw);
					    u_xlat3 = u_xlat3 + u_xlat8.wxyz;
					    u_xlat3 = u_xlat6.wxyz + u_xlat3;
					    u_xlat3 = u_xlat5.wxyz + u_xlat3;
					    u_xlat5 = _MainTex_TexelSize.xyxy * vec4(-5.5, 0.0, -7.5, 0.0) + vs_TEXCOORD0.xyxy;
					    u_xlat6 = texture(_MainTex, u_xlat5.xy);
					    u_xlat5 = texture(_MainTex, u_xlat5.zw);
					    u_xlat3 = u_xlat3 + u_xlat6.wxyz;
					    u_xlat3 = u_xlat5.wxyz + u_xlat3;
					    u_xlat10.xyz = u_xlat3.yzw * vec3(0.125, 0.125, 0.125);
					    u_xlat3.x = u_xlat3.x * 0.25 + -1.0;
					    u_xlat3.x = clamp(u_xlat3.x, 0.0, 1.0);
					    u_xlat10.x = dot(u_xlat10.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat5 = _MainTex_TexelSize.xyxy * vec4(0.0, -1.0, 0.0, 1.0) + vs_TEXCOORD0.xyxy;
					    u_xlat6 = texture(_MainTex, u_xlat5.xy);
					    u_xlat5 = texture(_MainTex, u_xlat5.zw);
					    u_xlat20 = dot(u_xlat6.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat30 = (-u_xlat20) + u_xlat10.x;
					    u_xlat10.x = (-u_xlat7.x) + u_xlat10.x;
					    u_xlat13.x = (-u_xlat20) + u_xlat7.x;
					    u_xlatb20 = u_xlat20==u_xlat7.x;
					    u_xlat30 = u_xlat30 / u_xlat13.x;
					    u_xlat30 = clamp(u_xlat30, 0.0, 1.0);
					    u_xlat20 = (u_xlatb20) ? 0.0 : u_xlat30;
					    u_xlat4 = u_xlat4 + (-u_xlat6);
					    u_xlat4 = vec4(u_xlat20) * u_xlat4 + u_xlat6;
					    u_xlat4 = (-u_xlat5) + u_xlat4;
					    u_xlat20 = dot(u_xlat5.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat30 = (-u_xlat20) + u_xlat7.x;
					    u_xlatb20 = u_xlat20==u_xlat7.x;
					    u_xlat10.x = u_xlat10.x / u_xlat30;
					    u_xlat10.x = u_xlat10.x + 1.0;
					    u_xlat10.x = clamp(u_xlat10.x, 0.0, 1.0);
					    u_xlat10.x = (u_xlatb20) ? 0.0 : u_xlat10.x;
					    u_xlat4 = u_xlat10.xxxx * u_xlat4 + u_xlat5;
					    u_xlat4 = (-u_xlat1) + u_xlat4;
					    u_xlat1 = u_xlat3.xxxx * u_xlat4 + u_xlat1;
					    u_xlatb10 = 0.0<u_xlat3.x;
					    u_xlatb0 = u_xlatb0 || u_xlatb10;
					    SV_Target0 = (bool(u_xlatb0)) ? u_xlat1 : u_xlat2;
					    return;
					}"
}
}
 }
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 178330
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
					
					    ps_3_0
					    def c1, -1, 0, 1, 0.5
					    def c2, 0.166666672, 0.330000013, 3, -0.100000001
					    def c3, 7.5, 0, -3.5, 0.200000003
					    def c4, 3.5, 0, 5.5, 0.125
					    def c5, 0.25, -1, 0, 0
					    def c6, -5.5, 0, -7.5, 0
					    def c7, -1.5, 0, 1.5, 2
					    dcl_texcoord v0.xy
					    dcl_2d s0
					    mov r0.xy, c0
					    mad r1, r0.xyxy, c4.yxyz, v0.xyxy
					    texld r2, r1.zwzw, s0
					    texld r1, r1, s0
					    mad r3, r0.xyxy, c7.yxyz, v0.xyxy
					    texld r4, r3.zwzw, s0
					    texld r3, r3, s0
					    add r1, r1.wxyz, r4.wxyz
					    add r4, r4, r3
					    add r1, r2.wxyz, r1
					    mad r2, r0.xyxy, c3.yxyz, v0.xyxy
					    texld r5, r2, s0
					    texld r2, r2.zwzw, s0
					    add r1, r1, r5.wxyz
					    add r1, r3.wxyz, r1
					    add r1, r2.wxyz, r1
					    mad r2, r0.xyxy, c6.yxyz, v0.xyxy
					    texld r3, r2, s0
					    texld r2, r2.zwzw, s0
					    add r1, r1, r3.wxyz
					    add r1, r2.wxyz, r1
					    mad_sat r0.z, r1.x, c5.x, c5.y
					    mul r1.xyz, r1.yzww, c4.w
					    dp3 r0.w, r1, c2.y
					    mad r1, r0.xyxy, c1.xyzy, v0.xyxy
					    texld r2, r1, s0
					    texld r1, r1.zwzw, s0
					    dp3 r3.x, r2, c2.y
					    add r3.y, r0.w, -r3.x
					    texld r5, v0, s0
					    dp3 r3.z, r5, c2.y
					    add r3.x, -r3.x, r3.z
					    rcp r3.w, r3.x
					    mul_sat r3.y, r3.w, r3.y
					    cmp r3.x, -r3_abs.x, c7.y, r3.y
					    lrp r6, r3.x, r5, r2
					    add r2.xyz, r1, r2
					    mad r2.xyz, r5, -c7.w, r2
					    mul r2.xyz, r2_abs, c1.w
					    dp3 r2.x, r2, c2.y
					    mad r2.x, r2.x, c2.z, c2.w
					    add r0.w, r0.w, -r3.z
					    dp3 r2.y, r1, c2.y
					    add r2.y, -r2.y, r3.z
					    rcp r2.z, r2.y
					    mad_sat r0.w, r0.w, r2.z, c1.z
					    cmp r0.w, -r2_abs.y, c7.y, r0.w
					    lrp r7, r0.w, r6, r1
					    mad r1, r0.xyxy, c7.xyzy, v0.xyxy
					    texld r6, r1, s0
					    texld r1, r1.zwzw, s0
					    add r8, r1, r6
					    add r9, r5, r5
					    mad r8, r8, c7.w, r9
					    mad r4, r4, c7.w, r9
					    mul r2.yzw, r8.xxyz, c2.x
					    mad r8, r8, c2.x, -r5
					    dp3 r0.w, r2.yzww, c2.y
					    rcp r0.w, r0.w
					    mad r9, r0.xyxy, c1.yxyz, v0.xyxy
					    texld r10, r9, s0
					    texld r9, r9.zwzw, s0
					    add r2.yzw, r9.xxyz, r10.xxyz
					    mad r2.yzw, r5.xxyz, -c7.w, r2
					    mul r2.yzw, r2_abs, c1.w
					    dp3 r2.y, r2.yzww, c2.y
					    mad r2.y, r2.y, c2.z, c2.w
					    mul_sat r0.w, r0.w, r2.y
					    mad r8, r0.w, r8, r5
					    mad r11, r4, c2.x, -r8
					    mul r2.yzw, r4.xxyz, c2.x
					    dp3 r0.w, r2.yzww, c2.y
					    rcp r0.w, r0.w
					    mul_sat r0.w, r0.w, r2.x
					    mul r0.w, r0.w, c1.w
					    mad r2, r0.w, r11, r8
					    lrp r4, r0.z, r7, r2
					    mad r7, r0.xyxy, c4.xyzy, v0.xyxy
					    texld r8, r7, s0
					    texld r7, r7.zwzw, s0
					    add r1, r1.wxyz, r8.wxyz
					    add r1, r7.wxyz, r1
					    mad r7, r0.xyxy, c3.xyzy, v0.xyxy
					    texld r8, r7, s0
					    texld r7, r7.zwzw, s0
					    add r1, r1, r8.wxyz
					    add r1, r6.wxyz, r1
					    add r1, r7.wxyz, r1
					    mad r6, r0.xyxy, c6.xyzy, v0.xyxy
					    texld r7, r6, s0
					    texld r6, r6.zwzw, s0
					    add r1, r1, r7.wxyz
					    add r1, r6.wxyz, r1
					    mad_sat r0.x, r1.x, c5.x, c5.y
					    mul r1.xyz, r1.yzww, c4.w
					    dp3 r0.y, r1, c2.y
					    dp3 r0.w, r10, c2.y
					    add r1.x, -r0.w, r0.y
					    add r0.y, -r3.z, r0.y
					    add r0.w, -r0.w, r3.z
					    rcp r1.y, r0.w
					    mul_sat r1.x, r1.y, r1.x
					    cmp r0.w, -r0_abs.w, c7.y, r1.x
					    lrp r1, r0.w, r5, r10
					    dp3 r0.w, r9, c2.y
					    add r0.w, -r0.w, r3.z
					    rcp r3.x, r0.w
					    mad_sat r0.y, r0.y, r3.x, c1.z
					    cmp r0.y, -r0_abs.w, c7.y, r0.y
					    lrp r3, r0.y, r1, r9
					    lrp_pp r1, r0.x, r3, r4
					    add r0.x, -r0.z, r0.x
					    add r0.x, -r0_abs.x, c3.w
					    cmp_pp oC0, r0.x, r2, r1
					
					// approximately 114 instruction slots used (21 texture, 93 arithmetic)"
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
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					float u_xlat5;
					vec4 u_xlat6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec4 u_xlat9;
					vec4 u_xlat10;
					vec3 u_xlat11;
					vec3 u_xlat16;
					float u_xlat22;
					bool u_xlatb22;
					float u_xlat27;
					float u_xlat33;
					bool u_xlatb33;
					void main()
					{
					    u_xlat0 = _MainTex_TexelSize.xyxy * vec4(0.0, 3.5, 0.0, 5.5) + vs_TEXCOORD0.xyxy;
					    u_xlat1 = texture(_MainTex, u_xlat0.zw);
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat2 = _MainTex_TexelSize.xyxy * vec4(0.0, -1.5, 0.0, 1.5) + vs_TEXCOORD0.xyxy;
					    u_xlat3 = texture(_MainTex, u_xlat2.zw);
					    u_xlat2 = texture(_MainTex, u_xlat2.xy);
					    u_xlat0 = u_xlat0.wxyz + u_xlat3.wxyz;
					    u_xlat3 = u_xlat3 + u_xlat2;
					    u_xlat0 = u_xlat1.wxyz + u_xlat0;
					    u_xlat1 = _MainTex_TexelSize.xyxy * vec4(0.0, 7.5, 0.0, -3.5) + vs_TEXCOORD0.xyxy;
					    u_xlat4 = texture(_MainTex, u_xlat1.xy);
					    u_xlat1 = texture(_MainTex, u_xlat1.zw);
					    u_xlat0 = u_xlat0 + u_xlat4.wxyz;
					    u_xlat0 = u_xlat2.wxyz + u_xlat0;
					    u_xlat0 = u_xlat1.wxyz + u_xlat0;
					    u_xlat1 = _MainTex_TexelSize.xyxy * vec4(0.0, -5.5, 0.0, -7.5) + vs_TEXCOORD0.xyxy;
					    u_xlat2 = texture(_MainTex, u_xlat1.xy);
					    u_xlat1 = texture(_MainTex, u_xlat1.zw);
					    u_xlat0 = u_xlat0 + u_xlat2.wxyz;
					    u_xlat0 = u_xlat1.wxyz + u_xlat0;
					    u_xlat11.xyz = u_xlat0.yzw * vec3(0.125, 0.125, 0.125);
					    u_xlat0.x = u_xlat0.x * 0.25 + -1.0;
					    u_xlat0.x = clamp(u_xlat0.x, 0.0, 1.0);
					    u_xlat11.x = dot(u_xlat11.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat1 = _MainTex_TexelSize.xyxy * vec4(-1.0, 0.0, 1.0, 0.0) + vs_TEXCOORD0.xyxy;
					    u_xlat2 = texture(_MainTex, u_xlat1.xy);
					    u_xlat1 = texture(_MainTex, u_xlat1.zw);
					    u_xlat22 = dot(u_xlat2.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat33 = (-u_xlat22) + u_xlat11.x;
					    u_xlat4 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat5 = dot(u_xlat4.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat16.x = (-u_xlat22) + u_xlat5;
					    u_xlatb22 = u_xlat22==u_xlat5;
					    u_xlat33 = u_xlat33 / u_xlat16.x;
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat22 = (u_xlatb22) ? 0.0 : u_xlat33;
					    u_xlat6 = (-u_xlat2) + u_xlat4;
					    u_xlat6 = vec4(u_xlat22) * u_xlat6 + u_xlat2;
					    u_xlat2.xyz = u_xlat1.xyz + u_xlat2.xyz;
					    u_xlat2.xyz = (-u_xlat4.xyz) * vec3(2.0, 2.0, 2.0) + u_xlat2.xyz;
					    u_xlat2.xyz = abs(u_xlat2.xyz) * vec3(0.5, 0.5, 0.5);
					    u_xlat22 = dot(u_xlat2.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat22 = u_xlat22 * 3.0 + -0.100000001;
					    u_xlat2 = (-u_xlat1) + u_xlat6;
					    u_xlat11.x = u_xlat11.x + (-u_xlat5);
					    u_xlat33 = dot(u_xlat1.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat16.x = (-u_xlat33) + u_xlat5;
					    u_xlatb33 = u_xlat33==u_xlat5;
					    u_xlat11.x = u_xlat11.x / u_xlat16.x;
					    u_xlat11.x = u_xlat11.x + 1.0;
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat11.x = (u_xlatb33) ? 0.0 : u_xlat11.x;
					    u_xlat1 = u_xlat11.xxxx * u_xlat2 + u_xlat1;
					    u_xlat2 = u_xlat4 + u_xlat4;
					    u_xlat3 = u_xlat3 * vec4(2.0, 2.0, 2.0, 2.0) + u_xlat2;
					    u_xlat16.xyz = u_xlat3.xyz * vec3(0.166666672, 0.166666672, 0.166666672);
					    u_xlat11.x = dot(u_xlat16.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat11.x = u_xlat22 / u_xlat11.x;
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat11.x = u_xlat11.x * 0.5;
					    u_xlat6 = _MainTex_TexelSize.xyxy * vec4(-1.5, 0.0, 1.5, 0.0) + vs_TEXCOORD0.xyxy;
					    u_xlat7 = texture(_MainTex, u_xlat6.xy);
					    u_xlat6 = texture(_MainTex, u_xlat6.zw);
					    u_xlat8 = u_xlat6 + u_xlat7;
					    u_xlat2 = u_xlat8 * vec4(2.0, 2.0, 2.0, 2.0) + u_xlat2;
					    u_xlat8 = u_xlat2 * vec4(0.166666672, 0.166666672, 0.166666672, 0.166666672) + (-u_xlat4);
					    u_xlat2.xyz = u_xlat2.xyz * vec3(0.166666672, 0.166666672, 0.166666672);
					    u_xlat22 = dot(u_xlat2.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat2 = _MainTex_TexelSize.xyxy * vec4(0.0, -1.0, 0.0, 1.0) + vs_TEXCOORD0.xyxy;
					    u_xlat9 = texture(_MainTex, u_xlat2.xy);
					    u_xlat2 = texture(_MainTex, u_xlat2.zw);
					    u_xlat16.xyz = u_xlat2.xyz + u_xlat9.xyz;
					    u_xlat16.xyz = (-u_xlat4.xyz) * vec3(2.0, 2.0, 2.0) + u_xlat16.xyz;
					    u_xlat16.xyz = abs(u_xlat16.xyz) * vec3(0.5, 0.5, 0.5);
					    u_xlat33 = dot(u_xlat16.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat33 = u_xlat33 * 3.0 + -0.100000001;
					    u_xlat22 = u_xlat33 / u_xlat22;
					    u_xlat22 = clamp(u_xlat22, 0.0, 1.0);
					    u_xlat8 = vec4(u_xlat22) * u_xlat8 + u_xlat4;
					    u_xlat4 = u_xlat4 + (-u_xlat9);
					    u_xlat3 = u_xlat3 * vec4(0.166666672, 0.166666672, 0.166666672, 0.166666672) + (-u_xlat8);
					    u_xlat3 = u_xlat11.xxxx * u_xlat3 + u_xlat8;
					    u_xlat1 = u_xlat1 + (-u_xlat3);
					    u_xlat1 = u_xlat0.xxxx * u_xlat1 + u_xlat3;
					    u_xlat8 = _MainTex_TexelSize.xyxy * vec4(3.5, 0.0, 5.5, 0.0) + vs_TEXCOORD0.xyxy;
					    u_xlat10 = texture(_MainTex, u_xlat8.xy);
					    u_xlat8 = texture(_MainTex, u_xlat8.zw);
					    u_xlat6 = u_xlat6.wxyz + u_xlat10.wxyz;
					    u_xlat6 = u_xlat8.wxyz + u_xlat6;
					    u_xlat8 = _MainTex_TexelSize.xyxy * vec4(7.5, 0.0, -3.5, 0.0) + vs_TEXCOORD0.xyxy;
					    u_xlat10 = texture(_MainTex, u_xlat8.xy);
					    u_xlat8 = texture(_MainTex, u_xlat8.zw);
					    u_xlat6 = u_xlat6 + u_xlat10.wxyz;
					    u_xlat6 = u_xlat7.wxyz + u_xlat6;
					    u_xlat6 = u_xlat8.wxyz + u_xlat6;
					    u_xlat7 = _MainTex_TexelSize.xyxy * vec4(-5.5, 0.0, -7.5, 0.0) + vs_TEXCOORD0.xyxy;
					    u_xlat8 = texture(_MainTex, u_xlat7.xy);
					    u_xlat7 = texture(_MainTex, u_xlat7.zw);
					    u_xlat6 = u_xlat6 + u_xlat8.wxyz;
					    u_xlat6 = u_xlat7.wxyz + u_xlat6;
					    u_xlat11.xyz = u_xlat6.yzw * vec3(0.125, 0.125, 0.125);
					    u_xlat16.x = u_xlat6.x * 0.25 + -1.0;
					    u_xlat16.x = clamp(u_xlat16.x, 0.0, 1.0);
					    u_xlat11.x = dot(u_xlat11.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat22 = dot(u_xlat9.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat33 = (-u_xlat22) + u_xlat11.x;
					    u_xlat11.x = (-u_xlat5) + u_xlat11.x;
					    u_xlat27 = (-u_xlat22) + u_xlat5;
					    u_xlatb22 = u_xlat22==u_xlat5;
					    u_xlat33 = u_xlat33 / u_xlat27;
					    u_xlat33 = clamp(u_xlat33, 0.0, 1.0);
					    u_xlat22 = (u_xlatb22) ? 0.0 : u_xlat33;
					    u_xlat4 = vec4(u_xlat22) * u_xlat4 + u_xlat9;
					    u_xlat4 = (-u_xlat2) + u_xlat4;
					    u_xlat22 = dot(u_xlat2.xyz, vec3(0.330000013, 0.330000013, 0.330000013));
					    u_xlat33 = (-u_xlat22) + u_xlat5;
					    u_xlatb22 = u_xlat22==u_xlat5;
					    u_xlat11.x = u_xlat11.x / u_xlat33;
					    u_xlat11.x = u_xlat11.x + 1.0;
					    u_xlat11.x = clamp(u_xlat11.x, 0.0, 1.0);
					    u_xlat11.x = (u_xlatb22) ? 0.0 : u_xlat11.x;
					    u_xlat2 = u_xlat11.xxxx * u_xlat4 + u_xlat2;
					    u_xlat2 = (-u_xlat1) + u_xlat2;
					    u_xlat1 = u_xlat16.xxxx * u_xlat2 + u_xlat1;
					    u_xlat0.x = (-u_xlat0.x) + u_xlat16.x;
					    u_xlatb0 = 0.200000003<abs(u_xlat0.x);
					    SV_Target0 = (bool(u_xlatb0)) ? u_xlat1 : u_xlat3;
					    return;
					}"
}
}
 }
}
Fallback Off
}