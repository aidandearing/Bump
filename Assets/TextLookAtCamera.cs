using UnityEngine;
using System.Collections;

public class TextLookAtCamera : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        transform.localScale = Vector3.one * Mathf.Clamp((Camera.main.transform.position - transform.position).magnitude / 50, 0.1f, Mathf.Infinity);
    }
}
