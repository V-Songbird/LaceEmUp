using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraOffsetByViewDir : MonoBehaviour
{
    [SerializeField] private Transform playerGFX;

    private CinemachineFramingTransposer virtualCameraFramingTransposer;

    private void Start()
    {
        virtualCameraFramingTransposer = GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineFramingTransposer>();

        if (!virtualCameraFramingTransposer)
            throw new ArgumentException("Virtual Camera requires a Framing Transposer");
    }

    private void Update()
    {
        virtualCameraFramingTransposer.m_TrackedObjectOffset.x = playerGFX.localScale.x >= 0 ? 1 : -1;
    }
}