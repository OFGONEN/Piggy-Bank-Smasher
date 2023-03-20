/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace FFStudio
{
	public class UI_Patrol_Scale : MonoBehaviour
	{
#region Fields
		public UnityEvent ui_OnComplete;

		Vector3 ui_StartScale;
		RectTransform ui_rectTransform;
		RecycledTween recycledTween = new RecycledTween();

		SafeEvent onComplete = new SafeEvent();
#endregion

#region Properties
		public Tween Tween => recycledTween.Tween;
        public RectTransform UI_RectTransform => ui_rectTransform;
#endregion

#region Unity API
        void Awake()
        {
			ui_rectTransform = GetComponent<RectTransform>();
			ui_StartScale = ui_rectTransform.localScale;
		}
#endregion

#region API
        public Tween DoScale_Target( Vector3 targetScale, float duration )
        {
			recycledTween.Recycle( 
				ui_rectTransform.DOScale( targetScale , duration ),
			 	OnTweenComplete );

			return recycledTween.Tween;
		}

        public Tween DoScale_Start( float duration )
        {
			recycledTween.Recycle( 
				ui_rectTransform.DOScale( ui_StartScale, duration ),
			 	OnTweenComplete );

			return recycledTween.Tween;
        }

		public void TeleportToTarget( Vector3 targetScale )
		{
			ui_rectTransform.localScale = targetScale;
		}

		public void TeleportToStart( Vector3 startScale )
		{
			ui_rectTransform.localScale = startScale;
		}

		public void Subscribe_OnComplete( UnityMessage callback )
		{
			onComplete.Subscribe( callback );
		}
#endregion

#region Implementation
        void OnTweenComplete()
        {
			onComplete.Invoke();
			ui_OnComplete.Invoke();

			onComplete.ClearInvokeList();
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}