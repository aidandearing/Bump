using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{
    public OboScript Obo;
    public Rigidbody Projectile;

    private int soulsCollected = 0;

    // Use this for initialization
    void Start()
    {
        OboScript.Player = gameObject;
        SoulScript.Player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (soulsCollected > 0)
            {
                Instantiate(Projectile, transform.position, transform.rotation);
                RemoveSouls(1);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Obo.ReturnToPlayer();
        }
    }

    public void AddSouls(int num)
    {
        soulsCollected += num;
    }

    public void RemoveSouls(int num)
    {
        soulsCollected -= num;

        if (soulsCollected < 0)
            soulsCollected = 0;
    }
}
