﻿using System;
using Microsoft.Azure.WebJobs.Description;

namespace Cosmonaut.WebJobs.Extensions.Trigger
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Parameter)]
    [Binding]
    public sealed class CosmosStoreTriggerAttribute : Attribute
    {
        public CosmosStoreTriggerAttribute(string databaseName)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new ArgumentException("Missing information for the collection to monitor", nameof(databaseName));
            }

            DatabaseName = databaseName;
            LeaseCollectionName = CosmosStoreTriggerConstants.DefaultLeaseCollectionName;
            LeaseDatabaseName = DatabaseName;
        }

        public CosmosStoreTriggerAttribute(string databaseName, string overridenCollectionName)
        {
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new ArgumentException("Missing information for the collection to monitor", nameof(databaseName));
            }

            CollectionName = overridenCollectionName;
            DatabaseName = databaseName;
            LeaseCollectionName = CosmosStoreTriggerConstants.DefaultLeaseCollectionName;
            LeaseDatabaseName = DatabaseName;
        }

        /// <summary>
        /// Connection string for the service containing the collection to monitor
        /// </summary>
        [AppSetting]
        public string ConnectionStringSetting { get; set; }
        
        /// <summary>
        /// Name of the collection to monitor for changes
        /// </summary>
        public string CollectionName { get; }

        /// <summary>
        /// Name of the database containing the collection to monitor for changes
        /// </summary>
        public string DatabaseName { get; }

        /// <summary>
        /// Connection string for the service containing the lease collection
        /// </summary>
        [AppSetting]
        public string LeaseConnectionStringSetting { get; set; }

        /// <summary>
        /// Name of the lease collection. Default value is "leases"
        /// </summary>
        public string LeaseCollectionName { get; set; }

        /// <summary>
        /// Name of the database containing the lease collection
        /// </summary>
        public string LeaseDatabaseName { get; set; }

        /// <summary>
        /// Optional.
        /// Only applies to lease collection.
        /// If true, the database and collection for leases will be automatically created if it does not exist.
        /// </summary>
        public bool CreateLeaseCollectionIfNotExists { get; set; } = false;

        /// <summary>
        /// Optional.
        /// When specified on an output binding and <see cref="CreateLeaseCollectionIfNotExists"/> is true, defines the throughput of the created
        /// collection.
        /// </summary>
        public int LeasesCollectionThroughput { get; set; }

        /// <summary>
        /// Optional.
        /// Defines a prefix to be used within a Leases collection for this Trigger. Useful when sharing the same Lease collection among multiple Triggers
        /// </summary>
        public string LeaseCollectionPrefix { get; set; }
        
        /// <summary>
        /// Optional.
        /// Customizes the amount of milliseconds between lease checkpoints. Default is always after a Function call.
        /// </summary>
        public int CheckpointInterval { get; set; }
        
        /// <summary>
        /// Optional.
        /// Customizes the amount of documents between lease checkpoints. Default is always after a Function call.
        /// </summary>
        public int CheckpointDocumentCount { get; set; }
        
        /// <summary>
        /// Optional.
        /// Customizes the delay in milliseconds in between polling a partition for new changes on the feed, after all current changes are drained.  Default is 5000 (5 seconds).
        /// </summary>
        public int FeedPollDelay { get; set; }
        
        /// <summary>
        /// Optional.
        /// Customizes the renew interval in milliseconds for all leases for partitions currently held by the Trigger. Default is 17000 (17 seconds).
        /// </summary>
        public int LeaseRenewInterval { get; set; }
        
        /// <summary>
        /// Optional.
        /// Customizes the interval in milliseconds to kick off a task to compute if partitions are distributed evenly among known host instances. Default is 13000 (13 seconds).
        /// </summary>
        public int LeaseAcquireInterval { get; set; }
        
        /// <summary>
        /// Optional.
        /// Customizes the interval in milliseconds for which the lease is taken on a lease representing a partition. If the lease is not renewed within this interval, it will cause it to expire and ownership of the partition will move to another Trigger instance. Default is 60000 (60 seconds).
        /// </summary>
        public int LeaseExpirationInterval { get; set; }
        
        /// <summary>
        /// Optional.
        /// Customizes the maximum amount of items received in an invocation
        /// </summary>
        public int MaxItemsPerInvocation { get; set; }

        /// <summary>
        /// Optional.
        /// Gets or sets whether change feed in the Azure Cosmos DB service should start from beginning (true) or from current (false). By default it's start from current (false).
        /// </summary>
        public bool StartFromBeginning { get; set; } = false;

        /// <summary>
        /// Optional.
        /// Defines preferred locations (regions) for geo-replicated database accounts in the Azure Cosmos DB service.
        /// Values should be comma-separated.
        /// </summary>
        /// <example>
        /// PreferredLocations = "East US,South Central US,North Europe"
        /// </example>
        public string PreferredLocations { get; set; }
    }
}