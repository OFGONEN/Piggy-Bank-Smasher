/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( fileName = "library_piggyBank_data", menuName = "FF/Game/Library Piggy Bank Data" ) ]
public class PiggyBankDataLibrary : ScriptableObject
{
#region Fields
    [ SerializeField ] PiggyBankData[] piggyBank_data_array;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public PiggyBankData GetPiggyBankData( int level )
    {
		return piggyBank_data_array[ Mathf.Clamp( level - 1, 0, piggyBank_data_array.Length - 1 ) ];
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}
