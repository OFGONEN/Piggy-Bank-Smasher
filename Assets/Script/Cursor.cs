/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class Cursor : MonoBehaviour
{
#region Fields
    [ SerializeField ] Rigidbody _rigidbody;

    Vector3 position_target;
    UnityMessage onFixedUpdate = Extensions.EmptyMethod;
#endregion

#region Properties
#endregion

#region Unity API
    private void FixedUpdate()
    {
		onFixedUpdate();
	}
#endregion

#region API
    public void OnFingerDown()
    {
		onFixedUpdate = Movement;
	}

    public void OnFingerUp()
    {
		onFixedUpdate = Extensions.EmptyMethod;
	}

    public void OnWorldPointSelect( Vector3 point )
    {
		position_target = point;
	}
#endregion

#region Implementation
    void Movement()
    {
		var position       = _rigidbody.position;
		var targetPosition = Vector3.Lerp( position, position_target, Time.deltaTime * GameSettings.Instance.cursor_movement_speed );

		_rigidbody.MovePosition( targetPosition );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}