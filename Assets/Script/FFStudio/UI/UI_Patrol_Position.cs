/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace FFStudio
{
	public class UI_Patrol_Position : MonoBehaviour
	{
#region Fields
		public RectTransform ui_Target;
		public UnityEvent ui_OnComplete;

		Vector3 ui_StartPosition;
		RectTransform ui_rectTransform;
        RecycledTween recycledTween = new RecycledTween();
#endregion

#region Properties
		public Tween Tween => recycledTween.Tween;
        public RectTransform UI_RectTransform => ui_rectTransform;
#endregion

#region Unity API
        void Awake()
        {
            ui_rectTransform = GetComponent< RectTransform >();
			ui_StartPosition = ui_rectTransform.position;
		}
#endregion

#region API
        public Tween DoGo_Target( float duration )
        {
			recycledTween.Recycle( 
				ui_rectTransform.DOMove( ui_Target.position, duration ),
			 	OnTweenComplete );

			return recycledTween.Tween;
        }

        public Tween DoGo_Start( float duration )
        {
			recycledTween.Recycle( 
				ui_rectTransform.DOMove( ui_StartPosition, duration ),
			 	OnTweenComplete );

			return recycledTween.Tween;
        }

		public void TeleportToTarget( Vector3 targetPosition )
		{
			ui_rectTransform.position = targetPosition;
		}

		public void TeleportToStart( Vector3 startPosition )
		{
			ui_rectTransform.position = startPosition;
		}
#endregion

#region Implementation
        void OnTweenComplete()
        {
			ui_OnComplete.Invoke();
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}