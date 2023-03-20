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
    [ SerializeField ] SharedIntNotifier notif_piggyBank_count;
	RecycledTween recycledTween = new RecycledTween();

	float spawn_cooldown;
	float spawn_cooldown_manual;
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
		recycledTween.Kill();
	}

	public void OnSpawnManual()
	{
		if( Time.time >= spawn_cooldown_manual )
			OnSpawnCooldownComplete();
	}
#endregion

#region Implementation
    void OnSpawnCooldownUpdate()
    {
		notif_spawn_progress.SharedValue = spawn_cooldown;
	}

    void OnSpawnCooldownComplete()
    {
		spawn_cooldown                   = 0;
		spawn_cooldown_manual            = Time.time + GameSettings.Instance.spawn_duration_manual;
		notif_spawn_progress.SharedValue = 0;
		recycledTween.Kill();

		if( notif_piggyBank_count.sharedValue >= system_economy.MaxSpawnCount )
			return;

		SpawnPiggyBank();
		StartSpawnCooldown();
	}

    void StartSpawnCooldown() 
    {
		recycledTween.Recycle( 
			DOTween.To( GetSpawnCooldown, SetSpawnCooldown,
			1,
			GameSettings.Instance.spawn_duration ), OnSpawnCooldownComplete );

		recycledTween.Tween.OnUpdate( OnSpawnCooldownUpdate );
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

	float GetSpawnCooldown()
	{
		return spawn_cooldown;
	}

	void SetSpawnCooldown( float value )
	{
		spawn_cooldown                   = value;
		notif_spawn_progress.SharedValue = value;
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}