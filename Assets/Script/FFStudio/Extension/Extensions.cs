/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace FFStudio
{
	public static class Extensions
	{
		public static readonly string SAVE_PATH = Application.persistentDataPath + "/Saves/";

		static List< Transform > baseModelBones   = new List< Transform >( 96 );
		static List< Transform > targetModelBones = new List< Transform >( 96 );

		public static bool FindSameColor( this List< Color > colors, Color color )
		{
			bool hasColor = false;

			for( int i = 0; i < colors.Count; i++ )
				hasColor |= colors[ i ].CompareColor( color );

			return hasColor;
		}

		public static bool FindSameColor( this List< Color > colors, Color color, out int index )
		{
			bool hasColor = false;
			index = -1;

			for( int i = 0; i < colors.Count; i++ )
			{
				hasColor |= colors[ i ].CompareColor( color );

				if( hasColor && index == -1 )
					index = i;
			}

			return hasColor;
		}

		public static bool CompareColor( this Color colorOne, Color colorTwo )
		{
			bool sameColor = true;

			sameColor &= Mathf.Abs( colorOne.r - colorTwo.r ) <= 0.01f;
			sameColor &= Mathf.Abs( colorOne.g - colorTwo.g ) <= 0.01f;
			sameColor &= Mathf.Abs( colorOne.b - colorTwo.b ) <= 0.01f;
			sameColor &= Mathf.Abs( colorOne.a - colorTwo.a ) <= 0.01f;

			return sameColor;
		}

		public static T ReturnLastItem< T >( this List< T > list )
		{
			var lastIndex = list.Count - 1;

			T item = list[ lastIndex ];
			list.RemoveAt( lastIndex );

			return item;
		}

		public static void LookAtOverTime( this Transform baseTransform, Vector3 targetPosition, float speed )
		{
			var directionVector = targetPosition - baseTransform.position;
			var step            = speed * Time.deltaTime;

			Vector3 newDirection = Vector3.RotateTowards( baseTransform.forward, directionVector, step, 0.0f );

			baseTransform.rotation = Quaternion.LookRotation( newDirection );
		}

		public static void LookAtAxis( this Transform baseTransform, Vector3 targetPosition, Vector3 axis )
		{
			var newDirection     = targetPosition - baseTransform.position;
			var eulerAngles      = baseTransform.eulerAngles;
			var newRotationEuler = Quaternion.LookRotation( newDirection ).eulerAngles;

			newRotationEuler.x = eulerAngles.x + ( newRotationEuler.x - eulerAngles.x ) * axis.x;
			newRotationEuler.y = eulerAngles.y + ( newRotationEuler.y - eulerAngles.y ) * axis.y;
			newRotationEuler.z = eulerAngles.z + ( newRotationEuler.z - eulerAngles.z ) * axis.z;

			baseTransform.rotation = Quaternion.Euler( newRotationEuler );
		}

		public static void LookAtAxis( this Transform baseTransform, Vector3 targetPosition, Vector3 axis, float directionCofactor )
		{
			var newDirection     = targetPosition - baseTransform.position;
			var eulerAngles      = baseTransform.eulerAngles;
			var newRotationEuler = Quaternion.LookRotation( newDirection * directionCofactor ).eulerAngles;

			newRotationEuler.x = eulerAngles.x + ( newRotationEuler.x - eulerAngles.x ) * axis.x;
			newRotationEuler.y = eulerAngles.y + ( newRotationEuler.y - eulerAngles.y ) * axis.y;
			newRotationEuler.z = eulerAngles.z + ( newRotationEuler.z - eulerAngles.z ) * axis.z;

			// baseTransform.rotation = Quaternion.LookRotation( newDirection );
			baseTransform.rotation = Quaternion.Euler( newRotationEuler );
		}

		public static void LookAtOverTimeAxis( this Transform baseTransform, Vector3 targetPosition, Vector3 axis, float speed )
		{

			var directionVector = targetPosition - baseTransform.position;
			var step             = speed * Time.deltaTime;

			Vector3 newDirection = Vector3.RotateTowards( baseTransform.forward, directionVector, step, 0.0f );

			var eulerAngles = baseTransform.eulerAngles;

			var newRotationEuler = Quaternion.LookRotation( newDirection ).eulerAngles;

			newRotationEuler.x = eulerAngles.x + ( newRotationEuler.x - eulerAngles.x ) * axis.x;
			newRotationEuler.y = eulerAngles.y + ( newRotationEuler.y - eulerAngles.y ) * axis.y;
			newRotationEuler.z = eulerAngles.z + ( newRotationEuler.z - eulerAngles.z ) * axis.z;

			// baseTransform.rotation = Quaternion.LookRotation( newDirection );
			baseTransform.rotation = Quaternion.Euler( newRotationEuler );
		}

		public static void LookAtDirectionOverTime( this Transform baseTransform, Vector3 direction, float speed )
		{
			Vector3 newDirection = Vector3.RotateTowards( baseTransform.forward, direction, speed * Time.deltaTime, 0.0f );

			baseTransform.rotation = Quaternion.LookRotation( newDirection );
		}

		public static void EmptyMethod()
		{
			/* Intentionally empty, by definition. */
		}
		
		public static void EmptyMethod( Vector2 vector2 )
		{
			/* Intentionally empty, by definition. */
		}
		
		public static void EmptyMethod( Lean.Touch.LeanFinger leanFinger )
		{
			/* Intentionally empty, by definition. */
		}

		public static void EmptyMethod( Camera camera )
		{
			/* Intentionally empty, by definition. */
		}

		public static TransformData GetTransformData( this Transform transform )
		{
			TransformData data;
			data.position = transform.position;
			data.rotation = transform.eulerAngles;
			data.scale    = transform.localScale;

			return data;
		}

		public static void SetTransformData( this Transform transform, TransformData data )
		{
			transform.position    = data.position;
			transform.eulerAngles = data.rotation;
			transform.localScale  = data.scale;
		}

		public static TransformData GetLocalTransformData( this Transform transform )
		{
			TransformData data;
			data.position = transform.localPosition;
			data.rotation = transform.localEulerAngles;
			data.scale = transform.localScale;

			return data;
		}

		public static void SetLocalTransformData( this Transform transform, TransformData data )
		{
			transform.localPosition = data.position;
			transform.localEulerAngles = data.rotation;
			transform.localScale = data.scale;
		}

		// Takes root bones as parameters that are children of a humanoid model.
		public static void ReplaceHumanoidModel( this Transform baseBone, Transform targetBone )
		{
			baseModelBones.Clear();
			targetModelBones.Clear();

			baseBone.GetComponentsInChildren< Transform >( true, baseModelBones );
			targetBone.GetComponentsInChildren< Transform >( true, targetModelBones );

			targetBone.parent.position = baseBone.parent.position;

			for( var bone = 0; bone < baseModelBones.Count; bone++ )
			{
				targetModelBones[ bone ].position = baseModelBones[ bone ].position;
				targetModelBones[ bone ].rotation = baseModelBones[ bone ].rotation;
			}
		}

		public static void UpdateSkinnedMeshRenderer( this GameObject gameObject, SkinnedMeshRenderer currentRender, SkinnedMeshRenderer newRenderer )
        {
            currentRender.sharedMesh      = newRenderer.sharedMesh;
            currentRender.sharedMaterials = newRenderer.sharedMaterials;
            currentRender.localBounds     = newRenderer.localBounds;

            baseModelBones.Clear();
            targetModelBones.Clear();

            gameObject.GetComponentsInChildren< Transform >( true, baseModelBones );

			for( int boneOrder = 0; boneOrder < newRenderer.bones.Length; boneOrder++ )
                targetModelBones.Add( baseModelBones.Find( c => c.name == newRenderer.bones[ boneOrder ].name ) );

            currentRender.bones = targetModelBones.ToArray();
        }

		public static void SetFieldValue( this object source, string fieldName, string value )
		{
			var fieldInfo = source.GetType().GetField( fieldName );

			if( fieldInfo == null )
				return;

			var fieldType = fieldInfo.FieldType;

			if( fieldType == typeof( int ) )
				fieldInfo.SetValue( source, int.Parse( value ) );
			else if( fieldType == typeof( float ) )
				fieldInfo.SetValue( source, float.Parse( value, CultureInfo.InvariantCulture ) );
			else if( fieldType == typeof( string ) )
				fieldInfo.SetValue( source, value );
			else if( fieldType == typeof( bool ) )
			{
				fieldInfo.SetValue( source, bool.Parse( value ) );
				FFLogger.Log( "Setting Bool: " + fieldName + " Value: " + value );
			}
			else
			{
				fieldInfo.SetValue( source, JsonUtility.FromJson( value, fieldType ));
				FFLogger.Log( "Setting Json: " + fieldName + " Value: " + value );
			}
		}

		public static DG.Tweening.Sequence KillProper( this DG.Tweening.Sequence sequence )
		{
			if( sequence != null )
			{
				sequence.Kill();
				sequence = null;
			}

			return sequence;
		}

		public static DG.Tweening.Tween KillProper( this DG.Tweening.Tween tween )
		{
			if( tween != null )
			{
				tween.Kill();
				tween = null;
			}

			return tween;
		}

		public static Color SetAlpha( this Color color, float alpha )
		{
			Color newColor = color;
			newColor.a = alpha;

			return newColor;
		}

		public static void SetAlpha( this Image image, float alpha )
		{
			Color newColor = image.color;
			newColor.a = alpha;

			image.color = newColor;
		}

		public static void SetAlpha( this TextMeshProUGUI textRenderer, float alpha )
		{
			Color newColor = textRenderer.color;
			newColor.a = alpha;

			textRenderer.color = newColor;
		}

		public static T ReturnRandom< T >( this T[] array )
		{
			return array[ Random.Range( 0, array.Length ) ];
		}

		public static void DestroyAllChildren( this Transform transform )
		{
			var childCount = transform.childCount;
			var childs = new List< Transform >( transform.childCount );

			for( var i = 0; i < childCount; i++ )
				childs.Add( transform.GetChild( i ) );
			
			for( var i = 0; i < childCount; i++ )
				GameObject.DestroyImmediate( childs[ i ].gameObject );
		}

		public static void ToggleKinematic( this Rigidbody rigidbody, bool value )
		{
			rigidbody.isKinematic = value;
			rigidbody.useGravity  = !value;
		}

		public static void Swap<T>( ref T parameter1, ref T parameter2 )
		{
			T temp = parameter1;
			parameter1 = parameter2;
			parameter2 = temp;
		}

#if FF_OBI_IMPORTED
		public static void MergeParticles( this Obi.ObiRope obiRope, int indexOfElementBefore, int indexOfElementOfInterest )
		{
			var solver = obiRope.solver;

			var previousElement = obiRope.elements[ indexOfElementBefore ];
			var elementOfInterest = obiRope.elements[ indexOfElementOfInterest ];

			solver.invMasses[ previousElement.particle2 ] /= 2; // Revert previous halving of the particle mass.

			obiRope.DeactivateParticle( solver.particleToActor[ elementOfInterest.particle1 ].indexInActor );

			elementOfInterest.particle1 = previousElement.particle2;
		}
#endif
	}
}