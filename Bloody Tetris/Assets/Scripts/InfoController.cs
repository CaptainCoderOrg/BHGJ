using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class InfoController : MonoBehaviour
{
    [SerializeField]
    private GameManager _manager;
    private Label _bloodLabel;
    
    private Label _linesLabel;
    private Label _levelLabel;
    void Awake()
    {
        UIDocument doc = GetComponent<UIDocument>();
        _bloodLabel = doc.rootVisualElement.Q<Label>("BloodLabel");
        _manager.OnBloodChanged.AddListener((value) => _bloodLabel.text = $"{value}");
        _linesLabel = doc.rootVisualElement.Q<Label>("LinesLabel");
        _manager.OnLevelChanged.AddListener((value) => _levelLabel.text = $"{value}");
        _levelLabel = doc.rootVisualElement.Q<Label>("LevelLabel");
        _manager.OnLinesChange.AddListener((value) => _linesLabel.text = $"{value}");
    }

}
