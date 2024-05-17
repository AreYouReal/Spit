using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Line : MonoBehaviour {

    #region Fields

    [SerializeField] private GameObject m_scoreCarrier;
    [SerializeField] private Vector2 m_xBounds;

    [SerializeField] private float m_MoveTime;

    [SerializeField] private Vector2 m_respawnCooldownInSeconds;

    #endregion

    #region Unity Event Functions
    private void Start() {
        LaunchTheScoreCarrier();
    }
    #endregion

    #region Helpers

    private void LaunchTheScoreCarrier() {

        Vector3 initialPos = transform.position;

        bool leftMove = UnityEngine.Random.Range(0, 100) > 50;
        
        initialPos.x = leftMove ? m_xBounds.y : m_xBounds.x;
        m_scoreCarrier.transform.position = initialPos;


        float aimXPos = leftMove ? m_xBounds.x : m_xBounds.y;
        m_scoreCarrier.transform.DOMoveX(aimXPos, m_MoveTime).OnComplete(() => {
            LaunchTheCarrierTask().Forget();
        });

        Vector3 localScale = m_scoreCarrier.transform.localScale;

        localScale.x *= -Mathf.Sign(aimXPos);

        m_scoreCarrier.transform.localScale = localScale;

    }

    private async UniTask LaunchTheCarrierTask() {
        float currentRespawnCooldown = UnityEngine.Random.Range(m_respawnCooldownInSeconds.x, m_respawnCooldownInSeconds.y);
        await UniTask.Delay(TimeSpan.FromSeconds(currentRespawnCooldown), cancellationToken: gameObject.GetCancellationTokenOnDestroy());
        LaunchTheScoreCarrier();
    }

    #endregion

    
}
