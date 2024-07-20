using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class HumanAiController : MonoBehaviour
{
	private NavMeshAgent agent;
	[SerializeField]
	private float minWalkingRadius;
	[SerializeField]
	private float maxWalkingRadius;
	[SerializeField]
	private int maxPauseMs;

	private bool humanIsWaiting = false;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		agent.destination = getRandomDestination();	
	}

	private void FixedUpdate()
	{
		if (agent.remainingDistance < 0.05f && !humanIsWaiting)
		{
			StartCoroutine(setNextDestination());
		}
	}

	private IEnumerator setNextDestination()
	{
		humanIsWaiting = true;
		do
		{
			agent.destination = getRandomDestination();
			
		} while (agent.pathStatus == NavMeshPathStatus.PathInvalid);
		yield return new WaitForSeconds(UnityEngine.Random.Range(0, maxPauseMs) / 1000f);
		humanIsWaiting = false;
	}

	private Vector3 getRandomDestination()
	{
		Vector2 destination2d = UnityEngine.Random.insideUnitCircle.normalized * UnityEngine.Random.Range(minWalkingRadius, maxWalkingRadius);
		Vector3 destination = new Vector3(destination2d.x, 0, destination2d.y);
		return agent.destination = destination;
	}
}
