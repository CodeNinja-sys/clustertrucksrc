using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000277 RID: 631
public class enableAfterSeconds : MonoBehaviour
{
	// Token: 0x06000F2D RID: 3885 RVA: 0x00063008 File Offset: 0x00061208
	private void Start()
	{
		this.activeScale = this.objectToEnable.transform.localScale;
		if (this.scaleIn.x != 0f)
		{
			this.objectToEnable.transform.localScale = new Vector3(0f, this.objectToEnable.transform.localScale.y, this.objectToEnable.transform.localScale.z);
		}
		if (this.scaleIn.y != 0f)
		{
			this.objectToEnable.transform.localScale = new Vector3(this.objectToEnable.transform.localScale.x, 0f, this.objectToEnable.transform.localScale.z);
		}
		if (this.scaleIn.z != 0f)
		{
			this.objectToEnable.transform.localScale = new Vector3(this.objectToEnable.transform.localScale.x, this.objectToEnable.transform.localScale.y, 0f);
		}
		this.objectToEnable.SetActive(false);
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x00063158 File Offset: 0x00061358
	private void FixedUpdate()
	{
		this.speed *= 0.999f;
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x0006316C File Offset: 0x0006136C
	private void Update()
	{
		if (this.timeToWait > -1f)
		{
			this.myText.text = Mathf.Ceil(this.timeToWait).ToString();
		}
		this.timeToWait -= Time.deltaTime;
		if (this.timeToWait < 0f && !this.done)
		{
			this.speed += Time.deltaTime * 10f;
			this.objectToEnable.SetActive(true);
			this.objectToEnable.transform.localScale = Vector3.MoveTowards(this.objectToEnable.transform.localScale, this.activeScale, Time.deltaTime * this.speed);
			if (Vector3.Distance(this.objectToEnable.transform.localScale, this.activeScale) < 1f)
			{
				this.done = true;
			}
		}
	}

	// Token: 0x04000BF9 RID: 3065
	public Text myText;

	// Token: 0x04000BFA RID: 3066
	public GameObject objectToEnable;

	// Token: 0x04000BFB RID: 3067
	public float timeToWait = 10f;

	// Token: 0x04000BFC RID: 3068
	public Vector3 scaleIn = Vector3.zero;

	// Token: 0x04000BFD RID: 3069
	private Vector3 activeScale;

	// Token: 0x04000BFE RID: 3070
	private float speed;

	// Token: 0x04000BFF RID: 3071
	private bool done;
}
