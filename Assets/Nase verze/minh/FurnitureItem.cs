using UnityEngine;

public class FurnitureItem : MonoBehaviour
{
    [HideInInspector] public Vector3 originalPosition;
    [HideInInspector] public Quaternion originalRotation;

    public float snapDistance = 1.5f; // jak blizko musi byt
    private bool isPlaced = false;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        // Reset záře na začátku
        SetGlow(Color.clear);
    }

    private bool scrambled = false;

    void Update()
    {
        if (!isPlaced && scrambled)
        {
            CheckPlacement();
        }
    }

    
    public void Scramble()
    {
        scrambled = true;
        isPlaced = false;
        SetGlow(Color.clear);
        float x, z;
        if (Random.value > 0.5f)
        {
            x = Random.Range(-6.5f, -1.5f);
            z = Random.Range(-6.5f, 6.5f);
        }
        else
        {
            x = Random.Range(1.5f, 6.5f);
            z = Random.Range(-6.5f, 6.5f);
        }
        transform.position = new Vector3(x, 0f, z);
        transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }

    public bool CheckPlacement()
    {
        float distance = Vector3.Distance(transform.position, originalPosition);
        if (distance < snapDistance)
        {
            // Automaticky placne na spravne misto!
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            isPlaced = true;
            SetGlow(Color.green);
            return true;
        }
        return false;
    }

    public bool IsPlaced() => isPlaced;

    void SetGlow(Color color)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            foreach (Material mat in r.materials)
            {
                if (color == Color.clear)
                {
                    mat.DisableKeyword("_EMISSION");
                }
                else
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", color * 2f);
                }
            }
        }
    }
}