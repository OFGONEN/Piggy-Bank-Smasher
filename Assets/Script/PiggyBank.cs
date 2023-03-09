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
    [ SerializeField ] Currency notif_currency;
    [ SerializeField ] PoolPiggyBank pool_piggyBank;

  [ Title( "Components" ) ]
    [ SerializeField ] Rigidbody _rigidbody;

// Private
    PiggyBankData data_current;
    float health_current;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
    public void Spawn( PiggyBankData data, Vector3 position )
    {
		data_current   = data;
		health_current = data.health;

		transform.position    = position;
		transform.eulerAngles = Vector3.zero.SetY( Random.Range( 0, 360 ) );

		gameObject.SetActive( true );
	}

    public void OnInteract()
    {
		health_current -= system_economy.Damage;
        
        if( health_current < 0 )
			OnSmashed();
		else
			OnDamaged();
	}
#endregion

#region Implementation
    void OnSmashed()
    {
		notif_currency.SharedValue += data_current.curreny_range.ReturnRandom();

		pool_piggyBank.ReturnEntity( this );
	}

    void OnDamaged()
    {

    }

    void UpdateVisual()
    {
    }
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
}