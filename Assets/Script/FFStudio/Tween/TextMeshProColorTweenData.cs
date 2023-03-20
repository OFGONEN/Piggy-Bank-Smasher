/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace FFStudio
{
	public class TextMeshProColorTweenData : TweenData
	{
#region Fields
	[ Title( "TextMeshPro Color Tween" ) ]
    	[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public TextMeshProUGUI textMeshProUGUI;
    	[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public bool keepCurrentStartColor = true;
    	[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), LabelText( "Start Value" ), HideIf( "keepCurrentStartColor" ) ] public Color color_start;
    	[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ), LabelText( "End Value" ) ] public Color color_end;
		[ BoxGroup( "Tween" ), PropertyOrder( int.MinValue ) ] public float duration;
        
		Color color_tweened;
#endregion

#region API
        /* public override void Initialize( Transform transform )
        {
			base.Initialize( transform );
		} */
		
		public override Tween CreateTween( bool isReversed = false )
		{
			color_tweened = keepCurrentStartColor ? textMeshProUGUI.color : color_start;

			recycledTween.Recycle( DOTween.To( GetColor, SetColor, color_end, duration ).OnUpdate( OnColorUpdate ),
								   unityEvent_onCompleteEvent.Invoke );

#if UNITY_EDITOR
			recycledTween.Tween.SetId( "_ff_TMP_color_tween___" + description );
#endif

			return base.CreateTween();
		}
        
        void OnColorUpdate()
        {
			textMeshProUGUI.color = color_tweened;
		}
        
        Color GetColor() => color_tweened;
        void SetColor( Color newColor ) => color_tweened = newColor;
#endregion

#region Implementation
#endregion

#region EditorOnly
#if UNITY_EDITOR
#endif
#endregion
	}
}