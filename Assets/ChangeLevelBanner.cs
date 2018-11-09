﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeLevelBanner : MonoBehaviour, IPointerEnterHandler {
	public Image levelBanner;
	int defaultLevelNumber;
	int levelPassed;

	// Use this for initialization
	void Start () {
		levelPassed = GameObject.Find("LevelMenuController").GetComponent<LevelMenuControllerScript>().levelPassed;
		if(levelPassed < 22) defaultLevelNumber = levelPassed + 1;
		if(levelPassed == 22) defaultLevelNumber = levelPassed;
		levelBanner = GameObject.Find("LevelBanner").GetComponent<Image>();
		levelBanner.sprite = GameObject.Find("banner" + defaultLevelNumber).GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData)
     {
        string getLevelNumber = Regex.Match(gameObject.name, @"\d+").Value;
		levelBanner.sprite = GameObject.Find("banner" + getLevelNumber).GetComponent<SpriteRenderer>().sprite;
     }
}