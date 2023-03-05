/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace FFStudio
{
	[ System.Serializable ]
	public abstract class TweenData
	{		
#region Fields (Inspector Interface)
		[ BoxGroup( "Other", false ) ] public string description;
		
	[ Title( "Sequencing" ) ]
		[ BoxGroup( "Other", false ), LabelText( "Mode" ) ] public SequenceElementType sequenceElementType;
#if UNITY_EDITOR
		[ ShowIf( "IsOfInsertType" ) ]
#endif
		[ BoxGroup( "Other", false ), LabelText( "Insert At" ), SuffixLabel( "seconds" ) ] public float insertion_time;
	
	[ Title( "Events" ), SerializeReference ]
		[ BoxGroup( "Other", false ) ] public TweenEventData[] tweenEventDatas;
		
#if UNITY_EDITOR
		[ HideIf( "HideBaseClassLoopCheckBox" ) ]
#endif
		[ BoxGroup( "Tween", false ) ] public Ease easing = Ease.Linear;
		[ BoxGroup( "Tween", false ) ] public bool loop;
		[ BoxGroup( "Tween", false ), ShowIf( "loop" ), LabelText( "Loop Type" ) ] public LoopType loop_type = LoopType.Restart;
		[ BoxGroup( "Tween", false ), ShowIf( "loop" ), LabelText( "Infinite Loop" ) ] public bool loop_isInfinite = true;
		[ BoxGroup( "Tween", false ), HideIf( "loop_isInfinite" ), LabelText( "Loop Count" ), Min( 1 ) ] public int loop_count = 1;
		[ BoxGroup( "Tween", false ), LabelText( "On Tween Completion" ) ] public UnityEvent unityEvent_onCompleteEvent;

		protected Transform transform;
		
		protected RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
		public Tween Tween => recycledTween.Tween;
#endregion

#region API
		public virtual void Initialize( Transform transform )
		{
			this.transform = transform;
		}
		
		public virtual Tween CreateTween( bool isReversed = false )
		{
			// Info: This base method does not actually create the Tween. So derived classes should handle that part and call this one.
			
			InitializeTweenEventDatasAndSetOnUpdate();
			SetEasingAndLoops();

			return Tween;
		}
		
        public void Pause()
        {
			Tween.Pause();
		}
		
        public void Stop()
        {
			Tween.Rewind();
		}
		
		public void Complete()
		{
			Tween?.Complete();
		}
		
		[ Button() ]
        public void Kill()
        {
			recycledTween.Kill();
		}
#endregion

#region Implementation
		void OnUpdate()
		{
			for( int i = 0; i < tweenEventDatas.Length; i++ )
			{
				TweenEventData tweenEventData = tweenEventDatas[ i ];
				
				if( tweenEventData.isConsumed == false )
					tweenEventData.InvokeEventIfThresholdIsPassed( Tween, easing );
			}
		}

		protected void InitializeTweenEventDatasAndSetOnUpdate()
		{
			if( tweenEventDatas != null && tweenEventDatas.Length > 0 )
			{
				for( int i = 0; i < tweenEventDatas.Length; i++ )
					tweenEventDatas[ i ].isConsumed = false;
					
				Tween.OnUpdate( OnUpdate );
			}
		}
		
		protected void SetEasingAndLoops()
		{
			recycledTween.Tween.SetEase( easing )
				 .SetLoops( loop
								? loop_isInfinite
								   	? -1
									: loop_count
								: 0,
							loop_type );
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
		public virtual bool HideBaseClassLoopCheckBox() => false;
		
		bool IsOfInsertType => sequenceElementType == SequenceElementType.Insert;
#endif
#endregion
	}
}