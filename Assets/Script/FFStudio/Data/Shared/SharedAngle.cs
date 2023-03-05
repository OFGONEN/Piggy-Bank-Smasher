/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	[ CreateAssetMenu( fileName = "shared_angle_", menuName = "FF/Data/Shared/Angle (Degrees)" ) ]
	public class SharedAngle : SharedFloat
	{
#if UNITY_EDITOR
		protected override string Suffix() => "°";
#endif
	}
}