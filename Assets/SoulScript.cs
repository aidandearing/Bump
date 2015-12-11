using UnityEngine;
using System.Collections;

public class SoulScript : MonoBehaviour
{
    public static GameObject Player;

    public float moveSpeed = 5;
    public float closeRange = 2;

    public float chaseDelay = 2;

    private float chaseDelayElapsed = 0;

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
            Vector3 delta = Player.transform.position - transform.position;

            Quaternion rotation = Quaternion.LookRotation(delta);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.05f);

            if (delta.magnitude < closeRange)
            {
                Player.GetComponent<PlayerBehaviour>().AddSouls(1);
                Destroy(gameObject);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, moveSpeed * Time.deltaTime);
    }
}