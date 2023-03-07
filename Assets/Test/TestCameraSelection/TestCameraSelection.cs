/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using FFStudio;
using UnityEngine;

public class TestCameraSelection : MonoBehaviour
{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnSelect( Vector2 fingerPosition )
    {
		var camera = Camera.main;

		Vector3 screenPositionNear = new Vector3( fingerPosition.x, fingerPosition.y, camera.nearClipPlane );
		Vector3 screenPositionFar  = new Vector3( fingerPosition.x, fingerPosition.y, camera.farClipPlane );

		var worldPointNear = camera.ScreenToWorldPoint( screenPositionNear );
		var worldPointFar  = camera.ScreenToWorldPoint( screenPositionFar );

		var direction = ( worldPointFar - worldPointNear ).normalized;

		Debug.DrawRay( worldPointNear, worldPointFar, Color.red, 1 );

		int layerMask = 1 << 0;

		RaycastHit hit;
		var isHit = Physics.Raycast( worldPointNear, direction, out hit, 100, layerMask );

        if( isHit )
            FFLogger.Log( "Hit: " + hit.collider, hit.collider );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
