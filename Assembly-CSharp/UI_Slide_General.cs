using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025E RID: 606
public class UI_Slide_General : MonoBehaviour
{
	// Token: 0x06000EB5 RID: 3765 RVA: 0x0005ECE4 File Offset: 0x0005CEE4
	private void Start()
	{
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x0005ECE8 File Offset: 0x0005CEE8
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			this.open = !this.open;
		}
		this.target.sizeDelta = new Vector2(this.target.sizeDelta.x + Time.deltaTime * this.speed, this.target.sizeDelta.y);
		if (this.open)
		{
			if (this.target.sizeDelta.x < this.width)
			{
				this.speed += Time.deltaTime * 300000f;
			}
			else if (this.target.sizeDelta.x > this.width + 200f)
			{
				this.speed -= Time.deltaTime * 30000f;
			}
		}
		else if (this.target.sizeDelta.x > 0f)
		{
			this.speed -= Time.deltaTime * 300000f;
		}
		else if (!this.open && !string.IsNullOrEmpty(this.infoHolder))
		{
			this.speed = 0f;
			this.open = true;
			this.setInfo();
		}
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x0005EE48 File Offset: 0x0005D048
	private void FixedUpdate()
	{
		this.speed *= 0.7f;
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x0005EE5C File Offset: 0x0005D05C
	public void changeText(string info)
	{
		if (info != this.infoHolder)
		{
			this.infoHolder = info;
			this.open = false;
		}
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x0005EE80 File Offset: 0x0005D080
	public void changeTextTemporary(string info, float amount = 2f)
	{
		if (info != this.infoHolder)
		{
			this.infoHolder = info;
			this.open = false;
			base.StartCoroutine(this.waitForText(amount));
		}
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x0005EEB0 File Offset: 0x0005D0B0
	public void changeTextTemporary(string info, bool Overload, float amount = 2f)
	{
		this.infoHolder = info;
		this.open = false;
		base.StartCoroutine(this.waitForText(amount));
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x0005EED0 File Offset: 0x0005D0D0
	private IEnumerator waitForText(float timeToWait)
	{
		yield return new WaitForSeconds(timeToWait);
		this.changeText(string.Empty);
		yield break;
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x0005EEFC File Offset: 0x0005D0FC
	private void setInfo()
	{
		this.infoText.text = this.infoHolder;
	}

	// Token: 0x04000B46 RID: 2886
	public RectTransform target;

	// Token: 0x04000B47 RID: 2887
	private bool open;

	// Token: 0x04000B48 RID: 2888
	private float speed;

	// Token: 0x04000B49 RID: 2889
	private string infoHolder;

	// Token: 0x04000B4A RID: 2890
	public Text infoText;

	// Token: 0x04000B4B RID: 2891
	public float width = 4000f;
}
