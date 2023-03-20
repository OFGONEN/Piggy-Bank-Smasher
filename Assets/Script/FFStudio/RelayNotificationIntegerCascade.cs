/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using UnityEngine.Events;

namespace FFStudio
{
	public class RelayNotificationIntegerCascade : RelayNotification< SharedIntNotifier, int >
    {
		public override bool HideBaseClassOnNotifyEvent => true;

		[ System.Serializable ]
        public struct Cascade
        {
			public int threshold;
			public UnityEvent< int > onEqual;
		}
        
        [ SerializeField ] Cascade[] cascades;
        
		public override void OnSharedDataChange()
		{
            for( var i = 0; i < cascades.Length; i++ )
			    if( sharedDataNotifier.sharedValue == cascades[ i ].threshold )
					cascades[ i ].onEqual.Invoke( sharedDataNotifier.sharedValue );
		}
	}
}