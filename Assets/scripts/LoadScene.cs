﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public string scene;

    public void StartScene()
    {
        SceneManager.LoadScene(scene);
    }
}
