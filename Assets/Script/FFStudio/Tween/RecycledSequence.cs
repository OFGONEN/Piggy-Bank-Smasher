/* Created by and for usage of FF Studios (2021). */

using DG.Tweening;

namespace FFStudio
{
	public class RecycledSequence
	{
#region Fields
		UnityMessage onComplete;
		Sequence sequence;
#endregion

#region Properties
		public Sequence Sequence => sequence;
		
		public bool IsPlaying => sequence != null && sequence.IsPlaying();
#endregion

#region API
		public Sequence Recycle( UnityMessage onComplete )
		{
			sequence = sequence.KillProper();

			this.onComplete = onComplete;

			sequence = DOTween.Sequence();
			sequence.OnComplete( OnComplete_Safe );

#if UNITY_EDITOR
			sequence.SetId( "_ff_RecycledSequence" );
#endif

			return sequence;
		}

		public Sequence Recycle()
		{
			sequence = sequence.KillProper();

			sequence   = DOTween.Sequence();
			onComplete = null;
			sequence.OnComplete( OnComplete_Safe );

#if UNITY_EDITOR
			sequence.SetId( "_ff_RecycledSequence" );
#endif
			
			return sequence;
		}

		public void Kill()
		{
			sequence = sequence.KillProper();
		}

		public void RewindAndKill()
		{
			sequence?.Rewind();
			sequence = sequence.KillProper();
		}

		public void CompleteAndKill()
		{
			sequence?.Complete();
			sequence = sequence.KillProper();
		}
#endregion

#region Implementation
		void OnComplete_Safe()
		{
			sequence = null;
			onComplete?.Invoke();
		}
#endregion
	}
}