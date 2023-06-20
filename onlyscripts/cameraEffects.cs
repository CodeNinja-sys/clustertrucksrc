using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

// Token: 0x0200026C RID: 620
public class cameraEffects : MonoBehaviour
{
	// Token: 0x06000EF1 RID: 3825 RVA: 0x00060C28 File Offset: 0x0005EE28
	private void Start()
	{
		this.startBloom = this.myBloom.bloomIntensity;
		if (!options.ssao)
		{
			this.ssao.enabled = false;
		}
	}

	// Token: 0x06000EF2 RID: 3826 RVA: 0x00060C54 File Offset: 0x0005EE54
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
		}
		if (info.playerControlledTime < 1f)
		{
			this.blueOn = true;
		}
		else
		{
			this.blueOn = false;
		}
		this.Shake();
		this.checkOverLays();
		this.CheckBloom();
		if (options.bloom)
		{
			if (this.blur.blurSpread <= 0f)
			{
				this.blur.enabled = false;
			}
			else
			{
				this.blur.enabled = true;
			}
			if (info.playing)
			{
				this.blur.blurSpread = this.extraBloom * 6f;
			}
			else
			{
				this.blur.blurSpread = Mathf.Lerp(this.blur.blurSpread, 2.5f, Time.deltaTime * 10f);
			}
		}
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x00060D34 File Offset: 0x0005EF34
	private void checkOverLays()
	{
		if (this.blueOn)
		{
			if (this.blueOverlay.intensity < 0.6f)
			{
				this.blueOverlay.intensity += Time.unscaledDeltaTime * 2f;
			}
			else
			{
				this.blueOverlay.intensity = 0.6f;
			}
			this.blueOverlay.enabled = true;
		}
		else if (this.blueOverlay.intensity > 0f)
		{
			this.blueOverlay.intensity -= Time.unscaledDeltaTime * 2f;
		}
		else
		{
			this.blueOverlay.intensity = 0f;
			this.blueOverlay.enabled = false;
		}
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x00060DF8 File Offset: 0x0005EFF8
	private void CheckBloom()
	{
		if (options.bloom)
		{
			if (this.extraBloom > 0f)
			{
				this.extraBloom -= Time.deltaTime * this.decrease / 10f;
			}
			this.myBloom.bloomIntensity = Mathf.Lerp(this.myBloom.bloomIntensity, Mathf.Clamp(this.extraBloom + this.startBloom, 0f, 1f), Time.deltaTime * 8f);
			this.orangeOverlay.intensity = Mathf.Lerp(this.orangeOverlay.intensity, this.extraBloom * 1f, Time.deltaTime * 4f);
		}
		else
		{
			this.myBloom.enabled = false;
			this.orangeOverlay.enabled = false;
		}
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x00060ED0 File Offset: 0x0005F0D0
	private void Shake()
	{
		if (this.shake > 0f)
		{
			if (this.shakeCd > 0.01f)
			{
				this.rot = new Vector3(UnityEngine.Random.Range(-this.shake, this.shake), UnityEngine.Random.Range(-this.shake, this.shake), UnityEngine.Random.Range(-this.shake, this.shake));
				this.shakeCd = 0f;
			}
			Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation, Quaternion.Euler(this.rot), Time.deltaTime * 20f);
			this.shakeCd += Time.deltaTime;
			this.shake -= Time.deltaTime * this.decrease;
		}
		else
		{
			this.shake = 0f;
		}
		if (this.uiShake > 0f)
		{
			if (this.uiShakeCd > 0.01f)
			{
				this.pos = new Vector3(UnityEngine.Random.Range(-this.uiShake, this.uiShake), UnityEngine.Random.Range(-this.uiShake, this.uiShake), UnityEngine.Random.Range(-this.uiShake, this.uiShake));
				this.uiShakeCd = 0f;
			}
			this.uiCam.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, Vector3.zero + this.pos, Time.deltaTime * 20f);
			this.uiShakeCd += Time.unscaledDeltaTime;
			this.uiShake -= Time.unscaledDeltaTime * this.decrease * 8f;
		}
		else
		{
			this.uiShake = 0f;
		}
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x000610A4 File Offset: 0x0005F2A4
	public void SetShake(float f, Vector3 pos)
	{
		float num = Mathf.Clamp(2f - Vector3.Distance(base.transform.position, pos) / 50f, 0.15f, 1.2f);
		if (pos == Vector3.zero)
		{
			num = 1f;
		}
		if (f * 5f * num > this.shake)
		{
			this.shake = f * this.amount * num * 5f;
		}
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x00061120 File Offset: 0x0005F320
	public void SetBloom(float b, Vector3 pos)
	{
		if (b > this.extraBloom)
		{
			this.extraBloom = b * this.amount;
		}
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x0006113C File Offset: 0x0005F33C
	public void SetUIShake(float f)
	{
		f *= 8f;
		if (f > this.uiShake)
		{
			this.uiShake = f;
		}
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x0006115C File Offset: 0x0005F35C
	public void SetAll(float f, Vector3 shakePos)
	{
		float num = Mathf.Clamp(1f - Vector3.Angle(base.transform.forward, shakePos - base.transform.position) / 60f, 0.15f, 1f);
		float num2 = Mathf.Clamp(2f - Vector3.Distance(base.transform.position, shakePos) / 40f, 0.15f, 1.5f);
		num *= num2 * 0.35f;
		if (shakePos == Vector3.zero)
		{
			num = 1f;
			num2 = 1f;
		}
		if (num > num2)
		{
			num = num2;
		}
		if (f * 5f * num2 > this.shake)
		{
			this.shake = f * this.amount * num2 * 5f;
		}
		if (f * num > this.extraBloom)
		{
			this.extraBloom = f * this.amount * num;
		}
	}

	// Token: 0x04000B97 RID: 2967
	public float shake;

	// Token: 0x04000B98 RID: 2968
	public float amount = 1f;

	// Token: 0x04000B99 RID: 2969
	public float decrease = 3f;

	// Token: 0x04000B9A RID: 2970
	public SENaturalBloomAndDirtyLens myBloom;

	// Token: 0x04000B9B RID: 2971
	public SESSAO ssao;

	// Token: 0x04000B9C RID: 2972
	public ScreenOverlay blueOverlay;

	// Token: 0x04000B9D RID: 2973
	public ScreenOverlay orangeOverlay;

	// Token: 0x04000B9E RID: 2974
	public LerpableBlur blur;

	// Token: 0x04000B9F RID: 2975
	private bool blueOn;

	// Token: 0x04000BA0 RID: 2976
	private float startBloom;

	// Token: 0x04000BA1 RID: 2977
	private float extraBloom;

	// Token: 0x04000BA2 RID: 2978
	private float shakeCd;

	// Token: 0x04000BA3 RID: 2979
	private float uiShakeCd;

	// Token: 0x04000BA4 RID: 2980
	private float uiShake;

	// Token: 0x04000BA5 RID: 2981
	public Transform uiCam;

	// Token: 0x04000BA6 RID: 2982
	private Vector3 rot = Vector3.zero;

	// Token: 0x04000BA7 RID: 2983
	private Vector3 pos = Vector3.zero;
}
