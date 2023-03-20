/* Created by and for usage of FF Studios (2023). */

using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;
using Shapes;

[ ExecuteAlways ]
public class HexGridShapeDrawer : ImmediateModeShapeDrawer
{
#region Fields
[ Title( "Setup" ) ]
	[ SerializeField, InlineEditor ] HexGridLayout grid_layout;
    [ SerializeField, LabelText( "Tile Color" ) ] Color tile_color = Color.white;
    [ SerializeField, LabelText( "Use Dashes" ) ] bool dash_enable;
	[ SerializeField, ShowIf( "dash_enable" ) ] float dash_thickness = 0.033f;
	[ SerializeField, ShowIf( "dash_enable" ) ] DashSnapping dash_snapping = DashSnapping.EndToEnd;
    [ SerializeField ] bool autoStartDrawing;

	DrawShape onDrawShape;
#endregion

#region Properties
#endregion

#region Unity API
    void Awake()
    {
        onDrawShape = Extensions.EmptyMethod;
	}
    
    void Start()
    {
        if( autoStartDrawing )
			StartDrawing();
	}
#endregion

#region API
    [ Button(), ShowIf( "@grid_layout != null" ) ]
    public void StartDrawing()
    {
        onDrawShape = DrawGrid;
    }

    [ Button(), ShowIf( "@grid_layout != null" ) ]
    public void StopDrawing()
    {
        onDrawShape = Extensions.EmptyMethod;
    }
    
    public override void DrawShapes( Camera cam )
    {
#if UNITY_EDITOR
        if( Application.isPlaying == false && onDrawShape == null )
			return;
#endif
		onDrawShape( cam );
    }
#endregion

#region Implementation
    void DrawGrid( Camera cam )
    {
#if UNITY_EDITOR
        if( grid_layout == null )
        {
            if( Application.isPlaying )
                FFLogger.LogError( name + ": Hex Grid is not assigned!", this );
            else
				return;
		}
#endif

        var angle = grid_layout.HasHorizontalConfiguration ? Mathf.PI / 3.0f : Mathf.PI / 6.0f;
        
		using( Draw.Command( cam ) )
		{
            for( var i = 0; i < grid_layout.GeneratedTileCenterPoints.Length; i++ )
            {
				var tileCenter = grid_layout.GeneratedTileCenterPoints[ i ];
				Draw.UseDashes = dash_enable;
				Draw.DashSnap  = dash_snapping;

				if( dash_enable )
			        Draw.RegularPolygonBorder( tileCenter, 6, grid_layout.TileRadius, dash_thickness, angle, tile_color );
				else
                    Draw.RegularPolygon( tileCenter, 6, grid_layout.TileRadius, angle, tile_color );
			}
		}
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
    void OnValidate()
    {
        if( Application.isPlaying == false && grid_layout != null && grid_layout.GeneratedTileCenterPoints != null )
			StartDrawing();
	}
#endif
#endregion
}
