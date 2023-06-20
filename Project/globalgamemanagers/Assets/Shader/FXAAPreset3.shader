Shader "Hidden/FXAA Preset 3" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" { }
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  GpuProgramID 58378
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
					    def c1, 0, -1, 1, 1.9632107
					    def c2, 0.125, -0.0416666679, 0.25, -0.25
					    def c3, 1.33333337, 0.75, -0.5, 0.5
					    def c4, 0, 0.111111112, 0, 0
					    defi i0, 16, 0, 0, 0
					    dcl_texcoord v0.xy
					    dcl_2d s0
					    mov r0.xyz, c1
					    mad r1, c0.xyxy, r0.xyyx, v0.xyxy
					    mul r2, r1.xyxx, c1.zzxx
					    texldl r2, r2, s0
					    mul r1, r1.zwxx, c1.zzxx
					    texldl r1, r1, s0
					    mul r3, c1.zzxx, v0.xyxx
					    texldl r3, r3, s0
					    mad r4, c0.xyxy, r0.zxxz, v0.xyxy
					    mul r5, r4.xyxx, c1.zzxx
					    texldl r5, r5, s0
					    mul r4, r4.zwxx, c1.zzxx
					    texldl r4, r4, s0
					    mad r0.w, r2.y, c1.w, r2.x
					    mad r1.w, r1.y, c1.w, r1.x
					    mad r2.w, r3.y, c1.w, r3.x
					    mad r3.w, r5.y, c1.w, r5.x
					    mad r4.w, r4.y, c1.w, r4.x
					    min r5.w, r1.w, r0.w
					    min r6.x, r3.w, r4.w
					    min r7.x, r6.x, r5.w
					    min r5.w, r7.x, r2.w
					    max r6.x, r0.w, r1.w
					    max r6.y, r4.w, r3.w
					    max r7.x, r6.x, r6.y
					    max r6.x, r2.w, r7.x
					    add r5.w, -r5.w, r6.x
					    mul r6.x, r6.x, c2.x
					    min r7.x, -r6.x, c2.y
					    if_lt r5.w, -r7.x
					    else
					      add r1.xyz, r1, r2
					      add r1.xyz, r3, r1
					      add r1.xyz, r5, r1
					      add r1.xyz, r4, r1
					      add r2.x, r0.w, r1.w
					      add r2.x, r3.w, r2.x
					      add r2.x, r4.w, r2.x
					      mad r2.x, r2.x, c2.z, -r2.w
					      rcp r2.y, r5.w
					      mad r2.x, r2_abs.x, r2.y, c2.w
					      mul r2.y, r2.x, c3.x
					      cmp r2.x, r2.x, r2.y, c1.x
					      min r4.x, r2.x, c3.y
					      add r5.xy, -c0, v0
					      mov r5.zw, c1.x
					      texldl r5, r5, s0
					      mad r6, c0.xyxy, r0.zyyz, v0.xyxy
					      mul r7, r6.xyxx, c1.zzxx
					      texldl r7, r7, s0
					      mul r6, r6.zwxx, c1.zzxx
					      texldl r6, r6, s0
					      add r8.xy, c0, v0
					      mov r8.zw, c1.x
					      texldl r8, r8, s0
					      add r2.xyz, r5, r7
					      add r2.xyz, r6, r2
					      add r2.xyz, r8, r2
					      add r1.xyz, r1, r2
					      mul r1.xyz, r4.x, r1
					      mad r0.y, r5.y, c1.w, r5.x
					      mad r2.x, r7.y, c1.w, r7.x
					      mad r2.y, r6.y, c1.w, r6.x
					      mad r2.z, r8.y, c1.w, r8.x
					      mul r4.y, r0.w, c3.z
					      mad r4.y, r0.y, c2.z, r4.y
					      mad r4.y, r2.x, c2.z, r4.y
					      mul r4.z, r1.w, c3.z
					      mad r5.x, r1.w, c3.w, -r2.w
					      mul r5.y, r3.w, c3.z
					      mad r5.x, r3.w, c3.w, r5.x
					      add r4.y, r4_abs.y, r5_abs.x
					      mul r5.x, r4.w, c3.z
					      mad r5.x, r2.y, c2.z, r5.x
					      mad r5.x, r2.z, c2.z, r5.x
					      add r4.y, r4.y, r5_abs.x
					      mad r0.y, r0.y, c2.z, r4.z
					      mad r0.y, r2.y, c2.z, r0.y
					      mad r2.y, r0.w, c3.w, -r2.w
					      mad r2.y, r4.w, c3.w, r2.y
					      add r0.y, r0_abs.y, r2_abs.y
					      mad r2.x, r2.x, c2.z, r5.y
					      mad r2.x, r2.z, c2.z, r2.x
					      add r0.y, r0.y, r2_abs.x
					      add r0.y, -r4.y, r0.y
					      cmp r2.x, r0.y, -c0.y, -c0.x
					      cmp r0.w, r0.y, r0.w, r1.w
					      cmp r1.w, r0.y, r4.w, r3.w
					      add r2.y, -r2.w, r0.w
					      add r2.z, -r2.w, r1.w
					      add r0.w, r2.w, r0.w
					      mul r0.w, r0.w, c3.w
					      add r1.w, r2.w, r1.w
					      mul r1.w, r1.w, c3.w
					      add r3.w, -r2_abs.z, r2_abs.y
					      cmp r0.w, r3.w, r0.w, r1.w
					      max r1.w, r2_abs.y, r2_abs.z
					      cmp r2.x, r3.w, r2.x, -r2.x
					      mul r2.y, r2.x, c3.w
					      cmp r2.z, r0.y, c1.x, r2.y
					      cmp r2.y, r0.y, r2.y, c1.x
					      add r5.xy, r2.zyzw, v0
					      mul r6, r0.zxxz, c0.xxxy
					      cmp r0.xz, r0.y, r6.xyyw, r6.zyww
					      add r2.yz, -r0.xxzw, r5.xxyw
					      add r4.yz, r0.xxzw, r5.xxyw
					      mov r5.xy, r2.yzzw
					      mov r5.zw, r4.xyyz
					      mov r3.w, r0.w
					      mov r4.w, r0.w
					      mov r6.xy, c1.x
					      rep i0
					        if_ne r6.x, -r6.x
					          mov r6.z, r3.w
					        else
					          mul r7, r5.xyxx, c1.zzxx
					          texldl r7, r7, s0
					          mad r6.z, r7.y, c1.w, r7.x
					        endif
					        if_ne r6.y, -r6.y
					          mov r6.w, r4.w
					        else
					          mul r7, r5.zwzz, c1.zzxx
					          texldl r7, r7, s0
					          mad r6.w, r7.y, c1.w, r7.x
					        endif
					        add r7.xy, -r0.w, r6.zwzw
					        mad r7.x, r1.w, -c2.z, r7_abs.x
					        cmp r7.x, r7.x, c1.z, c1.x
					        mad r7.y, r1.w, -c2.z, r7_abs.y
					        cmp r7.y, r7.y, c1.z, c1.x
					        add r7.xy, r6, r7
					        cmp r6.xy, -r7, c1.x, c1.z
					        mul r7.z, r6.y, r6.x
					        if_ne r7.z, -r7.z
					          mov r3.w, r6.z
					          mov r4.w, r6.w
					          break_ne c1.z, -c1.z
					        endif
					        add r7.zw, -r0.xyxz, r5.xyxy
					        cmp r5.xy, -r7.x, r7.zwzw, r5
					        add r7.xz, r0, r5.zyww
					        cmp r5.zw, -r7.y, r7.xyxz, r5
					        mov r3.w, r6.z
					        mov r4.w, r6.w
					      endrep
					      add r0.xz, -r5.xyyw, v0.xyyw
					      cmp r0.x, r0.y, r0.x, r0.z
					      add r2.yz, r5.xzww, -v0.xxyw
					      cmp r0.z, r0.y, r2.y, r2.z
					      add r1.w, -r0.z, r0.x
					      cmp r1.w, r1.w, r4.w, r3.w
					      add r2.y, -r0.w, r2.w
					      cmp r2.y, r2.y, c1.x, c1.z
					      add r0.w, -r0.w, r1.w
					      cmp r0.w, r0.w, -c1.x, -c1.z
					      add r0.w, r0.w, r2.y
					      cmp r0.w, -r0_abs.w, c1.x, r2.x
					      add r1.w, r0.x, r0.z
					      min r2.x, r0.z, r0.x
					      rcp r0.x, r1.w
					      mad r0.x, r2.x, -r0.x, c3.w
					      mul r0.x, r0.w, r0.x
					      cmp r0.z, r0.y, c1.x, r0.x
					      cmp r0.x, r0.y, r0.x, c1.x
					      add r2.xy, r0.zxzw, v0
					      mov r2.zw, c1.x
					      texldl r0, r2, s0
					      mad r1.xyz, r1, c4.y, r0
					      mad r3.xyz, -r4.x, r0, r1
					    endif
					    mov oC0.xyz, r3
					    mov oC0.w, c1.x
					
					// approximately 198 instruction slots used (24 texture, 174 arithmetic)"
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
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					int u_xlati6;
					vec4 u_xlat7;
					vec4 u_xlat8;
					vec2 u_xlat10;
					bool u_xlatb10;
					float u_xlat12;
					bool u_xlatb12;
					float u_xlat13;
					float u_xlat14;
					bool u_xlatb14;
					int u_xlati15;
					vec2 u_xlat16;
					int u_xlati16;
					bool u_xlatb16;
					float u_xlat19;
					vec2 u_xlat21;
					bool u_xlatb21;
					vec2 u_xlat22;
					vec2 u_xlat23;
					int u_xlati24;
					float u_xlat27;
					int u_xlati27;
					bool u_xlatb27;
					float u_xlat28;
					float u_xlat29;
					int u_xlati29;
					float u_xlat30;
					float u_xlat31;
					float u_xlat33;
					bool u_xlatb33;
					void main()
					{
					    u_xlat0 = _MainTex_TexelSize.xyxy * vec4(0.0, -1.0, -1.0, 0.0) + vs_TEXCOORD0.xyxy;
					    u_xlat1 = textureLod(_MainTex, u_xlat0.xy, 0.0);
					    u_xlat0 = textureLod(_MainTex, u_xlat0.zw, 0.0);
					    u_xlat2 = textureLod(_MainTex, vs_TEXCOORD0.xy, 0.0);
					    u_xlat3 = _MainTex_TexelSize.xyxy * vec4(1.0, 0.0, 0.0, 1.0) + vs_TEXCOORD0.xyxy;
					    u_xlat4 = textureLod(_MainTex, u_xlat3.xy, 0.0);
					    u_xlat3 = textureLod(_MainTex, u_xlat3.zw, 0.0);
					    u_xlat27 = u_xlat1.y * 1.9632107 + u_xlat1.x;
					    u_xlat28 = u_xlat0.y * 1.9632107 + u_xlat0.x;
					    u_xlat29 = u_xlat2.y * 1.9632107 + u_xlat2.x;
					    u_xlat30 = u_xlat4.y * 1.9632107 + u_xlat4.x;
					    u_xlat31 = u_xlat3.y * 1.9632107 + u_xlat3.x;
					    u_xlat5.x = min(u_xlat27, u_xlat28);
					    u_xlat14 = min(u_xlat30, u_xlat31);
					    u_xlat5.x = min(u_xlat14, u_xlat5.x);
					    u_xlat5.x = min(u_xlat29, u_xlat5.x);
					    u_xlat14 = max(u_xlat27, u_xlat28);
					    u_xlat23.x = max(u_xlat30, u_xlat31);
					    u_xlat14 = max(u_xlat23.x, u_xlat14);
					    u_xlat14 = max(u_xlat29, u_xlat14);
					    u_xlat5.x = (-u_xlat5.x) + u_xlat14;
					    u_xlat14 = u_xlat14 * 0.125;
					    u_xlat14 = max(u_xlat14, 0.0416666679);
					    u_xlatb14 = u_xlat5.x>=u_xlat14;
					    if(u_xlatb14){
					        u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
					        u_xlat0.xyz = u_xlat2.xyz + u_xlat0.xyz;
					        u_xlat0.xyz = u_xlat4.xyz + u_xlat0.xyz;
					        u_xlat0.xyz = u_xlat3.xyz + u_xlat0.xyz;
					        u_xlat1.x = u_xlat27 + u_xlat28;
					        u_xlat1.x = u_xlat30 + u_xlat1.x;
					        u_xlat1.x = u_xlat31 + u_xlat1.x;
					        u_xlat1.x = u_xlat1.x * 0.25 + (-u_xlat29);
					        u_xlat1.x = abs(u_xlat1.x) / u_xlat5.x;
					        u_xlat1.x = u_xlat1.x + -0.25;
					        u_xlat1.x = max(u_xlat1.x, 0.0);
					        u_xlat1.x = u_xlat1.x * 1.33333337;
					        u_xlat1.x = min(u_xlat1.x, 0.75);
					        u_xlat10.xy = vs_TEXCOORD0.xy + (-_MainTex_TexelSize.xy);
					        u_xlat5 = textureLod(_MainTex, u_xlat10.xy, 0.0);
					        u_xlat6 = _MainTex_TexelSize.xyxy * vec4(1.0, -1.0, -1.0, 1.0) + vs_TEXCOORD0.xyxy;
					        u_xlat7 = textureLod(_MainTex, u_xlat6.xy, 0.0);
					        u_xlat6 = textureLod(_MainTex, u_xlat6.zw, 0.0);
					        u_xlat10.xy = vs_TEXCOORD0.xy + _MainTex_TexelSize.xy;
					        u_xlat8 = textureLod(_MainTex, u_xlat10.xy, 0.0);
					        u_xlat3.xyz = u_xlat5.xyz + u_xlat7.xyz;
					        u_xlat3.xyz = u_xlat6.xyz + u_xlat3.xyz;
					        u_xlat3.xyz = u_xlat8.xyz + u_xlat3.xyz;
					        u_xlat0.xyz = u_xlat0.xyz + u_xlat3.xyz;
					        u_xlat0.xyz = u_xlat1.xxx * u_xlat0.xyz;
					        u_xlat10.x = u_xlat5.y * 1.9632107 + u_xlat5.x;
					        u_xlat19 = u_xlat7.y * 1.9632107 + u_xlat7.x;
					        u_xlat3.x = u_xlat6.y * 1.9632107 + u_xlat6.x;
					        u_xlat12 = u_xlat8.y * 1.9632107 + u_xlat8.x;
					        u_xlat21.x = u_xlat27 * -0.5;
					        u_xlat21.x = u_xlat10.x * 0.25 + u_xlat21.x;
					        u_xlat21.x = u_xlat19 * 0.25 + u_xlat21.x;
					        u_xlat4.x = u_xlat28 * -0.5;
					        u_xlat13 = u_xlat28 * 0.5 + (-u_xlat29);
					        u_xlat22.x = u_xlat30 * -0.5;
					        u_xlat13 = u_xlat30 * 0.5 + u_xlat13;
					        u_xlat21.x = abs(u_xlat21.x) + abs(u_xlat13);
					        u_xlat13 = u_xlat31 * -0.5;
					        u_xlat13 = u_xlat3.x * 0.25 + u_xlat13;
					        u_xlat13 = u_xlat12 * 0.25 + u_xlat13;
					        u_xlat21.x = u_xlat21.x + abs(u_xlat13);
					        u_xlat10.x = u_xlat10.x * 0.25 + u_xlat4.x;
					        u_xlat10.x = u_xlat3.x * 0.25 + u_xlat10.x;
					        u_xlat3.x = u_xlat27 * 0.5 + (-u_xlat29);
					        u_xlat3.x = u_xlat31 * 0.5 + u_xlat3.x;
					        u_xlat10.x = abs(u_xlat10.x) + abs(u_xlat3.x);
					        u_xlat19 = u_xlat19 * 0.25 + u_xlat22.x;
					        u_xlat19 = u_xlat12 * 0.25 + u_xlat19;
					        u_xlat10.x = abs(u_xlat19) + u_xlat10.x;
					        u_xlatb10 = u_xlat10.x>=u_xlat21.x;
					        u_xlat19 = (u_xlatb10) ? (-_MainTex_TexelSize.y) : (-_MainTex_TexelSize.x);
					        u_xlat27 = (u_xlatb10) ? u_xlat27 : u_xlat28;
					        u_xlat28 = (u_xlatb10) ? u_xlat31 : u_xlat30;
					        u_xlat3.x = (-u_xlat29) + u_xlat27;
					        u_xlat12 = (-u_xlat29) + u_xlat28;
					        u_xlat27 = u_xlat29 + u_xlat27;
					        u_xlat27 = u_xlat27 * 0.5;
					        u_xlat28 = u_xlat29 + u_xlat28;
					        u_xlat28 = u_xlat28 * 0.5;
					        u_xlatb21 = abs(u_xlat3.x)>=abs(u_xlat12);
					        u_xlat27 = (u_xlatb21) ? u_xlat27 : u_xlat28;
					        u_xlat28 = (u_xlatb21) ? abs(u_xlat3.x) : abs(u_xlat12);
					        u_xlat19 = (u_xlatb21) ? u_xlat19 : (-u_xlat19);
					        u_xlat3.x = u_xlat19 * 0.5;
					        u_xlat3.y = (u_xlatb10) ? 0.0 : u_xlat3.x;
					        u_xlat3.x = u_xlatb10 ? u_xlat3.x : float(0.0);
					        u_xlat4.xy = u_xlat3.yx + vs_TEXCOORD0.xy;
					        u_xlat28 = u_xlat28 * 0.25;
					        u_xlat3.y = float(0.0);
					        u_xlat3.z = float(0.0);
					        u_xlat3.xw = _MainTex_TexelSize.xy;
					        u_xlat3.xy = (bool(u_xlatb10)) ? u_xlat3.xy : u_xlat3.zw;
					        u_xlat21.xy = (-u_xlat3.xy) + u_xlat4.xy;
					        u_xlat4.xy = u_xlat3.xy + u_xlat4.xy;
					        u_xlat22.xy = u_xlat21.xy;
					        u_xlat5.xy = u_xlat4.xy;
					        u_xlat23.xy = vec2(u_xlat27);
					        u_xlati6 = int(0);
					        u_xlati15 = int(0);
					        u_xlati24 = int(0);
					        while(true){
					            u_xlatb33 = u_xlati24>=16;
					            if(u_xlatb33){break;}
					            if(u_xlati6 == 0) {
					                u_xlat7 = textureLod(_MainTex, u_xlat22.xy, 0.0);
					                u_xlat33 = u_xlat7.y * 1.9632107 + u_xlat7.x;
					            } else {
					                u_xlat33 = u_xlat23.x;
					            }
					            if(u_xlati15 == 0) {
					                u_xlat7 = textureLod(_MainTex, u_xlat5.xy, 0.0);
					                u_xlat7.x = u_xlat7.y * 1.9632107 + u_xlat7.x;
					            } else {
					                u_xlat7.x = u_xlat23.y;
					            }
					            u_xlat16.x = (-u_xlat27) + u_xlat33;
					            u_xlatb16 = abs(u_xlat16.x)>=u_xlat28;
					            u_xlati6 = int(uint(u_xlati6) | (uint(u_xlatb16) * 0xffffffffu));
					            u_xlat16.x = (-u_xlat27) + u_xlat7.x;
					            u_xlatb16 = abs(u_xlat16.x)>=u_xlat28;
					            u_xlati15 = int(uint(u_xlati15) | (uint(u_xlatb16) * 0xffffffffu));
					            u_xlati16 = int(uint(u_xlati15) & uint(u_xlati6));
					            if(u_xlati16 != 0) {
					                u_xlat23.x = u_xlat33;
					                u_xlat23.y = u_xlat7.x;
					                break;
					            }
					            u_xlat16.xy = (-u_xlat3.xy) + u_xlat22.xy;
					            u_xlat22.xy = (int(u_xlati6) != 0) ? u_xlat22.xy : u_xlat16.xy;
					            u_xlat16.xy = u_xlat3.xy + u_xlat5.xy;
					            u_xlat5.xy = (int(u_xlati15) != 0) ? u_xlat5.xy : u_xlat16.xy;
					            u_xlati24 = u_xlati24 + 1;
					            u_xlat23.x = u_xlat33;
					            u_xlat23.y = u_xlat7.x;
					        }
					        u_xlat3.xy = (-u_xlat22.xy) + vs_TEXCOORD0.xy;
					        u_xlat28 = (u_xlatb10) ? u_xlat3.x : u_xlat3.y;
					        u_xlat3.xy = u_xlat5.xy + (-vs_TEXCOORD0.xy);
					        u_xlat3.x = (u_xlatb10) ? u_xlat3.x : u_xlat3.y;
					        u_xlatb12 = u_xlat28<u_xlat3.x;
					        u_xlat21.x = (u_xlatb12) ? u_xlat23.x : u_xlat23.y;
					        u_xlat29 = (-u_xlat27) + u_xlat29;
					        u_xlati29 = int((u_xlat29<0.0) ? 0xFFFFFFFFu : uint(0));
					        u_xlat27 = (-u_xlat27) + u_xlat21.x;
					        u_xlati27 = int((u_xlat27<0.0) ? 0xFFFFFFFFu : uint(0));
					        u_xlatb27 = u_xlati27==u_xlati29;
					        u_xlat27 = (u_xlatb27) ? 0.0 : u_xlat19;
					        u_xlat19 = u_xlat28 + u_xlat3.x;
					        u_xlat28 = (u_xlatb12) ? u_xlat28 : u_xlat3.x;
					        u_xlat19 = -1.0 / u_xlat19;
					        u_xlat19 = u_xlat28 * u_xlat19 + 0.5;
					        u_xlat27 = u_xlat27 * u_xlat19;
					        u_xlat19 = (u_xlatb10) ? 0.0 : u_xlat27;
					        u_xlat3.x = u_xlat19 + vs_TEXCOORD0.x;
					        u_xlat27 = u_xlatb10 ? u_xlat27 : float(0.0);
					        u_xlat3.y = u_xlat27 + vs_TEXCOORD0.y;
					        u_xlat3 = textureLod(_MainTex, u_xlat3.xy, 0.0);
					        u_xlat0.xyz = u_xlat0.xyz * vec3(0.111111112, 0.111111112, 0.111111112) + u_xlat3.xyz;
					        u_xlat2.xyz = (-u_xlat1.xxx) * u_xlat3.xyz + u_xlat0.xyz;
					    }
					    SV_Target0.xyz = u_xlat2.xyz;
					    SV_Target0.w = 0.0;
					    return;
					}"
}
}
 }
}
Fallback "Hidden/FXAA II"
}