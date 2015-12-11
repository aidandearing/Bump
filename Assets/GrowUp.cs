using UnityEngine;
using System.Collections;

public class GrowUp : MonoBehaviour
{
    public Rigidbody adult;

    public float lifespan = 60;

    private float lifeGoal = 0;
    private float lifeElapsed = 0;

    private Vector3 goalScale;
    private Vector3 startScale;
    // Use this for initialization
    void Start()
    {
        lifeGoal = Random.Range(lifespan / 2, lifespan * 1.5f);

        goalScale = transform.localScale;
        startScale = transform.localScale / 2;

        NameScript name = GetComponent<NameScript>();
        if (name.GetName() == "")
        {
            name.RandomName();
        }
    }

    // Update is called once per frame
    void Update()
    {
        lifeElapsed += Time.deltaTime;

        transform.localScale = Vector3.Lerp(startScale, goalScale, lifeElapsed / lifeGoal);

        if (lifeElapsed >= lifeGoal)
        {
            adult.GetComponent<ColourOnStart>().ApplyColour(GetComponent<ColourOnStart>().myColour, GetComponent<ColourOnStart>().myColour);
            Instantiate(adult, transform.position, new Quaternion());
            adult.GetComponent<NameScript>().SetName(GetComponent<NameScript>().GetName());
            Destroy(gameObject);
        }
    }

    public void InstantlyGrow()
    {
        lifeElapsed = lifeGoal;
    }
}
