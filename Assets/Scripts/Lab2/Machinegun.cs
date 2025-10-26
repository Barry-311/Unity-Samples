// MachineGun.cs
using UnityEngine;

public class MachineGun : MonoBehaviour, IWeapon
{
    public Camera cam;
    public float range = 200f;
    public LayerMask hitMask = ~0;
    public float hitForce = 30f;

    public float fireRate = 10f;       // ÿ���ӵ���
    public float spreadDegrees = 2f;   // ɢ���Ƕȣ��ȣ�
    private float nextFireTime = 0f;

    void Reset() { cam = Camera.main; }

    public void OnPress() => TryShoot();
    public void OnHold() => TryShoot();
    public void OnRelease() { }

    void TryShoot()
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + 1f / fireRate;

        if (!cam) cam = Camera.main;
        Vector3 dir = GetSpreadDirection();
        Ray ray = new Ray(cam.transform.position, dir);

        if (Physics.Raycast(ray, out RaycastHit hit, range, hitMask))
        {
            if (hit.rigidbody != null)
                hit.rigidbody.AddForceAtPosition(dir * hitForce, hit.point, ForceMode.Impulse);
            // optional hit VFX...
        }

        // optional muzzle flash, sound...
    }

    // �򵥵���Ļ�ռ����ɢ��
    Vector3 GetSpreadDirection()
    {
        // ������λԲ
        Vector2 r = Random.insideUnitCircle * Mathf.Tan(spreadDegrees * Mathf.Deg2Rad);
        Vector3 dir = (cam.transform.forward + cam.transform.right * r.x + cam.transform.up * r.y).normalized;
        return dir;
    }
}
