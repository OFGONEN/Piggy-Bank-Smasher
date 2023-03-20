/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public static class Vector2Extensions
    {
		public static Vector3 ConvertToVector3( this Vector2 v2 )
		{
			return new Vector3( v2.x, v2.y, 0 );
		}

		public static Vector3 ConvertToVector3( this Vector2 v2, float z )
		{
			return new Vector3( v2.x, v2.y, z );
		}

		public static Vector2 Clamp( this Vector2 value, Vector2 min, Vector2 max )
		{
			value.x = Mathf.Clamp( value.x, min.x, max.x );
			value.y = Mathf.Clamp( value.y, min.y, max.y );
			return value;
		}

		public static Vector2 SetX( this Vector2 theVector, float newX )
		{
			theVector.x = newX;
			return theVector;
		}

		public static Vector2 SetY( this Vector2 theVector, float newY )
		{
			theVector.y = newY;
			return theVector;
		}

		public static Vector2 OffsetX( this Vector2 theVector, float delta )
		{
			theVector.x = theVector.x + delta;
			return theVector;
		}

		public static Vector2 OffsetY( this Vector2 theVector, float delta )
		{
			theVector.y = theVector.y + delta;
			return theVector;
		}

		public static Vector2 MultiplyX( this Vector2 theVector, float delta )
		{
			theVector.x *= delta;
			return theVector;
		}

		public static Vector2 MultiplyY( this Vector2 theVector, float delta )
		{
			theVector.y *= delta;
			return theVector;
		}

		public static float ReturnRandom( this Vector2 vector )
		{
			return Random.Range( vector.x, vector.y );
		}

		public static float ReturnProgress( this Vector2 vector, float progress )
		{
			return Mathf.Lerp( vector.x, vector.y, progress );
		}

		public static float ReturnProgressInverse( this Vector2 vector, float progress )
		{
			return Mathf.Lerp( vector.y, vector.x, progress );
		}

		public static float ReturnClamped( this Vector2 vector, float value )
		{
			return Mathf.Clamp( value, vector.x, vector.y );
		}
	}
}