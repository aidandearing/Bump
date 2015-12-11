using UnityEngine;
using System.Collections;

public class NameScript : MonoBehaviour
{
    public static NameManager nameManager;

    public TextMesh mesh;

    private string Name = "";

    public void SetName(string name)
    {
        Name = name;
        mesh.text = Name;
    }

    public string GetName()
    {
        return Name;
    }

    public void RandomName()
    {
        SetName(nameManager.names[Random.Range(0, nameManager.names.Length - 1)]);
    }
}
