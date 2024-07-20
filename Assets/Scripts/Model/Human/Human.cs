using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Human : MonoBehaviour
{
	public enum HealthStatus { HEALTHY, INFECTED };

	[SerializeField]
	private HealthStatus health;

	private HumanSettings settings;

	private Dictionary<Human, DateTime> possibleInfected = new Dictionary<Human, DateTime>();

	public HealthStatus Health
	{
		get => health;
		set
		{
			health = value;
			onHealthChange?.Invoke(this, value);
		}
	}
	public float InfectionProbability { get => Settings.InfectionProbability; set => Settings.InfectionProbability = value; }
	public TimeSpan TimeToInfect { get => Settings.TimeToInfect; set => Settings.TimeToInfect = value; }
	public float InfectionRadius { get => Settings.InfectionRadius; set => Settings.InfectionRadius = value; }
	public HumanSettings Settings 
	{ get => settings;
		set
		{
			settings = value;
			updateInfectionRadius();
		}
	}

	public static event EventHandler<HealthStatus> onHumanHealthChange;
	public static event EventHandler<bool> onHumanTryInfect;

	public event EventHandler<HealthStatus> onHealthChange;

	private void Start()
	{
		onHealthChange += (sender, health) => { onHumanHealthChange?.Invoke(sender, health); };
		updateInfectionRadius();
	}

	private void FixedUpdate()
	{
		foreach (var human in new Dictionary<Human, DateTime>(possibleInfected))
		{
			TimeSpan timeDifference = DateTime.Now - human.Value;
			if (timeDifference >= Settings.TimeToInfect)
			{
				if (human.Key.health == HealthStatus.HEALTHY)
				{
					tryInfect(human.Key, timeDifference, Settings.InfectionProbability);
				}
				possibleInfected.Remove(human.Key);
			}

		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Human otherHuman = other.GetComponent<Human>();
		if (otherHuman == null)
			return;
		if (this.Health == HealthStatus.INFECTED && otherHuman.Health == HealthStatus.HEALTHY)
		{
			possibleInfected.TryAdd(otherHuman, DateTime.Now);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Human otherHuman = other.GetComponent<Human>();
		if (otherHuman == null)
			return;
		if (possibleInfected.ContainsKey(otherHuman))
		{
			possibleInfected.Remove(otherHuman);
		}
	}

	private void tryInfect(Human otherHuman, TimeSpan timeDifference, float infectionProbability)
	{
		if (timeDifference >= Settings.TimeToInfect && UnityEngine.Random.value < infectionProbability)
		{
			otherHuman.Health = HealthStatus.INFECTED;
			onHumanTryInfect?.Invoke(this, true);
		}
		else
		{
			onHumanTryInfect?.Invoke(this, false);
		}
	}

	private void updateInfectionRadius()
	{
		Collider[] collider= GetComponents<Collider>();
		CapsuleCollider triggerCollider = (CapsuleCollider)collider.Where((x) => x.isTrigger == true).First();

		if (triggerCollider != null)
		{
			triggerCollider.radius = InfectionRadius;
		}
	}
}
