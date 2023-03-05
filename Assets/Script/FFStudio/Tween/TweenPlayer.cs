/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class TweenPlayer : MonoBehaviour
	{
#region Fields
	[ Title( "Setup" ) ]
		[ InfoBox( "Transform of this GO will be used unless another one is provided.", "@transform_target == null" ) ]
		[ SerializeField, LabelText( "Target Transform" ) ] Transform transform_target;

    [ Title( "Start Options" ) ]
        public bool playOnStart = false;
	
	[ Title( "Tween Data" ) ]
	[ SerializeReference ]
        public TweenData tweenData;

		Transform transform_ToTween;
#endregion

#region Properties
        [ ShowInInspector, ReadOnly ]
		public bool IsPlaying => tweenData != null && tweenData.Tween != null && tweenData.Tween.IsPlaying();
		public Tween Tween    => tweenData.Tween;
#endregion

#region Unity API
        void Awake()
        {
			transform_ToTween = transform_target == null ? transform : transform_target;
			tweenData.Initialize( transform_ToTween );
		}
		
		void OnDisable()
		{
			Kill();
		}

        void Start()
        {
            if( playOnStart ) 
				Play();
		}
#endregion

#region API
#endregion

#region Implementation
        [ Button() ]
        public void Play()
        {
			tweenData.CreateTween();
		}

        [ Button(), EnableIf( "IsPlaying" ) ]
        public void Pause()
        {
			tweenData.Pause();
		}
		
        [ Button(), EnableIf( "IsPlaying" ) ]
        public void Stop()
        {
			tweenData.Stop();
		}
		
        [ Button(), EnableIf( "IsPlaying" ) ]
		public void Complete()
		{
			tweenData.Complete();
		}
		
		[ Button() ]
        public void Kill()
        {
			tweenData.Kill();
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}