/* Created by and for usage of FF Studios (2021). */

using UnityEngine;

namespace FFStudio
{
	public static class Vector3IntExtensions
    {
		public static int GetUniqeHashCode_PositiveIntegers( this Vector3Int v3 )
		{
			var max = Mathf.Max( v3.x, v3.y, v3.z );

			var hash = max * max * max + ( 2 * max * v3.z ) + v3.z;
			if( max == v3.z )
			{
				var xyMax = Mathf.Max( v3.x, v3.y );
				hash += xyMax * xyMax;
			}
			if( v3.y >= v3.x )
				hash += v3.x + v3.y;
			else
				hash += v3.y;

			return hash;
		}

		public static int GetUniqeHashCode_AllIntegers( this Vector3Int v3 )
		{
			v3.x = v3.x >= 0 ? 2 * v3.x : -2 * v3.x - 1;
			v3.y = v3.y >= 0 ? 2 * v3.y : -2 * v3.y - 1;
			v3.z = v3.z >= 0 ? 2 * v3.z : -2 * v3.z - 1;

			var max = Mathf.Max( v3.x, v3.y, v3.z );

			var hash = max * max * max + ( 2 * max * v3.z ) + v3.z;
			if( max == v3.z )
			{
				var xyMax = Mathf.Max( v3.x, v3.y );
				hash += xyMax * xyMax;
			}
			if( v3.y >= v3.x )
				hash += v3.x + v3.y;
			else
				hash += v3.y;

			return hash;
		}

		public static Vector3Int SetX( this Vector3Int theVector, int newX )
		{
			theVector.x = newX;
			return theVector;
		}

		public static Vector3Int SetY( this Vector3Int theVector, int newY )
		{
			theVector.y = newY;
			return theVector;
		}

		public static Vector3Int SetZ( this Vector3Int theVector, int newZ )
		{
			theVector.z = newZ;
			return theVector;
		}

		public static Vector3Int OffsetX( this Vector3Int theVector, int deltaX )
		{
			theVector.x += deltaX;
			return theVector;
		}

		public static Vector3Int OffsetY( this Vector3Int theVector, int deltaY )
		{
			theVector.y += deltaY;
			return theVector;
		}

		public static Vector3Int OffsetZ( this Vector3Int theVector, int deltaZ )
		{
			theVector.z += deltaZ;
			return theVector;
		}

		public static Vector3Int NegateX( this Vector3Int theVector )
		{
			theVector.x *= -1;
			return theVector;
		}

		public static Vector3Int NegateY( this Vector3Int theVector )
		{
			theVector.y *= -1;
			return theVector;
		}

		public static Vector3Int NegateZ( this Vector3Int theVector )
		{
			theVector.z *= -1;
			return theVector;
		}

		public static Vector3Int MakeXAbsolute( this Vector3Int theVector )
		{
			theVector.x *= ( int )Mathf.Sign( theVector.x );
			return theVector;
		}

		public static Vector3Int MakeYAbsolute( this Vector3Int theVector )
		{
			theVector.y *= ( int )Mathf.Sign( theVector.y );
			return theVector;
		}

		public static Vector3Int MakeZAbsolute( this Vector3Int theVector )
		{
			theVector.z *= ( int )Mathf.Sign( theVector.z );
			return theVector;
		}
	}
}