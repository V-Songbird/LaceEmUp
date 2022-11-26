using JamOff.Scripts.Managers;
using UnityEngine;

public class PortalsManager : MonoBehaviour
{
    public PortalTeleport redPortal;
    public PortalTeleport bluePortal;

    public void MakePortal(bool isRedPortal)
    {
        if (!PortalsDistance()) return;

        var playerPosition = GamePlayManager.Instance.Player.transform.position;
        var portalToMake = isRedPortal ? redPortal : bluePortal;

        portalToMake.portalActive = true;
        portalToMake.transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + 0.2f);
    }

    private bool PortalsDistance()
    {
        var playerPosition = GamePlayManager.Instance.Player.transform.position;
        return Vector3.Distance(playerPosition, bluePortal.transform.position) > 1 &&
               Vector3.Distance(playerPosition, redPortal.transform.position) > 1;
    }
}