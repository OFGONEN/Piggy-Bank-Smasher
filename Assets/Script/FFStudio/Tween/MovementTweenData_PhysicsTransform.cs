/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class MovementTweenData_PhysicsTransform : TweenData
	{
#region Fields
	[ Title( "Movement Tween (Physics Transform)" ) ]
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public bool useDelta = true;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public bool speedBased = false;
#if UNITY_EDITOR
		[ InfoBox( "End Value is RELATIVE.", "useDelta" ) ]
		[ InfoBox( "End Value is ABSOLUTE.", "EndValueIsAbsolute" ) ]
#endif
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public Vector3 endValue;
#if UNITY_EDITOR
		[ InfoBox( "Duration is DURATION (seconds).", "DurationIsDuration" ) ]
		[ InfoBox( "Duration is VELOCITY (units/seconds).", "speedBased" ) ]
#endif
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public float duration;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public TweenSpace space;
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
			if( space == TweenSpace.Local )
				recycledTween.Recycle( transform.DOLocalMove( isReversed ? -endValue : endValue, duration ),
									   unityEvent_onCompleteEvent.Invoke );
			else
				recycledTween.Recycle( transform.DOMove( isReversed ? -endValue : endValue, duration ),
									   unityEvent_onCompleteEvent.Invoke );

			recycledTween.Tween
				.SetUpdate( UpdateType.Fixed ); // Info: This is the only differing line of code between this & the MovementTweenData.

			if( useDelta )
				recycledTween.Tween.SetRelative();

			if( speedBased )
				recycledTween.Tween.SetSpeedBased();
				
#if UNITY_EDITOR
			recycledTween.Tween.SetId( "_ff_movement_tween_physics_transform___" + description );
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