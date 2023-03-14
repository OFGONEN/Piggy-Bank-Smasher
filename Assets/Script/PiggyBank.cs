/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class PiggyBank : MonoBehaviour, IInteractable
{
#region Fields
  [ Title( "Setup" ) ]
  [ Title( "Shared" ) ]
    [ SerializeField ] SystemEconomy system_economy;
    [ SerializeField ] SystemMerger system_merger;
    [ SerializeField ] Currency notif_currency;
    [ SerializeField ] PoolPiggyBank pool_piggyBank;
    [ SerializeField ] PoolPiggyBankScatter pool_piggyBank_scatter;
    [ SerializeField ] SharedIntNotifier notif_piggyBank_count;
	[ SerializeField ] IntGameEvent event_haptic;
	[ SerializeField ] ParticleSpawnEvent event_particle_spawn;

  [ Title( "Components" ) ]
    [ SerializeField ] Rigidbody _rigidbody;
    [ SerializeField ] Collider _collider;
    [ SerializeField ] MeshFilter mesh_filter;
    [ SerializeField ] MeshRenderer mesh_renderer;
    [ SerializeField ] ColorSetter _colorSetter;

// Private
    [ ShowInInspector, ReadOnly ] PiggyBankData data_current;
    float health_current;
	RecycledTween recycledTween = new RecycledTween();	
	RecycledSequence recycledSequence = new RecycledSequence();
	Cooldown cooldown = new Cooldown();
#endregion

#region Properties
	public PiggyBankData Data => data_current;
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( PiggyBankData data, Vector3 position )
    {
		cooldown.Start( Time.deltaTime, _colorSetter.SetStartColors );

		data_current   = data;
		health_current = data.health;

		system_merger.AddPiggyBank( this );
		notif_piggyBank_count.SharedValue += 1;

		UpdateVisual();

		transform.position    = position;
		transform.eulerAngles = Vector3.zero.SetY( Random.Range( 0, 360 ) );

		recycledTween.Recycle( GameSettings.Instance.piggy_spawn_punchScale.CreateTween( mesh_renderer.transform ) );

		_rigidbody.isKinematic = false;
		_rigidbody.useGravity  = true;
		_collider.enabled      = true;

		gameObject.SetActive( true );
	}

    public void OnInteract()
    {
		event_haptic.Raise( 0 );

		health_current -= system_economy.Damage;
        
        if( health_current <= 0 )
			OnSmashed();
		else
			OnDamaged();
	}

	public void DoMerge( PiggyBank piggyBank )
	{
		_rigidbody.isKinematic = true;
		_rigidbody.useGravity  = false;
		_collider.enabled      = false;

		system_merger.RemovePiggyBank( this );
		notif_piggyBank_count.SharedValue -= 1;

		var sequence = recycledSequence.Recycle( OnDoMergeDone );

		sequence.AppendInterval( GameSettings.Instance.piggy_merge_lift_duration );
		sequence.AppendCallback( DoJumpRotation );
		sequence.Append( transform.DOJump( piggyBank.transform.position.SetY( GameSettings.Instance.piggy_merge_lift_height ),
			GameSettings.Instance.piggy_merge_jump_power,
			1,
			GameSettings.Instance.piggy_merge_jump_duration )
			.SetEase( GameSettings.Instance.piggy_merge_jump_ease ) 
		);
	}

	public void GetMerge()
	{
		system_merger.RemovePiggyBank( this );

		_rigidbody.isKinematic = true;
		_rigidbody.useGravity  = false;
		_collider.enabled      = false;

		var position = transform.position.SetY( GameSettings.Instance.piggy_merge_lift_height );

		event_particle_spawn.particle_alias        = "piggy_upgrade";
		event_particle_spawn.particle_spawn_point  = position + GameSettings.Instance.piggy_pfx_upgrade_offset;
		event_particle_spawn.particle_spawn_size   = GameSettings.Instance.piggy_pfx_upgrade_size;
		event_particle_spawn.particle_spawn_parent = transform;
		event_particle_spawn.keepParentRotation    = false;

		var sequence = recycledSequence.Recycle( OnGetMergeDone );
		sequence.Append( transform.DOMove( position,
			GameSettings.Instance.piggy_merge_lift_duration )
			.SetEase( GameSettings.Instance.piggy_merge_lift_ease ) 
		);

		sequence.AppendCallback( event_particle_spawn.Raise );
		sequence.AppendInterval( GameSettings.Instance.piggy_merge_jump_duration );
	}
#endregion

#region Implementation
	void DoJumpRotation()
	{
		transform.eulerAngles = transform.eulerAngles.SetX( 0 ).SetZ( 0 );
	}

	void OnGetMergeDone()
	{
		cooldown.Start( Time.deltaTime, _colorSetter.SetStartColors );
		
		data_current   = data_current.next_data;
		health_current = data_current.health;

		system_merger.AddPiggyBank( this );

		UpdateVisual();
		recycledTween.Recycle( GameSettings.Instance.piggy_spawn_punchScale.CreateTween( mesh_renderer.transform ) );

		_rigidbody.isKinematic = false;
		_rigidbody.useGravity  = true;
		_collider.enabled      = true;
	}

	void OnDoMergeDone()
	{
		pool_piggyBank.ReturnEntity( this );
	}

	void OnSmashed()
    {
		event_particle_spawn.Raise( "piggy_shatter", transform.position, null, GameSettings.Instance.piggy_pfx_shatter_size );

		notif_currency.SharedValue += data_current.curreny_range.ReturnRandom();
		notif_currency.Save();

		system_merger.RemovePiggyBank( this );
		notif_piggyBank_count.SharedValue -= 1;

		pool_piggyBank.ReturnEntity( this );
		pool_piggyBank_scatter.GetEntity().Execute( transform, data_current );
	}

    void OnDamaged()
    {
		var ratio = Mathf.InverseLerp( data_current.health, 0, health_current );
		_colorSetter.LerpAllColors( ratio, GameSettings.Instance.piggy_damaged_color );
	}

    void UpdateVisual()
    {
		mesh_filter.mesh              = data_current.mesh;
		mesh_renderer.sharedMaterials = data_current.material_array;
	}

	// [ Button() ]
	// void ReturnToPool()
	// {
	// 	system_merger.RemovePiggyBank( this );
	// 	notif_piggyBank_count.SharedValue -= 1;

	// 	pool_piggyBank.ReturnEntity( this );
	// }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}


//todo Trail on piggys