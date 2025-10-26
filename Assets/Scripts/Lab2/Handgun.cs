// Handgun.cs
using UnityEngine;

public class Handgun : MonoBehaviour, IWeapon
{
    public Camera cam;                    // 指向主相机
    public float range = 200f;
    public LayerMask hitMask = ~0;        // 可命中层
    public float hitForce = 50f;          // 命中刚体施加力
    public GameObject hitVfx;             // 可选命中特效（拖粒子预制体）

    void Reset()
    {
        cam = Camera.main;
    }

    public void OnPress()
    {
        ShootOnce();
    }
    public void OnHold() { }
    public void OnRelease() { }

    void ShootOnce()
    {
        if (!cam) cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth * 0.5f, cam.pixelHeight * 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, range, hitMask))
        {
            // Apply impulse to rigidbody if present
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForceAtPosition(ray.direction * hitForce, hit.point, ForceMode.Impulse);
            }

            // optional hit VFX
            if (hitVfx) Instantiate(hitVfx, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
