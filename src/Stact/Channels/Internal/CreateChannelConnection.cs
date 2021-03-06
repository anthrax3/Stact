﻿// Copyright 2010 Chris Patterson
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
namespace Stact.Internal
{
	using System;


	/// <summary>
	/// Allows channels and disposable items to be added to the connection
	/// </summary>
	public interface CreateChannelConnection
	{
		/// <summary>
		/// Adds a channel to the connection
		/// </summary>
		/// <param name="channel"></param>
		void AddChannel(Channel channel);

		/// <summary>
		/// Adds a disposable reference to the connection
		/// </summary>
		/// <param name="disposable"></param>
		void AddDisposable(IDisposable disposable);
	}
}