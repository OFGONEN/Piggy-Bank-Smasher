/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
	[ RequireComponent( typeof( Respond ) ) ]
	public class ParticleEffect : MonoBehaviour
	{
#region Fields
	[ Title( "Setup" ) ]
		public string alias;

		ParticleEffectPool particle_pool;
		ParticleEffectStopped particleEffectStopped;
		ParticleSystem particles;

		Vector3 particle_start_size;
#endregion

#region UnityAPI
		void Awake()
		{
			particles = GetComponentInChildren< ParticleSystem >();

			var mainParticle             = particles.main;
			    mainParticle.stopAction  = ParticleSystemStopAction.Callback;
			    mainParticle.playOnAwake = false;

			particle_start_size = transform.localScale;
		}
#endregion

#region API
		public void OnParticleStopped()
		{
			particleEffectStopped( this ); // Returns this back to pool
			transform.localScale = Vector3.one;
			transform.localRotation = Quaternion.identity;
		}

		public virtual void InitIntoPool( ParticleEffectPool pool, ParticleEffectStopped effectStoppedDelegate )
		{
			particle_pool         = pool;
			particleEffectStopped = effectStoppedDelegate;
		}

		public void PlayParticle( ParticleSpawnEvent particleEvent )
		{
			gameObject.SetActive( true );
			
			transform.position   = particleEvent.particle_spawn_point;
			transform.localScale = particle_start_size * particleEvent.particle_spawn_size;

			if( particleEvent.particle_spawn_parent != null )
			{
				transform.SetParent( particleEvent.particle_spawn_parent );
				if( particleEvent.keepParentRotation )
					transform.rotation = transform.parent.rotation;
			}

			particles.Play();
		}


		public void PlayParticle( Vector3 position, float scale, Transform parent = null )
		{
			gameObject.SetActive( true );

			transform.position = position;
			transform.localScale = particle_start_size * scale;

			if( parent != null )
				transform.SetParent( parent );

			particles.Play();
		}
#endregion

	}
}