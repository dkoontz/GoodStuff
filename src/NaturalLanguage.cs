using System;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;

namespace GoodStuff
{
	namespace NaturalLanguage {
		public static class IntExtensions {
			/// <summary>
			/// Calls the provided callback action repeatedly.
			/// </summary>
			/// <description>
			/// Used to invoke an action a fixed number of times.
			/// 
			/// 5.Times(() => Console.WriteLine("Hey!"));
			/// 
			/// is the equivalent of
			/// 
			/// for(var i = 0; i < 5; i++) {
			///     Console.WriteLine("Hey!");
			/// }
			/// </description>
			public static void Times(this int iterations, Action callback) {
				for(var i = 0; i < iterations; ++i) {
					callback();
				}
			}
			
			/// <summary>
			/// Iterates from the start up to the given value, calling the provided callback with each value in the sequence.
			/// </summary>
			/// <description>
			/// Used to iterate from a start value to a target value
			/// 
			/// 0.UpTo(5, i => Console.WriteLine(i));
			/// 
			/// is the equivalent of
			/// 
			/// for(var i = 0; i <= 5; i++) {
			///     Console.WriteLine(i);
			/// }
			/// </description>
			public static void UpTo(this int value, int endValue, Action<int> callback) {
				for(var i = value; i <= endValue; ++i) {
					callback(i);
				}
			}
			
			/// <summary>
			/// Iterates from the start down to the given value, calling the provided callback with each value in the sequence.
			/// </summary>
			/// <description>
			/// Used to iterate from a start value to a target value
			/// 
			/// 5.DownTo(0, i => Console.WriteLine(i));
			/// 
			/// is the equivalent of
			/// 
			/// for(var i = 5; i >= 0; i++) {
			///     Console.WriteLine(i);
			/// }
			/// </description>
			public static void DownTo(this int value, int endValue, Action<int> callback) {
				for(var i = value; i >= endValue; --i) {
					callback(i);
				}
			}
		}
		
		public static class IEnumerableExtensions {
			/// <summary>
			/// Iterates over each element in the IEnumerable, passing in the element to the provided callback
			/// </summary>
			public static void Each<T>(this IEnumerable<T> iteratable, Action<T> callback) {
				foreach(var value in iteratable) {
					callback(value);
				}
			}
			
			/// <summary>
			/// Iterates over each element in the IEnumerable, passing in the element and the index to the provided callback
			/// </summary>
			public static void EachWithIndex<T>(this IEnumerable<T> iteratable, Action<T, int> callback) {
				var i = 0;
				foreach(var value in iteratable) {
					callback(value, i);
					++i;
				}
			}
		}
	}
}