﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of Player found!");
            return;
        }

        instance = this;
    }
    #endregion
}
