/* Created by and for usage of FF Studios (2021). */

using System.IO;
using UnityEditor;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace FFEditor
{
	public static class FFShortcutUtility
	{
		static private TransformData currentTransformData;
		static private string path_playerPrefsTracker = "Assets/Editor/tracker_playerPrefs.asset";

		[ MenuItem( "FFShortcut/TakeScreenShot #F12" ) ]
		public static void TakeScreenShot()
		{
			int counter = 0;
			var path = Path.Combine( Application.dataPath, "../", "ScreenShot_" + counter + ".png" );

			while( File.Exists( path ) ) // If file is not exits new screen shot will be a new file
			{
				counter++;
				path = Path.Combine( Application.dataPath, "../", "ScreenShot_" + counter + ".png" ); // ScreenShot_1.png
			}

			ScreenCapture.CaptureScreenshot( "ScreenShot_" + counter + ".png" );
			AssetDatabase.SaveAssets();

			Debug.Log( "ScreenShot Taken: " + "ScreenShot_" + counter + ".png" );
		}
		
		[ MenuItem( "FFShortcut/Select PlayerPrefsTracker _F7" ) ]
		static private void SelectPlayerPrefsTracker()
		{
			var tracker = AssetDatabase.LoadAssetAtPath( path_playerPrefsTracker, typeof( ScriptableObject ) );
			( tracker as PlayerPrefsTracker ).Refresh();
			Selection.SetActiveObjectWithContext( tracker, tracker );
		}

		[ MenuItem( "FFShortcut/Delete Save File _F8" ) ]
		static void DeleteSaveFile()
		{
			if( File.Exists( Extensions.SAVE_PATH + "save.txt" ) )
			{
				FFStudio.FFLogger.Log( "SaveSystem: Found save file. Deleting it." );
				File.Delete( Extensions.SAVE_PATH + "save.txt" );
			}

			if( File.Exists( Extensions.SAVE_PATH + "save.txt" ) )
				FFStudio.FFLogger.LogError( "SaveSystem: Failed to delete save file." );
			else
				FFStudio.FFLogger.Log( "SaveSystem: Successfully deleted save file." );
		}

		[ MenuItem( "FFShortcut/Delete PlayerPrefs _F9" ) ]
		static private void ResetPlayerPrefs()
		{
			PlayerPrefsUtility.Instance.DeleteAll();
			Debug.Log( "PlayerPrefs Deleted" );
		}

		[ MenuItem( "FFShortcut/Previous Level _F10" ) ]
		static private void PreviousLevel()
		{
			var currentLevel = PlayerPrefs.GetInt( "Level" );

			currentLevel = Mathf.Max( currentLevel - 1, 1 );

			PlayerPrefs.SetInt( "Level", currentLevel );
			PlayerPrefs.SetInt( "Consecutive Level", currentLevel );

			Debug.Log( "Level Set:" + currentLevel );
		}

		[ MenuItem( "FFShortcut/Next Level _F11" ) ]
		static private void NextLevel()
		{
			var nextLevel = PlayerPrefs.GetInt( "Level" ) + 1;

			PlayerPrefs.SetInt( "Level", nextLevel );
			PlayerPrefs.SetInt( "Consecutive Level", nextLevel );

			Debug.Log( "Level Set:" + nextLevel );

		}

		[ MenuItem( "FFShortcut/Save All Assets _F12" ) ]
		static private void SaveAllAssets()
		{
			AssetDatabase.SaveAssets();
			Debug.Log( "AssetDatabase Saved" );
		}

		[ MenuItem( "FFShortcut/Select Game Settings &1" ) ]
		static private void SelectGameSettings()
		{
			var gameSettings = Resources.Load( "game_settings" );

			Selection.SetActiveObjectWithContext( gameSettings, gameSettings );
		}

		[ MenuItem( "FFShortcut/Select Level Data &2" ) ]
		static private void SelectLevelData()
		{
			var levelData = Resources.Load( "level_data_1" );

			Selection.SetActiveObjectWithContext( levelData, levelData );
		}

		[ MenuItem( "FFShortcut/Select App Scene &3" ) ]
		static private void SelectAppScene()
		{
			var appScene = AssetDatabase.LoadAssetAtPath( "Assets/Scenes/app.unity", typeof( SceneAsset ) );

			Selection.SetActiveObjectWithContext( appScene, appScene );
		}

		[ MenuItem( "FFShortcut/Open App Scene &#3" ) ]
		static private void OpenAppScene()
		{
			EditorSceneManager.OpenScene( "Assets/Scenes/app.unity", OpenSceneMode.Single );
		}

		[ MenuItem( "FFShortcut/Open Game Template Scene &#4" ) ]
		static private void OpenGameTemplateScene()
		{
			EditorSceneManager.OpenScene( "Assets/Scenes/game_template.unity", OpenSceneMode.Single );
		}

		[ MenuItem( "FFShortcut/Select Play Mode Settings &4" ) ]
		static private void SelectPlayModeSettings()
		{
			var playModeSettings = AssetDatabase.LoadAssetAtPath( "Assets/Editor/PlayModeUtilitySettings.asset", typeof( ScriptableObject ) );

			Selection.SetActiveObjectWithContext( playModeSettings, playModeSettings );
		}

		[ MenuItem( "FFShortcut/Copy Global Transform &c" ) ]
		static private void CopyTransform()
		{
			currentTransformData = Selection.activeGameObject.transform.GetTransformData();
		}

		[ MenuItem( "FFShortcut/Paste Global Transform &v" ) ]
		static private void PasteTransform()
		{
			var gameObject = Selection.activeGameObject.transform;
			EditorUtility.SetDirty( gameObject );
			gameObject.SetTransformData( currentTransformData );
		}

		[ MenuItem( "FFShortcut/Set Current Level Data #p" ) ]
		static private void SetCurrentLevelData()
		{
			var levelName = Selection.activeObject.name;

			for( var i = 0; i < GameSettings.Instance.game_level_data_array.Length; i++ )
			{
				if( GameSettings.Instance.game_level_data_array[ i ].name == levelName )
				{
					FFLogger.Log( levelName + " Index: " + ( i + 1 ) );
					PlayerPrefs.SetInt( "Level", i + 1 );
					PlayerPrefs.SetInt( "Consecutive Level", ( i + 1 ) );
					return;
				}
			}

			FFLogger.Log( "This level data is not in the game_settings" );
		}

		[ MenuItem( "FFShortcut/Log Level Data #l" ) ]
		static private void LogLevelData()
		{
			var sceneName = Selection.activeObject.name;

			for( var i = 0; i < GameSettings.Instance.game_level_data_array.Length; i++ )
			{
				if( Path.GetFileNameWithoutExtension( SceneUtility.GetScenePathByBuildIndex( GameSettings.Instance.game_level_data_array[ i ].scene_index ) ) == sceneName )
				{
					FFLogger.Log( "Scene: " + sceneName + " Level Data: " + GameSettings.Instance.game_level_data_array[ i ].name, GameSettings.Instance.game_level_data_array[ i ] );

					Selection.SetActiveObjectWithContext( GameSettings.Instance.game_level_data_array[ i ], GameSettings.Instance.game_level_data_array[ i ] );
					return;
				}
			}

			FFLogger.Log( "This scene does not have level data in game_settings" );
		}

		[ MenuItem( "FFShortcut/Kill All Tweens %#t" ) ]
		private static void KillAllTweens()
		{
			DOTween.KillAll();
			FFLogger.Log( "[FF] DOTween: Kill All" );
		}

		[ MenuItem( "FFShortcut/Clear Console %#x" ) ]
		private static void ClearLog()
		{
			var assembly = Assembly.GetAssembly( typeof( UnityEditor.Editor ) );
			var type = assembly.GetType( "UnityEditor.LogEntries" );
			var method = type.GetMethod( "Clear" );
			method.Invoke( new object(), null );
		}
	}
}