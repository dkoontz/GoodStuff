using System;
using System.Collections;
using System.Collections.Generic;

namespace GoodStuff
{
	namespace NaturalLanguage {
		public delegate void TwoParamDelegate<T>(T value, int i);
		
		public static class IntExtensions {
			/// <summary>
			/// Calls the provided callback action repeatedly.
			/// </summary>
			/// <description>
			/// Used to replace traditional for loop iteration.
			/// 5.Times(i => Console.WriteLine(i));
			/// 
			/// is the equivalent of
			/// 
			/// for(var i = 0; i < 5; i++) {
			///     Console.WriteLine(i);
			/// }
			/// </description>
			
			public static void Times(this int iterations, Action<int> callback) {
				for(var i = 0; i < iterations; ++i) {
					callback(i);
				}
			}
		}
		
		
		
		public static class IEnumerableExtensions {
			public static void Each<T>(this IEnumerable<T> iteratable, Action<T> callback) {
				foreach(var value in iteratable) {
					callback(value);
				}
			}
			
			public static void EachWithIndex<T>(this IEnumerable<T> iteratable, TwoParamDelegate<T> callback) {
				var i = 0;
				foreach(var value in iteratable) {
					callback(value, i);
					++i;
				}
			}
		}
	}
}