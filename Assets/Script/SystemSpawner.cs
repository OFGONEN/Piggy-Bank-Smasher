/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

[ CreateAssetMenu( fileName = "system_spawner", menuName = "FF/Game/Spawner System" ) ]
public class SystemSpawner : ScriptableObject
{
#region Fields
  [ Title( "Shared" ) ]
    [ SerializeField ] SharedFloatNotifier notif_spawn_progress;
    [ SerializeField ] SystemEconomy system_economy;
    [ SerializeField ] PoolPiggyBank pool_piggyBank;
    [ SerializeField ] PiggyBankDataLibrary library_piggyBank_data;

    Cooldown cooldown = new Cooldown();
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void OnLevelStarted()
    {
		StartSpawnCooldown();
	}

    public void OnLevelFinished()
    {
		cooldown.Kill();
	}
#endregion

#region Implementation
    void OnSpawnUpdate()
    {
		notif_spawn_progress.SharedValue = cooldown.GetElapsedPercentageSafe();
	}

    void OnSpawnComplete()
    {
		notif_spawn_progress.SharedValue = 0;

        SpawnPiggyBank();
		StartSpawnCooldown();
	}

    void StartSpawnCooldown() 
    {
		cooldown.Start( GameSettings.Instance.spawn_duration,
			OnSpawnUpdate,
			OnSpawnComplete
        );
    }

    void SpawnPiggyBank()
    {
		var spawnData     = system_economy.GetSpawnData();
		var count         = spawnData.count_range.ReturnRandom();
		var piggyBankData = library_piggyBank_data.GetPiggyBankData( spawnData.level );

		for( var i = 0; i < count; i++ )
        {
			var randomPoint = Random.insideUnitCircle * GameSettings.Instance.spawn_radius;

			pool_piggyBank.GetEntity().Spawn( piggyBankData, new Vector3( 
                randomPoint.x,
                GameSettings.Instance.spawn_height,
                randomPoint.y
            ) );
		}
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}