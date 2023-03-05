/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class PunchRotationTweenData : TweenData
	{
#region Fields
	[ Title( "Punch Rotation Tween" ) ]
        [ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), SuffixLabel( "units"   )           ] public Vector3 strength = Vector3.one;
        [ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), SuffixLabel( "seconds" ), Min( 0 ) ] public float duration = 1;
        [ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), SuffixLabel( "hz"      ), Min( 0 ) ] public int vibrato = 10;
        [ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), Range( 0, 1 ) ] public float elasticity = 1;
#endregion

#region API
        /* public override void Initialize( Transform transform )
        {
		} */
		
		public override Tween CreateTween( bool isReversed = false )
		{
			recycledTween.Recycle( transform.DOPunchRotation( strength, duration, vibrato, elasticity ),
								   unityEvent_onCompleteEvent.Invoke ) ;

#if UNITY_EDITOR
			recycledTween.Tween.SetId( "_ff_punch_rotation_tween___" + description );
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