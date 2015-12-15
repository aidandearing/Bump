using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject spawnable;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            float radius = Random.Range(0, 50);
            float angle = Random.Range(0, 360) * 0.0174533f;

            int colour = (int)Mathf.Floor(Random.Range(0, 11) % 3) * 4;

            GameObject adult = Instantiate(spawnable, new Vector3(radius * Mathf.Cos(angle), 1, radius * Mathf.Sin(angle)), new Quaternion()) as GameObject;
            adult.GetComponent<ColourOnStart>().ApplyColour(colour, colour);
            if (Random.Range(0, 9) < 5)
            {
                adult.GetComponent<LifeRules>().SetGender(true);
                adult.GetComponent<NameScript>().RandomName(true);
            }
            else
            {
                adult.GetComponent<LifeRules>().SetGender(false);
                adult.GetComponent<NameScript>().RandomName(false);
            }
            LifeRules.peopleCount++;
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
