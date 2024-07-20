using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSettings : MonoBehaviour
{
	[SerializeField]
	private GameObject healthyHumanPrefab;

	[SerializeField]
	private GameObject infectedHumanPrefab;

	[SerializeField]
	private int healtyHumanCount;

	[SerializeField]
	private int infectedHumanCount;

	[SerializeField]
	private Vector3 spawnAreaSize;

	public GameObject HealthyHumanPrefab { get => healthyHumanPrefab; set => healthyHumanPrefab = value; }
	
	public GameObject InfectedHumanPrefab { get => infectedHumanPrefab; set => infectedHumanPrefab = value; }
	
	public int HealtyHumanCount { get => healtyHumanCount; set => healtyHumanCount = value; }
	
	public int InfectedHumanCount { get => infectedHumanCount; set => infectedHumanCount = value; }
	
	public Vector3 SpawnAreaSize { get => spawnAreaSize; set => spawnAreaSize = value; }
}
