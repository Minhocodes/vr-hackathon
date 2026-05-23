using UnityEngine;

public class FurnitureItem : MonoBehaviour
{
    [HideInInspector] public Vector3 originalPosition;
    [HideInInspector] public Quaternion originalRotation;

    public float snapDistance = 0.5f;
    private bool isPlaced = false;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void Scramble()
    {
        isPlaced = false;
        // Náhodně vyber jednu ze dvou zón
        float x, z;
        if (Random.value > 0.5f)
        {
            // Levá část místnosti
            x = Random.Range(-6.5f, -1.5f);
            z = Random.Range(-6.5f, 6.5f);
        }
        else
        {
            // Pravá část místnosti
            x = Random.Range(1.5f, 6.5f);
            z = Random.Range(-6.5f, 6.5f);
        }
        transform.position = new Vector3(x, 1f, z);
        transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }

    public bool CheckPlacement()
    {
        float distance = Vector3.Distance(transform.position, originalPosition);
        if (distance < snapDistance)
        {
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            isPlaced = true;
            SetGlow(Color.green);
            return true;
        }
        return false;
    }

    void SetGlow(Color color)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            foreach (Material mat in r.materials)
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", color * 2f);
            }
        }
    }

    public bool IsPlaced() => isPlaced;
}