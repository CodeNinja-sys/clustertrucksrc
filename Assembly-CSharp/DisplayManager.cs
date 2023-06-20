using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E9 RID: 489
public class DisplayManager : MonoBehaviour
{
	// Token: 0x06000B7D RID: 2941 RVA: 0x00047A40 File Offset: 0x00045C40
	public static DisplayManager Instance()
	{
		if (!DisplayManager.displayManager)
		{
			DisplayManager.displayManager = (UnityEngine.Object.FindObjectOfType(typeof(DisplayManager)) as DisplayManager);
			if (!DisplayManager.displayManager)
			{
				Debug.LogError("There needs to be one active DisplayManager script on a GameObject in your scene.");
			}
		}
		return DisplayManager.displayManager;
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x00047A94 File Offset: 0x00045C94
	public void DisplayMessage(string message)
	{
		Debug.Log("Showing Message: " + message);
		this.displayText.text = message;
		this.SetAlpha();
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x00047AC4 File Offset: 0x00045CC4
	private void SetAlpha()
	{
		if (this.fadeAlpha != null)
		{
			base.StopCoroutine(this.fadeAlpha);
		}
		this.fadeAlpha = this.FadeAlpha();
		base.StartCoroutine(this.fadeAlpha);
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x00047B04 File Offset: 0x00045D04
	private IEnumerator FadeAlpha()
	{
		Color resetColor = this.displayText.color;
		resetColor.a = 1f;
		this.displayText.color = resetColor;
		yield return new WaitForSeconds(this.displayTime);
		while (this.displayText.color.a > 0f)
		{
			Color displayColor = this.displayText.color;
			displayColor.a -= Time.deltaTime / this.fadeTime;
			this.displayText.color = displayColor;
			yield return null;
		}
		yield return null;
		yield break;
	}

	// Token: 0x04000824 RID: 2084
	public Text displayText;

	// Token: 0x04000825 RID: 2085
	public float displayTime;

	// Token: 0x04000826 RID: 2086
	public float fadeTime;

	// Token: 0x04000827 RID: 2087
	private IEnumerator fadeAlpha;

	// Token: 0x04000828 RID: 2088
	private static DisplayManager displayManager;
}
