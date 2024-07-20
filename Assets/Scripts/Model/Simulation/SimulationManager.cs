using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SimulationManager : MonoBehaviour
{

	[SerializeField]
	private HumanSpawner spawner;

	[SerializeField]
	private HumanSettings humanSettings;

	private SimulationState state;

	public event EventHandler<SimulationState> onSimulationStateChange;

	public SimulationState State
	{
		get => state;
		private set
		{
			state = value;
			onSimulationStateChange?.Invoke(this, value);
		}
	}

	public int HealthyHumanCount { get => spawner.HealtyHumanCount; set => spawner.HealtyHumanCount = value; }
	public int InfectedHumanCount { get => spawner.InfectedHumanCount; set => spawner.InfectedHumanCount = value; }
	public GameObject HealthyHumanPrefab { get => spawner.HealthyHumanPrefab; set => state.HealthyHumanPrefab = value; }
	public GameObject InfectedHumanPrefab { get => spawner.InfectedHumanPrefab; set => spawner.InfectedHumanPrefab = value; }
	public Vector3 SpawnAreaSize { get => spawner.SpawnAreaSize; set => spawner.SpawnAreaSize = value; }
	public float InfectionProbability { get => humanSettings.InfectionProbability; set => humanSettings.InfectionProbability = value; }
	public TimeSpan TimeToInfect { get => humanSettings.TimeToInfect; set => humanSettings.TimeToInfect = value; }
	public float InfectionRadius { get => humanSettings.InfectionRadius; set => humanSettings.InfectionRadius = value; }

	private void Awake()
	{
		state = new StoppedState(this);
	}

	public void switchState()
	{
		state.switchState();
	}
}
