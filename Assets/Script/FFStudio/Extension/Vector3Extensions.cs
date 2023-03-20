/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public static class Vector3Extensions
    {
		public static Vector3Int ConvertToVector3Int( this Vector3 v3 )
		{
			return new Vector3Int( Mathf.RoundToInt( v3.x ), Mathf.RoundToInt( v3.y ), Mathf.RoundToInt( v3.z ) );
		}

		public static Vector3 RandomPointBetween( this Vector3 first, Vector3 second )
		{
			return Vector3.Lerp( first, second, Random.Range( 0f, 1f ) );
		}

		public static Hash128 GetQuantizedHash( this Vector3 v3 )
		{
			Hash128 hash128 = new Hash128();
			HashUtilities.QuantisedVectorHash( ref v3, ref hash128 );
			return hash128;
		}
		
		public static Vector3 Clamp( this Vector3 value, Vector3 min, Vector3 max )
		{
			value.x = Mathf.Clamp( value.x, min.x, max.x );
			value.y = Mathf.Clamp( value.y, min.y, max.y );
			value.z = Mathf.Clamp( value.z, min.z, max.z );
			return value;
		}

		public static Vector3 ClampXY( this Vector3 value, Vector2 min, Vector2 max )
		{
			value.x = Mathf.Clamp( value.x, min.x, max.x );
			value.y = Mathf.Clamp( value.y, min.y, max.y );
			return value;
		}

		public static Vector3 ClampXZ( this Vector3 value, Vector2 min, Vector2 max )
		{
			value.x = Mathf.Clamp( value.x, min.x, max.x );
			value.z = Mathf.Clamp( value.z, min.y, max.y );
			return value;
		}

		public static Vector3 ClampYZ( this Vector3 value, Vector2 min, Vector2 max )
		{
			value.y = Mathf.Clamp( value.y, min.x, max.x );
			value.z = Mathf.Clamp( value.z, min.y, max.y );
			return value;
		}

		public static Vector3 SetX( this Vector3 theVector, float newX )
		{
			theVector.x = newX;
			return theVector;
		}

		public static Vector3 SetY( this Vector3 theVector, float newY )
		{
			theVector.y = newY;
			return theVector;
		}

		public static Vector3 SetZ( this Vector3 theVector, float newZ )
		{
			theVector.z = newZ;
			return theVector;
		}

		public static Vector3 OffsetX( this Vector3 theVector, float deltaX )
		{
			theVector.x += deltaX;
			return theVector;
		}

		public static Vector3 OffsetY( this Vector3 theVector, float deltaY )
		{
			theVector.y += deltaY;
			return theVector;
		}

		public static Vector3 OffsetZ( this Vector3 theVector, float deltaZ )
		{
			theVector.z += deltaZ;
			return theVector;
		}

		public static Vector3 NegateX( this Vector3 theVector )
		{
			theVector.x *= -1;
			return theVector;
		}

		public static Vector3 NegateY( this Vector3 theVector )
		{
			theVector.y *= -1;
			return theVector;
		}

		public static Vector3 NegateZ( this Vector3 theVector )
		{
			theVector.z *= -1;
			return theVector;
		}

		public static Vector3 MakeXAbsolute( this Vector3 theVector )
		{
			theVector.x *= Mathf.Sign( theVector.x );
			return theVector;
		}

		public static Vector3 MakeYAbsolute( this Vector3 theVector )
		{
			theVector.y *= Mathf.Sign( theVector.y );
			return theVector;
		}

		public static Vector3 MakeZAbsolute( this Vector3 theVector )
		{
			theVector.z *= Mathf.Sign( theVector.z );
			return theVector;
		}

		public static Vector3 Absolute( this Vector3 theVector )
		{
			theVector.x *= Mathf.Sign( theVector.x );
			theVector.y *= Mathf.Sign( theVector.y );
			theVector.z *= Mathf.Sign( theVector.z );
			return theVector;
		}

		public static float ComponentSum( this Vector3 theVector )
		{
			return theVector.x + theVector.y + theVector.z;
		}
    }
}