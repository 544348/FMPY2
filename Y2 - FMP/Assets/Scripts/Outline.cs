using UnityEngine;

public class Outline : MonoBehaviour
{
    public Material[] defaultAndOutline;
    public Material[] defaultMaterials;
    void Start()
    {
        defaultMaterials = gameObject.GetComponent<MeshRenderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
