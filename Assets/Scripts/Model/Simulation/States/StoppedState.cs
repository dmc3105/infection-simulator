using System;
using UnityEngine;

public partial class SimulationManager
{
	public class StoppedState : SimulationState
	{
		private SimulationManager manager;

		public StoppedState(SimulationManager simulationManager) : base(simulationManager)
		{
			manager = simulationManager;
		}

		public override int HealtyHumanCount { get => manager.HealthyHumanCount; set => manager.HealthyHumanCount = value; }
		public override int InfectedHumanCount { get => manager.InfectedHumanCount; set => manager.InfectedHumanCount = value; }
		public override GameObject HealthyHumanPrefab { get => manager.HealthyHumanPrefab; set => manager.HealthyHumanPrefab = value; }
		public override GameObject InfectedHumanPrefab { get => manager.InfectedHumanPrefab; set => manager.InfectedHumanPrefab = value; }
		public override Vector3 SpawnAreaSize { get => manager.SpawnAreaSize; set => manager.SpawnAreaSize = value; }
		public override float InfectionProbability { get => manager.InfectionProbability; set => manager.InfectionProbability = value; }
		public override TimeSpan TimeToInfect { get => manager.TimeToInfect; set => manager.TimeToInfect = value; }
		public override float InfectionRadius { get => manager.InfectionRadius; set => manager.InfectionRadius = value; }

		public override void switchState()
		{
			simulationManager.spawner.Spawn();
			simulationManager.State = new PlayingState(simulationManager);
		}
	}

}
