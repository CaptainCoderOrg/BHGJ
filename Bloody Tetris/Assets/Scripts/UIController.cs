using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameManager _manager;
    [SerializeField]
    private AudioSource _music;
    [SerializeField]
    private AudioSource _sfx;
    private UIDocument _doc;
    void Awake()
    {
        _doc = GetComponent<UIDocument>();
        Slider musicSlider = _doc.rootVisualElement.Q<Slider>("MusicVolume");
        musicSlider.RegisterValueChangedCallback((value) => _music.volume = value.newValue);
        Slider sfxSlider = _doc.rootVisualElement.Q<Slider>("SFXVolume");
        sfxSlider.RegisterValueChangedCallback((value) => _sfx.volume = value.newValue);
        Button startGame = _doc.rootVisualElement.Q<Button>("StartGameButton");

        
        
        startGame.clicked += _manager.StartGame;
    }

}
