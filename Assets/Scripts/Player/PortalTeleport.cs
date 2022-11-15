using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamOff.Scripts.Managers;

public class PortalTeleport : InteractObject
{
    [HideInInspector] public bool portalActive = false;
    public bool redPortal = false;

    private void Awake()
    {
        if (redPortal)
        {
            GamePlayManager.Instance.PortalsManager.redPortal = this;
        }
        else
        {
            GamePlayManager.Instance.PortalsManager.bluePortal = this;
        }
    }
}