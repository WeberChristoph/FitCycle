using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;
using FitCycle.Models.FitCycleModels;

namespace FitCycle
{
    public enum ExerciseType
    { 
        isolation,
        basic
    }
    public class Exercise
    {
       
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

        [JsonProperty(PropertyName = "UserId")]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "ExerciseName")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "ExercisePic")]
        public string PicUrl { get; set; }

        [JsonProperty(PropertyName = "MuscleGroup")]
        public MuscleGroup MuscleGroup { get; set; }

        [JsonProperty(PropertyName = "CurrentMaxPower")]
        public int CurrentMaxPower { get; }

        [JsonProperty(PropertyName = "RMform")]
        public RMform RMform { get; set; }

        [JsonProperty(PropertyName = "CurrentWeight")]
        public int CurrentWeight { get; set; }

        [JsonProperty(PropertyName = "CurrentRPE")]
        public int CurrentRPE { get; set; }

        [JsonProperty(PropertyName = "Reps")]
        public int Reps { get; set; }

        [JsonProperty(PropertyName = "TimeBreak")]
        public TimeSpan TimeBreak { get; set; }

        [JsonProperty(PropertyName = "TimeRep")]
        public TimeSpan TimeRep { get; set; }

        [JsonProperty(PropertyName = "TimeDeviationSet")]
        public TimeSpan TimeDeviationSet { get; set; }

        [JsonProperty(PropertyName = "ExerciseType")]
        public ExerciseType Type { get; set; }

        [JsonProperty(PropertyName = "ExerciseDescription")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "ExerciseRecommendation")]
        public string Recommendation { get; set; }

        [JsonProperty(PropertyName = "IsEnabled")]
        public bool IsEnabled { get; set; }

        [Version]
		public string Version { get; set; }
	}
}
