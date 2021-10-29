using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance;

    [SerializeField] Menu[] menus = null;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++) //for each menu page
        {
            if (menus[i].menuName == menuName) //if it's the next menu to open
            {
                menus[i].Open(); //open it
            }
            else if (menus[i].open) //if it isn't the next menu to open
            {
                CloseMenu(menus[i]); //close it
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++) //for each menu page
        {
            if (menus[i].open) //if it's open
            {
                CloseMenu(menus[i]); //close it
            }
        }
        menu.Open(); //open the next, specified menu page
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close(); //close the specified menu
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
