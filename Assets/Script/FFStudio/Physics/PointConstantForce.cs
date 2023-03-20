/* Created by and for usage of FF Studios (#CREATION_YEAR#). */

using UnityEngine;
using FFStudio;
using Sirenix.OdinInspector;

public class PointConstantForce : MonoBehaviour
{
#region Fields
[ Title( "Properties" ) ]
    [ SerializeField ] ForceMode force_mode;
    [ SerializeField ] float force_cofactor = 1f;
    [ SerializeField ] float force;
    [ SerializeField ] float force_relative;
    
[ Title( "Setup" ) ]
    [ SerializeField ] bool startActive;
    
    TriggerMessage triggerMethod;
#endregion

#region Unity API
    void Awake()
    {
		if( startActive )
			ToggleOn();
        else
            ToggleOff();
	}
#endregion

#region API
	public void ToggleOn()  => triggerMethod = ApplyForce;
	public void ToggleOff() => triggerMethod = Extensions.EmptyMethod;

    void OnTriggerStay( Collider other )
    {
		triggerMethod.Invoke( other );
	}

    public void ChangeCofactor( float value )
    {
        force_cofactor = value;
    }
#endregion

#region Implementation
    void ApplyForce( Collider other )
    {
		var rb = other.attachedRigidbody;
		var forceDirection = ( transform.position - rb.position ).normalized;

		rb.AddForce( force * forceDirection * force_cofactor, force_mode );
		rb.AddRelativeForce( force_relative * forceDirection * force_cofactor, force_mode );
    }
#endregion
}