using UnityEngine;
using System.Collections;

public class FireWhenLookingAt : MonoBehaviour
{
    public string tag = "Player";
    public float checkTime = 0.1f;
    public float checkRadius = 10f;
    public float shootAngle = 45;
    public float shootDistance = 10;
    public float shootRadiusStart = 1;
    public float shootRadiusEnd = 5;
    public float shootForce = 2500;
    public float shootTime = 1;

    private float checkTimeElapsed = 0;
    private float shootTimeElapsed = 0;

    // Use this for initialization
    void Start()
    {
        checkTimeElapsed = checkTime;
        shootTimeElapsed = shootTime;
    }

    // Update is called once per frame
    void Update()
    {
        checkTimeElapsed += Time.deltaTime;
        shootTimeElapsed += Time.deltaTime;

        if (checkTimeElapsed >= checkTime)
        {
            checkTimeElapsed -= checkTime;

            GameObject obj = BehaviourUtil.NearestObjectByTag(gameObject, tag, checkRadius);

            if (obj == null)
                return;

            if (shootTimeElapsed >= shootTime)
            {
                Shoot shoot = GetComponent<Shoot>();

                if (shoot != null)
                {
                    shoot.ConicBlast(gameObject, transform.position, transform.forward, shootDistance, shootRadiusStart, shootRadiusEnd, shootForce);
                }

                shootTimeElapsed -= shootTime;
            }
        }
    }
}
