/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;

namespace FFStudio
{
    public class RotationTweenData_PhysicsRigidbody : TweenData
    {
#region Fields
    [ Title( "Rotation Tween (Physics Rigidbody)" ) ]
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public bool useDelta = true;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public bool speedBased = false;
#if UNITY_EDITOR
		[ InfoBox( "End Value is RELATIVE.", "useDelta" ) ]
		[ InfoBox( "End Value is ABSOLUTE.", "EndValueIsAbsolute" ) ]
#endif
        [ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), SuffixLabel( "Degrees (°)" ) ] public float endValue;
#if UNITY_EDITOR
		[ InfoBox( "Duration is DURATION (seconds).", "DurationIsDuration" ) ]
		[ InfoBox( "Duration is ANGULAR VELOCITY (degrees/seconds).", "speedBased" ) ]
#endif
        [ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), Min( 0 ) ] public float duration;
        [ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), ValueDropdown( "VectorValues" ), LabelText( "Rotate Around" ) ]
            public Vector3 rotationAxisMaskVector = Vector3.right;

        IEnumerable VectorValues = new ValueDropdownList< Vector3 >()
        {
            { "X",   Vector3.right      },
            { "Y",   Vector3.up         },
            { "Z",   Vector3.forward    }
        };
#endregion

#region Properties
#if UNITY_EDITOR
		bool EndValueIsAbsolute => !useDelta;
		bool DurationIsDuration => !speedBased;
#endif
#endregion

#region Unity API
#endregion

#region API
        public override Tween CreateTween( bool isReversed = false )
        {
			var theRigidbody = transform.GetComponent< Rigidbody >();

            recycledTween.Recycle( theRigidbody.DORotate( rotationAxisMaskVector * endValue, duration, useDelta ? RotateMode.LocalAxisAdd : RotateMode.Fast ),
                                   unityEvent_onCompleteEvent.Invoke );

			// Info: Don't need to set SetRelative() as RotateMode.XXXAxisAdd automatically means relative end value.

			if( useDelta )
				recycledTween.Tween.SetRelative();

			if( speedBased )
				recycledTween.Tween.SetSpeedBased();

#if UNITY_EDITOR
			recycledTween.Tween.SetId( "_ff_rotation_tween_physics_rigidbody___" + description );
#endif

			return base.CreateTween();
		}
#endregion

#region Implementation
#endregion

#region EditorOnly
#if UNITY_EDITOR
#endif
#endregion
    }
}
