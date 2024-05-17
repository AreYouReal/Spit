using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    #region Fields
    [SerializeField] private GameObject m_projectileDamagePrefab;
    #endregion
    
    #region Public
    public void BeginPlay(float a_distance) {
        transform.DOMoveZ(10, 0.5f);
        transform.DOMoveY(transform.position.y + a_distance, 0.5f).OnComplete(() => {
            Instantiate(m_projectileDamagePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        });
    }
    #endregion

    
    
}
