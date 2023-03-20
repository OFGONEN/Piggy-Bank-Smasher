/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

[ System.Serializable ]
public class ShakeScaleTween
{
	[ SuffixLabel( "units"   )           ] public Vector3 strength = Vector3.one;
	[ SuffixLabel( "seconds" ), Min( 0 ) ] public float duration = 1;
    [ SuffixLabel( "hz"      ), Min( 0 ) ] public int vibrato = 10;
    [ Range( 0, 180 ) ] public float randomness = 90;
	public bool fadeOut = true;
	public Ease ease = Ease.Linear;

    public Tweener CreateTween( Transform transform )
    {
        return transform.DOShakeScale( duration, strength, vibrato, randomness, fadeOut )
                        .SetEase( ease );
    }
}
