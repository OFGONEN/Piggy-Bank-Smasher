/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class PunchScaleTweenData : TweenData
	{
#region Fields
	[ Title( "Punch Scale Tween" ) ]
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), SuffixLabel( "units"   )           ] public Vector3 strength = Vector3.one;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), SuffixLabel( "seconds" ), Min( 0 ) ] public float duration = 1;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), SuffixLabel( "hz"      ), Min( 0 ) ] public int vibrato = 10;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), Range( 0, 1 ) ] public float elasticity = 1;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public bool revertToOriginalScaleOnPlay = true;
		
		Vector3 scale_local_original;
#endregion

#region API
        public override void Initialize( Transform transform )
        {
			base.Initialize( transform );

			scale_local_original = transform.localScale;
		}
		
		public override Tween CreateTween( bool isReversed = false )
		{
			if( revertToOriginalScaleOnPlay )
				transform.localScale = scale_local_original;

			recycledTween.Recycle( transform.DOPunchScale( strength, duration, vibrato, elasticity ),
								   unityEvent_onCompleteEvent.Invoke ) ;

#if UNITY_EDITOR
			recycledTween.Tween.SetId( "_ff_punch_scale_tween___" + description );
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