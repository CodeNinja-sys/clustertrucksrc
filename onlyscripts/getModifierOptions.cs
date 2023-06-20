using System;
using UnityEngine;

// Token: 0x02000227 RID: 551
[Serializable]
public abstract class getModifierOptions : MonoBehaviour
{
	// Token: 0x06000CE4 RID: 3300 RVA: 0x0004F364 File Offset: 0x0004D564
	public virtual void getOptions()
	{
		Debug.Log("Base Class");
	}

	// Token: 0x02000228 RID: 552
	[Serializable]
	public abstract class Parameter
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0004F378 File Offset: 0x0004D578
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x0004F380 File Offset: 0x0004D580
		public virtual string JSONType
		{
			get
			{
				return "Hej";
			}
			set
			{
			}
		}

		// Token: 0x06000CE8 RID: 3304
		public abstract getModifierOptions.Parameter copy();

		// Token: 0x06000CE9 RID: 3305
		public abstract void setValue(string value);

		// Token: 0x06000CEA RID: 3306
		public abstract void setName(string name);

		// Token: 0x06000CEB RID: 3307
		public abstract string getValue();

		// Token: 0x06000CEC RID: 3308
		public abstract string getName();

		// Token: 0x06000CED RID: 3309
		public abstract string getDescription();

		// Token: 0x06000CEE RID: 3310
		public abstract getModifierOptions.Parameter.ParameterTypes getType();

