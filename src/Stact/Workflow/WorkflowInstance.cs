// Copyright 2010 Chris Patterson
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
namespace Stact.Workflow
{
	using System;
	using System.Linq.Expressions;


	public interface WorkflowInstance
	{
		void RaiseEvent(string eventName);
		void RaiseEvent(string eventName, object body);

		void RaiseEvent(Event eevent);
		void RaiseEvent<TBody>(Event<TBody> eevent, TBody body);
	}


	public interface WorkflowInstance<TWorkflow> :
		WorkflowInstance
		where TWorkflow : class
	{
		/// <summary>
		/// Returns the current state for the instance
		/// </summary>
		State CurrentState { get; }

		void RaiseEvent(Expression<Func<TWorkflow, Event>> eventSelector);
		void RaiseEvent<TBody>(Expression<Func<TWorkflow, Event<TBody>>> eventSelector, TBody body);
	}
}