using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Mirror;
using UnityEngine;

public class PlayerCameraController : NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private CinemachineTransposer transposer;

    public Transform target;

    public override void OnStartAuthority()
    {
        if (isLocalPlayer)
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            virtualCamera.gameObject.SetActive(true);
            virtualCamera.Follow = target;
            virtualCamera.LookAt = target;
            enabled = true;
        }
    }

    private void Start()
    {
    }
}