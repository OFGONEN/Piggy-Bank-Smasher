/* Created by and for usage of FF Studios (2023). */

using UnityEngine;
using Sirenix.OdinInspector;

public class HexGridGizmoDrawer : MonoBehaviour
{
#region Fields
[ Title( "Setup" ) ]
    [ SerializeField, InlineEditor, LabelText( "Grid Layout" ) ] HexGridLayout grid_layout;
#endregion

#region Unity API
    void OnDrawGizmos()
    {
		if( grid_layout == null )
			return;
            
        var angle = grid_layout.HasHorizontalConfiguration ? Mathf.PI / 3.0f : Mathf.PI / 6.0f;

		for( var i = 0; i < grid_layout.GeneratedTileCenterPoints.Length; i++ )
        {
            var tileCenter = grid_layout.GeneratedTileCenterPoints[ i ];
            Shapes.Draw.UseDashes = true;
            Shapes.Draw.Thickness = 0.01f;
			Shapes.Draw.DashSnap = Shapes.DashSnapping.EndToEnd;

			Shapes.Draw.RegularPolygonBorder( tileCenter, 6, grid_layout.TileRadius, 0.033f, angle );
        }
    }
#endregion

#region API
#endregion

#region Implementation
#endregion
}
