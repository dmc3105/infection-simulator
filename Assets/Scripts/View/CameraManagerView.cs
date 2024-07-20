using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraManagerView : MonoBehaviour
{
    [SerializeField]
    private Button previousCameraButton;
    
    [SerializeField]
    private Button nextCameraButton;

	[SerializeField]
	private TextMeshProUGUI cameraNameLabel;

    [SerializeField]
    private CameraManager cameraManager;

	private void Awake()
	{
        nextCameraButton.onClick.AddListener(() =>
		{
			cameraManager.NextCamera();
			updateButtons();
		});
        previousCameraButton.onClick.AddListener(() =>
		{
			cameraManager.PreviousCamera();
			updateButtons();
		});
	}

	private void Start()
	{
		updateButtons();
	}

	private void updateButtons()
	{
		cameraNameLabel.SetText(cameraManager.ActiveCamera.name);
		nextCameraButton.interactable = cameraManager.IsPossibleChangeNextCamera;
		previousCameraButton.interactable = cameraManager.IsPossibleChangePreviousCamera;
	}
}
