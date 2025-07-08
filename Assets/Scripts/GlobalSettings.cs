using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalSettings
{
    public static event Action lifeSettingChanged;

    private static string[] lifeOptions = { "0", "3", "Unlimited" };

    private static int currentLifeOption = 0;

    public static void SetLifeRightOne()
    {
        currentLifeOption++;
        if (currentLifeOption > lifeOptions.Length - 1)
        {
            currentLifeOption = 0;
        }

        lifeSettingChanged?.Invoke();
    }

    public static void SetLifeLeftOne()
    {
        currentLifeOption--;
        if (currentLifeOption < 0)
        {
            currentLifeOption = lifeOptions.Length - 1;
        }
        lifeSettingChanged?.Invoke();
    }

    public static string GetLifeOption()
    {
        return lifeOptions[currentLifeOption];
    }

    public static int GetMaxLife()
    {
        if (int.TryParse(GetLifeOption(), out int num))
        {
            return num;
        }
        else
        {
            return -1;
        }
    }
}
