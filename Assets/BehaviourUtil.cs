using UnityEngine;
using System.Collections;

public static class BehaviourUtil
{
    /// <summary>
    /// Uses a Physics.OverlapSphere to get all collisions, then makes a collection of ones matching the tag and iterates across them in order to return either null or the farthest object
    /// </summary>
    /// <param name="me">Me!</param>
    /// <param name="tag">The Tag to search for the object having</param>
    /// <param name="radius">The radius in ingame units to search outwards from this object</param>
    /// <returns>A GameObject or null (be prepared for NULLS!)</returns>
    public static GameObject FarthestObjectByTag(GameObject me, string tag, float radius)
    {
        GameObject farthest = null;
        Collider[] info = Physics.OverlapSphere(me.transform.position, radius);

        ArrayList tagMatches = new ArrayList();

        foreach (Collider hit in info)
        {
            if (hit.gameObject != null)
            {
                if (hit.gameObject.CompareTag(tag) && hit.gameObject != me)
                    tagMatches.Add(hit.gameObject);
            }
        }

        if (tagMatches.Count < 1)
            return null;

        float distance = -Mathf.Infinity;

        foreach (GameObject obj in tagMatches)
        {
            float dist = (obj.transform.position - me.transform.position).sqrMagnitude;
            if (dist > distance)
            {
                distance = dist;
                farthest = obj;
            }
        }

        return farthest;
    }

    /// <summary>
    /// Uses a Physics.OverlapSphere to get all collisions, then makes a collection of ones matching the tag and iterates across them in order to return either null or the nearest object
    /// </summary>
    /// <param name="me">Me!</param>
    /// <param name="tag">The Tag to search for the object having</param>
    /// <param name="radius">The radius in ingame units to search outwards from this object</param>
    /// <returns>A GameObject or null (be prepared for NULLS!)</returns>
    public static GameObject NearestObjectByTag(GameObject me, string tag, float radius)
    {
        GameObject nearest = null;
        Collider[] info = Physics.OverlapSphere(me.transform.position, radius);

        ArrayList tagMatches = new ArrayList();

        foreach (Collider hit in info)
        {
            if (hit.gameObject != null)
            {
                if (hit.gameObject.CompareTag(tag) && hit.gameObject != me)
                    tagMatches.Add(hit.gameObject);
            }
        }

        if (tagMatches.Count < 1)
            return null;

        float distance = Mathf.Infinity;

        foreach (GameObject obj in tagMatches)
        {
            float dist = (obj.transform.position - me.transform.position).sqrMagnitude;
            if (dist < distance)
            {
                distance = dist;
                nearest = obj;
            }
        }

        return nearest;
    }

    /// <summary>
    /// Uses a Physics.OverlapSphere to get all collisions, then makes a collection of ones matching the tag and iterates across them in order to return either null or the nearest object
    /// </summary>
    /// <param name="me">Me!</param>
    /// <param name="tag">The Tag to search for the object having</param>
    /// <param name="radius">The radius in ingame units to search outwards from this object</param>
    /// <returns>A GameObject or null (be prepared for NULLS!)</returns>
    public static GameObject NearestObjectAtPositionByTag(Vector3 pos, string tag, float radius)
    {
        GameObject nearest = null;
        Collider[] info = Physics.OverlapSphere(pos, radius);

        ArrayList tagMatches = new ArrayList();

        foreach (Collider hit in info)
        {
            if (hit.gameObject != null)
            {
                tagMatches.Add(hit.gameObject);
            }
        }

        if (tagMatches.Count < 1)
            return null;

        float distance = Mathf.Infinity;

        foreach (GameObject obj in tagMatches)
        {
            float dist = (obj.transform.position - pos).sqrMagnitude;
            if (dist < distance)
            {
                distance = dist;
                nearest = obj;
            }
        }

        return nearest;
    }

    /// <summary>
    /// Uses a Physics.OverlapSphere to get all collisions, and returns all the collisions with matching Tags
    /// </summary>
    /// <param name="me">Me!</param>
    /// <param name="tag">The Tag to search for the object having</param>
    /// <param name="radius">The radius in ingame units to search outwards from this object</param>
    /// <returns>A GameObject or null (be prepared for NULLS!)</returns>
    public static ArrayList SurroundingObjectsByTag(GameObject me, string tag, float radius)
    {
        Collider[] info = Physics.OverlapSphere(me.transform.position, radius);

        ArrayList tagMatches = new ArrayList();

        foreach (Collider hit in info)
        {
            if (hit.gameObject != null)
            {
                if (hit.gameObject.CompareTag(tag) && hit.gameObject != me)
                    tagMatches.Add(hit.gameObject);
            }
        }

        if (tagMatches.Count < 1)
            return null;

        return tagMatches;
    }
}
