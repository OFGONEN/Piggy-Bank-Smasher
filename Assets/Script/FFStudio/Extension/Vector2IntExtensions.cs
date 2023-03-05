/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public static class Vector2IntExtensions
    {
		public static int GetUniqueHashCode_PositiveIntegers( this Vector2Int v2 )
		{
			if( Mathf.Max( v2.x, v2.y ) == v2.x )
				return v2.x * v2.x + v2.x + v2.y;
			else
				return v2.x + v2.y * v2.y;
		}

		public static Vector3 ConvertToVector3( this Vector2Int v2 )
		{
			return new Vector3( v2.x, v2.y, 0 );
		}

		public static Vector2Int Clamp( this Vector2Int value, Vector2Int min, Vector2Int max )
		{
			value.x = Mathf.Clamp( value.x, min.x, max.x );
			value.y = Mathf.Clamp( value.y, min.y, max.y );
			return value;
		}

		public static Vector2Int SetX( this Vector2Int theVector, int newX )
		{
			theVector.x = newX;
			return theVector;
		}

		public static Vector2Int SetY( this Vector2Int theVector, int newY )
		{
			theVector.y = newY;
			return theVector;
		}

		public static Vector2Int OffsetX( this Vector2Int theVector, int delta )
		{
			theVector.x = theVector.x + delta;
			return theVector;
		}

		public static Vector2Int OffsetY( this Vector2Int theVector, int delta )
		{
			theVector.y = theVector.y + delta;
			return theVector;
		}

		public static Vector2Int MultiplyX( this Vector2Int theVector, int delta )
		{
			theVector.x *= delta;
			return theVector;
		}

		public static Vector2Int MultiplyY( this Vector2Int theVector, int delta )
		{
			theVector.y *= delta;
			return theVector;
		}

		public static int ReturnRandom( this Vector2Int vector )
		{
			return Random.Range( vector.x, vector.y );
		}

		public static float ReturnProgress( this Vector2Int vector, float progress )
		{
			return Mathf.Lerp( vector.x, vector.y, progress );
		}

		public static float ReturnProgressInverse( this Vector2Int vector, float progress )
		{
			return Mathf.Lerp( vector.y, vector.x, progress );
		}

		public static float ReturnClamped( this Vector2Int vector, float value )
		{
			return Mathf.Clamp( value, vector.x, vector.y );
		}
	}
}