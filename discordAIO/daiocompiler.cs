﻿using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace discordAIO
{
	internal class Compiler
	{
		public static bool AIOcompilation(string string_1, string string_2, string string_3 = null)
		{
			string iconenv = Environment.CurrentDirectory + "\\icon.ico";
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("CompilerVersion", "v4.0");
			string compileOptions = "/target:winexe /platform:anycpu /optimize";
			if (string_3 != null)
			{
				File.Copy(string_3, iconenv, true);
				compileOptions = compileOptions + " /win32icon:\"" + iconenv + "\"";
			}
			bool result;
			using (CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider(dictionary))
			{
				CompilerParameters options = new CompilerParameters(Compiler.string_0)
				{
					GenerateExecutable = true,
					GenerateInMemory = false,
					OutputAssembly = string_2,
					CompilerOptions = compileOptions,
					TreatWarningsAsErrors = false,
					IncludeDebugInformation = false
				};
				CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromSource(options, new string[]
				{
					string_1
				});
				if (compilerResults.Errors.Count > 0)
				{
					MessageBox.Show(string.Format("The compiler has encountered {0} errors", compilerResults.Errors.Count), "Errors while compiling", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					foreach (object obj in compilerResults.Errors)
					{
						CompilerError compilerError = (CompilerError)obj;
						MessageBox.Show(string.Format("{0}\nLine: {1} - Column: {2}\nFile: {3}", new object[]
						{
							compilerError.ErrorText,
							compilerError.Line,
							compilerError.Column,
							compilerError.FileName
						}), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				File.Delete(iconenv);
				result = (compilerResults.Errors.Count == 0);
			}
			return result;
		}
		private static readonly string[] string_0 = new string[]
		{
			  "System.Net.dll",
			  "System.dll",
			  "System.Windows.Forms.dll",
			  "System.Drawing.dll",
			  "System.Management.dll",
			  "System.IO.dll",
			  "System.IO.compression.dll",
			  "System.IO.compression.filesystem.dll",
			  "System.Core.dll",
			  "System.Security.dll",
			  "System.Net.Http.dll",
			  "System.Xml.dll",
			  "RestSharp.dll",
			  "Newtonsoft.Json.dll"
		};
	}
}