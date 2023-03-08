/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "system_economy", menuName = "FF/Game/Economy System" ) ]
public class SystemEconomy : ScriptableObject
{
#region Fields
	[ SerializeField ] EconomyData[] economy_data_array;
    int economy_index;
#endregion

#region Properties
    public int Index        => economy_index;
    public bool IsMaxed     => economy_data_array.Length - 1 == economy_index;
    public float UnlockCost => economy_data_array[ economy_index ].unlock_cost;
    public float Damage     => economy_data_array[ economy_index ].damage;
#endregion

#region Unity API
#endregion

#region API
    public void Init()
    {
		economy_index = PlayerPrefsUtility.Instance.GetInt( Extensions.Key_Economy, 0 );
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
	public EconomySpawnData[] spawn_data_array;
}

[ System.Serializable ]
public struct EconomySpawnData
{
	public int percentage;
	public int level;
	public Vector2 count_range;
}