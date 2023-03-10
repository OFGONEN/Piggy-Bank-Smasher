/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FFStudio;
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
    [ SerializeField ] SharedIntNotifier notif_piggyBank_count;

  [ Title( "Components" ) ]
    [ SerializeField ] Rigidbody _rigidbody;
    [ SerializeField ] MeshFilter mesh_filter;
    [ SerializeField ] MeshRenderer mesh_renderer;

// Private
    [ ShowInInspector, ReadOnly ] PiggyBankData data_current;
    float health_current;
#endregion

#region Properties
	public PiggyBankData Data => data_current;
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( PiggyBankData data, Vector3 position )
    {
		data_current   = data;
		health_current = data.health;

		system_merger.AddPiggyBank( this );
		notif_piggyBank_count.SharedValue += 1;

		UpdateVisual();

		transform.position    = position;
		transform.eulerAngles = Vector3.zero.SetY( Random.Range( 0, 360 ) );

		gameObject.SetActive( true );
	}

    public void OnInteract()
    {
		//todo Haptic
		health_current -= system_economy.Damage;
        
        if( health_current <= 0 )
			OnSmashed();
		else
			OnDamaged();
	}

	public void DoMerge( PiggyBank piggyBank )
	{
		//todo Implement a sequence for it
		ReturnToPool();
	}

	public void GetMerge()
	{
		//todo Implement a sequence for it
		system_merger.RemovePiggyBank( this );
		data_current = data_current.next_data;
		system_merger.AddPiggyBank( this );

		UpdateVisual();
	}
#endregion

#region Implementation
    void OnSmashed()
    {
		notif_currency.SharedValue += data_current.curreny_range.ReturnRandom();
		ReturnToPool();
	}

    void OnDamaged()
    {
		// todo spawn PFX
    }

    void UpdateVisual()
    {
		mesh_filter.mesh              = data_current.mesh;
		mesh_renderer.sharedMaterials = data_current.material_array;
	}

	[ Button() ]
	void ReturnToPool()
	{
		system_merger.RemovePiggyBank( this );
		notif_piggyBank_count.SharedValue -= 1;

		pool_piggyBank.ReturnEntity( this );
	}
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}


//todo Trail on piggys