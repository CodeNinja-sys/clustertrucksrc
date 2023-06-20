using System;
using UnityEngine;

public class Modifier : getModifierOptions
{
	[Serializable]
	public class fakeParameter
	{
		[SerializeField]
		public Modifier.Faketypes TYPE;
		public string[] Arguments;
	}

	public enum Faketypes
	{
		Slider = 0,
		Bool = 1,
		Dropdown = 2,
		Color = 3,
		Input = 4,
	}

	public bool roadPoint;
	[TextAreaAttribute]
	public string LEGEND;
	public fakeParameter[] initParameters;
}
