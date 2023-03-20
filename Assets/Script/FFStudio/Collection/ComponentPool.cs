/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace FFStudio
{
	public abstract class ComponentPool< T > : RuntimeStack< T > where T: Component
	{
#region Fields
		[ AssetSelector ] public T pool_entity;

		protected Transform pool_parent;
		protected bool pool_active;
#if UNITY_EDITOR
		int count_spawned;
#endif
#endregion

#region Properties
#endregion

#region Unity API
#endregion

#region API
		public virtual void InitPool( Transform parent, bool active )
		{
			pool_parent = parent;
			pool_active = active;
#if UNITY_EDITOR
			count_spawned = 0;
#endif
			stack = new Stack< T >( stackSize );

            for( var i = 0; i < stackSize; i++ )
			{
				var entity = InitEntity();
				stack.Push( entity );
			}
		}

		public virtual T GetEntity()
		{
			T entity;

			if( stack.Count > 0 )
				entity = stack.Pop();
			else
			{
				entity = InitEntity();
				FFLogger.Log( "New Entity created.", this );
			}

			return entity;
		}

		public virtual void ReturnEntity( T entity )
        {
			entity.gameObject.SetActive( pool_active );
			entity.transform.SetParent( pool_parent );
			stack.Push( entity );
		}
#endregion

#region Implementation
		protected virtual T InitEntity()
        {
            var entity = GameObject.Instantiate( pool_entity );
			entity.gameObject.SetActive( pool_active );
			entity.transform.SetParent( pool_parent );
#if UNITY_EDITOR
			entity.name += "_" + ++count_spawned;
#endif
			return entity;
		}
#endregion
	}
}