/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "system_merger", menuName = "FF/Game/Merger System" ) ]
public class SystemMerger : ScriptableObject
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] PiggyBankDataLibrary library_piggyBank_data;
    [ SerializeField ] SharedBoolNotifier notif_bool_piggy_merge;

    Dictionary< int, Dictionary< int, PiggyBank > > piggyBank_dictionary;
    [ ShowInInspector, ReadOnly ] List< int > piggyBank_merge_list;
    List< PiggyBank > piggyBank_merge_cache;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Init()
    {
		var maxLevel = library_piggyBank_data.PiggyBankMaxLevel;

		piggyBank_dictionary  = new Dictionary< int, Dictionary< int, PiggyBank > >( maxLevel );
		piggyBank_merge_list  = new List<int>( maxLevel );
		piggyBank_merge_cache = new List< PiggyBank >( 32 );

		for( var i = 1; i <= maxLevel; i++ )
			piggyBank_dictionary.Add( i, new Dictionary< int, PiggyBank >( 32 ) );
    }

    public void AddPiggyBank( PiggyBank piggyBank )
    {
		var level      = piggyBank.Data.level;
		var mergeCount = piggyBank.Data.merge_count;

		Dictionary< int, PiggyBank > dictionary;
		piggyBank_dictionary.TryGetValue( level, out dictionary );

		dictionary.Add( piggyBank.GetInstanceID(), piggyBank );

        if( dictionary.Count >= mergeCount && !piggyBank_merge_list.Contains( level ) )
			piggyBank_merge_list.Add( level );

		notif_bool_piggy_merge.SharedValue = piggyBank_merge_list.Count > 0;
	}

	public void RemovePiggyBank( PiggyBank piggyBank )
	{
        var level      = piggyBank.Data.level;
        var mergeCount = piggyBank.Data.merge_count;


		Dictionary< int, PiggyBank > dictionary;
		piggyBank_dictionary.TryGetValue( level, out dictionary );

		dictionary.Remove( piggyBank.GetInstanceID() );

        if( dictionary.Count < mergeCount )
			piggyBank_merge_list.Remove( level );

		notif_bool_piggy_merge.SharedValue = piggyBank_merge_list.Count > 0;
	}

    public void Merge()
    {
        if( piggyBank_merge_list.Count <= 0 )
			return;

		var level = piggyBank_merge_list.ReturnRandom();

		Dictionary< int, PiggyBank > dictionary;
		piggyBank_dictionary.TryGetValue( level, out dictionary );

		int count = 0;
		foreach( var piggyBank in dictionary.Values )
		{
			if( count >= piggyBank.Data.merge_count )
				break;

			piggyBank_merge_cache.Add( piggyBank );
			count++;
		}

		var first = piggyBank_merge_cache[ 0 ];
		first.GetMerge();

		for( var i = 1; i < piggyBank_merge_cache.Count; i++ )
			piggyBank_merge_cache[ i ].DoMerge( first );

		piggyBank_merge_cache.Clear();
	}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}