using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }
    // Update is called once per frame
    private void Start()
    {
        _camera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 200 + PlayerPrefs.GetFloat("XSensitivity") * 10;
        _camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 200 + PlayerPrefs.GetFloat("YSensitivity") * 10;
        _camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_AccelTime = 0.15f - PlayerPrefs.GetFloat("MouseSmoothing") * 0.1f;
        _camera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_AccelTime = 0.15f - PlayerPrefs.GetFloat("MouseSmoothing") * 0.1f;
    }
}
