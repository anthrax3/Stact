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
namespace Stact.Routing.Configuration
{
    using Nodes;


    public class MessageConsumerNodeFactory<T> :
        ConsumerNodeFactory
    {
        public RemoveActivation Create<TMessage>(Consumer<Message<TMessage>> consumer,
                                                 RoutingEngineConfigurator configurator)
        {
            var messageConsumer = consumer as Consumer<Message<T>>;
            var consumerNode = new ConsumerNode<T>(configurator.Agenda, messageConsumer);

            return AddActivation(configurator, consumerNode);
        }

        public RemoveActivation Create<TMessage>(SelectiveConsumer<Message<TMessage>> consumer,
                                                 RoutingEngineConfigurator configurator)
        {
            var messageConsumer = consumer as SelectiveConsumer<Message<T>>;
            var consumerNode = new SelectiveConsumerNode<T>(configurator.Agenda, messageConsumer);

            return AddActivation(configurator, consumerNode);
        }

        static RemoveActivation AddActivation(RoutingEngineConfigurator configurator,
                                              Activation<T> consumerNode)
        {
            var messageActivation = new MessageNode<T>(consumerNode);
            return configurator.AddActivation(messageActivation);
        }
    }
}