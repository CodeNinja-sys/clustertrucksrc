using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200025D RID: 605
public class UI_Slide : MonoBehaviour
{
	// Token: 0x06000EAF RID: 3759 RVA: 0x0005EA98 File Offset: 0x0005CC98
	private void Start()
	{
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x0005EA9C File Offset: 0x0005CC9C
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			this.open = !this.open;
		}
		this.target.sizeDelta = new Vector2(this.target.sizeDelta.x + Time.deltaTime * this.speed, this.target.sizeDelta.y);
		if (this.open)
		{
			if (this.target.sizeDelta.x < 1000f)
			{
				this.speed += Time.deltaTime * 300000f;
			}
			else if (this.target.sizeDelta.x > 1200f)
			{
				this.speed -= Time.deltaTime * 30000f;
			}
		}
		else if (this.target.sizeDelta.x > 0f)
		{
			this.speed -= Time.deltaTime * 300000f;
		}
		else if (!this.open)
		{
			this.speed = 0f;
			this.open = true;
			this.setInfo();
		}
	}

	// Token: 0x06000EB1 RID: 3761 RVA: 0x0005EBE4 File Offset: 0x0005CDE4
	private void FixedUpdate()
	{
		this.speed *= 0.6f;
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x0005EBF8 File Offset: 0x0005CDF8
	public void changeAbility(string[] info, string cost)
	{
		this.soundPlayer.Ability();
		this.infoHolder = new string[3];
		this.infoHolder[0] = info[0];
		this.infoHolder[1] = info[1];
		this.infoHolder[2] = cost;
		this.open = false;
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x0005EC38 File Offset: 0x0005CE38
	private void setInfo()
	{
		for (int i = 0; i < this.infoHolder.Length; i++)
		{
			this.infoHolder[i] = this.infoHolder[i].Replace("*", "\n");
		}
		this.tName.text = this.infoHolder[0].ToUpper();
		this.tDescription.text = this.infoHolder[1].ToUpper();
		this.tCost.text = this.infoHolder[2].ToUpper() + " POINTS";
	}

	// Token: 0x04000B3B RID: 2875
	public RectTransform target;

	// Token: 0x04000B3C RID: 2876
	private bool open = true;

	// Token: 0x04000B3D RID: 2877
	private float speed;

	// Token: 0x04000B3E RID: 2878
	private string[] infoHolder = new string[5];

	// Token: 0x04000B3F RID: 2879
	public Text tName;

	// Token: 0x04000B40 RID: 2880
	public Text tDescription;

	// Token: 0x04000B41 RID: 2881
	public Text tRank1;

	// Token: 0x04000B42 RID: 2882
	public Text tRank2;

	// Token: 0x04000B43 RID: 2883
	public Text tRank3;

	// Token: 0x04000B44 RID: 2884
	public Text tCost;

	// Token: 0x04000B45 RID: 2885
	public menuSound soundPlayer;
}
