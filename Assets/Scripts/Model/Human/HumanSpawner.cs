using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class HumanSpawner : MonoBehaviour
{
	[SerializeField]
	private SpawnSettings settings;

	[SerializeField]
	private HumanSettings humanSettings;

	public GameObject HealthyHumanPrefab { get => settings.HealthyHumanPrefab; set => settings.HealthyHumanPrefab = value; }

	public GameObject InfectedHumanPrefab { get => settings.InfectedHumanPrefab; set => settings.InfectedHumanPrefab = value; }

	public int HealtyHumanCount { get => settings.HealtyHumanCount; set => settings.HealtyHumanCount = value; }

	public int InfectedHumanCount { get => settings.InfectedHumanCount; set => settings.InfectedHumanCount = value; }

	public Vector3 SpawnAreaSize { get => settings.SpawnAreaSize; set => settings.SpawnAreaSize = value; }

	private void SpawnHuman(Human.HealthStatus healthStatus)
	{
		GameObject newHumanObject = Instantiate(getHumanByHealthStatus(healthStatus), getRandomSpawnPosition(), new Quaternion(), this.transform);
		Human newHuman = newHumanObject.GetComponent<Human>();
		if (newHuman != null)
		{
			newHuman.Settings = humanSettings;
		}
	}

	private Vector3 getRandomSpawnPosition()
	{
		var x = Random.Range(-SpawnAreaSize.x / 2, SpawnAreaSize.x / 2) + transform.position.x;
		var y = Random.Range(-SpawnAreaSize.y / 2, SpawnAreaSize.y / 2) + transform.position.y;
		var z = Random.Range(-SpawnAreaSize.z / 2, SpawnAreaSize.z / 2) + transform.position.z;
		return new Vector3(x, y, z);
	}

	private GameObject getHumanByHealthStatus(Human.HealthStatus healthStatus)
	{
		switch (healthStatus)
		{
			case Human.HealthStatus.HEALTHY:
				return HealthyHumanPrefab;
			case Human.HealthStatus.INFECTED:
				return InfectedHumanPrefab;
			default:
				throw new System.Exception("Incorrect health status");
		}
	}

	public void Spawn()
	{
		for (int i = 0; i < HealtyHumanCount; i++)
		{
			SpawnHuman(Human.HealthStatus.HEALTHY);
		}
		for (int i = 0; i < InfectedHumanCount; i++)
		{
			SpawnHuman(Human.HealthStatus.INFECTED);
		}
	}

	private void OnDrawGizmos()
	{
		if (settings != null)
		{
			Gizmos.color = new Color(0, 0, 1, 0.1f);
			Gizmos.DrawCube(transform.position, settings.SpawnAreaSize);
		}
	}
}
