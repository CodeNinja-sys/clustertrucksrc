using System;
using UnityEngine;

// Token: 0x0200021D RID: 541
public class TutorialHandler : MonoBehaviour
{
	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0004E604 File Offset: 0x0004C804
	public RecieveOnClick[] SpecialEvents
	{
		get
		{
			return this._specialEventsArray;
		}
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0004E60C File Offset: 0x0004C80C
	public static TutorialHandler Instance
	{
		get
		{
			return TutorialHandler._instance;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0004E614 File Offset: 0x0004C814
	public int CurrentSlider
	{
		get
		{
			return this._currentSlide;
		}
	}

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0004E61C File Offset: 0x0004C81C
	public bool CanShowTips
	{
		get
		{
			return this._showTips;
		}
	}

	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0004E624 File Offset: 0x0004C824
	public bool IsHelpMenuOpen
	{
		get
		{
			return this.HelpMenu.activeInHierarchy;
		}
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x0004E634 File Offset: 0x0004C834
	private void Awake()
	{
		if (TutorialHandler._instance != null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		TutorialHandler._instance = this;
		this._specialEventsArray = new RecieveOnClick[Enum.GetNames(typeof(TutorialHandler.specialTutorialEvents)).Length];
		Debug.Log("SIZE: " + this._specialEventsArray.Length);
		this._specialEventsArray[0] = this.blockList;
		this._specialEventsArray[1] = this.spawnObject;
		this._specialEventsArray[2] = this.placeObject;
		this._specialEventsArray[3] = this.lookMouse;
		this._specialEventsArray[4] = this.moveMouse;
		this._specialEventsArray[5] = this.ascendDescendMouse;
		this._specialEventsArray[7] = this.placeObjectShift;
		this._specialEventsArray[6] = this.spawnObject2;
		this._specialEventsArray[8] = this.selectRedRoad;
		this._specialEventsArray[10] = this.selectYellowRoad;
		this._specialEventsArray[9] = this.paintRedRoad;
		this._specialEventsArray[11] = this.paintYellowRoad;
		this._specialEventsArray[12] = this.truckBrush;
		this._specialEventsArray[13] = this.spawnTrap;
		this._specialEventsArray[15] = this.modifyTrapDone;
		this._specialEventsArray[14] = this.selectModifier;
		this._specialEventsArray[16] = this.mapSettings;
		this._specialEventsArray[17] = this.mapSettingsDone;
		this._specialEventsArray[18] = this.goalPlayerQuick;
		for (int i = 0; i < this.SpecialEvents.Length; i++)
		{
			if (this.SpecialEvents[i] == null)
			{
				Debug.LogError(((TutorialHandler.specialTutorialEvents)i).ToString() + " Is not setup!");
			}
		}
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x0004E7F0 File Offset: 0x0004C9F0
	private void Update()
	{
		if (Application.isEditor && Input.GetKeyDown(KeyCode.M))
		{
			this.nextSlide();
		}
		if (this._currentSlide - 1 >= this.TutorialSlides.Length || !this._showTips)
		{
			return;
		}
		if ((levelEditorManager.Instance().IsBusy && !this.mapSettingsDone.gameObject.activeInHierarchy) || levelEditorManager.Instance().IsPlayTesting)
		{
			if (this.TutorialSlides[this._currentSlide - 1].activeInHierarchy)
			{
				this.TutorialSlides[this._currentSlide - 1].SetActive(false);
			}
		}
		else if (!this.TutorialSlides[this._currentSlide - 1].activeInHierarchy)
		{
			this.TutorialSlides[this._currentSlide - 1].SetActive(true);
		}
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x0004E8D0 File Offset: 0x0004CAD0
	public void nextSlide()
	{
		this.TutorialSlides[this._currentSlide - 1].SetActive(false);
		this._currentSlide++;
		if (this._currentSlide - 1 >= this.TutorialSlides.Length || !this._showTips)
		{
			return;
		}
		this.TutorialSlides[this._currentSlide - 1].SetActive(true);
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x0004E938 File Offset: 0x0004CB38
	public void StartAgain()
	{
		this._showTips = true;
		foreach (GameObject gameObject in this.TutorialSlides)
		{
			gameObject.SetActive(false);
		}
		this._currentSlide = 2;
		this.TutorialSlides[1].SetActive(true);
		this.HelpMenu.SetActive(false);
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x0004E994 File Offset: 0x0004CB94
	public void Stop(bool close = false)
	{
		this._showTips = false;
		if (close)
		{
			this.TutorialSlides[this._currentSlide - 1].SetActive(this._showTips);
			return;
		}
		this.OpenHelpMenu();
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x0004E9D0 File Offset: 0x0004CBD0
	public void OpenHelpMenu()
	{
		if (this._currentSlide - 1 < this.TutorialSlides.Length && this._showTips)
		{
			this.TutorialSlides[this._currentSlide - 1].SetActive(this.HelpMenu.activeInHierarchy && this._showTips);
		}
		if (!this.HelpMenu.activeInHierarchy)
		{
			levelEditorManager.Instance().openPopUpMenu(this.HelpMenu);
		}
		else
		{
			this.HelpMenu.SetActive(false);
		}
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x0004EA5C File Offset: 0x0004CC5C
	public void OpenFeedbackURL()
	{
		Application.OpenURL("https://docs.google.com/forms/d/1P7_PIBuonVaFclX8OFv_7hsqbdxDism13xwgeoAHzZI/viewform");
	}

	// Token: 0x0400091E RID: 2334
	public RecieveOnClick blockList;

	// Token: 0x0400091F RID: 2335
	public RecieveOnClick spawnObject;

	// Token: 0x04000920 RID: 2336
	public RecieveOnClick placeObject;

	// Token: 0x04000921 RID: 2337
	public RecieveOnClick lookMouse;

	// Token: 0x04000922 RID: 2338
	public RecieveOnClick moveMouse;

	// Token: 0x04000923 RID: 2339
	public RecieveOnClick ascendDescendMouse;

	// Token: 0x04000924 RID: 2340
	public RecieveOnClick spawnObject2;

	// Token: 0x04000925 RID: 2341
	public RecieveOnClick placeObjectShift;

	// Token: 0x04000926 RID: 2342
	public RecieveOnClick selectRedRoad;

	// Token: 0x04000927 RID: 2343
	public RecieveOnClick paintRedRoad;

	// Token: 0x04000928 RID: 2344
	public RecieveOnClick selectYellowRoad;

	// Token: 0x04000929 RID: 2345
	public RecieveOnClick paintYellowRoad;

	// Token: 0x0400092A RID: 2346
	public RecieveOnClick truckBrush;

	// Token: 0x0400092B RID: 2347
	public RecieveOnClick spawnTrap;

	// Token: 0x0400092C RID: 2348
	public RecieveOnClick selectModifier;

	// Token: 0x0400092D RID: 2349
	public RecieveOnClick modifyTrapDone;

	// Token: 0x0400092E RID: 2350
	public RecieveOnClick mapSettings;

	// Token: 0x0400092F RID: 2351
	public RecieveOnClick mapSettingsDone;

	// Token: 0x04000930 RID: 2352
	public RecieveOnClick goalPlayerQuick;

	// Token: 0x04000931 RID: 2353
	private RecieveOnClick[] _specialEventsArray;

	// Token: 0x04000932 RID: 2354
	private static TutorialHandler _instance;

	// Token: 0x04000933 RID: 2355
	public GameObject[] TutorialSlides;

	// Token: 0x04000934 RID: 2356
	public GameObject HelpMenu;

	// Token: 0x04000935 RID: 2357
	private int _currentSlide = 1;

	// Token: 0x04000936 RID: 2358
	private bool _showTips = true;

	// Token: 0x0200021E RID: 542
	public enum specialTutorialEvents
	{
		// Token: 0x04000938 RID: 2360
		blockList,
		// Token: 0x04000939 RID: 2361
		spawnObject,
		// Token: 0x0400093A RID: 2362
		placeObject,
		// Token: 0x0400093B RID: 2363
		lookMouse,
		// Token: 0x0400093C RID: 2364
		moveMouse,
		// Token: 0x0400093D RID: 2365
		ascendDescendMouse,
		// Token: 0x0400093E RID: 2366
		spawnObject2,
		// Token: 0x0400093F RID: 2367
		placeObjectSHIFT,
		// Token: 0x04000940 RID: 2368
		selectRedRoad,
		// Token: 0x04000941 RID: 2369
		paintRedRoad,
		// Token: 0x04000942 RID: 2370
		selectYellowRoad,
		// Token: 0x04000943 RID: 2371
		paintYellowRoad,
		// Token: 0x04000944 RID: 2372
		truckBrush,
		// Token: 0x04000945 RID: 2373
		spawnTrap,
		// Token: 0x04000946 RID: 2374
		selectModifierTool,
		// Token: 0x04000947 RID: 2375
		modifyTrapPanelDone,
		// Token: 0x04000948 RID: 2376
		mapSettings,
		// Token: 0x04000949 RID: 2377
		mapSettingsDone,
		// Token: 0x0400094A RID: 2378
		goalPlayerQuick
	}
}
