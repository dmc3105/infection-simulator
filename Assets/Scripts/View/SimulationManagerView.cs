using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManagerView : MonoBehaviour
{
	[SerializeField]
	private Button startStopButton;

	private TextMeshProUGUI startStopButtonText;

	[Header("Healthy panel settings")]

	[SerializeField]
	private Slider healthyHumansCountSlider;

	[SerializeField]
	private TextMeshProUGUI healthyHumanCountText;

	[Header("Infected panel settings")]

	[SerializeField]
	private Slider infectedHumansCountSlider;

	[SerializeField]
	private TextMeshProUGUI infectedHumanCountText;

	[Header("Human settings")]


	[Header("Infection probability panel")]

	[SerializeField]
	private Slider infectionProbabilitySlider;

	[SerializeField]
	private TextMeshProUGUI infectionProbabilityText;

	
	[Header("Infection radius panel")]

	[SerializeField]
	private Slider infectionRadiusSlider;

	[SerializeField]
	private TextMeshProUGUI infectionRadiusText;


	[Header("Time to infect panel")]

	[SerializeField]
	private Slider timeToInfectSlider;

	[SerializeField]
	private TextMeshProUGUI timeToInfectText;


	[SerializeField]
	private SimulationManager manager;

	private void updateUI()
	{
		updateStartStopButton();
		updateSlider(healthyHumansCountSlider, manager.HealthyHumanCount);
		updateSlider(infectedHumansCountSlider, manager.InfectedHumanCount);
		updateSlider(timeToInfectSlider, (float)manager.TimeToInfect.TotalSeconds);
		updateSlider(infectionProbabilitySlider, manager.InfectionProbability);
		updateSlider(infectionRadiusSlider, manager.InfectionRadius);
		healthyHumanCountText.text = manager.HealthyHumanCount.ToString();
		infectedHumanCountText.text = manager.InfectedHumanCount.ToString();
		updateTimeToInfectText();
		updateInfectionProbabilityText();
		updateInfectionRadiusText();
	}

	private void updateSlider(Slider slider, float value)
	{
		slider.value = value;
		switch (manager.State.stateAsString())
		{
			case "PLAYING":
				slider.interactable = false;
				break;
			case "STOPPED":
				slider.interactable = true;
				break;
			default:
				throw new ArgumentException("Incorrect SimulationState");
		}
	}

	private void updateStartStopButton()
	{
		startStopButtonText.SetText(getButtonTextByState(manager.State));
	}

	private string getButtonTextByState(SimulationManager.SimulationState state)
	{
		switch (state.stateAsString())
		{
			case "PLAYING":
				return "Остановить";
			case "STOPPED":
				return "Запустить";
			default:
				throw new ArgumentException("Incorrect SimulationState");
		}
	}

	private void Awake()
	{
		startStopButtonText = startStopButton.GetComponentInChildren<TextMeshProUGUI>();
		startStopButton.onClick.AddListener(() =>
		{
			manager.switchState();
			updateUI();
		});
		healthyHumansCountSlider.onValueChanged.AddListener((value) =>
		{
			manager.HealthyHumanCount = (int)value;
			healthyHumanCountText.text = manager.HealthyHumanCount.ToString();
		});
		infectedHumansCountSlider.onValueChanged.AddListener((value) =>
		{
			manager.InfectedHumanCount = (int)value;
			infectedHumanCountText.text = manager.InfectedHumanCount.ToString();
		});
		infectionProbabilitySlider.onValueChanged.AddListener((value) =>
		{
			manager.InfectionProbability = value;
			updateInfectionProbabilityText();
		});
		infectionRadiusSlider.onValueChanged.AddListener((value) =>
		{
			manager.InfectionRadius = value;
			updateInfectionRadiusText();
		});
		timeToInfectSlider.onValueChanged.AddListener((value) =>
		{
			manager.TimeToInfect = TimeSpan.FromSeconds((double)value);
			updateTimeToInfectText();
		});
		updateUI();
	}

	private void updateInfectionProbabilityText()
	{
		infectionProbabilityText.text = manager.InfectionProbability.ToString("0.##");
	}

	private void updateTimeToInfectText()
	{
		timeToInfectText.text = manager.TimeToInfect.TotalSeconds.ToString("0.##");
	}

	private void updateInfectionRadiusText()
	{
		infectionRadiusText.text = manager.InfectionRadius.ToString("0.##");
	}
}
