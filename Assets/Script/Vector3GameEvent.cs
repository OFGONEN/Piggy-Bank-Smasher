/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "event_", menuName = "FF/Event/Vector3GameEvent" ) ]
public class Vector3GameEvent : GameEvent
{
#region Fields
    public Vector3 event_value;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Raise( Vector3 value )
    {
		event_value = value;
		Raise();
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}