﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cuemon.Collections.Generic;

namespace Cuemon.Serialization.Formatters
{
    /// <summary>
    /// Specifies options that is related to <see cref="Formatter{TFormat}" /> operations.
    /// </summary>
    /// <typeparam name="TReader">The type of the object that will handle the deserialization operations.</typeparam>
    /// <typeparam name="TReaderOptions">The type of the object that will handle the options associated with <typeparamref name="TReader"/>.</typeparam>
    /// <typeparam name="TWriter">The type of the object that will handle the serialization operations.</typeparam>
    /// <typeparam name="TWriterOptions">The type of the object that will handle the options associated with <typeparamref name="TWriter"/>.</typeparam>
    /// <typeparam name="TConverter">The type of the object that will handle the serialization and deserialization conversions.</typeparam>
    public abstract class FormatterOptions<TReader, TReaderOptions, TWriter, TWriterOptions, TConverter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormatterOptions{TReader, TReaderOptions, TWriter, TWriterOptions, TConverter}"/> class.
        /// </summary>
        protected FormatterOptions()
        {
        }

        /// <summary>
        /// Gets or sets the object converter of <see cref="Formatter{TFormat}"/>.
        /// </summary>
        /// <value>The object that will handle the serialization and deserialization.</value>
        public TConverter Converter { get; set; }

        /// <summary>
        /// Gets or sets the function delegate that generates an object.
        /// </summary>
        /// <value>The function delegate that generates an object.</value>
        public abstract Func<TReader, TReaderOptions, Type, object> ReaderFormatter { get; set; }

        /// <summary>
        /// Gets or sets the delegate that converts an object.
        /// </summary>
        /// <value>The delegate that converts an object.</value>
        public abstract Action<TWriter, TWriterOptions, object> WriterFormatter { get; set; }

        /// <summary>
        /// Gets the, by <see cref="Type"/>, specialized delegate that converts an object.
        /// </summary>
        /// <value>A specialized delegate, by <see cref="Type"/>, that converts an object.</value>
        public abstract IDictionary<Type, Action<TWriter, TWriterOptions, object>> WriterFormatters { get; }

        /// <summary>
        /// Gets the, by <see cref="Type"/>, specialized function delegate that generates an object.
        /// </summary>
        /// <value>A specialized function delegate, by <see cref="Type"/>, that generates an object.</value>
        public abstract IDictionary<Type, Func<TReader, TReaderOptions, Type, object>> ReaderFormatters { get; }

        /// <summary>
        /// Resolves the formatter that will be used for <see cref="Formatter{TFormat}.Serialize(object)"/> operations.
        /// </summary>
        /// <param name="sourceType">The type of the object to resolve a formatter.</param>
        /// <value>The delegate that converts an object.</value>
        public Action<TWriter, TWriterOptions, object> ParseWriterFormatter(Type sourceType)
        {
            Action<TWriter, TWriterOptions, object> writerDelegate = null;
            if (WriterFormatters.Count > 0 && WriterFormatters.TryGetValue(sourceType, keys => keys.FirstOrDefault(type => TypeUtility.ContainsType(sourceType, type)), out writerDelegate))
            {
            }
            else
            {
                var interfaces = sourceType.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (WriterFormatters.TryGetValue(@interface, out writerDelegate))
                    {
                        break;
                    }
                }
            }
            return WriterFormatter ?? writerDelegate;
        }

        /// <summary>
        /// Resolves the formatter that will be used for <see cref="Formatter{TFormat}.Deserialize{T}"/> operations.
        /// </summary>
        /// <param name="sourceType">The type of the object to resolve a formatter.</param>
        /// <value>The delegate that converts an object.</value>
        public Func<TReader, TReaderOptions, Type, object> ParseReaderFormatter(Type sourceType)
        {
            Func<TReader, TReaderOptions, Type, object> readerDelegate = null;
            if (ReaderFormatters.Count > 0 && ReaderFormatters.TryGetValue(sourceType, keys => keys.FirstOrDefault(type => TypeUtility.ContainsType(sourceType, type)), out readerDelegate))
            {
            }
            else
            {
                var interfaces = sourceType.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (ReaderFormatters.TryGetValue(@interface, out readerDelegate))
                    {
                        break;
                    }
                }
            }
            return ReaderFormatter ?? readerDelegate;
        }
    }
}