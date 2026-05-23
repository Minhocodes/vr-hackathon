using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject EffectPrefab;

    public void ActivateEffect() {
        Instantiate(EffectPrefab, SpawnPoint.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision) {
        Instantiate(EffectPrefab, collision.contacts[0].point, Quaternion.identity);
    }
}
