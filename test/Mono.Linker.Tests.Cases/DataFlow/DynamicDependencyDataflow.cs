using Mono.Linker.Tests.Cases.Expectations.Assertions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Mono.Linker.Tests.Cases.DataFlow
{
	public class DynamicDependencyDataflow
	{
		public static void Main ()
		{
			DynamicDependencyFrom ();
		}

		[Kept]
		[KeptAttributeAttribute (typeof (DynamicallyAccessedMembersAttribute))]
		[DynamicallyAccessedMembers (DynamicallyAccessedMemberTypes.PublicMethods)]
		static Type TypeWithPublicMethods;

		[Kept]
		[UnrecognizedReflectionAccessPattern (typeof (Type), "GetField", new Type[] { typeof (string) })]
		[DynamicDependency ("DynamicDependencyTo")]
		static void DynamicDependencyFrom ()
		{
			_ = TypeWithPublicMethods.GetField ("f");
		}

		[Kept]
		[UnrecognizedReflectionAccessPattern (typeof (Type), "GetProperty", new Type[] { typeof (string) })]
		static void DynamicDependencyTo ()
		{
			_ = TypeWithPublicMethods.GetProperty ("p");
		}
	}
}
