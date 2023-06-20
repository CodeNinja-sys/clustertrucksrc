using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002F9 RID: 761
public class pointsHandler : MonoBehaviour
{
	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x060011EE RID: 4590 RVA: 0x0007304C File Offset: 0x0007124C
	public static float Points
	{
		get
		{
			return (float)pointsHandler._currentPoints;
		}
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x00073054 File Offset: 0x00071254
	private void Start()
	{
		pointsHandler._currentPoints = (int)PlayerPrefs.GetFloat("points", pointsHandler.Points);
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x0007306C File Offset: 0x0007126C
	private void Update()
	{
		this.pointsText.text = pointsHandler.Points.ToString("F0");
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x00073098 File Offset: 0x00071298
	public static void AddPoints(float p)
	{
		pointsHandler._currentPoints += (int)p;
		PlayerPrefs.SetFloat("points", pointsHandler.Points);
	}

	// Token: 0x04000F09 RID: 3849
	private static int _currentPoints;

	// Token: 0x04000F0A RID: 3850
	public Text pointsText;
}
