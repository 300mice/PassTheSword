using System;
using UnityEngine;

[RequireComponent(typeof(DamageComponent))]
public class AreaDamage : MonoBehaviour
{
    private DamageComponent _damageComponent;
    private PlayerCamera _camera;

    private float radius = 3f;
    private float maxDistance = 2f;
    void Awake()
    {
        _damageComponent = GetComponent<DamageComponent>();
        _damageComponent.OnHit.AddListener(OnHit);

        _camera = GameObject.FindAnyObjectByType<PlayerCamera>();

    }

    void OnHit()
    {
        RaycastHit[] hits;
        Vector3 p1 = transform.position + (Vector3.up * 0.5f);
        Vector3 p2 = p1 + (Vector3.forward * maxDistance);
        hits = Physics.CapsuleCastAll(p1, p2, radius, transform.forward, 0);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            HealthComponent hp = hit.transform.GetComponentInParent<HealthComponent>();
            if (hp && !hp.CompareTag("PartyMember"))
            {
                hp.UpdateHealth(-_damageComponent.Damage);
            }
        }
        
        _camera.CameraShake(0.25f,0.09f);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SwordSlash", transform.position);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Stab");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + (Vector3.up * 0.5f), radius);
        Gizmos.DrawSphere((transform.position + (Vector3.up * 0.5f)) + (Vector3.forward * maxDistance), radius);
    }*/
}
