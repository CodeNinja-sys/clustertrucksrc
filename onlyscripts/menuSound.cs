using System;
using UnityEngine;

// Token: 0x02000295 RID: 661
public class menuSound : MonoBehaviour
{
	// Token: 0x06000FD1 RID: 4049 RVA: 0x0006697C File Offset: 0x00064B7C
	private void Start()
	{
		this.au = base.GetComponent<AudioSource>();
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x0006698C File Offset: 0x00064B8C
	private void Update()
	{
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x00066990 File Offset: 0x00064B90
	public void Select()
	{
		this.au.PlayOneShot(this.select);
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x000669A4 File Offset: 0x00064BA4
	public void Submit()
	{
		this.au.PlayOneShot(this.submit);
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x000669B8 File Offset: 0x00064BB8
	public void Ability()
	{
		this.au2.PlayOneShot(this.abilitySound);
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x000669CC File Offset: 0x00064BCC
	public void Swoosh()
	{
		this.au2.PlayOneShot(this.menuSwoosh);
	}

	// Token: 0x04000CA9 RID: 3241
	public AudioClip select;

	// Token: 0x04000CAA RID: 3242
	public AudioClip submit;

	// Token: 0x04000CAB RID: 3243
	public AudioClip abilitySound;

	// Token: 0x04000CAC RID: 3244
	public AudioClip menuSwoosh;

	// Token: 0x04000CAD RID: 3245
	private AudioSource au;

	// Token: 0x04000CAE RID: 3246
	public AudioSource au2;
}
