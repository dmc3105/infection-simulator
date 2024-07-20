using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private List<Camera> cameras = new List<Camera>();
    private LinkedList<Camera> camerasInternal;

    private LinkedListNode<Camera> activeCamera;

	public bool IsPossibleChangeNextCamera
	{
		get => activeCamera.Next != null;
	}

	public bool IsPossibleChangePreviousCamera
	{
		get => activeCamera.Previous != null;
	}

	public Camera ActiveCamera { get => activeCamera.Value; }

	public void NextCamera()
    {
		activeCamera.Value.enabled = false;
		activeCamera = activeCamera.Next;
		activeCamera.Value.enabled = true;
		Camera.SetupCurrent(activeCamera.Value);
    }

    public void PreviousCamera()
    {
        activeCamera.Value.enabled= false;
        activeCamera = activeCamera.Previous;
        activeCamera.Value.enabled = true;
        Camera.SetupCurrent(activeCamera.Value);
	}

	private void Awake()
	{
		camerasInternal = new LinkedList<Camera>(cameras);
        disableAllCameras();
        activeCamera = camerasInternal.First;
        activeCamera.Value.enabled = true;
	}

	private void disableAllCameras()
	{
        foreach (var camera in camerasInternal)
        {
            camera.enabled = false;
        }
	}
}
