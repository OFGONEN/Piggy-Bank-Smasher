/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using FFStudio;
using DG.Tweening;

namespace FFStudio
{
	public class Cooldown
	{
#region Fields
		RecycledTween recycledTween_cooldown = new RecycledTween();
		TweenCallback onComplete;

#if UNITY_EDITOR
		bool popupTextOnComplete = false;
		string description;
#endif
#endregion

#region Properties
		public bool IsPlaying => recycledTween_cooldown.IsPlaying;
		public float ElapsedPercentage => recycledTween_cooldown.Tween.ElapsedPercentage();
#endregion

#region API
		public void Start( float duration, TweenCallback onCompleteDelegate = null, bool loop = false )
		{
			onComplete = onCompleteDelegate;
			recycledTween_cooldown.Recycle( DOVirtual.DelayedCall( duration, OnComplete ).SetLoops( loop ? -1 : 1 ) );
		}

		public void Start( float duration, TweenCallback onUpdate, TweenCallback onCompleteDelegate, bool loop = false )
		{
			onComplete = onCompleteDelegate;
			recycledTween_cooldown.Recycle( DOVirtual.DelayedCall( duration, OnComplete ).SetLoops( loop ? -1 : 1 ) );
			recycledTween_cooldown.Tween.OnUpdate( onUpdate );
		}

#if UNITY_EDITOR
		//! Do not use this on build code or wrap it inside of #if Unity_Editor
		public void Start( float duration, string description, TweenCallback onCompleteDelegate = null, bool loop = false, bool popupTextOnComplete = false )
		{
			this.description = description;
			this.popupTextOnComplete = popupTextOnComplete;

			FFStudio.FFLogger.Log( "Cooldown duration for " + description + " started." );
			FFLogger.PopUpText( Vector3.zero, "Cooldown duration for " + description + " started." );

			Start( duration,onCompleteDelegate, loop );
		}
#endif

		public void Kill()
		{
			recycledTween_cooldown.Kill();
		}
#endregion

#region Implementation
		void OnComplete()
		{
			onComplete.Invoke();

#if UNITY_EDITOR
			if( popupTextOnComplete )
			{
				FFStudio.FFLogger.Log( "Cooldown duration for " + description + " is over." );
				FFLogger.PopUpText( Vector3.zero, "Cooldown duration for " + description + " is over." );
			}
#endif
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}