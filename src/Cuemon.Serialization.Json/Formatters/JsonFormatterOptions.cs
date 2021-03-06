﻿using System;
using System.Collections;
using System.Collections.Generic;
using Cuemon.Serialization.Formatters;
using Cuemon.Serialization.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Cuemon.Serialization.Json.Formatters
{
    /// <summary>
    /// Specifies options that is related to <see cref="JsonFormatter"/> operations.
    /// </summary>
    /// <seealso cref="FormatterOptions{TReader,TReaderOptions,TWriter,TWriterOptions,TConverter}" />
    public class JsonFormatterOptions : FormatterOptions<JsonReader, JsonSerializerSettings, JsonWriter, JsonSerializerSettings, JsonConverter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonFormatterOptions"/> class.
        /// </summary>
        /// <remarks>
        /// The following table shows the initial property values for an instance of <see cref="JsonFormatterOptions"/>.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Property</term>
        ///         <description>Initial Value</description>
        ///     </listheader>
        ///     <item>
        ///         <term><see cref="FormatterOptions{TReader,TReaderOptions,TWriter,TWriterOptions,TConverter}.Converter"/></term>
        ///         <description><c>null</c></description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="Settings"/></term>
        ///         <description><see cref="JsonSerializerSettings"/></description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="SynchronizeWithJsonConvert"/></term>
        ///         <description><c>false</c></description>
        ///     </item>
        /// </list>
        /// A default initialization of <see cref="JsonSerializerSettings"/> is performed while adding a <see cref="CamelCasePropertyNamesContractResolver"/> to
        /// <see cref="JsonSerializerSettings.ContractResolver"/>, adding a <see cref="StringFlagsEnumConverter"/> to <see cref="JsonSerializerSettings.Converters"/> and 
        /// setting <see cref="JsonSerializerSettings.NullValueHandling"/> to <see cref="NullValueHandling.Ignore"/>.
        /// </remarks>
        public JsonFormatterOptions()
        {
            Converter = null;
            Settings = new JsonSerializerSettings()
            {
                CheckAdditionalContent = true,
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            Settings.Converters.Add(new StringFlagsEnumConverter());
            Settings.Converters.Add(new StringEnumConverter());
            SynchronizeWithJsonConvert = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="Settings"/> should be synchronized on <see cref="JsonConvert.DefaultSettings"/>.
        /// </summary>
        /// <value><c>true</c> if <see cref="Settings"/> should be synchronized on <see cref="JsonConvert.DefaultSettings"/>; otherwise, <c>false</c>.</value>
        public bool SynchronizeWithJsonConvert { get; set; }

        /// <summary>
        /// Gets or sets the settings to support the <see cref="JsonFormatter"/>.
        /// </summary>
        /// <returns>A <see cref="JsonSerializerSettings"/> instance that specifies a set of features to support the <see cref="JsonFormatter"/> object.</returns>
        public JsonSerializerSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the function delegate that generates an object from its JSON representation.
        /// </summary>
        /// <value>The function delegate that generates an object from its JSON representation.</value>
        public override Func<JsonReader, JsonSerializerSettings, Type, object> ReaderFormatter { get; set; }

        /// <summary>
        /// Gets or sets the delegate that converts an object into its JSON representation.
        /// </summary>
        /// <value>The delegate that converts an object into its JSON representation.</value>
        public override Action<JsonWriter, JsonSerializerSettings, object> WriterFormatter { get; set; }

        /// <summary>
        /// Gets the, by <see cref="Type"/>, specialized delegate that converts an object into its JSON representation.
        /// </summary>
        /// <value>A specialized delegate, by <see cref="Type"/>, that converts an object into its JSON representation.</value>
        public override IDictionary<Type, Action<JsonWriter, JsonSerializerSettings, object>> WriterFormatters { get; } = new Dictionary<Type, Action<JsonWriter, JsonSerializerSettings, object>>()
        {
            {
                typeof(TimeSpan), (writer, settings, o) =>
                {
                    var span = (TimeSpan)o;
                    writer.WriteStartObject();
                    writer.WritePropertyName("ticks");
                    writer.WriteValue(span.Ticks);
                    writer.WritePropertyName("days");
                    writer.WriteValue(span.Days);
                    writer.WritePropertyName("hours");
                    writer.WriteValue(span.Hours);
                    writer.WritePropertyName("minutes");
                    writer.WriteValue(span.Minutes);
                    writer.WritePropertyName("seconds");
                    writer.WriteValue(span.Seconds);
                    writer.WritePropertyName("totalDays");
                    writer.WriteValue(span.TotalDays);
                    writer.WritePropertyName("totalHours");
                    writer.WriteValue(span.TotalHours);
                    writer.WritePropertyName("totalMilliseconds");
                    writer.WriteValue(span.TotalMilliseconds);
                    writer.WritePropertyName("totalMinutes");
                    writer.WriteValue(span.TotalMinutes);
                    writer.WritePropertyName("totalSeconds");
                    writer.WriteValue(span.TotalSeconds);
                    writer.WriteEndObject();
                }
            },
            {
                typeof(ExceptionDescriptor), (writer, settings, o) =>
                {
                    ExceptionDescriptor descriptor = (ExceptionDescriptor) o;
                    writer.WriteStartObject();
                    if (!descriptor.RequestId.IsNullOrWhiteSpace())
                    {
                        writer.WritePropertyName("requestId");
                        writer.WriteValue(descriptor.RequestId);
                    }
                    writer.WritePropertyName("code");
                    writer.WriteValue(descriptor.Code);
                    writer.WritePropertyName("message");
                    writer.WriteValue(descriptor.Message);
                    writer.WritePropertyName("failure");
                    writer.WriteStartObject();
                    writer.WritePropertyName("type");
                    writer.WriteValue(descriptor.Failure.GetType().FullName);
                    writer.WritePropertyName("message");
                    writer.WriteValue(descriptor.Failure.Message);
                    if (descriptor.Failure.Data.Count > 0)
                    {
                        writer.WritePropertyName("data");
                        writer.WriteStartObject();
                        foreach (DictionaryEntry entry in descriptor.Failure.Data)
                        {
                            writer.WritePropertyName(entry.Key.ToString());
                            writer.WriteValue(entry.Value);
                        }
                        writer.WriteEndObject();
                    }
                    writer.WriteEndObject();
                    if (descriptor.HelpLink != null)
                    {
                        writer.WritePropertyName("helpLink");
                        writer.WriteValue(descriptor.HelpLink.OriginalString);
                    }
                    writer.WriteEndObject();
                }
            },
            {
                typeof(Exception), (writer, settings, o) =>
                {
                    Exception ex = (Exception) o;
                    WriteException(writer, ex, true);
                }
            }
        };

        private static void WriteException(JsonWriter writer, Exception exception, bool includeStackTrace)
        {
            Type exceptionType = exception.GetType();
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteValue(exceptionType.FullName);
            WriteExceptionCore(writer, exception, includeStackTrace);
            writer.WriteEndObject();
        }

        private static void WriteEndElement<T>(T counter, JsonWriter writer)
        {
            writer.WriteEndObject();
        }

        private static void WriteExceptionCore(JsonWriter writer, Exception exception, bool includeStackTrace)
        {
            if (!exception.Source.IsNullOrWhiteSpace())
            {
                writer.WritePropertyName("source");
                writer.WriteValue(exception.Source);
            }

            if (!exception.Message.IsNullOrWhiteSpace())
            {
                writer.WritePropertyName("message");
                writer.WriteValue(exception.Message);
            }

            if (exception.StackTrace != null && includeStackTrace)
            {
                writer.WritePropertyName("stack");
                writer.WriteStartArray();
                string[] lines = exception.StackTrace.Split(new[] { StringUtility.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    writer.WriteValue(line.Trim());
                }
                writer.WriteEndArray();
            }

            if (exception.Data.Count > 0)
            {
                writer.WritePropertyName("data");
                writer.WriteStartObject();
                foreach (DictionaryEntry entry in exception.Data)
                {
                    writer.WritePropertyName(entry.Key.ToString());
                    writer.WriteValue(entry.Value);
                }
                writer.WriteEndObject();
            }

            WriteInnerExceptions(writer, exception, includeStackTrace);
        }

        private static void WriteInnerExceptions(JsonWriter writer, Exception exception, bool includeStackTrace)
        {
            var aggregated = exception as AggregateException;
            var innerExceptions = new List<Exception>();
            if (aggregated != null) { innerExceptions.AddRange(aggregated.InnerExceptions); }
            if (exception.InnerException != null) { innerExceptions.Add(exception.InnerException); }
            if (innerExceptions.Count > 0)
            {
                int endElementsToWrite = 0;
                foreach (var inner in innerExceptions)
                {
                    writer.WritePropertyName("inner");
                    Type exceptionType = inner.GetType();
                    writer.WriteStartObject();
                    writer.WritePropertyName("type");
                    writer.WriteValue(exceptionType.FullName);
                    WriteExceptionCore(writer, inner, includeStackTrace);
                    endElementsToWrite++;
                }
                LoopUtility.For(endElementsToWrite, WriteEndElement, writer);
            }
        }

        /// <summary>
        /// Gets the, by <see cref="Type"/>, specialized function delegate that generates an object from its JSON representation.
        /// </summary>
        /// <value>A specialized function delegate, by <see cref="Type"/>, that generates an object from its JSON representation.</value>
        public override IDictionary<Type, Func<JsonReader, JsonSerializerSettings, Type, object>> ReaderFormatters { get; } = new Dictionary<Type, Func<JsonReader, JsonSerializerSettings, Type, object>>()
        {
            {
                typeof(TimeSpan), (reader, settings, type) => reader.ToHierarchy().UseTimeSpanFormatter()
            }
        };
    }
}