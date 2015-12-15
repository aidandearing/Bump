using UnityEngine;
using System.Collections;

public class LifeRules : MonoBehaviour
{
    public static GameObject Player;

    public static int peopleCount;
    public const int PEOPLEMAX = 50;

    public GameObject child;
    public GameObject dead;
    public static GameObject Obo;
    public GameObject soul;

    public float lifespan = 60;
    public float reproductionAgeLimit = 0.5f;
    public float reproductionTime = 5;
    public float reproductionCooldown = 20;
    public float reproductionRange = 2;
    public string reproductionTag = "Player";
    public float ageCheck = 1;

    public float oboAgeMult = 2;

    private int numAround = 1;

    private float ageCheckElapsed = 0;
    private float lifeGoal = 0;
    private float lifeElapsed = 0;
    private float reproductionCounter = 0;
    private float reproductionCDtimer = 0;

    private bool isOld = false;

    private Vector3 originalScale;

    private bool isGirl;

    // Use this for initialization
    void Start()
    {
        lifeGoal = Random.Range(lifespan / 2, lifespan * 1.5f);
        reproductionCDtimer = reproductionCooldown;

        Vector3 scale = new Vector3(Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f));
        originalScale = transform.localScale = new Vector3(transform.localScale.x * scale.x, transform.localScale.y * scale.y, transform.localScale.z * scale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeElapsed / lifeGoal >= reproductionAgeLimit)
            lifeElapsed += Time.deltaTime / numAround;

        if ((Obo.transform.position - transform.position).magnitude <= 20)
            lifeElapsed += Time.deltaTime * oboAgeMult;

        ageCheckElapsed += Time.deltaTime;

        transform.localScale = originalScale * (1 - (lifeElapsed / lifeGoal / 8));

        if (ageCheckElapsed >= ageCheck)
        {
            ArrayList around = BehaviourUtil.SurroundingObjectsByTag(gameObject, "Player", reproductionRange);

            if (around != null)
                numAround = around.Count;
            else
                numAround = 1;

            ageCheckElapsed -= ageCheck;
        }

        if (lifeElapsed / lifeGoal <= reproductionAgeLimit)
        {
            reproductionCDtimer += Time.deltaTime;

            if (isGirl)
            {
                ArrayList objs = BehaviourUtil.SurroundingObjectsByTag(gameObject, "Player", reproductionRange);

                if (objs != null)
                {
                    bool canReproduce = false;
                    GameObject obj = null;

                    foreach (GameObject adult in objs)
                    {
                        LifeRules other = adult.GetComponent<LifeRules>();
                        if (other != null)
                        {
                            if (!other.isGirl && !other.isOld)
                            {
                                canReproduce = true;
                                obj = adult;
                            }
                        }
                    }

                    if (canReproduce)
                    {
                        if (reproductionCDtimer >= reproductionCooldown)
                        {
                            reproductionCounter += Time.deltaTime;

                            if (reproductionCounter >= reproductionTime)
                            {
                                if (peopleCount < PEOPLEMAX)
                                {
                                    Vector3 delta = obj.transform.position - transform.position;
                                    GameObject baby = Instantiate(child, transform.position + delta / 2, new Quaternion()) as GameObject;
                                    baby.transform.localScale = (transform.localScale + obj.transform.localScale) / 2;
                                    baby.GetComponent<ColourOnStart>().ApplyColour(obj.GetComponent<ColourOnStart>().myColour + Random.Range(-1, 1), GetComponent<ColourOnStart>().myColour + Random.Range(-1, 1));
                                }
                                reproductionCounter -= Random.Range(reproductionTime / 2, reproductionTime * 1.5f);
                                reproductionCDtimer -= Random.Range(reproductionCooldown / 2, reproductionCooldown * 1.5f);
                            }
                        }
                    }
                }
            }
        }

        if (lifeElapsed / lifeGoal > reproductionAgeLimit)
        {
            if (!isOld)
            {
                isOld = true;
                GetComponent<ColourOnStart>().MakeOld();
            }

            if (lifeElapsed >= lifeGoal)
            {
                dead.transform.localScale = transform.localScale;
                Instantiate(dead, transform.position, new Quaternion());

                if ((Obo.transform.position - transform.position).magnitude <= 10)
                {
                    Quaternion look = new Quaternion();
                    look.SetLookRotation(Player.transform.position - transform.position + new Vector3(0, 1000, 0));
                    Instantiate(soul, transform.position, look);
                }

                peopleCount--;
                Destroy(gameObject);
            }
        }
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
