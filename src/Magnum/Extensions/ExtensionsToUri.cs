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
namespace Magnum.Extensions
{
	using System;

	public static class ExtensionsToUri
	{
		/// <summary>
		///   Appends a path to an existing Uri
		/// </summary>
		/// <param name = "uri"></param>
		/// <param name = "path"></param>
		/// <returns></returns>
		public static Uri AppendPath(this Uri uri, string path)
		{
			string absolutePath = uri.AbsolutePath.TrimEnd('/') + "/" + path;
			return new UriBuilder(uri.Scheme, uri.Host, uri.Port, absolutePath, uri.Query).Uri;
		}
	}
}