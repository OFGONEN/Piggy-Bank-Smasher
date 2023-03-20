/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using DG.Tweening;

[ System.Serializable ]
public class RotationTween
{
#region Fields
    public bool useDelta = true;
    
#if UNITY_EDITOR
    [ InfoBox( "End Value is RELATIVE.", "useDelta" ) ]
    [ InfoBox( "End Value is ABSOLUTE.", "EndValueIsAbsolute" ) ]
#endif
    [ SuffixLabel( "Degrees (°)" ) ] public float endValue;
#if UNITY_EDITOR
    [ InfoBox( "Duration is DURATION (seconds).", "DurationIsDuration" ) ]
    [ InfoBox( "Duration is ANGULAR VELOCITY (degrees/seconds).", "speedBased" ) ]
#endif
    [ Min( 0 ) ] public float duration;
    public TweenSpace space;
    [ ValueDropdown( "VectorValues" ), LabelText( "Rotate Around" ) ] public Vector3 rotationAxisMaskVector = Vector3.right;

    public Ease ease = Ease.Linear;
	public bool loop;
	[ ShowIf( "loop" ), LabelText( "Loop Type" ) ] public LoopType loop_type = LoopType.Restart;
	[ ShowIf( "loop" ), LabelText( "Infinite Loop" ) ] public bool loop_isInfinite = true;
	[ HideIf( "loop_isInfinite" ), LabelText( "Loop Count" ), Min( 1 ) ] public int loop_count = 1;

    IEnumerable VectorValues = new ValueDropdownList< Vector3 >()
    {
        { "X",   Vector3.right      },
        { "Y",   Vector3.up         },
        { "Z",   Vector3.forward    }
    };
#endregion

#region Properties
#if UNITY_EDITOR
    bool EndValueIsAbsolute => !useDelta;
#endif
#endregion

#region Unity API
#endregion

#region API
    public Tweener CreateTween( Transform transform )
    {
		Tweener tween;
		if( space == TweenSpace.Local )
            
            tween = transform.DOLocalRotate( rotationAxisMaskVector * endValue, duration, useDelta ? RotateMode.LocalAxisAdd : RotateMode.Fast );
        else
            tween = transform.DORotate( rotationAxisMaskVector * endValue, duration, useDelta ? RotateMode.WorldAxisAdd : RotateMode.Fast );

		// Info: Don't need to set SetRelative() as RotateMode.XXXAxisAdd automatically means relative end value.

		tween
            .SetEase( ease )
			.SetLoops( loop
                            ? loop_isInfinite
                                ? -1 
                                : loop_count
                            : 0,
						loop_type );

		return tween;
	}
#endregion
}
