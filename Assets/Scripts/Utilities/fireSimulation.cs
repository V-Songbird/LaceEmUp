using UnityEngine;

public class fireSimulation : MonoBehaviour
{
    [SerializeField] private Light pointLight;
    public float timeInterval = 0.2f;

    [Space] public float minIntensity;
    public float maxIntensity;
    public float minRange;
    public float maxRange;
    [Space] public float minYPosition;
    public float maxYPosition;

    private float m_nextTime;

    // Update is called once per frame
    private void Update()
    {
        if (!(Time.time >= m_nextTime)) return;

        pointLight.range = Random.Range(minRange, maxRange);
        pointLight.intensity = Random.Range(minIntensity, maxIntensity);

        transform.localPosition =
            new Vector3(0, Random.Range(minYPosition, maxYPosition), 0);
        m_nextTime += timeInterval;
    }
}