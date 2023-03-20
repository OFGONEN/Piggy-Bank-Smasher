/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using FFStudio;
using DG.Tweening;
using Sirenix.OdinInspector;

public class PiggyBankScatter : MonoBehaviour
{
#region Fields
  [ Title( "Setup" ) ]
	[ SerializeField ] PoolPiggyBankScatter pool_piggyBank_scatter;

  [ Title( "Setup" ) ]
    [ SerializeField, LabelText( "Force Range" ) ] Vector2 force_range = new Vector2( 1, 10 );
	[ SerializeField ] float explosionRadius = 1.0f;
	[ SerializeField ] float upwardsModifier = 0.0f;
	[ SerializeField ] ForceMode forceMode = ForceMode.Force;

  [ Title( "Components" ) ]
	[ SerializeField ] Rigidbody[] rigidbody_array;
	[ SerializeField ] Renderer[] renderer_array;
	[ SerializeField ] MeshFilter[] meshFilter_array;

	Cooldown cooldown = new Cooldown();
	TransformData[] transform_data_array;
#endregion

#region Unity API
	void Awake()
	{
		transform_data_array = new TransformData[ rigidbody_array.Length ];

		for( var i = 0; i < rigidbody_array.Length; i++ )
			transform_data_array[ i ] = rigidbody_array[ i ].transform.GetLocalTransformData();
	}
#endregion

#region API
    public void Execute( Transform target, PiggyBankData data )
    {
		SetLocalTransformData();
		UpdateVisual( data );

		gameObject.SetActive( true );
		transform.position = target.position;
		transform.rotation = target.rotation;

		var force = force_range.ReturnRandom();

		for( var i = 0; i < rigidbody_array.Length; i++ )
		{
			var rb = rigidbody_array[ i ];
			rb.AddExplosionForce( force, transform.position, explosionRadius, upwardsModifier, forceMode );
		}

		cooldown.Start( GameSettings.Instance.piggy_scatter_duration, OnCooldownComplete );
	}
#endregion

#region Implementation
	void SetLocalTransformData()
	{
		for( var i = 0; i < rigidbody_array.Length; i++ )
			rigidbody_array[ i ].transform.SetLocalTransformData( transform_data_array[ i ] );
	}

	void UpdateVisual( PiggyBankData data )
	{
		for( var i = 0; i < renderer_array.Length; i++ )
		{
			renderer_array  [ i ].sharedMaterial = data.scatter_data.material;
			meshFilter_array[ i ].mesh           = data.scatter_data.mesh_array[ i ];
		}
	}
	
	void OnCooldownComplete()
	{
		pool_piggyBank_scatter.ReturnEntity( this );
	}
#endregion
}