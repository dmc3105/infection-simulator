using System;
using UnityEngine;

public partial class SimulationManager
{
	public class PlayingState : SimulationState
	{
		private SimulationManager manager;

		public PlayingState(SimulationManager simulationManager) : base(simulationManager)
		{
			manager = simulationManager;
		}

		public override int HealtyHumanCount { get => manager.HealthyHumanCount; set { } }
		public override int InfectedHumanCount { get => manager.InfectedHumanCount; set { } }
		public override GameObject HealthyHumanPrefab { get => manager.HealthyHumanPrefab; set { } }
		public override GameObject InfectedHumanPrefab { get => manager.InfectedHumanPrefab; set { } }
		public override Vector3 SpawnAreaSize { get => manager.SpawnAreaSize; set { } }
		public override float InfectionProbability { get => manager.InfectionProbability; set { } }
		public override TimeSpan TimeToInfect { get => manager.TimeToInfect; set { } }
		public override float InfectionRadius { get => manager.InfectionRadius; set { } }

		public override void switchState()
		{
			destroyHumans();
			simulationManager.State = new StoppedState(simulationManager);
		}

		private void destroyHumans()
		{
			foreach (Transform human in simulationManager.spawner.transform)
			{
				Destroy(human.gameObject);
			}
		}
	}

}
