using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public void ConicBlast(GameObject me, Vector3 position, Vector3 direction, float distance, float radiusStart, float radiusEnd, float force)
    {
        //Ray ray = new Ray(position, direction);
        //RaycastHit[] hits = Physics.SphereCastAll(ray, radius);
        ArrayList list = new ArrayList();

        float traveled = 0;

        while (traveled < distance)
        {
            float radiusCurrent = radiusStart + (radiusEnd - radiusStart) * (traveled / distance);
            traveled += radiusCurrent / 2.5f;

            Collider[] colliders = Physics.OverlapSphere(position + direction * traveled, radiusCurrent);

            foreach (Collider collide in colliders)
                list.Add(collide);
        }

        ArrayList uniques = new ArrayList();

        for (int i = 0; i < list.Count; i++)
        {
            if (!uniques.Contains(list[i]))
                uniques.Add(list[i]);
        }

        foreach (Collider collide in uniques)
        {
            if (collide.gameObject != me)
            {
                if (collide.GetComponent<Rigidbody>() != null)
                    collide.GetComponent<Rigidbody>().AddExplosionForce(force, position, distance + radiusEnd);
            }
        }
    }
}
