using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public Rigidbody spawnable;
    public float GoalValue = 60;

    private float timeGoal;
    private float elapsedTime;

    // Use this for initialization
    void Start()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > timeGoal)
        {
            for (int i = 0; i < Random.Range(10, 20); i++)
            {
                float radius = Random.Range(0, 50);
                float angle = Random.Range(0, 360) * 0.0174533f;

                int colour = (int)Mathf.Floor(Random.Range(0, 11) % 3) * 4;

                spawnable.GetComponent<ColourOnStart>().ApplyColour(colour, colour);

                Instantiate(spawnable, new Vector3(radius * Mathf.Cos(angle), 1, radius * Mathf.Sin(angle)), new Quaternion());
            }

            timeGoal = Random.Range(GoalValue / 2, GoalValue * 1.5f);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    elapsedTime += Time.deltaTime;

    //    if (elapsedTime > timeGoal)
    //    {
    //        for (int i = 0; i < Random.Range(50, 100); i++)
    //        {
    //            float radius = Random.Range(0, 200);
    //            float angle = Random.Range(0, 360) * 0.0174533f;
    //            Instantiate(spawnable, new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle)), new Quaternion());
    //        }

    //        timeGoal = Random.Range(GoalValue / 2, GoalValue * 1.5f);
    //    }
    //}
}
