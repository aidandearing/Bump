using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    public float dayLength = 30;
    public float dayStart = 15;
    public float updateThreshold = 0.1f;

    private float dayElapsed;
    private float updateElapsed = 0;

    // Use this for initialization
    void Start()
    {
        dayElapsed = dayStart;
        updateElapsed = updateThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        updateElapsed += Time.deltaTime;
        dayElapsed += Time.deltaTime;

        if (updateElapsed >= updateThreshold)
        {
            updateElapsed -= updateThreshold;

            if (dayElapsed >= dayLength)
                dayElapsed -= dayLength;

            float inclination = (dayElapsed / dayLength) * (Mathf.PI * 2);
            transform.LookAt(new Vector3(0, Mathf.Sin(inclination), Mathf.Cos(inclination)));
        }
    }
}
