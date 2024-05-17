using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour {
    
    #region Fields
    [SerializeField] private Camera m_mainCamera;
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private Vector2 m_xMoveBounds;

    [SerializeField] private float m_maxDistance = 3;
    
    [SerializeField] private float m_maxForceAccumulateTime = 3.0f;
    [SerializeField] private float m_elapsedAccumulateForceTime = 0.0f;
    
    [SerializeField] private Projectile m_projectilePrefab;


    public float CurrentProjectileForce { get; private set; }

    private float m_aimXPos;
    #endregion
    

    #region Unity Event Functions
    private void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
            m_aimXPos = m_mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
            Vector3 newPos = transform.position;
            newPos.x = Mathf.Clamp(Mathf.Lerp(transform.position.x, m_aimXPos, m_moveSpeed * Time.deltaTime), m_xMoveBounds.x, m_xMoveBounds.y);
            transform.position = newPos;
            
            m_elapsedAccumulateForceTime += Time.deltaTime;

            CurrentProjectileForce = m_elapsedAccumulateForceTime / m_maxForceAccumulateTime;
        }

        if (Input.GetMouseButtonUp(0)) {
            Spawn();
        }
    }
    #endregion

    #region Public
    public void Spawn() {
        Projectile proj = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity);
        proj.BeginPlay(m_maxDistance * CurrentProjectileForce);
        m_elapsedAccumulateForceTime = 0.0f;
        CurrentProjectileForce = 0;
    }
    #endregion
    
}
