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
namespace Stact
{
	using System;
	using Magnum.Reflection;
	using Magnum.Serialization;


	/// <summary>
	/// Accepts a MessageEnvelope as input and deserializes the message body on the supplied fiber,
	/// after which the message object is sent to the untyped output channel.
	/// </summary>
	/// <typeparam name="T">The input message type, which must implement MessageEnvelope</typeparam>
	public class DeserializeMessageEnvelopeChannel<T> :
		Channel<T>
		where T : MessageEnvelope
	{
		readonly Fiber _fiber;
		readonly UntypedChannel _output;
		readonly Serializer _serializer;

		/// <summary>
		/// Constructs an instance
		/// </summary>
		/// <param name="fiber">The fiber used to perform message deserialization</param>
		/// <param name="serializer">The serializer to use on the message body</param>
		/// <param name="output">The output channel for the deserialized message</param>
		public DeserializeMessageEnvelopeChannel(Fiber fiber, Serializer serializer, UntypedChannel output)
		{
			_fiber = fiber;
			_serializer = serializer;
			_output = output;
		}

		public void Send(T message)
		{
			_fiber.Add(() =>
				{
					Type messageType = Type.GetType(message.MessageType, true, true);
					if (messageType == null)
						return;

					this.FastInvoke(new[] {messageType}, "Deserialize", message.Body);
				});
		}

		void Deserialize<TMessage>(string body)
		{
			var message = _serializer.Deserialize<TMessage>(body);

			_output.Send(message);
		}
	}
}