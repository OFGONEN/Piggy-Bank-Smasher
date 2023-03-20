/* Created by and for usage of FF Studios (2023). */

using System.Collections;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "hex_grid_layout_", menuName = "FF/Data/Hex Grid Layout" ) ]
public class HexGridLayout : ScriptableObject
{
#region Fields
[ Title( "Setup" ) ]
	[ SerializeField ] bool useHorizontalConfiguration = false;
    [ SerializeField, LabelText( "Row Count"), Min( 1 ) ] int row_count = 5;
	[ SerializeField, LabelText( "Column Count" ), Min( 1 ) ] int column_count = 5;
    [ SerializeField, ShowIf( "@row_count > 1" ) ] bool firstRowIsWide = true;
    [ SerializeField, LabelText( "Tile Radius" ), Min( 0.01f ), SuffixLabel( "units" ) ] float tile_radius = 1.0f;
    [ SerializeField, LabelText( "Gap Size" ), SuffixLabel( "units" ) ] float gap_size = 0.1f;
	[ SerializeField ] bool placePivotOnCenter;

#if UNITY_EDITOR
	[ ValueDropdown( "VectorValues" ) ]
#endif
	[ SerializeField, LabelText( "Direction of Growth" ) ]
	Vector3 direction_grow = new Vector3( +1, +1 );

#if UNITY_EDITOR
	IEnumerable VectorValues = new ValueDropdownList< Vector3 >()
    {
        { "↗", new Vector3( +1, +1 ) },
        { "↘", new Vector3( +1, -1 ) },
        { "↙", new Vector3( -1, -1 ) },
        { "↖", new Vector3( -1, +1 ) },
    };
#endif
    
	[ ShowInInspector, ReadOnly, SerializeField ]
	Vector3[] tile_center_array;
    
	float tile_width;
    float gap_size_half;
#endregion

#region Properties
	public Vector3[] 	GeneratedTileCenterPoints 		=> tile_center_array;
	public float 		TileRadius                   	=> tile_radius;
	public bool 		FirstRowIsWide                	=> firstRowIsWide;
	public int 			RowCount                      	=> row_count;
	public int 			ColumnCount                   	=> column_count;
	public int 			TileCount						=> tile_center_array.Length;
	public bool 		HasHorizontalConfiguration		=> useHorizontalConfiguration;
#endregion

#region Unity API
	void Reset()
	{
		Execute();
	}
#endregion

#region API
#endregion

#region Implementation
    [ Button, PropertyOrder( int.MinValue ) ]
    void Execute()
    {
		if( useHorizontalConfiguration )
			GenerateHorizontalLayout();
		else
			GenerateVerticalLayout();
	}

