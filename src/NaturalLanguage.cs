using System;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace GoodStuff
{
	namespace NaturalLanguage {
		
		public delegate bool Predicate<T1, T2>(T1 item1, T2 item2);
		
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
			/// Iterates from the start up to the given end value inclusive, calling the provided callback with each value in the sequence.
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
			/// Iterates from the start down to the given end value inclusive, calling the provided callback with each value in the sequence.
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
		
		public static class FloatExtensions {
			/// <summary>
			/// Maps a value in one range to the equivalent value in another range.
			/// </summary>
			public static float MapToRange(this float value, float range1Min, float range1Max, float range2Min, float range2Max) {
				return MapToRange(value, range1Min, range1Max, range2Min, range2Max, true);
			}
				
			/// <summary>
			/// Maps a value in one range to the equivalent value in another range.  Clamps the value to be valid within the range if clamp is specified as true.
			/// </summary>
			public static float MapToRange(this float value, float range1Min, float range1Max, float range2Min, float range2Max, bool clamp) {
				
				value = range2Min + ((value - range1Min) / (range1Max - range1Min)) * (range2Max - range2Min);
				
				if(clamp) {
					if(range2Min < range2Max) {
						if(value > range2Max) value = range2Max;
						if(value < range2Min) value = range2Min;
					}
					// Range that go negative are possible, for example from 0 to -1
					else {
						if(value > range2Min) value = range2Min;
						if(value < range2Max) value = range2Max;
					}
				}
				return value;
			}
		}
		
		public static class IEnumerableExtensions {
			/// <summary>
			/// Iterates over each element in the IEnumerable, passing in the element to the provided callback.
			/// </summary>
			public static void Each<T>(this IEnumerable<T> iterable, Action<T> callback) {
				foreach(var value in iterable) {
					callback(value);
				}
			}
			
			/// <summary>
			/// Iterates over each element in the IEnumerable, passing in the element to the provided callback.  Since the IEnumerable is
			/// not generic, a type must be specified as a type parameter to Each.
			/// </summary>
			/// <description>
			/// IEnumerable myCollection = new List<int>();
			/// ...
			/// myCollection.Each<int>(i => Debug.Log("i: " + i));
			/// </description>
			public static void Each<T>(this IEnumerable iterable, Action<T> callback) {
				foreach(T value in iterable) {
					callback(value);
				}
			}
			
//			/// <summary>
//			/// Iterates over each element in the IEnumerable, passing in the element to the provided callback.
//			/// </summary>
//			public static void Each(this IEnumerable iterable, Action<object> callback) {
//				foreach(object value in iterable) {
//					callback(value);
//				}
//			}
			
			/// <summary>
			/// Iterates over each element in the IEnumerable, passing in the element and the index to the provided callback.
			/// </summary>
			public static void EachWithIndex<T>(this IEnumerable<T> iterable, Action<T, int> callback) {
				var i = 0;
				foreach(var value in iterable) {
					callback(value, i);
					++i;
				}
			}
			
			/// <summary>
			/// Iterates over each element in the IEnumerable, passing in the element and the index to the provided callback.
			/// </summary>
			public static void EachWithIndex<T>(this IEnumerable iterable, Action<T, int> callback) {
				var i = 0;
				foreach(T value in iterable) {
					callback(value, i);
					++i;
				}
			}
			
			/// <summary>
			/// Iterates over each element in both the iterable1 and iterable2 collections, passing in the current element of each collection into the provided callback.
			/// </summary>
			public static void InParallelWith<T, U>(this IEnumerable<T> iterable1, IEnumerable<U> iterable2, Action<T, U> callback) {
				if(iterable1.Count() != iterable2.Count()) throw new ArgumentException(string.Format("Both IEnumerables must be the same length, iterable1: {0}, iterable2: {2}", iterable1.Count(), iterable2.Count()));
				
				var i1Enumerator = iterable1.GetEnumerator();
				var i2Enumerator = iterable2.GetEnumerator();
				
				while(i1Enumerator.MoveNext()) {
					i2Enumerator.MoveNext();
					callback(i1Enumerator.Current, i2Enumerator.Current);
				}
			}
			
			/// <summary>
			/// Iterates over each element in both the iterable1 and iterable2 collections, passing in the current element of each collection into the provided callback.
			/// </summary>
			public static void InParallelWith(this IEnumerable iterable1, IEnumerable iterable2, Action<object, object> callback) {
				var i1Enumerator = iterable1.GetEnumerator();
				var i2Enumerator = iterable2.GetEnumerator();
				var i1Count = 0;
				var i2Count = 0;
				while(i1Enumerator.MoveNext()) ++i1Count;
				while(i2Enumerator.MoveNext()) ++i2Count;
				if(i1Count != i2Count) throw new ArgumentException(string.Format("Both IEnumerables must be the same length, iterable1: {0}, iterable2: {2}", i1Count, i2Count));
				
				i1Enumerator.Reset();
				i2Enumerator.Reset();
				while(i1Enumerator.MoveNext()) {
					i2Enumerator.MoveNext();
					callback(i1Enumerator.Current, i2Enumerator.Current);
				}
			}
			
			public static bool IsEmpty<T>(this IEnumerable<T> iterable) {
				return iterable.Count() == 0;
			}
			
			public static bool IsEmpty(this IEnumerable iterable) {
				// MoveNext returns false if we are at the end of the collection
				return !iterable.GetEnumerator().MoveNext();
			}

#region MoreLINQ project code
			// MinBy and MoreBy methods are provided via the MoreLINQ project (c) Jon Skeet 
			// https://code.google.com/p/morelinq/source/browse/MoreLinq/MinBy.cs
			// https://code.google.com/p/morelinq/source/browse/MoreLinq/MaxBy.cs

			/// <summary>
			/// Returns the first element that has the smallest value (as determined by the selector) within the collection 
			/// (as determined by the comparer).  This is equivalent to using Min except that the element itself
			/// is returned, and not the value used to make the Min determination.
			/// </summary>
			public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) {
				return source.MinBy(selector, Comparer<TKey>.Default);
			}

			/// <summary>
			/// Returns the first element that has the smallest value (as determined by the selector) within the collection 
			/// (as determined by the comparer).  This is equivalent to using Min except that the element itself
			/// is returned, and not the value used to make the Min determination.
			/// </summary>
			public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
			{
				if (source == null) throw new ArgumentNullException("source");
				if (selector == null) throw new ArgumentNullException("selector");
				if (comparer == null) throw new ArgumentNullException("comparer");
				using (var sourceIterator = source.GetEnumerator()) {
					if (!sourceIterator.MoveNext()) {
						throw new InvalidOperationException("Sequence contains no elements");
					}
					var minValue = sourceIterator.Current;
					var minKey = selector(minValue);
					while (sourceIterator.MoveNext()) {
						var candidate = sourceIterator.Current;
						var candidateProjected = selector(candidate);
						if (comparer.Compare(candidateProjected, minKey) < 0) {
							minValue = candidate;
							minKey = candidateProjected;
						}
					}
					return minValue;
				}
			}

			/// <summary>
			/// Returns the first element that has the largest value (as determined by the selector) within the collection 
			/// (as determined by the comparer).  This is equivalent to using Max except that the element itself
			/// is returned, and not the value used to make the Max determination.
			/// </summary>
			public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector) {
				return source.MaxBy(selector, Comparer<TKey>.Default);
			}

			/// <summary>
			/// Returns the first element that has the largest value (as determined by the selector) within the collection 
			/// (as determined by the comparer).  This is equivalent to using Max except that the element itself
			/// is returned, and not the value used to make the Max determination.
			/// </summary>
			public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer) {
				if (source == null) throw new ArgumentNullException("source");
				if (selector == null) throw new ArgumentNullException("selector");
				if (comparer == null) throw new ArgumentNullException("comparer");
				using (var sourceIterator = source.GetEnumerator()) {
					if (!sourceIterator.MoveNext()) {
						throw new InvalidOperationException("Sequence contains no elements");
					}
					var maxValue = sourceIterator.Current;
					var maxKey = selector(maxValue);
					while (sourceIterator.MoveNext()) {
						var candidate = sourceIterator.Current;
						var candidateProjected = selector(candidate);
						if (comparer.Compare(candidateProjected, maxKey) > 0) {
							maxValue = candidate;
							maxKey = candidateProjected;
						}
					}
					return maxValue;
				}
			}
