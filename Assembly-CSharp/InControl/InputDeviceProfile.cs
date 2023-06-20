using System;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000EF RID: 239
	public abstract class InputDeviceProfile
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x00025FC8 File Offset: 0x000241C8
		public InputDeviceProfile()
		{
			this.Name = string.Empty;
			this.Meta = string.Empty;
			this.AnalogMappings = new InputControlMapping[0];
			this.ButtonMappings = new InputControlMapping[0];
			this.SupportedPlatforms = new string[0];
			this.ExcludePlatforms = new string[0];
			this.MinUnityVersion = new VersionInfo(3, 0, 0, 0);
			this.MaxUnityVersion = new VersionInfo(9, 0, 0, 0);
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x000260C0 File Offset: 0x000242C0
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x000260C8 File Offset: 0x000242C8
		[SerializeField]
		public string Name { get; protected set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x000260D4 File Offset: 0x000242D4
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x000260DC File Offset: 0x000242DC
		[SerializeField]
		public string Meta { get; protected set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x000260E8 File Offset: 0x000242E8
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x000260F0 File Offset: 0x000242F0
		[SerializeField]
		public InputControlMapping[] AnalogMappings { get; protected set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x000260FC File Offset: 0x000242FC
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x00026104 File Offset: 0x00024304
		[SerializeField]
		public InputControlMapping[] ButtonMappings { get; protected set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00026110 File Offset: 0x00024310
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x00026118 File Offset: 0x00024318
		[SerializeField]
		public string[] SupportedPlatforms { get; protected set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00026124 File Offset: 0x00024324
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0002612C File Offset: 0x0002432C
		[SerializeField]
		public string[] ExcludePlatforms { get; protected set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00026138 File Offset: 0x00024338
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x00026140 File Offset: 0x00024340
		[SerializeField]
		public VersionInfo MinUnityVersion { get; protected set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0002614C File Offset: 0x0002434C
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x00026154 File Offset: 0x00024354
		[SerializeField]
		public VersionInfo MaxUnityVersion { get; protected set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00026160 File Offset: 0x00024360
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x00026168 File Offset: 0x00024368
		[SerializeField]
		public float Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
			protected set
			{
				this.sensitivity = Mathf.Clamp01(value);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00026178 File Offset: 0x00024378
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00026180 File Offset: 0x00024380
		[SerializeField]
		public float LowerDeadZone
		{
			get
			{
				return this.lowerDeadZone;
			}
			protected set
			{
				this.lowerDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00026190 File Offset: 0x00024390
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x00026198 File Offset: 0x00024398
		[SerializeField]
		public float UpperDeadZone
		{
			get
			{
				return this.upperDeadZone;
			}
			protected set
			{
				this.upperDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x000261A8 File Offset: 0x000243A8
		public bool IsSupportedOnThisPlatform
		{
			get
			{
				if (!this.IsSupportedOnThisVersionOfUnity)
				{
					return false;
				}
				if (this.ExcludePlatforms != null)
				{
					foreach (string text in this.ExcludePlatforms)
					{
						if (InputManager.Platform.Contains(text.ToUpper()))
						{
							return false;
						}
					}
				}
				if (this.SupportedPlatforms == null || this.SupportedPlatforms.Length == 0)
				{
					return true;
				}
				foreach (string text2 in this.SupportedPlatforms)
				{
					if (InputManager.Platform.Contains(text2.ToUpper()))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0002625C File Offset: 0x0002445C
		private bool IsSupportedOnThisVersionOfUnity
		{
			get
			{
				VersionInfo a = VersionInfo.UnityVersion();
				return a >= this.MinUnityVersion && a <= this.MaxUnityVersion;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000504 RID: 1284
		public abstract bool IsKnown { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000505 RID: 1285
		public abstract bool IsJoystick { get; }

		// Token: 0x06000506 RID: 1286
		public abstract bool HasJoystickName(string joystickName);

		// Token: 0x06000507 RID: 1287
		public abstract bool HasLastResortRegex(string joystickName);

		// Token: 0x06000508 RID: 1288
		public abstract bool HasJoystickOrRegexName(string joystickName);

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00026290 File Offset: 0x00024490
		public bool IsNotJoystick
		{
			get
			{
				return !this.IsJoystick;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0002629C File Offset: 0x0002449C
		internal static void Hide(Type type)
		{
			InputDeviceProfile.hideList.Add(type);
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x000262AC File Offset: 0x000244AC
		internal bool IsHidden
		{
			get
			{
				return InputDeviceProfile.hideList.Contains(base.GetType());
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000262C0 File Offset: 0x000244C0
		public int AnalogCount
		{
			get
			{
				return this.AnalogMappings.Length;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x000262CC File Offset: 0x000244CC
		public int ButtonCount
		{
			get
			{
				return this.ButtonMappings.Length;
			}
		}

		// Token: 0x0400039E RID: 926
		private static HashSet<Type> hideList = new HashSet<Type>();

		// Token: 0x0400039F RID: 927
		private float sensitivity = 1f;

		// Token: 0x040003A0 RID: 928
		private float lowerDeadZone;

		// Token: 0x040003A1 RID: 929
		private float upperDeadZone = 1f;

		// Token: 0x040003A2 RID: 930
		protected static InputControlSource MouseButton0 = new UnityMouseButtonSource(0);

		// Token: 0x040003A3 RID: 931
		protected static InputControlSource MouseButton1 = new UnityMouseButtonSource(1);

		// Token: 0x040003A4 RID: 932
		protected static InputControlSource MouseButton2 = new UnityMouseButtonSource(2);

		// Token: 0x040003A5 RID: 933
		protected static InputControlSource MouseXAxis = new UnityMouseAxisSource("x");

		// Token: 0x040003A6 RID: 934
		protected static InputControlSource MouseYAxis = new UnityMouseAxisSource("y");

		// Token: 0x040003A7 RID: 935
		protected static InputControlSource MouseScrollWheel = new UnityMouseAxisSource("z");
	}
}
