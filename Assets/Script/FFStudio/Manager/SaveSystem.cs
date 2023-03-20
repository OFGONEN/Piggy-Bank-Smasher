/* Created by and for usage of FF Studios (2021). */

using System.IO;
using UnityEngine;

namespace FFStudio
{
	[ CreateAssetMenu( fileName = "system_save", menuName = "FF/System/Save" ) ]
	public class SaveSystem : ScriptableObject
	{
#region Fields
		[ SerializeField ] SharedStringNotifier save_string;

		static SaveSystem instance;
#endregion

#region Properties
		public static SaveSystem Instance => instance;
#endregion

#region Unity API
#endregion

#region API
		public void Initialize()
		{
			instance = this;

			// Create folder.
			if( Directory.Exists( Extensions.SAVE_PATH ) == false )
				Directory.CreateDirectory( Extensions.SAVE_PATH );
		}

		public void SaveOverride_WithSharedString()
		{
			File.WriteAllText( Extensions.SAVE_PATH + "save.txt", save_string.sharedValue );
			FFStudio.FFLogger.Log( "Savemanager: Saved Succesfully. Data saved: " + save_string.sharedValue );
		}

		public void SaveOverride( string save )
		{
			File.WriteAllText( Extensions.SAVE_PATH + "save.txt", save );
			FFStudio.FFLogger.Log( "Savemanager: Saved Succesfully. Data saved: " + save );
		}

		public string LoadSave()
		{
			if( File.Exists( Extensions.SAVE_PATH + "save.txt" ) == false )
				return null;

			var json = File.ReadAllText( Extensions.SAVE_PATH + "save.txt" );
			FFStudio.FFLogger.Log( "SaveSystem: Loaded Succesfully. Data read: " + json );

			return json;
		}

		public void LoadSave_ToSharedString()
		{
			if( File.Exists( Extensions.SAVE_PATH + "save.txt" ) == false )
			{
				save_string.SharedValue = null;
				return;
			}

			save_string.SharedValue = File.ReadAllText( Extensions.SAVE_PATH + "save.txt" );
			FFStudio.FFLogger.Log( "SaveSystem: Loaded Succesfully. Data read: " + save_string.sharedValue );
		}

		public void DeleteSave()
		{
			if( File.Exists( Extensions.SAVE_PATH + "save.txt" ) )
			{
				FFStudio.FFLogger.Log( "SaveSystem: Found save file. Deleting it." );
				File.Delete( Extensions.SAVE_PATH + "save.txt" );
			}
		}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}