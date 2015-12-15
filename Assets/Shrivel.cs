using UnityEngine;
using System.Collections;

public class Shrivel : MonoBehaviour
{
    public float shrivelTime = 10;
    public float shrivelStart = 7.5f;

    private float shrivelGoal = 0;
    private float shrivelElapsed = 0;

    // Use this for initialization
    void Start()
    {
        shrivelGoal = shrivelTime;
    }

    // Update is called once per frame
    void Update()
    {
        shrivelElapsed += Time.deltaTime;

        if (shrivelElapsed >= shrivelStart)
        {
            GetComponent<Collider>().isTrigger = false;
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider collide in colliders)
            {
                collide.isTrigger = true;
            }
        }

        if (shrivelElapsed >= shrivelGoal)
            Destroy(gameObject);
    }
}
