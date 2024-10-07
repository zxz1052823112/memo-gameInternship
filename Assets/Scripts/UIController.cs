using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Slider explvlSlider;
    public TMP_Text expLvlText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateExperience(int currentExp, int levelExp, int currentLvl)
    {
        explvlSlider.maxValue = levelExp;
        explvlSlider.value = currentExp;

        expLvlText.text = "Lecel: " + currentLvl;
    }
}
