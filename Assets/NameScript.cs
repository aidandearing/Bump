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

    public void RandomName(bool isGirl)
    {
        if (isGirl)
            SetName(nameManager.names[Random.Range(0, (nameManager.names.Length / 2) - 1)]);
        else
            SetName(nameManager.names[Random.Range((nameManager.names.Length / 2) - 1, nameManager.names.Length - 1)]);
    }
}
