using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusIndicator : MonoBehaviour
{
    [SerializeField]private RectTransform healthBarRect;
    [SerializeField] private TextMeshProUGUI healthText;

    void Start() {
        if(healthBarRect == null) {
            Debug.Log("STATUS INDICATOR: No health bar reference");
        }
        if(healthText == null) {
            Debug.Log("STATUS INDICATOR: No health text reference");
        }
    }

    public void setHealth(int _cur, int _max) {
        float _value = (float)_cur / _max;
        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.x);
        healthText.text = _cur.ToString() + "/" + _max.ToString() + " HP";
        if(_value <= .3) {
            healthBarRect.GetComponentInParent<Image>().color = new Color(255, 165, 0);
        }
    }
}
