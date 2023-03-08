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
	[ SerializeField ] Vector3GameEvent event_selection_point;

// Private
    Camera camera_main;

	Vector2Delegate onFingerUpdate = Extensions.EmptyMethod;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnFingerUpdate( Vector2 fingerPosition )
    {
		onFingerUpdate( fingerPosition );
	}

    public void OnCameraReferenceUpdate( object cameraObject )
    {
		camera_main = ( cameraObject as Transform ).GetComponent< Camera >();
	}

    public void OnLevelStart()
    {
		onFingerUpdate = SelectPointOnWorld;
	}

    public void OnLevelFinished()
    {
		onFingerUpdate = Extensions.EmptyMethod;
	}
#endregion

#region Implementation
    void SelectPointOnWorld( Vector2 fingerPosition )
    {
		var worldPointNear = camera_main.ScreenToWorldPoint( fingerPosition.ConvertToVector3( camera_main.nearClipPlane ) );
		var worldPointFar  = camera_main.ScreenToWorldPoint( fingerPosition.ConvertToVector3( camera_main.farClipPlane ) );

		var direction = ( worldPointFar - worldPointNear ).normalized;
		var layerMask = 1 << GameSettings.Instance.selection_layer;

		//Info: Since the environment surface always cover the whole screen this raycast will hit it %100
		RaycastHit hitInfo;
		Physics.Raycast( worldPointNear, direction, out hitInfo, GameSettings.Instance.selection_distance, layerMask );

		event_selection_point.Raise( hitInfo.point );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}