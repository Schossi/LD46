﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private Rigidbody _body;

    public static int Active = 0;
    public static int Score = 0;

    private float _jumpTimer;
    private bool _jump;

    private ParticleSystem _parts;

    // Use this for initialization
    void Start()
    {
        Active++;

        _body = GetComponent<Rigidbody>();
        _parts = GetComponent<ParticleSystem>();

        _jumpTimer = Random.Range(1.5f, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (IsGrounded())
            _jumpTimer -= Time.deltaTime;
        if (_jumpTimer < 0f)
        {
            _jumpTimer = Random.Range(1.5f, 3.5f);
            _jump = true;
        }

        if (transform.position.y < -10f)
        {
            if (!Tree.GameOver)
                Score++;

            Active--;
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (_jump)
        {
            _parts.Play();
            TerrainModifier.Instance.CoverArea(transform.position, 12, true);
            _body.AddForce((Holy.Instance.transform.position - transform.position).normalized * Random.Range(10f, 50f), ForceMode.Impulse);
            _jump = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.6f * transform.localScale.x, LayerMask.GetMask("Terrain"));
    }
}