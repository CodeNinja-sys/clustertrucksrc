using System;
using System.IO;
using UnityEngine;

// Token: 0x020001F2 RID: 498
public class HiResScreenShots : MonoBehaviour
{
	// Token: 0x06000BC2 RID: 3010 RVA: 0x00049010 File Offset: 0x00047210
	public static string ScreenShotName(int width, int height)
	{
		return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", new object[]
		{
			Application.dataPath,
			width,
			height,
			DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")
		});
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x0004905C File Offset: 0x0004725C
	public void TakeHiResShot()
	{
		this.takeHiResShot = true;
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x00049068 File Offset: 0x00047268
	private void LateUpdate()
	{
		this.takeHiResShot |= Input.GetKeyDown(KeyCode.Mouse1);
		if (this.takeHiResShot)
		{
			RenderTexture renderTexture = new RenderTexture(this.resWidth, this.resHeight, 24);
			base.GetComponent<Camera>().targetTexture = renderTexture;
			Texture2D texture2D = new Texture2D(this.resWidth, this.resHeight, TextureFormat.RGB24, false);
			base.GetComponent<Camera>().Render();
			RenderTexture.active = renderTexture;
			texture2D.ReadPixels(new Rect(0f, 0f, (float)this.resWidth, (float)this.resHeight), 0, 0);
			base.GetComponent<Camera>().targetTexture = null;
			RenderTexture.active = null;
			UnityEngine.Object.Destroy(renderTexture);
			byte[] bytes = texture2D.EncodeToPNG();
			string text = HiResScreenShots.ScreenShotName(this.resWidth, this.resHeight);
			File.WriteAllBytes(text, bytes);
			Debug.Log(string.Format("Took screenshot to: {0}", text));
			this.takeHiResShot = false;
		}
	}

	// Token: 0x0400085E RID: 2142
	public int resWidth = 2550;

	// Token: 0x0400085F RID: 2143
	public int resHeight = 3300;

	// Token: 0x04000860 RID: 2144
	private bool takeHiResShot;
}
