﻿using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cuemon
{
    /// <summary>
    /// This utility class is designed to make some event related operations easier to work with.
    /// </summary>
    public static class EventUtility
    {
        /// <summary>
        /// A generic helper method method for raising an event.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event data generated by the event.</typeparam>
        /// <param name="handler">The method that will handle the event.</param>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Raise<TEventArgs>(EventHandler<TEventArgs> handler, object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            if (handler != null) { handler(sender, e); }
        }

        /// <summary>
        /// A generic helper method for adding an event to a custom backing field <paramref name="backingFieldHandler"/>.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event data generated by the event.</typeparam>
        /// <param name="handler">The method that will handle the event.</param>
        /// <param name="backingFieldHandler">The backing field reference for the <paramref name="handler"/>.</param>
        /// <remarks>The methodology is the same as in .NET 4.0.</remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void AddEvent<TEventArgs>(EventHandler<TEventArgs> handler, ref EventHandler<TEventArgs> backingFieldHandler) where TEventArgs : EventArgs
        {
            EventHandler<TEventArgs> previousHandler;
            EventHandler<TEventArgs> newBackingFieldHandler = backingFieldHandler;
            do
            {
                previousHandler = newBackingFieldHandler;
                EventHandler<TEventArgs> newHandler = (EventHandler<TEventArgs>)Delegate.Combine(previousHandler, handler);
                newBackingFieldHandler = Interlocked.CompareExchange<EventHandler<TEventArgs>>(ref backingFieldHandler, newHandler, previousHandler);

            } while (newBackingFieldHandler != previousHandler);
        }

        /// <summary>
        /// A generic helper method for removing an event from a custom backing field <paramref name="backingFieldHandler"/>.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event data generated by the event.</typeparam>
        /// <param name="handler">The method that will handle the event.</param>
        /// <param name="backingFieldHandler">The backing field reference for the <paramref name="handler"/>.</param>
        /// <remarks>The methodology is the same as in .NET 4.0.</remarks>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void RemoveEvent<TEventArgs>(EventHandler<TEventArgs> handler, ref EventHandler<TEventArgs> backingFieldHandler) where TEventArgs : EventArgs
        {
            EventHandler<TEventArgs> previousHandler;
            EventHandler<TEventArgs> newBackingFieldHandler = backingFieldHandler;
            do
            {
                previousHandler = newBackingFieldHandler;
                EventHandler<TEventArgs> newHandler = (EventHandler<TEventArgs>)Delegate.Remove(previousHandler, handler);
                newBackingFieldHandler = Interlocked.CompareExchange<EventHandler<TEventArgs>>(ref backingFieldHandler, newHandler, previousHandler);

            } while (newBackingFieldHandler != previousHandler);
        }
    }
}