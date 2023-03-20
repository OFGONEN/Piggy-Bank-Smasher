/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
	// [ CreateAssetMenu( fileName = "shared_", menuName = "FF/Data/Shared/Library/" ) ]
	public class SharedDataLibrary< DataType > : ScriptableObject 
	{
#region Fields
	[ Title( "Setup" ) ]
        [ SerializeField ] DataType[] data_array;
#endregion

#region Properties
        public int LibrarySize => data_array.Length;
#endregion

#region Unity API
#endregion

#region API
        public DataType GetData( int index )
        {
			return data_array[ index ];
		}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}