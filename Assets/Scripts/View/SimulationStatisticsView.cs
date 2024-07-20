using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimulationStatisticsView : MonoBehaviour
{
    [SerializeField]
    private SimulationStatistics statistics;


	[Header("Healthy human count at start")]

	[SerializeField]
	private string healthyHumanCountAtStartLabel;

	[SerializeField]
	private TextMeshProUGUI healthyHumanCountAtStartText;


	[Header("Infected human count at start")]

	[SerializeField]
	private string infectedHumanCountAtStartLabel;

	[SerializeField]
	private TextMeshProUGUI infectedHumanCountAtStartText;


	[Header("Infection attempt count")]

	[SerializeField]
	private string infectionAttemptCountLabel;

	[SerializeField]
	private TextMeshProUGUI infectionAttemptCountText;


	[Header("Successful infection count")]

	[SerializeField]
	private string successfulInfectionCountLabel;

	[SerializeField]
	private TextMeshProUGUI successfulInfectionCountText;


	[Header("Simulation duration")]

	[SerializeField]
	private string simulationDurationLabel;

	[SerializeField]
	private TextMeshProUGUI simulationDurationText;

	private void Start()
	{
		updateStatistics();
		statistics.onStatisticsChange += () =>
		{
			updateStatistics();
		};
	}

	private void updateStatistics()
	{
		healthyHumanCountAtStartText.text = healthyHumanCountAtStartLabel + statistics.HealthyHumanCountAtStart;
		infectedHumanCountAtStartText.text = infectedHumanCountAtStartLabel + statistics.InfectedHumanCountAtStart;
		infectionAttemptCountText.text = infectionAttemptCountLabel + statistics.InfectionAttemptCount;
		successfulInfectionCountText.text = successfulInfectionCountLabel + statistics.SuccessfulInfectionCount;
		simulationDurationText.text = simulationDurationLabel + statistics.SimulationDuration.TotalSeconds.ToString("0.00");
	}

}
