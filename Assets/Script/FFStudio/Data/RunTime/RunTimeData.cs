/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FFStudio
{
	public abstract class RunTimeData< DataType > : MonoBehaviour
	{
#region Fields
		public DataType runTimeData;
		public UnityEvent< DataType > onUpdateEvent;
		public UnityEvent< DataType > onComparisonEvent;
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
        public virtual void SetData( DataType value )
        {
            runTimeData = value;
			onUpdateEvent.Invoke( runTimeData );
		}

		public abstract void OffsetData( DataType value );
		public abstract void CompareData( DataType value );
#endregion

#region Implementation
#endregion

#region Editor Only
#if UNITY_EDITOR
#endif
#endregion
	}
}