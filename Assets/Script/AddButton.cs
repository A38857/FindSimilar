using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddButton : MonoBehaviour
{
    [SerializeField]
    private Transform panel;
    [SerializeField]
    private GameObject Button;
    GameObject btn;

   

    private void Awake()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Level1: " + level);
        CreButton(level);
    }

    void CreButton(int size)
    {
        for (int i = 0; i < (2*(int)Math.Pow(2, size)); i++)
        {
            btn = Instantiate(Button);
            btn.name = "" + i;
            btn.transform.SetParent(panel, false);

        }
    }

}
