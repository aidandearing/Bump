using UnityEngine;
using System.Collections;

public class SoulShotScript : MonoBehaviour
{
    public float homingRange = 5;
    public float moveSpeed = 20;
    public float closeRange = 2;
    public float lifeSpan = 10;

    public float chaseDelay = 2;

    private float chaseDelayElapsed = 0;

    private GameObject Target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        chaseDelayElapsed += Time.deltaTime;

        if (chaseDelayElapsed > chaseDelay)
        {
            if (Target == null)
                SetTarget();
            else
            {
                Vector3 delta = Target.transform.position - transform.position;

                Quaternion rotation = Quaternion.LookRotation(delta);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.05f);

                if (delta.magnitude < closeRange)
                {
                    // Get what object type it collided with and do stuff!
                    GrowUp grow = Target.GetComponent<GrowUp>();

                    if (grow != null)
                        grow.InstantlyGrow();

                    Destroy(gameObject);
                }
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, moveSpeed * Time.deltaTime);

        if (chaseDelayElapsed >= lifeSpan)
            Destroy(gameObject);
    }

    void SetTarget()
    {
        GameObject obj;

        obj = BehaviourUtil.NearestObjectByTag(gameObject, "Child", homingRange);

        Target = obj;
    }
}