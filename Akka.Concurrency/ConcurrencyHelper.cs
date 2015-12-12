using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Routing;

namespace Akka.Concurrency
{
    public class ConcurrencyHelper : IConcurrencyHelper
    {
        public async Task<IEnumerable<TResult>> RunConcurrentlyAsync<TActor, TMessage, TResult>(IEnumerable<TMessage> messages, int numberOfInstances) where TActor : ActorBase
        {
            var routerConfig = new RoundRobinPool(numberOfInstances);
            return await this.RunConcurrentlyAsync<TActor, TMessage, TResult>(messages, routerConfig);
        }

        public IEnumerable<TResult> RunConcurrently<TActor, TMessage, TResult>(IEnumerable<TMessage> messages, int numberOfInstances) where TActor : ActorBase
        {
            var routerConfig = new RoundRobinPool(numberOfInstances);
            return this.RunConcurrently<TActor, TMessage, TResult>(messages, routerConfig);
        }

        public async Task<IEnumerable> RunConcurrentlyAsync<TActor, TMessage>(IEnumerable<TMessage> messages, int numberOfInstances) where TActor : ActorBase
        {
            var routerConfig = new RoundRobinPool(numberOfInstances);
            return await this.RunConcurrentlyAsync<TActor, TMessage, object>(messages, routerConfig);
        }

        public IEnumerable RunConcurrently<TActor, TMessage>(IEnumerable<TMessage> messages, int numberOfInstances) where TActor : ActorBase
        {
            var routerConfig = new RoundRobinPool(numberOfInstances);
            return this.RunConcurrently<TActor, TMessage, object>(messages, routerConfig);
        }

        public async Task<IEnumerable> RunConcurrentlyAsync<TActor, TMessage>(IEnumerable<TMessage> messages, RouterConfig routerConfig) where TActor : ActorBase
        {
            return await this.RunConcurrentlyAsync<TActor, TMessage, object>(messages, routerConfig);
        }

        public IEnumerable RunConcurrently<TActor, TMessage>(IEnumerable<TMessage> messages, RouterConfig routerConfig) where TActor : ActorBase
        {
            return this.RunConcurrently<TActor, TMessage, object>(messages, routerConfig);
        }

        public async Task<IEnumerable<TResult>> RunConcurrentlyAsync<TActor, TMessage, TResult>(IEnumerable<TMessage> messages, RouterConfig routerConfig) where TActor : ActorBase
        {
            var router = CreateRouter<TActor>(routerConfig);
            var tasks = messages.Select(message => router.Ask<TResult>(message)).ToArray();
            await Task.WhenAll(tasks);
            return tasks.Select(t => t.Result).ToList();
        }

        public IEnumerable<TResult> RunConcurrently<TActor, TMessage, TResult>(IEnumerable<TMessage> messages, RouterConfig routerConfig) where TActor : ActorBase
        {
            var router = CreateRouter<TActor>(routerConfig);
            var tasks = messages.Select(message => router.Ask<TResult>(message)).ToArray();
            Task.WhenAll(tasks).Wait();
            return tasks.Select(t => t.Result).ToList();
        }

        static IActorRef CreateRouter<TActor>(RouterConfig routerConfig) where TActor : ActorBase
        {
            var systemName = $"system{Guid.NewGuid().ToString("N")}";
            var system = ActorSystem.Create(systemName);
            var props = Props.Create<TActor>().WithRouter(routerConfig);
            return system.ActorOf(props);
        }
    }
}