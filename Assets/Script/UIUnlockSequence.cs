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
    [ SerializeField ] float tween_image_scale_start;
    [ SerializeField ] float tween_image_scale_duration;
    [ SerializeField ] Ease tween_image_scale_ease;
    [ SerializeField ] PunchScaleTween tween_image_punchScale;
    [ SerializeField ] float sequence_cooldown;

  [ Title( "Components" ) ]
    [ SerializeField ] GameObject gfx_parent;
    [ SerializeField ] RectTransform transform_text;
    [ SerializeField ] RectTransform transform_glow;
    [ SerializeField ] Image image_piggyBank_target;
    [ SerializeField ] Button _button;

  [ Title( "Shared" ) ]
	[ SerializeField ] SystemEconomy system_economy;

    RecycledSequence recycledSequence_text = new RecycledSequence();
    RecycledTween    recycledTween_glow    = new RecycledTween();
    RecycledTween    recycledTween_target  = new RecycledTween();
	Cooldown cooldown = new Cooldown();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnEconomyUnlock()
    {
		Time.timeScale = 0;
		_button.interactable = false;

		gfx_parent.SetActive( true );

		image_piggyBank_target.rectTransform.localScale = Vector3.one * tween_image_scale_start;
		transform_text.eulerAngles                      = Vector3.forward * -tween_text_rotation_value;
		transform_glow.rotation                         = Quaternion.identity;
		image_piggyBank_target.sprite                   = system_economy.UnlockSprite;

		recycledTween_target.Recycle( image_piggyBank_target.rectTransform.DOScale(
			1,
			tween_image_scale_duration )
			.SetEase( tween_image_scale_ease )
			.SetUpdate( true ),
			OnTargetImageScaleDone
		);

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
		cooldown.Kill();

		Time.timeScale = 1;
	}
#endregion

#region Implementation
	void OnTargetImageScaleDone()
	{
		_button.interactable = true;

		recycledTween_target.Recycle( tween_image_punchScale.CreateTween( image_piggyBank_target.rectTransform )
		.SetUpdate( true ) );

		cooldown.StartUnscaled( sequence_cooldown, _button.onClick.Invoke );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
