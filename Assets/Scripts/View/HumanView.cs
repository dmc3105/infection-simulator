using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Human))]
public class HumanView : MonoBehaviour
{
	private Human human;

	[SerializeField]
	private Material HealtyHumanMaterial;

	[SerializeField]
	private Material InfectedHumanMaterial;

	private void Awake()
	{
		human = GetComponent<Human>();
		human.onHealthChange += Human_HealthChange;
	}

	private void Human_HealthChange(object sender, Human.HealthStatus health)
	{
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.material = getMaterialByHealth(health);
	}

	private Material getMaterialByHealth(Human.HealthStatus health)
	{
		switch (health)
		{
			case Human.HealthStatus.HEALTHY:
				return HealtyHumanMaterial;
			case Human.HealthStatus.INFECTED:
				return InfectedHumanMaterial;
			default:
				throw new System.Exception("Incorrect HealthStatus");
		}
	}
}
