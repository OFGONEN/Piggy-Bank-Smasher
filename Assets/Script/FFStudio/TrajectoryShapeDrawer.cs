/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Shapes;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class TrajectoryShapeDrawer : ImmediateModeShapeDrawer
	{
#region Fields
    [ Title( "Setup" ) ]
		[ SerializeField, LabelText( "Transform of Character" ) ] Transform transform_character;
        [ SerializeField ] float height = 0.05f;
        [ SerializeField ] float thickness = 0.25f;
        [ SerializeField ] float length = 10.0f;
        [ SerializeField, LabelText( "Dash Size" ) ] float dash_size = 0.05f;
        [ SerializeField, LabelText( "Dash Spacing" ) ] float dash_spacing = 0.05f;
        [ SerializeField, LabelText( "Start Color" ) ] Color color_start = Color.black;
        [ SerializeField, LabelText( "End Color" ) ] Color color_end = new Color( 0, 0, 0, 0 );

		DrawShape onDrawShape;
#endregion

#region Properties
#endregion

#region Unity API
		void Awake()
		{
			onDrawShape = Extensions.EmptyMethod;
		}
#endregion

#region API
		[ Button() ]
		public void StartDrawing()
		{
			onDrawShape = DrawLine;
		}

		[ Button() ]
		public void StopDrawing()
		{
			onDrawShape = Extensions.EmptyMethod;
		}
        
		public override void DrawShapes( Camera cam )
		{
			onDrawShape( cam );
		}
#endregion

#region Implementation
		void DrawLine( Camera cam )
		{
			using( Draw.Command( cam ) )
			{
				Draw.UseDashes = true;
				Draw.DashStyle = DashStyle.RelativeDashes( DashType.Rounded, dash_size, dash_spacing );
				Draw.Thickness = thickness;
				var posA = transform_character.position.SetY( height );
				var posB = posA + transform_character.forward * length;

				Draw.Line( posA, posB, color_start, color_end );
			}
		}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}