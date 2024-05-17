using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddScoreVFX : MonoBehaviour {

    public TMP_Text m_text;
    
    // Start is called before the first frame update
    void Start() {
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
