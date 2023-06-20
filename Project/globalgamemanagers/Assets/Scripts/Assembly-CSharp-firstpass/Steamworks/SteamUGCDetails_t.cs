using System;

namespace Steamworks
{
	public struct SteamUGCDetails_t
	{
		public PublishedFileId_t m_nPublishedFileId;
		public EResult m_eResult;
		public EWorkshopFileType m_eFileType;
		public AppId_t m_nCreatorAppID;
		public AppId_t m_nConsumerAppID;
		public string m_rgchTitle;
		public string m_rgchDescription;
		public ulong m_ulSteamIDOwner;
		public uint m_rtimeCreated;
		public uint m_rtimeUpdated;
		public uint m_rtimeAddedToUserList;
		public ERemoteStoragePublishedFileVisibility m_eVisibility;
		public bool m_bBanned;
		public bool m_bAcceptedForUse;
		public bool m_bTagsTruncated;
		public string m_rgchTags;
		public UGCHandle_t m_hFile;
		public UGCHandle_t m_hPreviewFile;
		public string m_pchFileName;
		public int m_nFileSize;
		public int m_nPreviewFileSize;
		public string m_rgchURL;
		public uint m_unVotesUp;
		public uint m_unVotesDown;
		public float m_flScore;
		public uint m_unNumChildren;
	}
}
