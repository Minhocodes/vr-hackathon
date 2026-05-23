using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ParticleEffectPrefab;
    public float ExplosionRadius = 3f;
    public float ExplosionForce = 10f;
    public float Speed;

    void Update() {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) return;

        Explode();
    }

    void Explode() {
        Instantiate(ParticleEffectPrefab, transform.position, Quaternion.identity);
        PhysicsExplodePush();
        Destroy(this.gameObject);
    }

    void PhysicsExplodePush() {
        Collider[] hits = Physics.OverlapSphere(transform.position, ExplosionRadius);

        foreach (var hit in hits) {
            Rigidbody rb = hit.attachedRigidbody;
            if (rb != null) {
                Vector3 direction = (rb.position - transform.position).normalized;

                float distance = Vector3.Distance(rb.position, transform.position);
                float distanceFactor = 1f - Mathf.Clamp01(distance / ExplosionRadius);

                rb.AddForce(direction * ExplosionForce * distanceFactor, ForceMode.Impulse);
            }
        }
    }
}
