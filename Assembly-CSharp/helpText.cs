using System;
using InControl;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000282 RID: 642
public class helpText : MonoBehaviour
{
	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06000F6D RID: 3949 RVA: 0x000648BC File Offset: 0x00062ABC
	private bool HelpMeMovement
	{
		get
		{
			return PlayerPrefs.GetString("Help", bool.TrueString) == bool.TrueString;
		}
	}

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x06000F6E RID: 3950 RVA: 0x000648D8 File Offset: 0x00062AD8
	private bool HelpMeAbility
	{
		get
		{
			return !string.IsNullOrEmpty(info.abilityName) && PlayerPrefs.GetInt(info.abilityName + info.ABILITY_MOVEMENT_KEY, 0) != 1;
		}
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00064918 File Offset: 0x00062B18
	private bool HelpMeUtility
	{
		get
		{
			return !string.IsNullOrEmpty(info.utilityName) && PlayerPrefs.GetInt(info.utilityName + info.ABILITY_UTILITY_KEY, 0) != 1;
		}
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00064958 File Offset: 0x00062B58
	// (set) Token: 0x06000F71 RID: 3953 RVA: 0x000649F4 File Offset: 0x00062BF4
	private bool _helpMe
	{
		get
		{
			bool flag = !string.IsNullOrEmpty(info.abilityName) && PlayerPrefs.GetInt(info.abilityName + info.ABILITY_MOVEMENT_KEY, 0) != 1;
			bool flag2 = !string.IsNullOrEmpty(info.utilityName) && PlayerPrefs.GetInt(info.utilityName + info.ABILITY_UTILITY_KEY, 0) != 1;
			return PlayerPrefs.GetString("Help", bool.TrueString) == bool.TrueString || flag || flag2;
		}
		set
		{
			PlayerPrefs.SetString("Help", value.ToString());
		}
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x00064A08 File Offset: 0x00062C08
	public static helpText Instance()
	{
		if (!helpText._helpText)
		{
			helpText._helpText = (UnityEngine.Object.FindObjectOfType(typeof(helpText)) as helpText);
			if (!helpText._helpText)
			{
				Debug.LogError("There needs to be one active _helpText script on a GameObject in your scene.");
			}
		}
		return helpText._helpText;
	}

	// Token: 0x06000F73 RID: 3955 RVA: 0x00064A5C File Offset: 0x00062C5C
	private void Awake()
	{
		if (extensionMethods.getInput(helpText._currentInputMethod != helpText.currentInputMethod.Controller))
		{
			this.switchInputMethod();
		}
		helpText._helpStrings = ((!Manager.Controller) ? this._helpStringsRef : this._helpStringsRefController);
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x00064A9C File Offset: 0x00062C9C
	public UI_Slide_General getBigTextComponent()
	{
		return this.UI_Slide_Big;
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x00064AA4 File Offset: 0x00062CA4
	private void cleanUpHelpStrings()
	{
		if (helpText._helpStrings == null)
		{
			Debug.Log("Helpstrings == null, returning");
			return;
		}
		Debug.Log("Cleaning Helpstrings");
		if (helpText.walked || !this.HelpMeMovement)
		{
			helpText._helpStrings[0] = string.Empty;
		}
		if (helpText.sprinted || !this.HelpMeMovement)
		{
			helpText._helpStrings[3] = string.Empty;
		}
		if (helpText.looked || !this.HelpMeMovement)
		{
			helpText._helpStrings[1] = string.Empty;
		}
		if (helpText.jumped || !this.HelpMeMovement)
		{
			helpText._helpStrings[2] = string.Empty;
		}
		if (helpText.movementAbility || !this.HelpMeAbility)
		{
			helpText._helpStrings[4] = string.Empty;
		}
		if (helpText.utilityAbility || !this.HelpMeUtility)
		{
			helpText._helpStrings[5] = string.Empty;
		}
	}

	// Token: 0x06000F76 RID: 3958 RVA: 0x00064B98 File Offset: 0x00062D98
	private void Start()
	{
		this.cleanUpHelpStrings();
		this._playerRef = UnityEngine.Object.FindObjectOfType<player>();
		foreach (string text in helpText._helpStrings)
		{
			if (!string.IsNullOrEmpty(text) && this._helpMe)
			{
				Debug.Log("Start Value helptext: " + text);
				this.uiSlide.changeText(text);
				break;
			}
		}
	}

	// Token: 0x06000F77 RID: 3959 RVA: 0x00064C0C File Offset: 0x00062E0C
	public void Init()
	{
		Debug.Log("Initializing HelpText");
		helpText.utilityAbility = false;
		helpText.movementAbility = false;
		if (string.IsNullOrEmpty(this._helpStringsRef[5]))
		{
			Debug.Log("No Utility Ability Helptext");
			helpText.utilityAbility = true;
		}
		if (string.IsNullOrEmpty(this._helpStringsRef[4]))
		{
			Debug.Log("No Movement Ability Helptext");
			helpText.movementAbility = true;
		}
		this.cleanUpHelpStrings();
		bool flag = false;
		for (int i = 0; i < helpText._helpStrings.Length; i++)
		{
			string text = helpText._helpStrings[i];
			if (!string.IsNullOrEmpty(text) && this._helpMe)
			{
				this.uiSlide.changeText(text);
				helpText._currentTip = i + 1;
				Debug.Log(string.Concat(new object[]
				{
					"Helpme init (",
					text,
					") CurrentTip: ",
					helpText._currentTip
				}));
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			Debug.Log("Helpme init == false");
			this.uiSlide.changeText(string.Empty);
		}
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x00064D20 File Offset: 0x00062F20
	public void SetUtilityToolTip(AbilityBaseClass ub)
	{
		this._helpStringsRef[5] = ub.getToolTip();
		this._helpStringsRefController[5] = ub.getToolTipController();
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x00064D40 File Offset: 0x00062F40
	public void SetMovementToolTip(AbilityBaseClass ab)
	{
		this._helpStringsRef[4] = ab.getToolTip();
		this._helpStringsRefController[4] = ab.getToolTipController();
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x00064D60 File Offset: 0x00062F60
	private void Update()
	{
		if (this._helpMe)
		{
			if (extensionMethods.getInput(helpText._currentInputMethod != helpText.currentInputMethod.Controller))
			{
				this.switchInputMethod();
			}
			if (!helpText.walked && helpText._currentTip - 1 == 0 && this._playerRef.getWalkingstate())
			{
				this._walkCounter += Time.deltaTime;
				if (this._walkCounter >= 1f)
				{
					helpText._helpStrings[0] = string.Empty;
					Debug.Log("MOVED TASK");
					helpText.walked = true;
					this.nextTip();
				}
			}
			if (!helpText.sprinted && helpText._currentTip - 1 == 3 && this._playerRef.getRunningState())
			{
				this._sprintCounter += Time.deltaTime;
				if (this._sprintCounter >= 0.3f)
				{
					helpText._helpStrings[3] = string.Empty;
					Debug.Log("SPRINT TASK");
					helpText.sprinted = true;
					this.nextTip();
				}
			}
			if (!helpText.looked && helpText._currentTip - 1 == 1)
			{
				this._lookCounter += (Manager.Controller ? InputManager.ActiveDevice.RightStick.Value.magnitude : (Mathf.Abs(Input.GetAxis("Mouse X")) + Mathf.Abs(Input.GetAxis("Mouse Y"))));
				if (this._lookCounter > 10f)
				{
					helpText._helpStrings[1] = string.Empty;
					Debug.Log("Look TASK");
					helpText.looked = true;
					this.nextTip();
				}
			}
			if (!helpText.jumped && helpText._currentTip - 1 == 2 && (Input.GetButton("Jump") || InputManager.ActiveDevice.Action1.IsPressed))
			{
				helpText._helpStrings[2] = string.Empty;
				Debug.Log("JUMP TASK");
				helpText.jumped = true;
				this.nextTip();
			}
			if (!helpText.movementAbility && helpText._currentTip - 1 == 4 && (Input.GetKey(KeyCode.Mouse0) || InputManager.ActiveDevice.LeftBumper.IsPressed))
			{
				helpText._helpStrings[4] = string.Empty;
				PlayerPrefs.SetInt(info.abilityName + info.ABILITY_MOVEMENT_KEY, 1);
				Debug.Log("movementAbility TASK");
				helpText.movementAbility = true;
				this.nextTip();
			}
			if (!helpText.utilityAbility && helpText._currentTip - 1 == 5 && (Input.GetKey(KeyCode.Mouse1) || InputManager.ActiveDevice.RightBumper.IsPressed))
			{
				helpText._helpStrings[5] = string.Empty;
				PlayerPrefs.SetInt(info.utilityName + info.ABILITY_UTILITY_KEY, 1);
				Debug.Log("utilityAbility TASK");
				helpText.utilityAbility = true;
			}
			if (helpText.looked && helpText.walked && helpText.jumped && helpText.sprinted && helpText.movementAbility && helpText.utilityAbility)
			{
				Debug.Log("All tasks completed!");
				this._helpMe = false;
				this.uiSlide.changeText(string.Empty);
			}
		}
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x00065098 File Offset: 0x00063298
	private void switchInputMethod()
	{
		Debug.Log("Switching Inputmethod to " + helpText._currentInputMethod.ToString());
		if (helpText._currentInputMethod == helpText.currentInputMethod.Controller)
		{
			helpText._currentInputMethod = helpText.currentInputMethod.Keyboard;
			helpText._helpStrings = extensionMethods.syncHelpTexts(this._helpStringsRefController, this._helpStringsRef);
			Manager.Controller = false;
			this.uiSlide.changeText(helpText._helpStrings[helpText._currentTip - 1]);
		}
		else
		{
			helpText._currentInputMethod = helpText.currentInputMethod.Controller;
			Manager.Controller = true;
			helpText._helpStrings = extensionMethods.syncHelpTexts(this._helpStringsRef, this._helpStringsRefController);
			this.uiSlide.changeText(helpText._helpStrings[helpText._currentTip - 1]);
		}
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x00065148 File Offset: 0x00063348
	private void nextTip()
	{
		Debug.Log("Nexttip called");
		this._tipCounter = 0f;
		if (helpText._currentTip >= helpText._helpStrings.Length)
		{
			helpText._currentTip = 1;
		}
		else
		{
			helpText._currentTip++;
		}
		if (!string.IsNullOrEmpty(helpText._helpStrings[helpText._currentTip - 1]) || !this._helpMe)
		{
			Debug.Log("NextTip: " + helpText._helpStrings[helpText._currentTip - 1]);
			this.uiSlide.changeText(helpText._helpStrings[helpText._currentTip - 1]);
			return;
		}
		if (helpText.looked && helpText.walked && helpText.jumped && helpText.sprinted && helpText.movementAbility && helpText.utilityAbility)
		{
			return;
		}
		if (this._tipsOverflowed >= 10)
		{
			Debug.Log("Helptips Oveflowed, returning!");
			this._tipsOverflowed = 0;
			this.uiSlide.changeText(string.Empty);
			return;
		}
		Debug.Log("Nexttip Skipping current: calling nexttip again TipsOverflowed: " + this._tipsOverflowed);
		this._tipsOverflowed++;
		this.nextTip();
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x00065284 File Offset: 0x00063484
	public static void reset()
	{
		helpText.jumped = false;
		helpText.walked = false;
		helpText.sprinted = false;
		helpText.looked = false;
		helpText.movementAbility = false;
		helpText.utilityAbility = false;
		helpText._currentTip = 1;
	}

	// Token: 0x04000C31 RID: 3121
	private const string HELP_KEY_PREFS = "Help";

	// Token: 0x04000C32 RID: 3122
	private const int NUM_OF_TIPSKIPS = 10;

	// Token: 0x04000C33 RID: 3123
	public Text _helpTextField;

	// Token: 0x04000C34 RID: 3124
	private string[] _helpStringsRef = new string[]
	{
		"WASD TO MOVE",
		"MOUSE TO LOOK AROUND",
		"SPACE TO JUMP",
		"SHIFT TO SPRINT",
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C35 RID: 3125
	private string[] _helpStringsRefController = new string[]
	{
		"LEFT STICK TO MOVE",
		"RIGHT STICK TO LOOK AROUND",
		"A TO JUMP",
		"RT TO SPRINT",
		string.Empty,
		string.Empty
	};

	// Token: 0x04000C36 RID: 3126
	private float _tipCounter;

	// Token: 0x04000C37 RID: 3127
	private float _walkCounter;

	// Token: 0x04000C38 RID: 3128
	private float _sprintCounter;

	// Token: 0x04000C39 RID: 3129
	private float _lookCounter;

	// Token: 0x04000C3A RID: 3130
	private int _tipsOverflowed;

	// Token: 0x04000C3B RID: 3131
	private player _playerRef;

	// Token: 0x04000C3C RID: 3132
	public UI_Slide_General uiSlide;

	// Token: 0x04000C3D RID: 3133
	public UI_Slide_General UI_Slide_Big;

	// Token: 0x04000C3E RID: 3134
	private static helpText _helpText;

	// Token: 0x04000C3F RID: 3135
	private static int _currentTip = 1;

	// Token: 0x04000C40 RID: 3136
	public static string[] _helpStrings;

	// Token: 0x04000C41 RID: 3137
	public static bool jumped;

	// Token: 0x04000C42 RID: 3138
	public static bool walked;

	// Token: 0x04000C43 RID: 3139
	public static bool looked;

	// Token: 0x04000C44 RID: 3140
	public static bool sprinted;

	// Token: 0x04000C45 RID: 3141
	public static bool movementAbility;

	// Token: 0x04000C46 RID: 3142
	public static bool utilityAbility;

	// Token: 0x04000C47 RID: 3143
	private static helpText.currentInputMethod _currentInputMethod;

	// Token: 0x02000283 RID: 643
	public enum tasks
	{
		// Token: 0x04000C49 RID: 3145
		Moved,
		// Token: 0x04000C4A RID: 3146
		Looked,
		// Token: 0x04000C4B RID: 3147
		Jumped,
		// Token: 0x04000C4C RID: 3148
		Sprinted,
		// Token: 0x04000C4D RID: 3149
		MovementAbility,
		// Token: 0x04000C4E RID: 3150
		UtilityAbility
	}

	// Token: 0x02000284 RID: 644
	private enum currentInputMethod
	{
		// Token: 0x04000C50 RID: 3152
		Keyboard,
		// Token: 0x04000C51 RID: 3153
		Controller
	}
}
