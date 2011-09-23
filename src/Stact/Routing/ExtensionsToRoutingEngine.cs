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
namespace Stact.Routing
{
    using Configuration;


    public static class ExtensionsToRoutingEngine
    {
        static readonly ConsumerNodeFactory _consumerFactory = new DynamicConsumerNodeFactory();


        public static RemoveActivation Receive<T>(this RoutingEngineConfigurator configurator, Consumer<T> consumer)
        {
            return _consumerFactory.Create(consumer, configurator);
        }

        public static RemoveActivation SelectiveReceive<T>(this RoutingEngineConfigurator configurator,
                                                           SelectiveConsumer<T> consumer)
        {
            return _consumerFactory.Create(consumer, configurator);
        }

        /* considering yanking the message join feature as unnecessary and complicated
        public static void Receive<T1, T2>(this RoutingEngineConfigurator configurator,
                                           Consumer<Tuple<T1, T2>> consumer)
        {
            var consumerNode = new ConsumerNode<Tuple<T1,T2>>(configurator.Engine, consumer);

            configurator.Add(consumerNode);
        }
         **/
    }
}