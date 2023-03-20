/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

public class RigidbodyFreezer : MonoBehaviour
{
#region Fields
    [ SerializeField ] Rigidbody target;
    
    RigidbodyConstraints constraints;
#endregion

#region Unity API
    void Awake()
    {
		constraints = target.constraints;
	}
#endregion

#region API
    [ Button ]
    public void FreezePositionOnly()
    {
		target.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
	}

	[ Button ]
    public void FreezePositionAppend()
	{
		target.constraints |= RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
	}

	[ Button ]
    public void FreezeRotationOnly()
	{
		target.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}

	[ Button ]
    public void FreezeRotationAppend()
	{
		target.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}

	[ Button ]
    public void UnfreezePositionOnly()
	{
		target.constraints &= ~( RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ );
	}

	[ Button ]
    public void UnfreezeRotationOnly()
	{
		target.constraints &= ~( RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ );
	}
    
    [ Button ]
    public void Unfreeze()
    {
		target.constraints = RigidbodyConstraints.None;
	}
    
    [ Button ]
    public void RevertToOriginal()
    {
		target.constraints = constraints;
	}
#endregion

#region Implementation
#endregion
}
