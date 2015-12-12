using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Routing;

namespace Akka.Concurrency
{
    public interface IConcurrencyHelper
    {
        Task<IEnumerable<TResult>> RunConcurrentlyAsync<TActor, TMessage, TResult>(IEnumerable<TMessage> messages, int numberOfInstances) where TActor : ActorBase;

        IEnumerable<TResult> RunConcurrently<TActor, TMessage, TResult>(IEnumerable<TMessage> messages, int numberOfInstances) where TActor : ActorBase;

        Task<IEnumerable> RunConcurrentlyAsync<TActor, TMessage>(IEnumerable<TMessage> messages, int numberOfInstances) where TActor : ActorBase;

        IEnumerable RunConcurrently<TActor, TMessage>(IEnumerable<TMessage> messages, int numberOfInstances) where TActor : ActorBase;

        Task<IEnumerable> RunConcurrentlyAsync<TActor, TMessage>(IEnumerable<TMessage> messages, RouterConfig routerConfig) where TActor : ActorBase;

        IEnumerable RunConcurrently<TActor, TMessage>(IEnumerable<TMessage> messages, RouterConfig routerConfig) where TActor : ActorBase;

        Task<IEnumerable<TResult>> RunConcurrentlyAsync<TActor, TMessage, TResult>(IEnumerable<TMessage> messages, RouterConfig routerConfig) where TActor : ActorBase;

        IEnumerable<TResult> RunConcurrently<TActor, TMessage, TResult>(IEnumerable<TMessage> messages, RouterConfig routerConfig) where TActor : ActorBase;
    }
}