	void GenerateHorizontalLayout()
	{
		int wideRowCount   = Mathf.CeilToInt( row_count / 2.0f );
		int narrowRowCount = row_count - wideRowCount;

		if( firstRowIsWide == false )
			Extensions.Swap( ref wideRowCount, ref narrowRowCount );

		int tileCount = wideRowCount * column_count + narrowRowCount * ( column_count - 1 );
        
		tile_center_array = new Vector3[ tileCount ];

		var sin60 = Mathf.Sin( Mathf.PI / 3.0f );

		int index = 0;
		bool nextRowIsWide = firstRowIsWide;

		var tile_diameter  = tile_radius * 2.0f;
		    tile_width     = Mathf.Sin( Mathf.PI / 3.0f ) * tile_diameter;
		    gap_size_half  = gap_size / 2.0f;
		var naturalGap     = tile_diameter - tile_width;
		var halfNaturalGap = 0;

		var startPos = new Vector3( direction_grow.x * tile_radius, direction_grow.y * tile_width / 2.0f );
		var currentPos = startPos;

		currentPos = currentPos.SetX( nextRowIsWide ? startPos.x : startPos.x + direction_grow.x * ( -halfNaturalGap + tile_radius * 1.5f + sin60 * gap_size ) );

		var countOfRowsWithMoreHeight = firstRowIsWide ? wideRowCount   : narrowRowCount;
		var countOfRowsWithLessHeight = firstRowIsWide ? narrowRowCount : wideRowCount;
		var additionalOffset = row_count % 2 == 0 ? tile_width / 2.0f - gap_size_half : 0;
		var offsetDueToPivot = placePivotOnCenter
									? new Vector3( -direction_grow.x * ( column_count * tile_diameter + ( column_count - 1 ) * ( gap_size * sin60 * 2 + tile_radius ) ) / 2.0f,
												   -direction_grow.y * ( countOfRowsWithMoreHeight * tile_width + countOfRowsWithLessHeight * gap_size + additionalOffset ) / 2.0f )
									: Vector3.zero;

		for( var i = 0; i < row_count; i++ )
        {
			var currentColumnCount = nextRowIsWide ? column_count : column_count - 1;

			for( var j = 0; j < currentColumnCount; j++ )
            {
				tile_center_array[ index++ ] = currentPos + offsetDueToPivot;
				currentPos = currentPos.OffsetX( direction_grow.x * ( tile_radius * 3.0f + 2 * sin60 * gap_size ) );
			}

			nextRowIsWide = !nextRowIsWide;

			currentPos = currentPos
				.SetX( nextRowIsWide ? startPos.x : startPos.x + direction_grow.x * ( -halfNaturalGap + tile_radius * 1.5f + sin60 * gap_size ) )
				.OffsetY( direction_grow.y * tile_width / 2.0f + gap_size_half );
		}
	}
	
	void GenerateVerticalLayout()
	{
		int wideRowCount   = Mathf.CeilToInt( row_count / 2.0f );
		int narrowRowCount = row_count - wideRowCount;

		if( firstRowIsWide == false )
			Extensions.Swap( ref wideRowCount, ref narrowRowCount );

		int tileCount = wideRowCount * column_count + narrowRowCount * ( column_count - 1 );
        
		tile_center_array = new Vector3[ tileCount ];

		var startPos   = direction_grow * tile_radius;
		var currentPos = startPos;

		int index = 0;
		bool nextRowIsWide = firstRowIsWide;

		var tile_diameter  = tile_radius * 2.0f;
		    tile_width     = Mathf.Sin( Mathf.PI / 3.0f ) * tile_diameter;
		    gap_size_half  = gap_size / 2.0f;
		var naturalGap     = tile_diameter - tile_width;
		var halfNaturalGap = naturalGap / 2.0f;

		currentPos = currentPos.SetX( nextRowIsWide ? startPos.x : startPos.x + direction_grow.x * ( -halfNaturalGap + tile_radius + gap_size_half ) );

		var offsetDueToPivot = placePivotOnCenter
									? new Vector3( -direction_grow.x * ( column_count * tile_width + ( column_count - 1 ) * gap_size + naturalGap ) / 2.0f,
												   -direction_grow.y * ( wideRowCount * tile_diameter + narrowRowCount * tile_radius + ( row_count - 1 ) * gap_size ) / 2.0f )
									: Vector3.zero;

		for( var i = 0; i < row_count; i++ )
        {
			var currentColumnCount = nextRowIsWide ? column_count : column_count - 1;

			for( var j = 0; j < currentColumnCount; j++ )
            {
				tile_center_array[ index++ ] = currentPos + offsetDueToPivot;
				currentPos = currentPos.OffsetX( direction_grow.x * ( tile_width + gap_size ) );
			}

			nextRowIsWide = !nextRowIsWide;

			currentPos = currentPos
				.SetX( nextRowIsWide ? startPos.x : startPos.x + direction_grow.x * ( -halfNaturalGap + tile_radius + gap_size_half  ) )
				.OffsetY( direction_grow.y * ( tile_radius * 1.5f + gap_size ) );
		}
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
    void OnValidate()
    {
		Execute();
	}
#endif
#endregion
}
