using UnityEngine;
using System.Collections;

public class OboScript : MonoBehaviour
{
    public static GameObject Player;
    
    private bool isReturning = false;

    public string tag = "Player";
    public float checkTime = 0.1f;
    public float checkRadius = 100;
    public float checkClose = 2.5f;
    public float checkSpeed = 5;

    public bool avoid = false;
    public string avoidTag = "Player";
    public float avoidTime = 0.1f;
    public float avoidRadius = 10;
    public float avoidSpeed = 5;

    public Light light;
    public float lightFlickerTime = 0.2f;
    public float lightFlickerRange = 0.5f;
    public float lightFlickerIntensity = 0.5f;

    private float checkTimeElapsed = 0.0f;
    private float avoidTimeElapsed = 0.0f;
    private float elapsedTime = 0.0f;
    private float lightFlickerGoal = 0.0f;
    private float lightTimeElapsed = 0.0f;

    private float startY = 0;

    private float lightBeginRange;
    private float lightStartRange;
    private float lightGoalRange;
    private float lightBeginIntensity;
    private float lightStartIntensity;
    private float lightGoalIntensity;

    private Vector3 Target;
    private Vector3 Avoid;

    // Use this for initialization
    void Start()
    {
        LifeRules.Obo = gameObject;
        checkTimeElapsed = checkTime;
        avoidTimeElapsed = avoidTime;
        lightTimeElapsed = lightFlickerTime;
        startY = transform.position.y;
        lightBeginRange = light.range;
        lightStartRange = lightBeginRange;
        lightBeginIntensity = light.intensity;
        lightStartIntensity = lightBeginIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        checkTimeElapsed += Time.deltaTime;
        elapsedTime += Time.deltaTime;
        lightTimeElapsed += Time.deltaTime;

        if (avoid)
        {
            avoidTimeElapsed += Time.deltaTime;
        }

        if (checkTimeElapsed >= checkTime)
        {
            checkTimeElapsed -= Random.Range(checkTime / 2, checkTime * 1.5f);
            SetTarget();
        }

        if (avoid)
        {
            if (avoidTimeElapsed >= avoidTime)
            {
                avoidTimeElapsed -= Random.RandomRange(avoidTime / 2, avoidTime * 1.5f);
                SetAvoid();
            }
        }

        if (isReturning)
        {
            Avoid = Vector3.zero;
            Target = Player.transform.position;

            Vector3 delta = Target - transform.position;
            delta.y = 0;

            if (delta.magnitude <= avoidRadius)
                isReturning = false;

            Quaternion rotation = Quaternion.LookRotation(delta);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);
            
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, checkSpeed * Time.deltaTime);
        }
        else
        {
            if (Avoid != Vector3.zero && avoid)
            {
                Vector3 delta = transform.position - Avoid;
                delta.y = 0;

                Quaternion rotation = Quaternion.LookRotation(delta);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);

                if (delta.magnitude < avoidRadius)
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, avoidSpeed * Time.deltaTime);
            }
            else if (Target != Vector3.zero)
            {
                Vector3 delta = Target - transform.position;
                delta.y = 0;

                Quaternion rotation = Quaternion.LookRotation(delta);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);

                if (delta.magnitude > checkClose)
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, checkSpeed * Time.deltaTime);

                if (delta.magnitude > checkRadius / 2)
                    SetTarget();
            }
            else
            {
                checkTimeElapsed = checkTime;
            }
        }

        transform.position = new Vector3(transform.position.x, startY + Mathf.Sin(elapsedTime), transform.position.z);

        light.range = Mathf.Lerp(lightStartRange, lightGoalRange, lightTimeElapsed / lightFlickerGoal);
        light.intensity = Mathf.Lerp(lightStartIntensity, lightGoalIntensity, lightTimeElapsed / lightFlickerGoal);

        if (lightTimeElapsed >= lightFlickerGoal)
        {
            lightTimeElapsed -= lightFlickerGoal;

            lightFlickerGoal = Random.Range(lightFlickerTime / 2, lightFlickerTime * 1.5f);

            lightGoalRange = lightBeginRange + Random.Range(-lightFlickerRange, lightFlickerRange);
            lightGoalIntensity = lightBeginIntensity + Random.Range(-lightFlickerIntensity, lightFlickerIntensity);

            lightStartRange = light.range;
            lightStartIntensity = light.intensity;
        }
    }

    void SetTarget()
    {

        //ArrayList objs = BehaviourUtil.SurroundingObjectsByTag(gameObject, tag, checkRadius);
        GameObject obj = BehaviourUtil.NearestObjectByTag(gameObject, tag, checkRadius);

        //if (objs == null)
        if (obj == null)
        {
            Target = Player.transform.position;
        }
        else
        {
            //if ((Player.transform.position - transform.position).magnitude < checkRadius)
            //{
            //    foreach (GameObject obj in objs)
            //    {
            //        if (obj != Player)
            //        {
            //            Target = obj.transform.position;
            //            break;
            //        }
            //    }
            //}
            //else
            //    Target = BehaviourUtil.NearestObjectByTag(gameObject, tag, checkRadius).transform.position;
            Target = obj.transform.position;
        }
    }

    void SetAvoid()
    {
        GameObject obj;

        obj = BehaviourUtil.NearestObjectByTag(gameObject, avoidTag, avoidRadius);

        if (obj == null)
        {
            Avoid = Vector3.zero;
        }
        else
        {
            Avoid = obj.transform.position;
        }
    }

    public void ReturnToPlayer()
    {
        if (isReturning)
            return;
        else
        {
            isReturning = true;
        }
    }

}
