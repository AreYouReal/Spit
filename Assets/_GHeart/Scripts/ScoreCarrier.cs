using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreCarrier : MonoBehaviour {

    #region Fields

    [SerializeField] private float m_score;
    [SerializeField] private GameObject m_scoreVFXPrefab;

    #endregion

    #region Unity Event Functions
    private void OnTriggerEnter2D(Collider2D other) {
        GameMode.I.AddScore(m_score);
        Instantiate(m_scoreVFXPrefab, transform.position, Quaternion.identity).GetComponent<AddScoreVFX>().m_text.text = $"+{m_score}s";
    }
    #endregion
    

}
