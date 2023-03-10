/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "system_economy", menuName = "FF/Game/Economy System" ) ]
public class SystemEconomy : ScriptableObject
{
#region Fields
  [ Title( "Shared" ) ]
	[ SerializeField ] Currency notif_currency;

  [ Title( "Setup" ) ]
	[ SerializeField ] EconomyData[] economy_data_array;
    int economy_index;
#endregion

#region Properties
    public int Index         => economy_index;
    public bool IsMaxed      => economy_data_array.Length - 1 == economy_index;
    public float UnlockCost  => economy_data_array[ Mathf.Min( economy_data_array.Length - 1, economy_index + 1 ) ].unlock_cost;
    public float Damage      => economy_data_array[ economy_index ].damage;
    public int MaxSpawnCount => economy_data_array[ economy_index ].spawn_count_max;
#endregion

#region Unity API
#endregion

#region API
    public void Init()
    {
		economy_index = PlayerPrefsUtility.Instance.GetInt( Extensions.Key_Economy, 0 );
	}

	[ Button() ]
	public void Unlock()
	{
		notif_currency.SharedValue -= UnlockCost;
		economy_index = Mathf.Min( economy_data_array.Length - 1, economy_index + 1 );
	}

	public EconomySpawnData GetSpawnData()
	{
		var random = Random.Range( 0, 100 );

		for( var x = 0; x < economy_data_array[ economy_index ].spawn_data_array.Length; x++ )
		{
			if( random <= economy_data_array[ economy_index ].spawn_data_array[ x ].percentage )
				return economy_data_array[ economy_index ].spawn_data_array[ x ];
		}

		FFLogger.LogError( "Wrong Spawn Return" );
		return economy_data_array[ economy_index ].spawn_data_array[ 0 ];
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}

[ System.Serializable ]
public struct EconomyData
{
	public float damage;
	public float unlock_cost;
	public int spawn_count_max;
	public EconomySpawnData[] spawn_data_array;
}

[ System.Serializable ]
public struct EconomySpawnData
{
	public int percentage;
	public int level;
	public Vector2 count_range;
}