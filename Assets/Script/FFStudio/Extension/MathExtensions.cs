/* Created by and for usage of FF Studios (2021). */

using System.Collections.Generic;
using UnityEngine;

namespace FFStudio
{
	public static class MathExtensions
	{
		private static readonly int format_float_charA = System.Convert.ToInt32( 'a' );
		private static readonly Dictionary< int, string > format_float_units = new Dictionary< int, string >
		{
			{ 0, ""  } ,
			{ 1, "K" },
			{ 2, "M" },
			{ 3, "B" },
			{ 4, "T" }
		};

		public static string FormatBigNumberAANotation( double value, string format = "0.##" )
		{
			if( value < 1d )
			{
				return "0";
			}

			var n = ( int )System.Math.Log( value, 1000 );
			var m = value / System.Math.Pow( 1000, n );
			var unit = "";

			if( n < format_float_units.Count )
			{
				unit = format_float_units[ n ];
			}
			else
			{
				var unitInt = n - format_float_units.Count;
				var secondUnit = unitInt % 26;
				var firstUnit = unitInt / 26;
				unit = System.Convert.ToChar( firstUnit + format_float_charA ).ToString() + System.Convert.ToChar( secondUnit + format_float_charA ).ToString();
			}

			// Math.Floor(m * 100) / 100) fixes rounding errors
			return ( System.Math.Floor( m * 100 ) / 100 ).ToString( format ) + unit;
		}

		public static Vector2 ConvertToDirection( this float unsignedAngle )
		{
			switch( ( int )unsignedAngle )
			{
				case 0: return Vector2.up;
				case 90: return Vector2.right;
				case 180: return Vector2.down;
				case 270: return Vector2.left;

				default: return Vector2.zero;
			}
		}
		
		public static float RoundTo( this float number, float step )
		{
			int   quotient = Mathf.FloorToInt( number / step );
			var   reminder = number % step;
			float rounded  = quotient * step;

			if( reminder >= step / 2f )
				rounded += step;

			return rounded;
		}

		public static float Hypotenuse( float edge1, float edge2 )
		{
			return Mathf.Sqrt( edge1 * edge1 + edge2 * edge2 );
		}

		public static float NonHypothenuseEdge( float hypotenuseEdge, float edge1 )
		{
			return Mathf.Sqrt( hypotenuseEdge * hypotenuseEdge - edge1 * edge1 );
		}

		public static float Remap( float originalFrom, float originalTo, float value, float targetFrom, float targetTo )
		{
			return Mathf.Lerp( targetFrom, targetTo, Mathf.InverseLerp( originalFrom, originalTo, value ) );
		}
	}
}