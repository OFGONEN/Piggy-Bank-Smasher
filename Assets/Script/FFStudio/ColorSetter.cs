/* Created by and for usage of FF Studios (2021). */

using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using System.Collections.Generic;

namespace FFStudio
{
	public class ColorSetter : MonoBehaviour
	{
#region Fields
		[ TitleGroup( "Setup" ), SerializeField ] Renderer theRenderer;
		static int SHADER_ID_COLOR = Shader.PropertyToID( "_BaseColor" );

		MaterialPropertyBlock propertyBlock;
#endregion

#region Properties
#endregion

#region Unity API
		void Awake()
		{
			propertyBlock = new MaterialPropertyBlock();
		}
#endregion

#region API
		public void SetStartColors()
		{
			for( var i = 0; i < theRenderer.sharedMaterials.Length; i++ )
				SetColor( theRenderer.sharedMaterials[ i ].color, i );
		}

		[ Button() ]
		public void SetColor( Color color, int index )
		{
			theRenderer.GetPropertyBlock( propertyBlock, index );
			propertyBlock.SetColor( SHADER_ID_COLOR, color );
			theRenderer.SetPropertyBlock( propertyBlock, index );
		}

		public void SetColor( Color color )
		{
			theRenderer.GetPropertyBlock( propertyBlock );
			propertyBlock.SetColor( SHADER_ID_COLOR, color );
			theRenderer.SetPropertyBlock( propertyBlock );
		}

		public void LerpAllColors( float ratio, Color target )
		{
			for( var i = 0; i < theRenderer.sharedMaterials.Length; i++ )
				SetColor( Color.Lerp( theRenderer.sharedMaterials[ i ].color, target, ratio ), i );
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