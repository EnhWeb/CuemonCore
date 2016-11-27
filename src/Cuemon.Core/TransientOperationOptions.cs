﻿using System;

namespace Cuemon
{
    /// <summary>
    /// Specifies options that is related to <see cref="TransientOperation"/> handling.
    /// </summary>
    /// <seealso cref="TransientOperation"/>.
    public class TransientOperationOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransientOperationOptions"/> class.
        /// </summary>
        public TransientOperationOptions()
        {
            RetryAttempts = DefaultRetryAttempts;
            EnableRecovery = RetryAttempts > 0;
            RetryStrategyCallback = currentAttempt => TimeSpan.FromSeconds(Math.Pow(2, currentAttempt > 5 ? 5 : currentAttempt));
            DetectionStrategyCallback = exception => false;
        }

        /// <summary>
        /// Gets or sets the amount of retry attempts for transient faults. Default value is <see cref="DefaultRetryAttempts"/>.
        /// </summary>
        /// <value>The retry attempts for transient faults.</value>
        public int RetryAttempts { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a transient operation should be attempted gracefully recovered in case of a transient fault or simply be invoked like a normal operation.
        /// </summary>
        /// <value><c>true</c> if a transient operation should be attempted gracefully recovered in case of a transient fault; otherwise, <c>false</c>.</value>
        /// <remarks>For testing or diagnostic purposes, it can sometimes come in handy to turn off transient fault recovery.</remarks>
        public bool EnableRecovery { get; set; }

        /// <summary>
        /// Gets or sets the callback function delegate that determines the amount of time to wait for a transient fault to recover gracefully before trying a new attempt.
        /// </summary>
        /// <returns>A <see cref="Func{TResult}"/> that determines the amount of time to wait for a transient fault to recover gracefully.</returns>
        /// <remarks>Default implementation is <see cref="int"/> + 2^ to a maximum of 5; eg. 1, 2, 4, 8, 16 to a total of 32 seconds.</remarks>
        public Func<int, TimeSpan> RetryStrategyCallback { get; set; }

        /// <summary>
        /// Gets or sets the callback function delegate that determines if an <see cref="Exception"/> contains clues that would suggest a transient fault.
        /// </summary>
        /// <value>A <see cref="Func{TResult}"/> that determines if an <see cref="Exception"/> contains clues that would suggest a transient fault.</value>
        /// <remarks>Default implementation is fixed to none-transient failure.</remarks>
        public Func<Exception, bool> DetectionStrategyCallback { get; set; }

        /// <summary>
        /// Gets or sets the default amount of retry attempts for transient faults. Default is 5 attempts.
        /// </summary>
        /// <value>The default amount of retry attempts for transient faults.</value>
        public static byte DefaultRetryAttempts { get; set; } = 5;
    }
}