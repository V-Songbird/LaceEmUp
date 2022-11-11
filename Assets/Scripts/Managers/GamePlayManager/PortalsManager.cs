using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamOff.Scripts.Managers;

public class PortalsManager : MonoBehaviour
{

    [HideInInspector] public PortalTeleport redPortal;
    [HideInInspector] public PortalTeleport bluePortal;

    public void MakePortal(bool isRedPortal)
    {
        if (portalsDistance())
        {
            if (isRedPortal)
            {
                redPortal.portalActive = true;
                redPortal.transform.position = new Vector3(GamePlayManager.Instance.Player.transform.position.x, GamePlayManager.Instance.Player.transform.position.y, GamePlayManager.Instance.Player.transform.position.z + 0.2f);
            }
            else
            {
                bluePortal.portalActive = true;
                bluePortal.transform.position = new Vector3(GamePlayManager.Instance.Player.transform.position.x, GamePlayManager.Instance.Player.transform.position.y, GamePlayManager.Instance.Player.transform.position.z + 0.2f);
            }
        }
    }

    bool portalsDistance()
    {
        if (Vector3.Distance(GamePlayManager.Instance.Player.transform.position, bluePortal.transform.position) > 1 &&
        Vector3.Distance(GamePlayManager.Instance.Player.transform.position, redPortal.transform.position) > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
