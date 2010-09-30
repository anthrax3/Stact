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
namespace Stact.ForNHibernate.Auditing.Internal
{
	using System;
	using System.Collections.Generic;
	using Stact.Channels;
	using NHibernate.Event;


	public class PreUpdateListener :
		EntityEventListener<PreUpdateEvent>,
		IPreUpdateEventListener
	{
		public PreUpdateListener(UntypedChannel channel, HashSet<Type> types)
			: base(channel, types)
		{
		}

		public bool OnPreUpdate(PreUpdateEvent @event)
		{
			OnEvent(@event);

			return false;
		}

		protected override Type GetDispatchKey(PreUpdateEvent e)
		{
			return e.Entity.GetType();
		}

		protected override void SendEvent<T>(PreUpdateEvent e)
		{
			var entity = (T)e.Entity;
			IList<PropertyChange> changes = GetChanges(e.Persister, e.State);

			PreUpdateEventImpl<T> message = SetGenericEventProperties(new PreUpdateEventImpl<T>(), e.Session);
			message.Entity = entity;
			message.Changes = changes;

			Send<PreUpdateEvent<T>>(message);
		}
	}
}