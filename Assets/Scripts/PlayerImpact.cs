using UnityEngine;
using System.Collections;

public class PlayerImpact : MonoBehaviour
{
    public float Radius;
    public float Force;

    private float _timeAlive = 0f;

    // Use this for initialization
    void Start()
    {
        foreach (var enemy in Physics.OverlapSphere(transform.position, Radius, LayerMask.GetMask("Enemy")))
        {
            enemy.attachedRigidbody.AddExplosionForce(Force, transform.position, Radius, 3f);
        }

        TerrainModifier.Instance.CoverArea(transform.position, 20, false);
    }

    // Update is called once per frame
    void Update()
    {
        _timeAlive += Time.deltaTime;
        if (_timeAlive > 5f)
            Destroy(gameObject);
    }
}
