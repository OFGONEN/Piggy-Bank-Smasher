/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class UIUnlockSequence : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
    [ SerializeField ] float tween_text_rotation_value;
    [ SerializeField ] float tween_text_rotation_duration;
    [ SerializeField ] Ease tween_text_rotation_ease;
    [ SerializeField ] float tween_text_rotation_interval;
    [ SerializeField ] float tween_glow_rotation_speed;

  [ Title( "Components" ) ]
    [ SerializeField ] GameObject gfx_parent;
    [ SerializeField ] RectTransform transform_text;
    [ SerializeField ] RectTransform transform_glow;
    [ SerializeField ] Image image_piggyBank_target;
    [ SerializeField ] Button _button;

    RecycledSequence recycledSequence_text = new RecycledSequence();
    RecycledTween    recycledTween_glow    = new RecycledTween();
    RecycledTween    recycledTween_target  = new RecycledTween();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnEconomyUnlock()
    {
		Time.timeScale = 0;

		_button.interactable = true;

		gfx_parent.SetActive( true );

		image_piggyBank_target.rectTransform.localScale = Vector3.one;
		transform_text.eulerAngles                      = Vector3.forward * -tween_text_rotation_value;
		transform_glow.rotation                         = Quaternion.identity;

		var sequenceText = recycledSequence_text.Recycle();

		sequenceText.Append( 
            transform_text.DOLocalRotate(
			Vector3.forward * tween_text_rotation_value,
			tween_text_rotation_duration )
		);

		sequenceText.AppendInterval( tween_text_rotation_interval );

		sequenceText.Append( 
            transform_text.DOLocalRotate(
			Vector3.forward * -tween_text_rotation_value,
			tween_text_rotation_duration )
		);

		sequenceText.SetEase( tween_text_rotation_ease );
		sequenceText.SetUpdate( true );
		sequenceText.SetLoops( -1, LoopType.Yoyo );

		recycledTween_glow.Recycle( transform_glow.DOLocalRotate(
			Vector3.forward * -180f,
			tween_glow_rotation_speed )
			.SetEase( Ease.Linear )
			.SetLoops( -1, LoopType.Incremental ) 
            .SetUpdate( true )
        );
	}

    public void OnButtonClick()
    {
		recycledSequence_text.Kill();
		recycledTween_glow.Kill();
		recycledTween_target.Kill();

		Time.timeScale = 1;
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
