using System;
using System.IO;
using UnityEngine;

// Token: 0x02000226 RID: 550
public class getFile : MonoBehaviour
{
	// Token: 0x06000CDF RID: 3295 RVA: 0x0004F340 File Offset: 0x0004D540
	private void Start()
	{
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x0004F344 File Offset: 0x0004D544
	private void Update()
	{
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0004F348 File Offset: 0x0004D548
	public FileInfo getFileInfo()
	{
		return this.m_File;
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x0004F350 File Offset: 0x0004D550
	public void setFile(FileInfo file)
	{
		this.m_File = file;
	}

	// Token: 0x04000968 RID: 2408
	private FileInfo m_File;
}
