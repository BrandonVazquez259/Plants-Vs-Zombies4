using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float damage = 20f;
    [SerializeField]
    private UnityEvent<Transform> onHitEnemy;
    private Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        rigidbody.linearVelocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            SoundManager.instance.Play("hit_object");
            onHitEnemy?.Invoke(transform);
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
