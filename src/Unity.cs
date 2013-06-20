using UnityEngine;
using System;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace GoodStuff
{
	namespace Unity {
		public static class Vector3Extensions {
			/// <summary>
			/// Returns a Vector2 comprised of the x and y components of the Vector3
			/// </summary>
			public static Vector2 XY(this Vector3 vector) {
				return new Vector2(vector.x, vector.y);
			}

			/// <summary>
			/// Returns a Vector2 comprised of the x and z components of the Vector3
			/// </summary>
			public static Vector2 XZ(this Vector3 vector) {
				return new Vector2(vector.x, vector.z);
			}

			/// <summary>
			/// Returns a Vector2 comprised of the y and z components of the Vector3
			/// </summary>
			public static Vector2 YZ(this Vector3 vector) {
				return new Vector2(vector.y, vector.z);
			}
		}
	}
}