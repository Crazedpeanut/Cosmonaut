﻿using Cosmonaut.Attributes;
using Newtonsoft.Json;

namespace Cosmonaut.System.Models
{
    [SharedCosmosCollection("shared")]
    public class Lion : Animal, ISharedCosmosEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string CosmosEntityName { get; set; }
    }
}