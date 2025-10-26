// Handgun.cs
using UnityEngine;

public class Handgun : MonoBehaviour, IWeapon
{
    public Camera cam;                    // ָ�������
    public float range = 200f;
    public LayerMask hitMask = ~0;        // �����в�
    public float hitForce = 50f;          // ���и���ʩ����
    public GameObject hitVfx;             // ��ѡ������Ч��������Ԥ���壩

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
