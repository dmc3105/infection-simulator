using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSettings : MonoBehaviour
{	
	[SerializeField, Range(0, 1)]
	private float infectionProbability = 0.5f;

	[SerializeField, Min(0)]
	private int timeToInfectMs;

	[SerializeField, Min(0)]
	private float infectionRadius = 1f;

	public float InfectionProbability { get => infectionProbability; set => infectionProbability = value; }
	public TimeSpan TimeToInfect { get => TimeSpan.FromMilliseconds(timeToInfectMs); set => timeToInfectMs = (int)value.TotalMilliseconds ; }
	public float InfectionRadius { get => infectionRadius; set => infectionRadius = value; }
}
