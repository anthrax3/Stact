// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Magnum.Specs.Invoker
{
	using System;
	using Activator.Classes;
	using Classes;
	using Magnum.Invoker;
	using NUnit.Framework;
	using TestFramework;

	[TestFixture]
	public class When_using_the_fast_invoker
	{
		[Test]
		public void The_generic_method_should_be_invoked()
		{
			var target = new ClassWithGenericMethods();

			object name = "Name";

			target.FastInvoke(x => x.OneGenericArgument(0), name);

			target.OneGenericArgumentCalled.ShouldBeTrue();
			target.FirstArgumentType.ShouldEqual(typeof (string));
			target.FirstArgumentValue.ShouldEqual(name);
		}

		[Test]
		public void The_two_argument_generic_method_should_be_invoked()
		{
			var target = new ClassWithGenericMethods();

			object name = "Name";
			object when = DateTime.Now;

			target.FastInvoke(x => x.TwoGenericArguments(0, 0), name, when);

			target.TwoGenericArgumentsCalled.ShouldBeTrue();
			target.FirstArgumentType.ShouldEqual(typeof (string));
			target.FirstArgumentValue.ShouldEqual(name);
			target.SecondArgumentType.ShouldEqual(typeof (DateTime));
			target.SecondArgumentValue.ShouldEqual(when);
		}

		[Test]
		public void The_single_argument_two_type_method_should_be_invoked()
		{
			var target = new ClassWithGenericMethods();

			object value = new ClassWithGuidConstraint(Guid.NewGuid());

			target.FastInvoke(x => x.TwoGenericArgumentsOneParameter<ClassWithStringConstraint, string>(null), value);

			target.TwoGenericArgumentsOneParameterCalled.ShouldBeTrue();
			target.FirstArgumentType.ShouldEqual(typeof(ClassWithGuidConstraint));
			target.FirstArgumentValue.ShouldEqual(value);
			target.SecondArgumentType.ShouldEqual(typeof (Guid));
		}

		[Test]
		public void The_generic_method_should_be_invoked_and_not_cached_improperly()
		{
			var target = new ClassWithGenericMethods();

			object name = "Name";
			const int value = 47;

			target.FastInvoke(x => x.OneGenericArgument(0), name);
			target.FastInvoke(x => x.OneGenericArgument(0), value);

			target.OneGenericArgumentCalled.ShouldBeTrue();
			target.FirstArgumentType.ShouldEqual(typeof (int));
			target.FirstArgumentValue.ShouldEqual(value);
		}

		[Test]
		public void The_simple_method_should_be_invoked()
		{
			var target = new ClassWithSimpleMethods();

			target.FastInvoke(x => x.NoArguments());

			target.NoArgumentsCalled.ShouldBeTrue();
		}
	}
}