using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{
    public float dayLength = 30;
    public float dayStart = 15;
    public float updateThreshold = 0.1f;
    public float[] dayIntensities;

    private float dayElapsed;
    private float updateElapsed = 0;

    private Light me;

    // Use this for initialization
    void Start()
    {
        dayElapsed = dayStart;
        updateElapsed = updateThreshold;
        me = GetComponent<Light>();
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

            float percentage = dayElapsed / dayLength;

            UpdateAngle(percentage);
            UpdateIntensity(percentage);
        }
    }

    void UpdateAngle(float percentage)
    {
        float inclination = percentage * (Mathf.PI * 2);
        transform.LookAt(new Vector3(0, Mathf.Sin(inclination), Mathf.Cos(inclination)));
    }

    void UpdateIntensity(float percentage)
    {
        int steps = dayIntensities.Length - 1;

        int currentStep = Mathf.CeilToInt(percentage * steps);

        currentStep = Mathf.Clamp(currentStep, 0, steps);

        int previous = currentStep - 1;

        if (previous < 0)
            previous = steps;

        // I need a measure of completion between the last step and the current step as a function of total percentage done.
        me.intensity = me.bounceIntensity = Mathf.Lerp(dayIntensities[previous], dayIntensities[currentStep], (percentage * steps) - (currentStep - 1));
    }
}
