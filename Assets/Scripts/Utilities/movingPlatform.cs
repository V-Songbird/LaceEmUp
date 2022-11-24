using System.Collections;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    [SerializeField] private Transform startMarker;
    [SerializeField] private Transform endMarker;
    [SerializeField] private Transform platform;
    
    // Movement speed of the platform
    [SerializeField] private float speed = 1.0F;

    // Seconds to wait after repeating the movement
    [SerializeField] private float idleSeconds;

    private bool isMovementInverted;
    private bool isMovementPaused;
    private float journeyLength;
    private float startTime;

    private void Start()
    {
        if (!startMarker || !endMarker || !platform) return;
        
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    private void Update()
    {
        if (!startMarker || !endMarker || !platform || isMovementPaused) return;
        
        var distCovered = (Time.time - startTime) * speed;
        var fractionOfJourney = distCovered / journeyLength;

        // Reinitialize the variables to invert the movement
        if (fractionOfJourney >= 1)
        {
                StartCoroutine(WaitToStartMoving());
                distCovered = 0;
                fractionOfJourney = 0;
                isMovementInverted = !isMovementInverted;
        }

        // Set our position as a fraction of the distance between the markers.
        platform.position = !isMovementInverted
            ? Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney)
            : Vector3.Lerp(endMarker.position, startMarker.position, fractionOfJourney);
    }

    private void OnDrawGizmos()
    {
        if (startMarker && endMarker) Gizmos.DrawLine(startMarker.position, endMarker.position);
    }

    private IEnumerator WaitToStartMoving()
    {
        isMovementPaused = true;
        yield return new WaitForSeconds(idleSeconds);
        isMovementPaused = false;
        startTime = Time.time;
    }
}