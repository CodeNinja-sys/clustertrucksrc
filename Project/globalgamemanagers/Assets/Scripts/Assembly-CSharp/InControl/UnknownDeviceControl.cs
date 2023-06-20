using System;

namespace InControl
{
	public struct UnknownDeviceControl
	{
		public UnknownDeviceControl(InputControlType control, InputRangeType sourceRange) : this()
		{
		}

		public InputControlType Control;
		public InputRangeType SourceRange;
	}
}
