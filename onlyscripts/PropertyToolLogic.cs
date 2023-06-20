using System;
using UnityEngine;

// Token: 0x0200020F RID: 527
public class PropertyToolLogic : MonoBehaviour
{
	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0004D1B0 File Offset: 0x0004B3B0
	public PropertyToolLogic.PropertySlide CurrentSlide
	{
		get
		{
			return this._currentSlide;
		}
	}

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0004D1B8 File Offset: 0x0004B3B8
	public bool IsOpen
	{
		get
		{
			return this.Popup.activeInHierarchy;
		}
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0004D1C8 File Offset: 0x0004B3C8
	public GameObject[] CurrentBlocks
	{
		get
		{
			return this._currentBlocks;
		}
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06000C64 RID: 3172 RVA: 0x0004D1D0 File Offset: 0x0004B3D0
	public Modifier CurrentModifier
	{
		get
		{
			return this._currentModifier;
		}
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0004D1D8 File Offset: 0x0004B3D8
	public static PropertyToolLogic Instance
	{
		get
		{
			return PropertyToolLogic._instance;
		}
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x0004D1E0 File Offset: 0x0004B3E0
	private void Awake()
	{
		if (PropertyToolLogic._instance)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		PropertyToolLogic._instance = this;
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x0004D200 File Offset: 0x0004B400
	private void Start()
	{
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x0004D204 File Offset: 0x0004B404
	private void Update()
	{
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x0004D208 File Offset: 0x0004B408
	public void Initialize(Modifier newModifier, GameObject[] arr)
	{
		if (!this.IsOpen)
		{
			this.Open();
		}
		this.EventButton.SetActive(true);
		this._currentModifier = newModifier;
		this._currentBlocks = arr;
		Debug.Log("Selected Objects: " + arr[0].name, arr[0]);
		if (this._currentModifier != null)
		{
			this.ModifierButton.SetActive(true);
			this.BehaviourButton.SetActive(true);
		}
		this.SwitchSlide((!(this._currentModifier == null)) ? 0 : 1);
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x0004D2A4 File Offset: 0x0004B4A4
	public void SwitchSlide(int slide)
	{
		if (this._currentSlide == (PropertyToolLogic.PropertySlide)slide && this.IsCurrentSlideActive())
		{
			return;
		}
		this._currentSlide = (PropertyToolLogic.PropertySlide)slide;
		this.EventSlide.SetActive(this._currentSlide == PropertyToolLogic.PropertySlide.events);
		this.ModifierSlide.SetActive(this._currentSlide == PropertyToolLogic.PropertySlide.modifier);
		this.BehaviourSlide.SetActive(this._currentSlide == PropertyToolLogic.PropertySlide.behaviour);
		switch (this._currentSlide)
		{
		case PropertyToolLogic.PropertySlide.modifier:
			if (this._currentModifier != modifierToolLogic.Instance.CurrentModifier)
			{
				this._currentModifier.getOptions(this._currentBlocks);
			}
			break;
		case PropertyToolLogic.PropertySlide.events:
			EventToolLogic.Instance.Init(this._currentBlocks);
			break;
		case PropertyToolLogic.PropertySlide.behaviour:
			BehaviourToolLogic.Instance.Init(this._currentBlocks);
			break;
		}
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x0004D38C File Offset: 0x0004B58C
	public void Done()
	{
		modifierToolLogic.Instance.OnButtonDone();
		EventToolLogic.Instance.OnClickDone();
		BehaviourToolLogic.Instance.OnClickDone();
		this.Reset();
		this.Close();
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x0004D3C4 File Offset: 0x0004B5C4
	public void Cancel()
	{
		modifierToolLogic.Instance.Cancel();
		EventToolLogic.Instance.OnClickCancel();
		BehaviourToolLogic.Instance.OnClickCancel();
		this.Reset();
		this.Close();
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x0004D3FC File Offset: 0x0004B5FC
	private bool IsCurrentSlideActive()
	{
		switch (this._currentSlide)
		{
		case PropertyToolLogic.PropertySlide.modifier:
			return modifierToolLogic.Initalized;
		case PropertyToolLogic.PropertySlide.events:
			return EventToolLogic.Initalized;
		case PropertyToolLogic.PropertySlide.behaviour:
			return BehaviourToolLogic.Initalized;
		default:
			Debug.LogError("Invalid EVentSlide: " + this._currentSlide.ToString());
			return true;
		}
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x0004D45C File Offset: 0x0004B65C
	private void Open()
	{
		this.Popup.SetActive(true);
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x0004D46C File Offset: 0x0004B66C
	public void Close()
	{
		this.Popup.SetActive(false);
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x0004D47C File Offset: 0x0004B67C
	private void Reset()
	{
		this.BehaviourButton.SetActive(false);
		this.EventButton.SetActive(false);
		this.ModifierButton.SetActive(false);
		this._currentBlocks = null;
		this._currentModifier = null;
	}

	// Token: 0x040008F3 RID: 2291
	private PropertyToolLogic.PropertySlide _currentSlide;

	// Token: 0x040008F4 RID: 2292
	[SerializeField]
	private GameObject ModifierButton;

	// Token: 0x040008F5 RID: 2293
	[SerializeField]
	private GameObject EventButton;

	// Token: 0x040008F6 RID: 2294
	[SerializeField]
	private GameObject BehaviourButton;

	// Token: 0x040008F7 RID: 2295
	private GameObject[] _currentBlocks;

	// Token: 0x040008F8 RID: 2296
	private Modifier _currentModifier;

	// Token: 0x040008F9 RID: 2297
	public GameObject EventSlide;

	// Token: 0x040008FA RID: 2298
	public GameObject ModifierSlide;

	// Token: 0x040008FB RID: 2299
	public GameObject BehaviourSlide;

	// Token: 0x040008FC RID: 2300
	public GameObject Popup;

	// Token: 0x040008FD RID: 2301
	private static PropertyToolLogic _instance;

	// Token: 0x02000210 RID: 528
	public enum PropertySlide
	{
		// Token: 0x040008FF RID: 2303
		modifier,
		// Token: 0x04000900 RID: 2304
		events,
		// Token: 0x04000901 RID: 2305
		behaviour
	}
}
