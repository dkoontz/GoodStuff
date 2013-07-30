using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using GoodStuff.NaturalLanguage;

namespace GoodStuff {
	[TestFixture()]
	public class IntTests {
		// Times
		[Test()]
		public void TimesShouldIterate0Times() {
			var counter = 0;
			0.Times(() => counter++);
			Assert.AreEqual(0, counter);
		}
		
		[Test()]
		public void TimesShouldIterate5Times() {
			var counter = 0;
			5.Times(() => counter++);
			Assert.AreEqual(5, counter);
		}

		[Test()]
		public void TimesShouldIterate0TimesAndHaveParam() {
			var counter = 0;
			0.Times(i => counter += i);
			Assert.AreEqual(0, counter);
		}

		[Test()]
		public void TimesShouldIterate5TimesAndHaveParam() {
			var counter = 0;
			5.Times(i => counter += i);
			Assert.AreEqual(0+1+2+3+4, counter);
		}

		[Test()]
		public void UpToShouldIterate3Times() {
			var counter = 0;
			1.UpTo(3, i => counter++);
			Assert.AreEqual(3, counter);
		}
		
		[Test()]
		public void DownToShouldIterate3Times() {
			var counter = 0;
			3.DownTo(1, i => counter++);
			Assert.AreEqual(3, counter);
		}
		
	}
	
	[TestFixture()]
	public class IEnumerableTests {
		// Each
		[Test()]
		public void EachIteratesOverEveryItem()	{
			var values = new int[] {1,2,3,4,5};
			var sum = 0;
			values.Each(i => sum += i);
			Assert.AreEqual(15, sum);
		}
		
		[Test()]
		public void EachWorksWithEmptyCollections()	{
			var values = new int[] {};
			var run = false;
			values.Each(i => run = true);
			Assert.IsFalse(run);
		}
		
		//EachWithIndex
		[Test()]
		public void EachWithIndexProvidesBothValueAndIndex() {
			var values = new string[] {"first", "second"};
			var valuesFromLambda = new List<object>();
			values.EachWithIndex((e, i) => {
				valuesFromLambda.Add(e);
				valuesFromLambda.Add(i);
			});
			
			Assert.AreEqual(values[0], valuesFromLambda[0]);
			Assert.AreEqual(0, valuesFromLambda[1]);
			Assert.AreEqual(values[1], valuesFromLambda[2]);
			Assert.AreEqual(1, valuesFromLambda[3]);
		}
	}
}