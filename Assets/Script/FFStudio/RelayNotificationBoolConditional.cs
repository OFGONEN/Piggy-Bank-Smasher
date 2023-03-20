/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public class RelayNotificationBoolConditional : RelayNotification< SharedBoolNotifier, bool >
    {
#if UNITY_EDITOR
		[ InfoBox( "Unity Event will only be invoked if the shared data becomes TRUE", "value_target" ) ]
		[ InfoBox( "Unity Event will only be invoked if the shared data becomes FALSE", "ValueToLookForIsFalse" ) ]
#endif
		[ SerializeField, LabelText( "Target Value" ), Tooltip( "Unity Event will only be invoked when the shared data is equal to this value" ) ] bool value_target;
		
		public override void OnSharedDataChange()
		{
            if( sharedDataNotifier.sharedValue == value_target )
				notify_event.Invoke( sharedDataNotifier.sharedValue );
		}

#if UNITY_EDITOR
		bool ValueToLookForIsFalse() => !value_target;
#endif
	}
}