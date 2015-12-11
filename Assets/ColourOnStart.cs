using UnityEngine;
using System.Collections;

public class ColourOnStart : MonoBehaviour
{
    public Material[] colours;
    public MeshRenderer renderer;

    public int myColour;

    // Use this for initialization
    public void ApplyColour(int parent1, int parent2)
    {
        // The colours as of now are
        // 0 = red
        // 1 = fuschia
        // 2 = magenta
        // 3 = purple
        // 4 = blue
        // 5 = aqua
        // 6 = cyan
        // 7 = mint
        // 8 = green
        // 9 = lime
        // 10= yellow
        // 11= orange
        // 12= grey

        // I should be able to add the parent ints together and divide by 2 to pick a new colour
        // But that will require me treating red as both 0 and 12
        // What is the logic for this, if either parent has a value greater than 5 and the other has a value of 0, set the value of zero to 12

        if (parent1 == 0 && parent2 >= 5)
            parent1 = 12;
        else if (parent2 == 0 && parent1 >= 5)
            parent2 = 12;

        myColour = (parent1 + parent2) / 2;

        renderer.material = colours[myColour];
    }

    public void MakeOld()
    {
        myColour = 12;

        renderer.material = colours[myColour];
    }
}
