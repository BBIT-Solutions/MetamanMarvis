using TMPro;
using UnityEngine;


public class TimerDisplay : MonoBehaviour  {

    public TimerController timer;

    private TextMeshProUGUI _uiTextPro;

    void Start() {
        _uiTextPro = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        // Note: This is not optimized and you should avoid updating time each frame.
        _uiTextPro.text = timer.GetFormattedTimeFromSeconds();
    }
}