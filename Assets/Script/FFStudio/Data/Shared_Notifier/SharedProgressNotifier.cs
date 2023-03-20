	/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using FFStudio;

[ CreateAssetMenu( fileName = "notif_progress_", menuName = "FF/Data/Shared/Notifier/Progress" ) ]
public class SharedProgressNotifier : SharedFloatNotifier
{
#region Fields
    float numerator = 0, denominator = 100;
#endregion

#region Properties
	public float Numerator   => numerator;
	public float Denominator => denominator;
#endregion

#region Unity API
	void Reset()
	{
		numerator = 0;
		denominator = 100;
	}
#endregion

#region API
    public void SetDenominator( float denominator )
    {
#if UNITY_EDITOR
		if( Mathf.Approximately( denominator, 0 ) )
		{
			FFLogger.LogWarning( name + ": Denominator can not be zero.", this );
			denominator = 1;
			return;
		}
#endif

		this.denominator = denominator;
	}
    
    public void SetNumerator( float numerator )
    {
		this.numerator = numerator;

#if UNITY_EDITOR
		if( Mathf.Approximately( denominator, 0 ) )
		{
			FFLogger.LogWarning( name + ": Denominator was zero. Setting it to 1 for now. Please inspect your code.", this );
			denominator = 1;
			return;
		}
#endif

		SharedValue = numerator / denominator;
	}

	public void Complete()
	{
		SetNumerator( denominator );
	}
#endregion

#region Implementation
#endregion
}
