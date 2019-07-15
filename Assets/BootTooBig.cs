using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;
using rnd = UnityEngine.Random;

public class BootTooBig : MonoBehaviour 
{
	public KMBombInfo bomb;
	public KMAudio Audio;

	//Logging
	static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

	void Awake()
	{
		moduleId = moduleIdCounter++;
		GetComponent<KMBombModule>().OnActivate += Activate;

		//btn.OnInteract += delegate () { PressButton(); return false; };
	}

	void Activate()
	{
		
	}

	void Start () 
	{
        Debug.LogFormat("[Template #{0}] ------------Example------------", moduleId);
	}
	
	void Update () 
	{
		
	}
}
