﻿// <copyright file="Program.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
// </copyright>

namespace Sample.OnlineMeeting
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Default program class.
    /// </summary>
    public class Program
    {
        private static string appSecret = "__placeholder__";
        private static string appId = "__placeholder__";

        private static string meetingId = "__placeholder__";
        private static string tenantId = "__placeholder__";
        private static string organizerID = "__placeholder__";

        private static Uri graphUri = new Uri("https://graph.microsoft.com/beta/");

        /// <summary>
        /// Gets the online meeting asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="meetingId">The meeting identifier.</param>
        /// <returns> The onlinemeeting details. </returns>
        public static async Task<Microsoft.Graph.OnlineMeeting> GetOnlineMeetingAsync(string tenantId, string meetingId)
        {
            var onlineMeeting = new OnlineMeeting(
                        new RequestAuthenticationProvider(appId, appSecret),
                        graphUri);

            var meetingDetails = await onlineMeeting.GetOnlineMeetingAsync(tenantId, meetingId, default(Guid)).ConfigureAwait(false);

            Console.WriteLine(meetingDetails.Id);
            Console.WriteLine(meetingDetails.ChatInfo.ThreadId);

            return meetingDetails;
        }

        /// <summary>
        /// Creates the online meeting asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="organizerId">The organizer identifier.</param>
        /// <returns> The newly created onlinemeeting. </returns>
        public static async Task<Microsoft.Graph.OnlineMeeting> CreateOnlineMeetingAsync(string tenantId, string organizerId)
        {
            var onlineMeeting = new OnlineMeeting(
                        new RequestAuthenticationProvider(appId, appSecret),
                        graphUri);

            var meetingDetails = await onlineMeeting.CreateOnlineMeetingAsync(tenantId, organizerId, default(Guid)).ConfigureAwait(false);

            Console.WriteLine(meetingDetails.Id);
            Console.WriteLine(meetingDetails.ChatInfo.ThreadId);

            return meetingDetails;
        }

        /// <summary>
        /// The Main entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                try
                {
                    var meetingDetails = await GetOnlineMeetingAsync(tenantId, meetingId).ConfigureAwait(false);

                    var createdMeetingDetails = await CreateOnlineMeetingAsync(tenantId, organizerID).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

            Console.ReadKey();
        }
    }
}
