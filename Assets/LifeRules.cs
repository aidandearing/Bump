using UnityEngine;
using System.Collections;

public class LifeRules : MonoBehaviour
{
    public static int peopleCount;
    public const int PEOPLEMAX = 50;

    public Rigidbody child;
    public Rigidbody dead;
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
    private bool hasMadeDead = false;

    private Vector3 originalScale;

    // Use this for initialization
    void Start()
    {
        lifeGoal = Random.Range(lifespan / 2, lifespan * 1.5f);
        reproductionCDtimer = reproductionCooldown;

        Vector3 scale = new Vector3(Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f));
        originalScale = transform.localScale = new Vector3(transform.localScale.x * scale.x, transform.localScale.y * scale.y, transform.localScale.z * scale.z);

        NameScript name = GetComponent<NameScript>();
        if (name.GetName() == "")
        {
            name.RandomName();
        }

        peopleCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeElapsed / lifeGoal <= reproductionAgeLimit)
            lifeElapsed += Time.deltaTime * numAround;
        else
            lifeElapsed += Time.deltaTime / numAround;

        if ((Obo.transform.position - transform.position).magnitude <= 20)
            lifeElapsed += Time.deltaTime * oboAgeMult;

        reproductionCDtimer += Time.deltaTime;
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

            GameObject obj = BehaviourUtil.NearestObjectByTag(gameObject, "Player", reproductionRange);

            if (obj != null && obj.GetComponent<LifeRules>() != null && obj.GetComponent<GrowUp>() == null)
            {
                if (!obj.GetComponent<LifeRules>().isOld)
                {
                    Vector3 delta = obj.transform.position - transform.position;

                    if (reproductionCDtimer >= reproductionCooldown)
                    {
                        reproductionCounter += Time.deltaTime;

                        if (reproductionCounter >= reproductionTime)
                        {
                            if (peopleCount < PEOPLEMAX)
                            {
                                child.GetComponent<ColourOnStart>().ApplyColour(obj.GetComponent<ColourOnStart>().myColour, GetComponent<ColourOnStart>().myColour);
                                if (child != null)
                                {
                                    //child.GetComponent<ColourOnStart>().ApplyColour(obj.GetComponent<ColourOnStart>().myColour, GetComponent<ColourOnStart>().myColour);
                                    child.transform.localScale = (transform.localScale + obj.transform.localScale) / 2;
                                    Instantiate(child, transform.position + delta / 2, new Quaternion());
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
                if (hasMadeDead)
                {
                    peopleCount--;
                    Destroy(gameObject);
                }
                else
                {
                    hasMadeDead = true;
                    dead.transform.localScale = transform.localScale;
                    Instantiate(dead, transform.position, new Quaternion());

                    if ((Obo.transform.position - transform.position).magnitude <= 10)
                    {
                        Quaternion look = transform.rotation;
                        look.SetLookRotation(transform.position + new Vector3(0, 1000, 0));
                        Instantiate(soul, transform.position, look);
                    }
                }
            }
        }
    }
}
