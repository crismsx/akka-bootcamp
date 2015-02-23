using System;
using Akka.Actor;
using System.Threading;

namespace WinTail
{
    /// <summary>
    /// Actor responsible for reading FROM the console. 
    /// Also responsible for calling <see cref="ActorSystem.Shutdown"/>.
    /// </summary>
    class ConsoleReaderActor : UntypedActor
    {
        public const string ExitCommand = "exit";
        private ActorRef _consoleWriterActor;

        public ConsoleReaderActor(ActorRef consoleWriterActor)
        {
            _consoleWriterActor = consoleWriterActor;
        }

        protected override void OnReceive(object message)
        {
            var read = Console.ReadLine();
            if (!string.IsNullOrEmpty(read) && read.ToLowerInvariant().Equals(ExitCommand))
            {
                // shut down the system (acquire handle to system via
                // this actors context)
                Context.System.Shutdown();
                return;
            }

            // send input to the console writer to process and print
            _consoleWriterActor.Tell(string.Format("Reader tid:{0}, msg: {1}", Thread.CurrentThread.ManagedThreadId, read));

            // continue reading messages from the console
            Self.Tell("continue");
        }

    }
}