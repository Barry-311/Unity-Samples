// MachineGun.cs
using UnityEngine;

public class MachineGun : MonoBehaviour, IWeapon
{
    public Camera cam;
    public float range = 200f;
    public LayerMask hitMask = ~0;
    public float hitForce = 30f;

    public float fireRate = 10f;       // 每秒子弹数
    public float spreadDegrees = 2f;   // 散布角度（度）
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

    // 简单的屏幕空间随机散布
    Vector3 GetSpreadDirection()
    {
        // 采样单位圆
        Vector2 r = Random.insideUnitCircle * Mathf.Tan(spreadDegrees * Mathf.Deg2Rad);
        Vector3 dir = (cam.transform.forward + cam.transform.right * r.x + cam.transform.up * r.y).normalized;
        return dir;
    }
}
