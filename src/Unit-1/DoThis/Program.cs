﻿using Akka.Actor;
using System;

namespace WinTail
{
    #region Program
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
            // initialize MyActorSystem
            MyActorSystem = ActorSystem.Create("MyActorSystem");

            // time to make your first actors!
            // make consoleWriterActor using these props: Props.Create(() => new ConsoleWriterActor())
            var consoleWriterActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleWriterActor()));
            // make consoleReaderActor using these props: Props.Create(() => new ConsoleReaderActor(consoleWriterActor))
            var consoleReaderActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleReaderActor(consoleWriterActor)));


            // tell console reader to begin
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.AwaitTermination();
        }
    }
    #endregion
}
