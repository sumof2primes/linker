﻿using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;
using Mono.Linker.Tests.Cases.DynamicDependencies.Dependencies;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.DynamicDependencies
{
	/// <summary>
	/// This is an acceptable bug with the currently implementation.  Embedded link xml files will not be processed
	/// </summary>
	[IgnoreDescriptors (false)]
	[SetupCompileBefore ("base.dll", new[] { "Dependencies/DynamicDependencyMethodInNonReferencedAssemblyBase.cs" })]
	[SetupCompileBefore (
		"DynamicDependencyMethodInNonReferencedAssemblyLibrary.dll",
		new[] { "Dependencies/DynamicDependencyMethodInNonReferencedAssemblyLibrary.cs" },
		references: new[] { "base.dll" },
		resources: new[] { "Dependencies/DynamicDependencyMethodInNonReferencedAssemblyLibrary.xml" },
		addAsReference: false)]
	[KeptAssembly ("base.dll")]
	[RemovedMemberInAssembly ("DynamicDependencyMethodInNonReferencedAssemblyLibrary.dll", "Mono.Linker.Tests.Cases.DynamicDependencies.Dependencies.DynamicDependencyMethodInNonReferencedAssemblyLibrary", "UnusedMethod()")]
	public class DynamicDependencyMethodInNonReferencedAssemblyWithEmbeddedXml
	{
		public static void Main ()
		{
			var obj = new Foo ();
			var val = obj.Method ();
			Dependency ();
		}

		[Kept]
		[DynamicDependency ("#ctor()", "Mono.Linker.Tests.Cases.DynamicDependencies.Dependencies.DynamicDependencyMethodInNonReferencedAssemblyLibrary", "DynamicDependencyMethodInNonReferencedAssemblyLibrary")]
		static void Dependency ()
		{
		}

		[Kept]
		[KeptMember (".ctor()")]
		[KeptBaseType (typeof (DynamicDependencyMethodInNonReferencedAssemblyBase))]
		class Foo : DynamicDependencyMethodInNonReferencedAssemblyBase
		{
			[Kept]
			public override string Method ()
			{
				return "Foo";
			}
		}
	}
}