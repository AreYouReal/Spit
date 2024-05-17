using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProjectileForceProgressBar : MonoBehaviour  {

    #region Fields

    [SerializeField] private ProjectileSpawner m_spawner;

    [SerializeField] private Slider m_slider;

    #endregion

    #region Unity Event Functions

    private void Update() {
        if (m_spawner) {
            m_slider.value = m_spawner.CurrentProjectileForce;
        }
    }

    #endregion
    
}
