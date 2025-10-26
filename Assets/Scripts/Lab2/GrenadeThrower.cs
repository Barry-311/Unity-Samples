// GrenadeThrower.cs
using UnityEngine;

public class GrenadeThrower : MonoBehaviour, IWeapon
{
    public Camera cam;
    public Transform firePoint;         // ������㣨WeaponRoot������Ϊ�ս�ʹ�� cam.transform
    public GameObject grenadePrefab;    // ���� Grenade prefab
    public float minImpulse = 8f;
    public float maxImpulse = 20f;
    public float maxChargeTime = 3.0f;

    private float pressStart = -1f;

    void Reset() { cam = Camera.main; }

    public void OnPress()
    {
        pressStart = Time.time;
    }

    public void OnHold()
    {
        // ����������ʾ����UI������ fillAmount = (Time.time-pressStart)/maxChargeTime
    }

    public void OnRelease()
    {
        if (pressStart < 0f) return;
        float held = Mathf.Clamp(Time.time - pressStart, 0f, maxChargeTime);
        float t = held / maxChargeTime;
        float impulse = Mathf.Lerp(minImpulse, maxImpulse, t);
        Throw(impulse);
        pressStart = -1f;
    }

    void Throw(float impulse)
    {
        if (!cam) cam = Camera.main;
        Transform spawn = firePoint != null ? firePoint : cam.transform;
        GameObject g = Instantiate(grenadePrefab, spawn.position + spawn.forward * 0.5f, Quaternion.identity);
        Rigidbody rb = g.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 dir = cam.transform.forward;
            // ������һ�������ǣ��������
            dir = (dir + 0.25f * cam.transform.up).normalized;
            rb.AddForce(dir * impulse, ForceMode.Impulse);
        }
    }
}
