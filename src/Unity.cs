// This project is licensed under The MIT License (MIT)
//
// Copyright 2013 David Koontz, Logan Barnett, Corey Nolan, Alex Burley
//
//	Permission is hereby granted, free of charge, to any person obtaining a copy
//		of this software and associated documentation files (the "Software"), to deal
//		in the Software without restriction, including without limitation the rights
//		to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//		copies of the Software, and to permit persons to whom the Software is
//		furnished to do so, subject to the following conditions:
//
//		The above copyright notice and this permission notice shall be included in
//		all copies or substantial portions of the Software.
//
//		THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//		IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//		FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//		AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//		LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//		OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//		THE SOFTWARE.
//
// Please direct questions, patches, and suggestions to the project page at
// https://github.com/dkoontz/GoodStuff

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WII || UNITY_IPHONE || UNITY_ANDROID || UNITY_PS3 || UNITY_XBOX360 || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY

using UnityEngine;
using System;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace GoodStuff {
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

		public static class StringExtensions {
			public static int NameToLayerMask(this string name) {
				return 1 << LayerMask.NameToLayer(name);
			}
		}
		
		public static class TransformExtensions {
			public static void SetParentAndReset(this Transform transform, Transform parent) {
				transform.parent = parent;
				transform.localPosition = Vector3.zero;
				transform.localRotation = Quaternion.identity;
				transform.localScale = Vector3.one;
			}
		}
		
		public static class GameObjectExtensions {
			public static T GetComponentOrInterface<T>(this GameObject go) where T : class {
				return go.GetComponent(typeof(T)) as T;
			}
		}
	}
}

#endif