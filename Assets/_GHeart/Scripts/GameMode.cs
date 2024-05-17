using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMode : MonoBehaviour {


    public const string BEST_SCORE_KEY = "BestScore";

    public static GameMode I => m_instance;
    
    #region Fields

    [SerializeField] private float m_initialTime = 10f;

    [SerializeField] private float m_elapsedTime = 0.0f;
    [SerializeField] private float m_remainedTime = 0.0f;
    
    [SerializeField] private bool m_running = false;


    [SerializeField] private ProjectileSpawner m_projectileSpawner;
        
    [SerializeField] private TMP_Text m_time;

    [SerializeField] private GameObject m_uiPlay;

    [SerializeField] private TMP_Text m_uiCurrentScore;
    [SerializeField] private TMP_Text m_uiBestScore;
    [SerializeField] private GameObject m_uiNewBestScoreLabel;
    

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
            
            if (m_remainedTime <= 0.0f) {
                EndPlay();
            }
        }
    }
    #endregion

    #region Public
    public void BeginPlay() {
        m_remainedTime = m_initialTime;
        m_uiPlay.gameObject.SetActive(false);
        m_projectileSpawner.enabled = true;
        m_running = true;
    }
    
    public void AddScore(float a_score) {
        if (m_running) {
            m_remainedTime += a_score;
        }
    }

    public void EndPlay() {
        m_running = false;
        
        bool newBestScore = UpdateBestScore(m_elapsedTime);
        m_uiNewBestScoreLabel.SetActive(newBestScore);
        
        
        m_uiBestScore.text = Mathf.RoundToInt(GetBestScore()).ToString();
        m_uiCurrentScore.text = Mathf.RoundToInt(m_elapsedTime).ToString();
        
        m_elapsedTime = 0.0f;
        m_remainedTime = 0.0f;
        
        m_uiPlay.gameObject.SetActive(true);
        m_projectileSpawner.enabled = false;
    }
    #endregion


    #region Helpers


    private bool UpdateBestScore(float a_newScore) {
        float currentBest = GetBestScore();
        if (currentBest < a_newScore) {
            PlayerPrefs.SetFloat(BEST_SCORE_KEY, a_newScore);
            return (true);
        }

        return (false);
    }

    private float GetBestScore() {
        return (PlayerPrefs.GetFloat(BEST_SCORE_KEY, 0));
    }


    #endregion


}
