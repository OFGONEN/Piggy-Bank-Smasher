/* Created by and for usage of FF Studios (2021). */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace FFStudio
{
	[ System.Serializable ]
	public abstract class EventListenerUnityEventResponseBase : EventListener
	{
		public bool isEnabled = true;
		public bool isDelayed = false;
		[ ShowIf( "isDelayed" ) ] public float delayDuration = 0;

		protected Cooldown cooldown = new Cooldown();
		protected UnityMessage onEventRaised;
	}

	public abstract class GenericEventListenerUnityEventResponse< GameEventType > : EventListenerUnityEventResponseBase where GameEventType: GameEvent
	{
		public GameEventType gameEvent;

		public override void OnEnable()
		{
			if( isEnabled )
				gameEvent.RegisterListener( this );
			
			if( isDelayed )
				onEventRaised = OnEventResponseDelayed;
			else
				onEventRaised = OnEventResponse;
		}

		public override void OnDisable()
		{
			gameEvent.UnregisterListener( this );
		}

		public override void OnEventRaised()
		{
			onEventRaised();
		}

		protected void OnEventResponseDelayed()
		{
			cooldown.Start( delayDuration, OnEventResponse );
		}

		protected abstract void OnEventResponse();
	}

	public class BasicGameEventResponse : GenericEventListenerUnityEventResponse< GameEvent >
	{
		public UnityEvent unityEvent;

		protected override void OnEventResponse()
		{
			unityEvent.Invoke();
		}
	}

	public abstract class GenericEventListenerGenericUnityEventResponse< GameEventType, ArgumentType  > : GenericEventListenerUnityEventResponse< GameEventType > where GameEventType : GameEvent 
	{
		public UnityEvent< ArgumentType > unityEvent;
	}

	public class BoolGameEventResponse : GenericEventListenerGenericUnityEventResponse< BoolGameEvent, bool >
	{
		protected override void OnEventResponse()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}

	public class IntGameEventResponse : GenericEventListenerGenericUnityEventResponse< IntGameEvent, int >
	{
		protected override void OnEventResponse()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}

	public class FloatGameEventResponse : GenericEventListenerGenericUnityEventResponse< FloatGameEvent, float >
	{
		protected override void OnEventResponse()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}

	public class ReferenceGameEventResponse : GenericEventListenerGenericUnityEventResponse< ReferenceGameEvent, object >
	{
		protected override void OnEventResponse()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}

	public class Vector2GameEventResponse : GenericEventListenerGenericUnityEventResponse< Vector2GameEvent, Vector2 >
	{
		protected override void OnEventResponse()
		{
			unityEvent.Invoke( gameEvent.eventValue );
		}
	}
}