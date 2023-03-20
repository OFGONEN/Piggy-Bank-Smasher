/* Created by and for usage of FF Studios (2021). */

using Lean.Touch;
using UnityEngine;

namespace FFStudio
{
	public delegate void ChangeEvent();
	public delegate void DrawShape( Camera cam );
    public delegate void TriggerMessage( Collider other );
	public delegate void TriggerMessage2D( Collider2D other );
    public delegate void CollisionMessage( Collision collision );
	public delegate void CollisionMessage2D( Collision2D collision );
	public delegate void UnityMessage();
	public delegate void OnCompleteMessage();
	public delegate void LeanFingerDelegate( LeanFinger finger );
	public delegate void Vector2Delegate( Vector2 vector );
	public delegate void Vector3Delegate( Vector3 vector );
	public delegate void ParticleEffectStopped( ParticleEffect effect );
}
