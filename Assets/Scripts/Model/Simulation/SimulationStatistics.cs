using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationStatistics : MonoBehaviour
{
	[SerializeField]
	private SimulationManager simulationManager;

	private int healthyHumanCountAtStart;
	
	private int infectedHumanCountAtStart;
	
	private int infectionAttemptCount;
	
	private int successfulInfectionCount;
	
	private DateTime simulationStart;
	
	private DateTime simulationEnd;

	public int HealthyHumanCountAtStart { get => healthyHumanCountAtStart; private set => healthyHumanCountAtStart = value; }
	public int InfectedHumanCountAtStart { get => infectedHumanCountAtStart; private set => infectedHumanCountAtStart = value; }
	public int InfectionAttemptCount { get => infectionAttemptCount; private set => infectionAttemptCount = value; }
	public int SuccessfulInfectionCount { get => successfulInfectionCount; private set => successfulInfectionCount = value; }
	public TimeSpan SimulationDuration 
	{
		get
		{
			if (simulationManager.State.stateAsString() == "PLAYING")
			{
				return DateTime.Now - simulationStart;
			}
			else
			{
				return simulationEnd - simulationStart;
			}
		}
	}

	public event Action onStatisticsChange;

	private void Awake()
	{
		simulationManager.onSimulationStateChange += (sender, state) =>
		{
			if (state.stateAsString() == "PLAYING")
			{
				clearStatistics();
				HealthyHumanCountAtStart = simulationManager.HealthyHumanCount;
				InfectedHumanCountAtStart = simulationManager.InfectedHumanCount;
				simulationStart = DateTime.Now;
			}
			else if (state.stateAsString() == "STOPPED")
			{
				simulationEnd = DateTime.Now;
			}
		};

		Human.onHumanTryInfect += (sender, isSuccessful) =>
		{
			InfectionAttemptCount++;
			SuccessfulInfectionCount += isSuccessful ? 1 : 0;
		};
	}

	private void FixedUpdate()
	{
		if (simulationManager.State.stateAsString() == "PLAYING")
		{
			onStatisticsChange?.Invoke();
		}
	}

	private void clearStatistics()
	{
		InfectionAttemptCount = 0;
		SuccessfulInfectionCount = 0;
	}
}
