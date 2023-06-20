using System;
using UnityEngine;

namespace AmplifyColor
{
	[Serializable]
	public class VersionInfo
	{
		private VersionInfo()
		{
		}

		[SerializeField]
		private int m_major;
		[SerializeField]
		private int m_minor;
		[SerializeField]
		private int m_release;
	}
}