		// Token: 0x02000229 RID: 553
		public enum ParameterTypes
		{
			// Token: 0x0400096A RID: 2410
			SliderType,
			// Token: 0x0400096B RID: 2411
			BoolType,
			// Token: 0x0400096C RID: 2412
			Dropdown,
			// Token: 0x0400096D RID: 2413
			Color,
			// Token: 0x0400096E RID: 2414
			InputField
		}
	}

	// Token: 0x0200022A RID: 554
	[Serializable]
	public class SliderParameter : getModifierOptions.Parameter
	{
		// Token: 0x06000CEF RID: 3311 RVA: 0x0004F384 File Offset: 0x0004D584
		public SliderParameter(string name, float min, float max, float value, bool clamp)
		{
			this.m_Type = getModifierOptions.Parameter.ParameterTypes.SliderType;
			this._clamp = clamp;
			this._Name = name;
			this._min = min;
			this._max = max;
			if (value < this._min || value > max)
			{
				this._value = min;
				throw new Exception("Invalid Value!");
			}
			this._value = value;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0004F3F8 File Offset: 0x0004D5F8
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x0004F400 File Offset: 0x0004D600
		public override string JSONType
		{
			get
			{
				return "A";
			}
			set
			{
			}
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0004F404 File Offset: 0x0004D604
		public override getModifierOptions.Parameter copy()
		{
			return new getModifierOptions.SliderParameter(this._Name, this._min, this._max, this._value, this._clamp);
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0004F42C File Offset: 0x0004D62C
		public float getMinValue()
		{
			return this._min;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0004F434 File Offset: 0x0004D634
		public float getMaxValue()
		{
			return this._max;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0004F43C File Offset: 0x0004D63C
		public override string getValue()
		{
			return this._value.ToString();
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0004F44C File Offset: 0x0004D64C
		public override string getName()
		{
			return this._Name;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0004F454 File Offset: 0x0004D654
		public override string getDescription()
		{
			return "SliderDescription";
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0004F45C File Offset: 0x0004D65C
		public override void setValue(string val)
		{
			MonoBehaviour.print("Setting: " + this._Name + " Value: " + val);
			this._value = float.Parse(val);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0004F488 File Offset: 0x0004D688
		public override void setName(string val)
		{
			MonoBehaviour.print("Setting: " + this._Name + " Value: " + val);
			this._Name = val;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0004F4B8 File Offset: 0x0004D6B8
		public override getModifierOptions.Parameter.ParameterTypes getType()
		{
			return this.m_Type;
		}

		// Token: 0x0400096F RID: 2415
		public getModifierOptions.Parameter.ParameterTypes m_Type;

		// Token: 0x04000970 RID: 2416
		public string _Name = "SliderParam";

		// Token: 0x04000971 RID: 2417
		[SerializeField]
		public float _min;

		// Token: 0x04000972 RID: 2418
		[SerializeField]
		public float _max;

		// Token: 0x04000973 RID: 2419
		[SerializeField]
		public float _value;

		// Token: 0x04000974 RID: 2420
		public bool _clamp;
	}

	// Token: 0x0200022B RID: 555
	[Serializable]
	public class BoolParameter : getModifierOptions.Parameter
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x0004F4C0 File Offset: 0x0004D6C0
		public BoolParameter(string name, bool startValue)
		{
			this.m_Type = getModifierOptions.Parameter.ParameterTypes.BoolType;
			this._Name = name;
			this._value = startValue;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0004F4F4 File Offset: 0x0004D6F4
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x0004F4FC File Offset: 0x0004D6FC
		public override string JSONType
		{
			get
			{
				return "B";
			}
			set
			{
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0004F500 File Offset: 0x0004D700
		public override getModifierOptions.Parameter copy()
		{
			return new getModifierOptions.BoolParameter(this._Name, this._Value);
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0004F514 File Offset: 0x0004D714
		public bool _Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0004F51C File Offset: 0x0004D71C
		public override string getValue()
		{
			return this._Value.ToString();
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0004F538 File Offset: 0x0004D738
		public override string getName()
		{
			return this._Name;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0004F540 File Offset: 0x0004D740
		public override string getDescription()
		{
			return "BoolDescription";
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0004F548 File Offset: 0x0004D748
		public override void setValue(string val)
		{
			MonoBehaviour.print("Setting: " + this._Name + " Value: " + val);
			this._value = bool.Parse(val);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0004F574 File Offset: 0x0004D774
		public override void setName(string val)
		{
			MonoBehaviour.print("Setting: " + this._Name + " Value: " + val);
			this._Name = val;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0004F5A4 File Offset: 0x0004D7A4
		public override getModifierOptions.Parameter.ParameterTypes getType()
		{
			return this.m_Type;
		}

		// Token: 0x04000975 RID: 2421
		public string _Name = "BoolParam";

		// Token: 0x04000976 RID: 2422
		public getModifierOptions.Parameter.ParameterTypes m_Type;

		// Token: 0x04000977 RID: 2423
		private bool _value;
	}

	// Token: 0x0200022C RID: 556
	[Serializable]
	public class DropdownParameter : getModifierOptions.Parameter
	{
		// Token: 0x06000D06 RID: 3334 RVA: 0x0004F5AC File Offset: 0x0004D7AC
		public DropdownParameter(string name, string[] values, int value)
		{
			this.m_Type = getModifierOptions.Parameter.ParameterTypes.Dropdown;
			this._Name = name;
			this._Values = values;
			this._Value = value;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0004F5DC File Offset: 0x0004D7DC
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x0004F5E4 File Offset: 0x0004D7E4
		public override string JSONType
		{
			get
			{
				return "C";
			}
			set
			{
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0004F5E8 File Offset: 0x0004D7E8
		public override getModifierOptions.Parameter copy()
		{
			return new getModifierOptions.DropdownParameter(this._Name, this._Values, this._Value);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0004F604 File Offset: 0x0004D804
		public override string getValue()
		{
			return this._Value.ToString();
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0004F614 File Offset: 0x0004D814
		public override string getName()
		{
			return this._Name;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0004F61C File Offset: 0x0004D81C
		public override string getDescription()
		{
			return "DropDownDescription";
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0004F624 File Offset: 0x0004D824
		public override void setValue(string val)
		{
			MonoBehaviour.print("Setting: " + this._Name + " Value: " + val);
			this._Value = int.Parse(val);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0004F650 File Offset: 0x0004D850
		public override void setName(string val)
		{
			MonoBehaviour.print("Setting: " + this._Name + " Value: " + val);
			this._Name = val;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0004F680 File Offset: 0x0004D880
		public override getModifierOptions.Parameter.ParameterTypes getType()
		{
			return this.m_Type;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0004F688 File Offset: 0x0004D888
		public string[] getOptions()
		{
			return this._Values;
		}

		// Token: 0x04000978 RID: 2424
		public string _Name = "DropdownParameter";

		// Token: 0x04000979 RID: 2425
		public string[] _Values;

		// Token: 0x0400097A RID: 2426
		public getModifierOptions.Parameter.ParameterTypes m_Type;

		// Token: 0x0400097B RID: 2427
		public int _Value;
	}

	// Token: 0x0200022D RID: 557
	[Serializable]
	public class ColorParameter : getModifierOptions.Parameter
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x0004F690 File Offset: 0x0004D890
		public ColorParameter(string name, string hexStartValue)
		{
			this.m_Type = getModifierOptions.Parameter.ParameterTypes.Color;
			this._Name = name;
			this._Value = hexStartValue;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0004F6C4 File Offset: 0x0004D8C4
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x0004F6CC File Offset: 0x0004D8CC
		public override string JSONType
		{
			get
			{
				return "D";
			}
			set
			{
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0004F6D0 File Offset: 0x0004D8D0
		public override getModifierOptions.Parameter copy()
		{
			return new getModifierOptions.ColorParameter(this._Name, this._Value);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0004F6E4 File Offset: 0x0004D8E4
		public override string getValue()
		{
			return this._Value.ToString();
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0004F6F4 File Offset: 0x0004D8F4
		public override string getName()
		{
			return this._Name;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0004F6FC File Offset: 0x0004D8FC
		public override string getDescription()
		{
			return "ColorDesciption";
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0004F704 File Offset: 0x0004D904
		public override void setValue(string val)
		{
			MonoBehaviour.print("Setting: " + this._Name + " Value: " + val);
			this._Value = val;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0004F734 File Offset: 0x0004D934
		public override void setName(string val)
		{
			MonoBehaviour.print("Setting: " + this._Name + " Value: " + val);
			this._Name = val;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0004F764 File Offset: 0x0004D964
		public override getModifierOptions.Parameter.ParameterTypes getType()
		{
			return this.m_Type;
		}

		// Token: 0x0400097C RID: 2428
		public string _Name = "ColorParameter";

		// Token: 0x0400097D RID: 2429
		public getModifierOptions.Parameter.ParameterTypes m_Type;

		// Token: 0x0400097E RID: 2430
		public string _Value;
	}

	// Token: 0x0200022E RID: 558
	[Serializable]
	public class InputFieldParameter : getModifierOptions.Parameter
	{
		// Token: 0x06000D1B RID: 3355 RVA: 0x0004F76C File Offset: 0x0004D96C
		public InputFieldParameter(string name, string value)
		{
			this.m_Type = getModifierOptions.Parameter.ParameterTypes.InputField;
			this._Name = name;
			this._Value = value;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0004F7A0 File Offset: 0x0004D9A0
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x0004F7A8 File Offset: 0x0004D9A8
		public override string JSONType
		{
			get
			{
				return "E";
			}
			set
			{
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0004F7AC File Offset: 0x0004D9AC
		public override getModifierOptions.Parameter copy()
		{
			return new getModifierOptions.InputFieldParameter(this._Name, this._Value);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0004F7C0 File Offset: 0x0004D9C0
		public override string getValue()
		{
			return this._Value.ToString();
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0004F7D0 File Offset: 0x0004D9D0
		public override string getName()
		{
			return this._Name;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0004F7D8 File Offset: 0x0004D9D8
		public override string getDescription()
		{
			return "InputfieldDesciption";
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0004F7E0 File Offset: 0x0004D9E0
		public override void setValue(string val)
		{
			MonoBehaviour.print("Setting: " + this._Name + " Value: " + val);
			this._Value = val;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0004F810 File Offset: 0x0004DA10
		public override void setName(string val)
		{
			MonoBehaviour.print("Setting: " + this._Name + " Value: " + val);
			this._Name = val;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0004F840 File Offset: 0x0004DA40
		public override getModifierOptions.Parameter.ParameterTypes getType()
		{
			return this.m_Type;
		}

		// Token: 0x0400097F RID: 2431
		public string _Name = "ColorParameter";

		// Token: 0x04000980 RID: 2432
		public getModifierOptions.Parameter.ParameterTypes m_Type;

		// Token: 0x04000981 RID: 2433
		public string _Value;
	}
}
