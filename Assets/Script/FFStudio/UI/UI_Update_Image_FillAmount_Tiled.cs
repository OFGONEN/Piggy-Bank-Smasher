/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class UI_Update_Image_FillAmount_Tiled : MonoBehaviour
	{
#region Fields
		[ SerializeField, OnValueChanged( "OnValidatePROPER" ) ] Image image_foreground;
		[ SerializeField, OnValueChanged( "OnValidatePROPER" ) ] Image image_background;
		
		[ InfoBox( "@\"Number of segments = Current Denominator of the shared progress notifier, which is \" + notifier_progress.Denominator + \" currently.\nPlease set it to the number of segments you want to see, preferably on level load.\"") ]
		[ SerializeField ] SharedProgressNotifier notifier_progress;
		
		Vector2 dimensions;
		int count_total;
#endregion

#region Properties
#endregion

#region Unity API
		void Awake()
		{
			dimensions = new Vector2( image_foreground.mainTexture.width, image_foreground.mainTexture.height );

			image_foreground.rectTransform.sizeDelta = dimensions;
			image_background.rectTransform.sizeDelta = dimensions;
		}
#endregion

#region API
		public void OnProgressChange( float newProgress )
		{
			ChangeSegmentCount( Mathf.CeilToInt( notifier_progress.Denominator ) );

			var filledSegmentCount = Mathf.FloorToInt( newProgress * count_total );
			FillSegments( filledSegmentCount );
		}
#endregion

#region Implementation
		void ChangeSegmentCount( int count )
		{
			count_total = count;

			image_background.rectTransform.sizeDelta = dimensions.MultiplyX( count_total );
			image_foreground.rectTransform.sizeDelta = dimensions.MultiplyX( count_total );
		}
		
		void FillSegments( int count )
		{
			var newX = count * dimensions.x;
			image_foreground.rectTransform.anchoredPosition = image_foreground.rectTransform.anchoredPosition.SetX( newX );
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
		void OnValidatePROPER()
		{
			if( image_foreground == null )
				return;
				
			dimensions = new Vector2( image_foreground.mainTexture.width, image_foreground.mainTexture.height );

			image_foreground.rectTransform.sizeDelta = dimensions;
			
			if( image_background != null )
				image_background.rectTransform.sizeDelta = dimensions;
		}
#endif
#endregion
	}
}