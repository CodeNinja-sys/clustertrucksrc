using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200013D RID: 317
public class BindFramebufferToRawImage : MonoBehaviour
{
	// Token: 0x060006D3 RID: 1747 RVA: 0x0002DD8C File Offset: 0x0002BF8C
	private void OnEnable()
	{
		this.moviePlayer.OnPlay += this.HandleOnPlay;
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x0002DDA8 File Offset: 0x0002BFA8
	private void OnDisable()
	{
		this.moviePlayer.OnPlay -= this.HandleOnPlay;
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x0002DDC4 File Offset: 0x0002BFC4
	private void HandleOnPlay(MoviePlayerBase caller)
	{
		this.rawImage.texture = this.moviePlayer.framebuffer;
	}

	// Token: 0x04000508 RID: 1288
	public MoviePlayerBase moviePlayer;

	// Token: 0x04000509 RID: 1289
	public RawImage rawImage;
}
