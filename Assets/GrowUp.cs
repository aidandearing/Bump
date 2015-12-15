using UnityEngine;
using System.Collections;

public class GrowUp : MonoBehaviour
{
    public GameObject adult;

    public float lifespan = 60;

    private float lifeGoal = 0;
    private float lifeElapsed = 0;

    private Vector3 goalScale;
    private Vector3 startScale;

    private bool isGirl;
    // Use this for initialization
    void Start()
    {
        lifeGoal = Random.Range(lifespan / 2, lifespan * 1.5f);

        goalScale = transform.localScale;
        startScale = transform.localScale / 2;

        NameScript name = GetComponent<NameScript>();
        if (name.GetName() == "")
        {
            name.RandomName(isGirl);
        }

        LifeRules.peopleCount++;
    }

    // Update is called once per frame
    void Update()
    {
        lifeElapsed += Time.deltaTime;

        transform.localScale = Vector3.Lerp(startScale, goalScale, lifeElapsed / lifeGoal);

        if (lifeElapsed >= lifeGoal)
        {
            GameObject grownSelf = Instantiate(adult, transform.position, new Quaternion()) as GameObject;
            grownSelf.GetComponent<NameScript>().SetName(GetComponent<NameScript>().GetName());
            grownSelf.GetComponent<ColourOnStart>().ApplyColour(GetComponent<ColourOnStart>().myColour, GetComponent<ColourOnStart>().myColour);
            grownSelf.GetComponent<LifeRules>().SetGender(isGirl);
            Destroy(gameObject);
        }
    }

    public void InstantlyGrow()
    {
        lifeElapsed = lifeGoal;
    }

    /// <summary>
    /// Gets the gender of this adult
    /// </summary>
    /// <returns>true if girl false if boy</returns>
    public bool GetGender()
    {
        return isGirl;
    }

    /// <summary>
    /// Sets the gender of this adult
    /// </summary>
    /// <param name="gender">True for girl false for boy</param>
    public void SetGender(bool gender)
    {
        isGirl = gender;
    }
}
