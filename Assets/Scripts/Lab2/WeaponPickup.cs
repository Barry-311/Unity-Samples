using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponPickup : MonoBehaviour
{
    public WeaponManager.WeaponType weaponType = WeaponManager.WeaponType.MachineGun;
    public float respawnTime = 10f;     // ����ʱ�䣨�룩
    public bool autoSwitch = true;      // ʰȡ���Ƿ��Զ��л���������
    public Renderer[] visuals;          // ��ѡ��ָ����Щ��Ⱦ������/��ʾ���������Զ���

    Collider col;
    WeaponManager wm;

    void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true; // ʰȡ��������

        if (visuals == null || visuals.Length == 0)
            visuals = GetComponentsInChildren<Renderer>(true);
    }

    void Start()
    {
        // �ҳ������ WeaponManager������ Main Camera �� WeaponRoot��
        wm = FindObjectOfType<WeaponManager>();
        if (!wm) Debug.LogWarning("WeaponManager not found in scene.");
    }

    void OnTriggerEnter(Collider other)
    {
        // ʶ����ң��Ƽ��� Capsule �� Tag=Player
        if (!other.CompareTag("Player")) return;
        if (!wm) return;

        // �������������Զ��л���
        wm.Unlock(weaponType, autoSwitch);

        // ���ز���ʼ����Э��
        StartCoroutine(DoRespawn());
    }

    IEnumerator DoRespawn()
    {
        SetEnabled(false);
        yield return new WaitForSeconds(respawnTime);
        SetEnabled(true);
    }

    void SetEnabled(bool enabled)
    {
        if (col) col.enabled = enabled;
        if (visuals != null)
            foreach (var r in visuals) if (r) r.enabled = enabled;
        // Ҳ���������� SetActive����������/���÷��㱣��Э��������
    }
}
