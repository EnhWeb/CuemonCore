﻿using System;

namespace Cuemon.AspNetCore.Mvc.Configuration
{
    /// <summary>
    /// Specifies options that is related to <see cref="DynamicCacheBustingOptions"/> operations.
    /// </summary>
    /// <seealso cref="CacheBustingOptions" />
    public class DynamicCacheBustingOptions : CacheBustingOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicCacheBustingOptions"/> class.
        /// </summary>
        /// <remarks>
        /// The following table shows the initial property values for an instance of <see cref="DynamicCacheBustingOptions"/>.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Property</term>
        ///         <description>Initial Value</description>
        ///     </listheader>
        ///     <item>
        ///         <term><see cref="PreferredLength"/></term>
        ///         <description>8</description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="PreferredCharacters"/></term>
        ///         <description><see cref="StringUtility.AlphanumericCharactersCaseSensitive"/></description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="TimeToLive"/></term>
        ///         <description>20 minutes</description>
        ///     </item>
        /// </list>
        /// </remarks>
        public DynamicCacheBustingOptions()
        {
            TimeToLive = TimeSpan.FromMinutes(20);
            PreferredLength = 8;
            PreferredCharacters = StringUtility.AlphanumericCharactersCaseSensitive;
        }

        /// <summary>
        /// Gets or sets the preferred length of <see cref="CacheBusting.Version"/>.
        /// </summary>
        /// <value>The preferred length of <see cref="CacheBusting.Version"/>.</value>
        public int PreferredLength { get; set; }

        /// <summary>
        /// Gets or sets the preferred characters of <see cref="CacheBusting.Version"/>.
        /// </summary>
        /// <value>The preferred characters of <see cref="CacheBusting.Version"/>.</value>
        public string PreferredCharacters { get; set; }

        /// <summary>
        /// Gets or sets the TTL of <see cref="CacheBusting.Version"/>.
        /// </summary>
        /// <value>The TTL of <see cref="CacheBusting.Version"/>.</value>
        public TimeSpan TimeToLive { get; set; }
    }
}