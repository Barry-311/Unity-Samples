using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponPickup : MonoBehaviour
{
    public WeaponManager.WeaponType weaponType = WeaponManager.WeaponType.MachineGun;
    public float respawnTime = 10f;     // 重生时间（秒）
    public bool autoSwitch = true;      // 拾取后是否自动切换到该武器
    public Renderer[] visuals;          // 可选：指定哪些渲染体隐藏/显示；留空则自动找

    Collider col;
    WeaponManager wm;

    void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true; // 拾取靠触发器

        if (visuals == null || visuals.Length == 0)
            visuals = GetComponentsInChildren<Renderer>(true);
    }

    void Start()
    {
        // 找场景里的 WeaponManager（挂在 Main Camera 或 WeaponRoot）
        wm = FindObjectOfType<WeaponManager>();
        if (!wm) Debug.LogWarning("WeaponManager not found in scene.");
    }

    void OnTriggerEnter(Collider other)
    {
        // 识别玩家：推荐给 Capsule 设 Tag=Player
        if (!other.CompareTag("Player")) return;
        if (!wm) return;

        // 解锁武器（可自动切换）
        wm.Unlock(weaponType, autoSwitch);

        // 隐藏并开始重生协程
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
        // 也可以整物体 SetActive，但用启用/禁用方便保留协程与引用
    }
}
