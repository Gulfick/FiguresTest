using System.Collections.Generic;
using UnityEngine;

public class FigureChecker : MonoBehaviour
{
    public List<string> Names { get; } = new List<string>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Objects"))
        {
            Names.Add(other.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Objects"))
        {
            if (Names.Contains(other.name))
                Names.Remove(other.name);
        }
    }
}