#endregion
		}
		
		public static class ArrayExtensions {
			[ThreadStatic]
			static System.Random randomNumberGenerator = new Random(DateTime.Now.Millisecond + System.Threading.Thread.CurrentThread.GetHashCode());
			
			/// <summary>
			/// Returns the first index in the array where the target exists.  If the target cannot be found, returns -1.
			/// </summary>
			public static int IndexOf<T>(this T[] array, T target) {
				for(var i = 0; i < array.Length; ++i) {
					if(array[i].Equals(target)) return i;
				}
				return -1;
			}
			
			/// <summary>
			/// Returns a sub-section of the current array, starting at the specified index and continuing to the end of the array.
			/// </summary>
			public static T[] FromIndexToEnd<T>(this T[] array, int start) {
				var subSection = new T[array.Length - start];
				array.CopyTo(subSection, start);
				return subSection;
			}

			/// <summary>
			/// Wrapper for System.Array.FindIndex to allow it to be called directly on an array.
			/// </summary>
			public static int FindIndex<T>(this T[] array, Predicate<T> match) {
				return Array.FindIndex(array, match);
			}

			/// <summary>
			/// Wrapper for System.Array.FindIndex to allow it to be called directly on an array.
			/// </summary>
			public static int FindIndex<T>(this T[] array, int startIndex, Predicate<T> match) {
				return Array.FindIndex(array, startIndex, match);
			}

			/// <summary>
			/// Wrapper for System.Array.FindIndex to allow it to be called directly on an array.
			/// </summary>
			public static int FindIndex<T>(this T[] array, int startIndex, int count, Predicate<T> match) {
				return Array.FindIndex(array, startIndex, count, match);
			}

			/// Returns a randomly selected item from the array
			public static T RandomElement<T>(this T[] array) {
				if(array.Length == 0) throw new IndexOutOfRangeException("Cannot retrieve a random value from an empty array");
				
				return array[randomNumberGenerator.Next(array.Length)];
			}
			
			/// Returns a randomly selected item from the array determined by a float array of weights
			public static T RandomElement<T>(this T[] array, float[] weights) {
				return array.RandomElement(weights.ToList());
			}
			
			/// Returns a randomly selected item from the array determined by a List<float> of weights
			public static T RandomElement<T>(this T[] array, List<float> weights) {
				if(array.IsEmpty()) throw new IndexOutOfRangeException("Cannot retrieve a random value from an empty array");
				if(array.Count() != weights.Count()) throw new IndexOutOfRangeException("array of weights must be the same size as input array");
		
				var randomWeight = randomNumberGenerator.NextDouble() * weights.Sum();
				var totalWeight = 0f;
				var index = weights.FindIndex(weight => {
					totalWeight += weight;
					return randomWeight <= totalWeight;
				});
				
				return array[index];
			}
		}
		
		public static class ListExtensions {
			[ThreadStatic]
			static System.Random randomNumberGenerator = new Random(DateTime.Now.Millisecond + System.Threading.Thread.CurrentThread.GetHashCode());
			
			/// <summary>
			/// Returns a sub-section of the current list, starting at the specified index and continuing to the end of the list.
			/// </summary>
			public static List<T> FromIndexToEnd<T>(this List<T> list, int start) {
				return list.GetRange(start, list.Count - start);
			}
			
			/// <summary>
			/// Returns the first index in the List<T> where the target exists.  If the target cannot be found, returns -1.
			/// </summary>
			public static int IndexOf<T>(this List<T> list, T target) {
				for(var i = 0; i < list.Count; ++i) {
					if(list[i].Equals(target)) return i;
				}
				return -1;
			}
			
			/// Returns a randomly selected item from List<T>
			public static T RandomElement<T>(this List<T> list) {
				if(list.IsEmpty()) throw new IndexOutOfRangeException("Cannot retrieve a random value from an empty list");
				
				return list[randomNumberGenerator.Next(list.Count)];
			}
			
			/// Returns a randomly selected item from List<T> determined by a float array of weights
			public static T RandomElement<T>(this List<T> list, float[] weights) {
				return list.RandomElement(weights.ToList());
			}
			
			/// Returns a randomly selected item from List<T> determined by a List<float> of weights
			public static T RandomElement<T>(this List<T> list, List<float> weights) {
				if(list.IsEmpty()) throw new IndexOutOfRangeException("Cannot retrieve a random value from an empty list");
				if(list.Count() != weights.Count()) throw new IndexOutOfRangeException("List of weights must be the same size as input list");
		
				var randomWeight = randomNumberGenerator.NextDouble() * weights.Sum();
				var totalWeight = 0f;
				var index = weights.FindIndex(weight => {
					totalWeight += weight;
					return randomWeight <= totalWeight;
				});
				
				return list[index];
			}
			
			public static List<T> Shuffle<T>(this List<T> list) {
				return list.OrderBy(e => randomNumberGenerator.Next()).ToList();
			}
		}
		
		public static class DictionaryExtensions {
			/// <summary>
			/// Iterates over a Dictionary<T> passing in both the key and value to the provided callback.
			/// </summary>
			public static void Each<T1, T2>(this Dictionary<T1, T2> dictionary, Action<T1, T2> callback) {
				foreach(var keyValuePair in dictionary) {
					callback(keyValuePair.Key, keyValuePair.Value);
				}
			}
			
			public static void RemoveAll<T1, T2>(this Dictionary<T1, T2> dictionary, Predicate<T1, T2> callback) {
				var keysToRemove = new List<T1>();
				foreach(var keyValuePair in dictionary) {
					if(callback(keyValuePair.Key, keyValuePair.Value)) {
						keysToRemove.Add(keyValuePair.Key);
					}
				}
				
				foreach(var key in keysToRemove) {
					dictionary.Remove(key);
				}
			}
		}
		
		public static class StringExtensions {
			/// <summary>
			/// Interpolates the arguments into the string using string.Format
			/// </summary>
			/// <param name="formatString">The string to be interpolated into</param>
			/// <param name="args">The values to be interpolated into the string </param>
			public static string Interpolate(this string formatString, params object[] args) {
				return string.Format(formatString, args);
			}

			public static T ToEnum<T>(this string enumValueName) {
				return (T)Enum.Parse(typeof(T), enumValueName);
			}
			
			public static T ToEnum<T>(this string enumValueName, bool ignoreCase) {
				return (T)Enum.Parse(typeof(T), enumValueName, ignoreCase);
			}
			
			public static string Last(this string value, int count) {
				if(count > value.Length) throw new ArgumentOutOfRangeException(string.Format("Cannot return more characters than exist in the string (wanted {0} string contains {1}", count, value.Length));
				
				return value.Substring(value.Length - count, count);
			}
			
			public static string SnakeCase(this string camelizedString) {
				var parts = new List<string>();
		        var currentWord = new StringBuilder();
		
		        foreach(var c in camelizedString) {
		            if (char.IsUpper(c) && currentWord.Length > 0) {
		                parts.Add(currentWord.ToString());
		                currentWord = new StringBuilder();
		            }
		            currentWord.Append(char.ToLower(c));
		        }
		
		        if(currentWord.Length > 0) {
		            parts.Add(currentWord.ToString());
		        }
		
		        return string.Join("_", parts.ToArray());
			}
			
			public static string Capitalize(this string word) {
				return word.Substring(0, 1).ToUpper() + word.Substring(1);
			}
		}
		
		public static class TypeExtensions {
			/// <summary>
			/// Returns an array of all concrete subclasses of the provided type.
			/// </summary>
			public static Type[] Subclasses(this Type type) {
				var typeList = new List<System.Type>();
				System.AppDomain.CurrentDomain.GetAssemblies().Each(a => typeList.AddRange(a.GetTypes()));
				return typeList.Where(t => t.IsSubclassOf(type) && !t.IsAbstract).ToArray();
			}
			
			/// <summary>
			/// Returns an array of the provided type and all concrete subclasses of that type.
			/// </summary>
			public static Type[] TypeAndSubclasses(this Type type) {
				var typeList = new List<System.Type>();
				System.AppDomain.CurrentDomain.GetAssemblies().Each(a => typeList.AddRange(a.GetTypes()));
				return typeList.Where(t => (t == type || t.IsSubclassOf(type)) && !t.IsAbstract).ToArray();
			}
		}
	}
}