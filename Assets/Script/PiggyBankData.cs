/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( fileName = "shared_piggyBank_data", menuName = "FF/Game/Piggy Bank Data" ) ]
public class PiggyBankData : ScriptableObject
{
#region Fields
    public float health;
    public Vector2 curreny_range;
    public int merge_count;
    public PiggyBankData next_data;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}