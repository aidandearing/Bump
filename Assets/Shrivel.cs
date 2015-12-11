using UnityEngine;
using System.Collections;

public class Shrivel : MonoBehaviour
{
    public float shrivelTime = 10;
    public float shrivelStart = 7.5f;

    private Vector3 startScale;

    private float shrivelGoal = 0;
    private float shrivelElapsed = 0;

    // Use this for initialization
    void Start()
    {
        shrivelGoal = shrivelTime;
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        shrivelElapsed += Time.deltaTime;

        float percent = (shrivelElapsed - shrivelStart) / (shrivelGoal - shrivelStart);

        transform.localScale = Vector3.Lerp(startScale, Vector3.zero, percent);

        if (percent >= 1)
            Destroy(gameObject);
    }
}
