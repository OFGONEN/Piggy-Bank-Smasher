/* Created by and for usage of FF Studios (2023). */

using UnityEngine;

namespace FFStudio
{
	public static class Vector4Extensions
	{
		public static Vector4 SetX( this Vector4 theVector, float newX )
		{
			theVector.x = newX;
			return theVector;
		}

		public static Vector4 SetY( this Vector4 theVector, float newY )
		{
			theVector.y = newY;
			return theVector;
		}

		public static Vector4 SetZ( this Vector4 theVector, float newZ )
		{
			theVector.z = newZ;
			return theVector;
		}

		public static Vector4 SetW( this Vector4 theVector, float newW )
		{
			theVector.w = newW;
			return theVector;
		}

		public static Vector4 OffsetX( this Vector4 theVector, float deltaX )
		{
			theVector.x += deltaX;
			return theVector;
		}

		public static Vector4 OffsetY( this Vector4 theVector, float deltaY )
		{
			theVector.y += deltaY;
			return theVector;
		}

		public static Vector4 OffsetZ( this Vector4 theVector, float deltaZ )
		{
			theVector.z += deltaZ;
			return theVector;
		}

		public static Vector4 OffsetW( this Vector4 theVector, float deltaW )
		{
			theVector.w += deltaW;
			return theVector;
		}

		public static Vector4 NegateX( this Vector4 theVector )
		{
			theVector.x *= -1;
			return theVector;
		}

		public static Vector4 NegateY( this Vector4 theVector )
		{
			theVector.y *= -1;
			return theVector;
		}

		public static Vector4 NegateZ( this Vector4 theVector )
		{
			theVector.z *= -1;
			return theVector;
		}

		public static Vector4 NegateW( this Vector4 theVector )
		{
			theVector.w *= -1;
			return theVector;
		}

		public static Vector4 MakeXAbsolute( this Vector4 theVector )
		{
			theVector.x *= Mathf.Sign( theVector.x );
			return theVector;
		}

		public static Vector4 MakeYAbsolute( this Vector4 theVector )
		{
			theVector.y *= Mathf.Sign( theVector.y );
			return theVector;
		}

		public static Vector4 MakeZAbsolute( this Vector4 theVector )
		{
			theVector.z *= Mathf.Sign( theVector.z );
			return theVector;
		}

		public static Vector4 MakeWAbsolute( this Vector4 theVector )
		{
			theVector.w *= Mathf.Sign( theVector.w );
			return theVector;
		}
	}
}