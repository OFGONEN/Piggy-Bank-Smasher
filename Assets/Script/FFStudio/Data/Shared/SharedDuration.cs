/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	[ CreateAssetMenu( fileName = "shared_duration_", menuName = "FF/Data/Shared/Duration" ) ]
	public class SharedDuration : SharedFloat
	{
#if UNITY_EDITOR
		protected override string Suffix() => "seconds";
#endif
	}
}