using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
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

    private float checkTimeElapsed = 0.0f;
    private float avoidTimeElapsed = 0.0f;

    private Vector3 Target;
    private Vector3 Avoid;

    // Use this for initialization
    void Start()
    {
        checkTimeElapsed = checkTime;
        avoidTimeElapsed = avoidTime;
    }

    // Update is called once per frame
    void Update()
    {
        checkTimeElapsed += Time.deltaTime;

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

        if (Avoid != Vector3.zero && avoid)
        {
            Vector3 delta = transform.position - Avoid;
            delta.y = 0;

            Quaternion rotation = Quaternion.LookRotation(delta);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);

            if (delta.magnitude < avoidRadius)
                //transform.Translate(transform.forward * checkSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, avoidSpeed * Time.deltaTime);
        }
        else if (Target != Vector3.zero)
        {
            Vector3 delta = Target - transform.position;
            delta.y = 0;

            Quaternion rotation = Quaternion.LookRotation(delta);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);

            if (delta.magnitude > checkClose)
                //transform.Translate(transform.forward * checkSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, checkSpeed * Time.deltaTime);

            if (delta.magnitude > checkRadius / 2)
                SetTarget();
        }
        else
        {
            checkTimeElapsed = checkTime;
        }
    }

    void SetTarget()
    {
        GameObject obj;

        obj = BehaviourUtil.NearestObjectByTag(gameObject, tag, checkRadius);

        if (obj == null)
        {
            Target = Vector3.zero;
        }
        else
        {
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
}
