using System;
using UnityEngine;

// Token: 0x02000305 RID: 773
public abstract class EffectBase : MonoBehaviour
{
	// Token: 0x06001229 RID: 4649 RVA: 0x00073D0C File Offset: 0x00071F0C
	public void EnsureMaterial(ref Material material, Shader shader)
	{
		if (shader != null)
		{
			if (material == null || material.shader != shader)
			{
				material = new Material(shader);
			}
			if (material != null)
			{
				material.hideFlags = HideFlags.DontSave;
			}
		}
		else
		{
			Debug.LogWarning("missing shader", this);
		}
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x00073D74 File Offset: 0x00071F74
	public void EnsureKeyword(Material material, string name, bool enabled)
	{
		if (enabled != material.IsKeywordEnabled(name))
		{
			if (enabled)
			{
				material.EnableKeyword(name);
			}
			else
			{
				material.DisableKeyword(name);
			}
		}
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x00073DA8 File Offset: 0x00071FA8
	public void EnsureRenderTarget(ref RenderTexture rt, int width, int height, RenderTextureFormat format, FilterMode filterMode, int depthBits = 0)
	{
		if (rt != null && (rt.width != width || rt.height != height || rt.format != format || rt.filterMode != filterMode))
		{
			RenderTexture.ReleaseTemporary(rt);
			rt = null;
		}
		if (rt == null)
		{
			rt = RenderTexture.GetTemporary(width, height, depthBits, format);
			rt.filterMode = filterMode;
			rt.wrapMode = TextureWrapMode.Clamp;
		}
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x00073E30 File Offset: 0x00072030
	public void FullScreenQuad()
	{
		GL.PushMatrix();
		GL.LoadOrtho();
		GL.Begin(7);
		GL.MultiTexCoord2(0, 0f, 0f);
		GL.Vertex3(0f, 0f, 0f);
		GL.MultiTexCoord2(0, 1f, 0f);
		GL.Vertex3(1f, 0f, 0f);
		GL.MultiTexCoord2(0, 1f, 1f);
		GL.Vertex3(1f, 1f, 0f);
		GL.MultiTexCoord2(0, 0f, 1f);
		GL.Vertex3(0f, 1f, 0f);
		GL.End();
		GL.PopMatrix();
	}
}
