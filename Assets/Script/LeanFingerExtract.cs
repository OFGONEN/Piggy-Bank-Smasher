/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;

public class LeanFingerExtract : MonoBehaviour
{
#region Fields
    [ SerializeField ] UnityEvent< Vector2 > onScreenPosition;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void ExtractLeanFinger( LeanFinger finger )
    {
		onScreenPosition.Invoke( finger.ScreenPosition );
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
