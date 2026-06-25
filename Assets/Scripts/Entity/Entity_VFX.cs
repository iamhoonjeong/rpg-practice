using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("On taking damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = 0.2f;
    private Material originalMaterial;
    private Coroutine onDamageVfxCoroutine;

    [Header("On Doing Damge VFX")]
    [SerializeField] private Color hitVfxColor = Color.white;
    [SerializeField] private GameObject hitVfx;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    public void CreateOnHitVFX(Transform target)
    {
        GameObject vfx = Instantiate(hitVfx, target.position, Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;
    }

    public void PlayOnDamageVfx()
    {
        if (onDamageVfxCoroutine != null)
            StopCoroutine(OnDamageVfxCoroutine());

        onDamageVfxCoroutine = StartCoroutine(OnDamageVfxCoroutine());
    }

    private IEnumerator OnDamageVfxCoroutine()
    {
        sr.material = onDamageMaterial;

        yield return new WaitForSeconds(onDamageVfxDuration);

        sr.material = originalMaterial;
    }
}
