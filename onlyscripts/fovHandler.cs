using System;
using UnityEngine;

// Token: 0x0200027E RID: 638
public class fovHandler : MonoBehaviour
{
	// Token: 0x06000F5B RID: 3931 RVA: 0x00063EF0 File Offset: 0x000620F0
	private void Start()
	{
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x00063EF4 File Offset: 0x000620F4
	private void Update()
	{
		this.cam.fieldOfView = Mathf.Clamp(Mathf.Lerp(this.cam.fieldOfView, (float)options.FOV + this.myPlayer.rig.velocity.magnitude * 0.3f, Time.deltaTime * 25f), 25f, 170f);
	}

	// Token: 0x04000C18 RID: 3096
	public Camera cam;

	// Token: 0x04000C19 RID: 3097
	public player myPlayer;

	// Token: 0x04000C1A RID: 3098
	public Rigidbody rig;
}
