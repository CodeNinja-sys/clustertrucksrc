using System.Collections.Generic;

namespace InControl
{
	public class InControlManager : SingletonMonoBehavior<InControlManager>
	{
		public bool logDebugInfo;
		public bool invertYAxis;
		public bool useFixedUpdate;
		public bool dontDestroyOnLoad;
		public bool enableXInput;
		public int xInputUpdateRate;
		public int xInputBufferSize;
		public bool enableICade;
		public List<string> customProfiles;
	}
}
