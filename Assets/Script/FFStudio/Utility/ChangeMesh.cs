/* Created by and for usage of FF Studios (2023). */

using UnityEngine;
using Sirenix.OdinInspector;

public class ChangeMesh : MonoBehaviour
{
#region Fields
[ Title( "Setup" ) ]
    [ SerializeField, LabelText( "Meshes" ), AssetSelector( Paths = "Assets/Model" ) ] Mesh[] mesh_array;
    [ SerializeField ] MeshFilter meshFilter;
    [ SerializeField ] MeshCollider meshCollider;
#endregion

#region Unity API
#endregion

#region API
    public void Execute( int index )
    {
		meshFilter.mesh = mesh_array[ index ];
	}
    
    public void Execute_AlsoChangeColliderMesh( int index )
    {
		Execute( index );
		meshCollider.sharedMesh = meshFilter.mesh;
	}
#endregion

#region Implementation
#endregion
}
