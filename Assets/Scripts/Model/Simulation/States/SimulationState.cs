using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class SimulationManager
{
	public abstract class SimulationState
	{
		protected SimulationManager simulationManager;
		protected SimulationState(SimulationManager simulationManager)
		{
			this.simulationManager = simulationManager;
		}

		public abstract int HealtyHumanCount { get; set; }
		public abstract int InfectedHumanCount { get; set; }
		public abstract GameObject HealthyHumanPrefab { get; set; }
		public abstract GameObject InfectedHumanPrefab { get; set; }
		public abstract Vector3 SpawnAreaSize { get; set; }
		public abstract float InfectionProbability { get; set; }
		public abstract TimeSpan TimeToInfect { get; set; }
		public abstract float InfectionRadius { get; set; }

		public abstract void switchState();

		public string stateAsString()
		{
			return GetType().Name.Replace("State", "").ToUpper();
		}
	}

}
