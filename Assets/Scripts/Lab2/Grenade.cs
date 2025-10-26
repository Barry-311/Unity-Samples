// Grenade.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Grenade : MonoBehaviour
{
    public float explodeRadius = 5f;
    public float explodeForce = 600f;
    public float upwardsModifier = 0.5f;
    public LayerMask affectMask = ~0;
    public GameObject explosionVfx;
    public float lifeTime = 10f; // ��������

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // ����ײ�㱬ը
        Vector3 pos = collision.GetContact(0).point;
        Explode(pos);
    }

    void Explode(Vector3 pos)
    {
        if (explosionVfx) Instantiate(explosionVfx, pos, Quaternion.identity);

        Collider[] cols = Physics.OverlapSphere(pos, explodeRadius, affectMask);
        foreach (var c in cols)
        {
            if (c.attachedRigidbody != null)
            {
                c.attachedRigidbody.AddExplosionForce(explodeForce, pos, explodeRadius, upwardsModifier, ForceMode.Impulse);
            }
        }

        // ��ѡ�������Ч�����ӡ������
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}
