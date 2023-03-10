/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "library_piggyBank_data", menuName = "FF/Game/Library Piggy Bank Data" ) ]
public class PiggyBankDataLibrary : ScriptableObject
{
#region Fields
    [ SerializeField ] PiggyBankData[] piggyBank_data_array;
    [ SerializeField, ReadOnly ] int piggyBank_level_max;
#endregion

#region Properties
    public int PiggyBankMaxLevel => piggyBank_level_max;
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
    private void OnValidate()
    {
		UnityEditor.EditorUtility.SetDirty( this );

		piggyBank_level_max = 0;
		for( var i = 0; i < piggyBank_data_array.Length; i++ )
        {
            if( piggyBank_data_array[ i ].level >= piggyBank_level_max )
				piggyBank_level_max = piggyBank_data_array[ i ].level;
		}
    }
#endif
#endregion
}
