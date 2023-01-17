/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	public class RunTimeVector2Data : RunTimeData<Vector2>
	{
#region Fields
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
		public override void OffsetData( Vector2 value )
		{
			runTimeData += value;
			onUpdateEvent.Invoke( runTimeData );
		}

		public override void CompareData( Vector2 value )
		{
			if( runTimeData == value )
				onComparisonEvent.Invoke( runTimeData );
		}
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}