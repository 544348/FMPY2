using UnityEngine;

public class Outline : MonoBehaviour
{
    public Material[] defaultAndOutline;
    public Material[] defaultMaterials;

    public MeshRenderer meshRenderer;
    void Start()
    {
        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            defaultMaterials = gameObject.GetComponent<MeshRenderer>().materials;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
