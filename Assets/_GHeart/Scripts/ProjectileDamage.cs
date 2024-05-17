using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class ProjectileDamage : MonoBehaviour {

    #region Fields
    [SerializeField] private float m_lifetime = 1.0f;
    #endregion

    #region Unity Event Functions
    private void Start() {
        Destroy(gameObject, m_lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.LogWarning($"ON TRIGGER ENTER : {other.name}");
    }
    #endregion
    
}
