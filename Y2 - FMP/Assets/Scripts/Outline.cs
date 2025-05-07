using UnityEngine;

public class Outline : MonoBehaviour
{
    public Material[] defaultAndOutline;
    public Material[] defaultMaterials;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public bool isInteractable = true;

    public MeshRenderer meshRenderer;
    void Start()
    {
        if (isInteractable)
        {
            if (gameObject.GetComponent<MeshRenderer>() != null)
            {
                defaultMaterials = gameObject.GetComponent<MeshRenderer>().materials;
            }
            else if (gameObject.GetComponent<SkinnedMeshRenderer>() != null)
            {
                defaultMaterials = gameObject.GetComponent<MeshRenderer>().materials;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
