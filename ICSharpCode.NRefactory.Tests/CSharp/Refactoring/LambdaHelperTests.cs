//
// LambdaHelperTests.cs
//
// Author:
//       Luís Reis <luiscubal@gmail.com>
//
// Copyright (c) 2013 Luís Reis
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using NUnit.Framework;
using System;
using System.Linq;
using ICSharpCode.NRefactory.CSharp.Refactoring;
using ICSharpCode.NRefactory.CSharp.CodeActions;
using ICSharpCode.NRefactory.CSharp;

namespace ICSharpCode.NRefactory.CSharp.Refactoring
{
	[TestFixture]
	public class LambdaHelperTests
	{
		RefactoringContext MakeContext(string input, bool expectErrors = false)
		{
			var context = TestRefactoringContext.Create(input, expectErrors);
			return context;
		}

		[Test]
		public void TestExtensionMethod()
		{
			string input = @"
using System.Linq;
class Test
{
	void Method() {
		System.Linq.Enumerable.Empty<int> ().Where(<-i => i > 0->);
	}
}
";

			var context = MakeContext(input);
			var lambda = context.GetSelectedNodes().OfType<LambdaExpression>().First();
			var type = LambdaHelper.GetLambdaReturnType(context, lambda);
			
			Assert.AreEqual("System.Boolean", type.ReflectionName);
		}
	}
}

