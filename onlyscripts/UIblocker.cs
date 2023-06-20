using System;
using UnityEngine;

// Token: 0x0200021F RID: 543
public class UIblocker : MonoBehaviour
{
	// Token: 0x06000CB4 RID: 3252 RVA: 0x0004EA70 File Offset: 0x0004CC70
	private void Start()
	{
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x0004EA74 File Offset: 0x0004CC74
	private void Update()
	{
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x0004EA78 File Offset: 0x0004CC78
	public void Activate()
	{
		MonoBehaviour.print("Activateing Ui blokc!");
		base.GetComponent<Collider>().enabled = true;
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x0004EA90 File Offset: 0x0004CC90
	public void DeActivate()
	{
		MonoBehaviour.print("DeActivateing Ui blokc!");
		base.GetComponent<Collider>().enabled = false;
	}
}
