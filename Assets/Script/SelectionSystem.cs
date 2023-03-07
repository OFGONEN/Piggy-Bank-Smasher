/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "system_selection", menuName = "FF/Game/Selection System" ) ]
public class SelectionSystem : ScriptableObject
{
#region Fields

// Private
    Camera camera_main;

	Vector2Delegate onFingerUpdate = Extensions.EmptyMethod;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    [ Button() ]
    public void OnFingerUpdate( Vector2 fingerPosition )
    {
		onFingerUpdate( fingerPosition );
	}

    public void OnCameraReferenceUpdate( object cameraObject )
    {
		camera_main = ( cameraObject as Transform ).GetComponent< Camera >();
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}