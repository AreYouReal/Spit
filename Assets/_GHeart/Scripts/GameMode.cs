using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMode : MonoBehaviour {


    public static GameMode I => m_instance;
    
    #region Fields

    [SerializeField] private float m_initialTime = 10f;

    [SerializeField] private float m_elapsedTime = 0.0f;
    [SerializeField] private float m_remainedTime = 0.0f;
    
    [SerializeField] private bool m_running = false;


    [SerializeField] private ProjectileSpawner m_projectileSpawner;
        
    [SerializeField] private TMP_Text m_time;

    [SerializeField] private GameObject m_uiPlay;
    

    private static GameMode m_instance;
    #endregion
    
    #region Unity Event Functions

    private void Awake() {
        m_instance = this;
    }

    private void Start() {
        BeginPlay();
    }

    private void Update() {
        if (m_running) {
            m_elapsedTime += Time.deltaTime;
            m_remainedTime -= Time.deltaTime;

            m_time.text = Mathf.RoundToInt(m_remainedTime).ToString();
        }

        if (m_remainedTime <= 0.0f) {
            EndPlay();
        }
    }
    #endregion

    #region Public
    public void BeginPlay() {
        m_remainedTime = m_initialTime;
        m_uiPlay.gameObject.SetActive(false);
        m_projectileSpawner.enabled = true;

    }
    
    public void AddScore(float a_score) {
        if (m_running) {
            m_remainedTime += a_score;
        }
    }

    public void EndPlay() {

        m_elapsedTime = 0.0f;
        m_remainedTime = 0.0f;
        
        m_uiPlay.gameObject.SetActive(true);
        m_projectileSpawner.enabled = false;
    }
    #endregion


}
