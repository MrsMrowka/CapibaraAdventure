﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject deathGameObject;
    [SerializeField] TextMeshProUGUI deathText;


    public void PlayerDeath(DeathTypes deathType)
    {
        GenerateTxtFile();
        GlobalVariables.TryNumber += 1;
        deathGameObject.SetActive(true);
        // deathText.text = deathType.ToString();
    }

    void GenerateTxtFile()
    {
        string path = Application.dataPath + "/" + SceneManager.GetActiveScene().name + "_" + GlobalVariables.TryNumber + ".txt";
        using (StreamWriter sw = new StreamWriter(path, true))
        {
            sw.WriteLine("Klawisze; " + GlobalVariables.KeysPressed);
            sw.WriteLine("Czas; " + Timer.FormatTime());
            sw.WriteLine("Stracone HP; " + GlobalVariables.LostHP);
            sw.WriteLine("Wyleczone HP; " + GlobalVariables.RecoveredHP);
            sw.WriteLine("Zabici przeciwnicy; " + GlobalVariables.KilledEnemies);
            sw.WriteLine("Zebrane diamenty; " + GlobalVariables.CoinsCollected);
            sw.WriteLine("Użyty podwójny skok; " + GlobalVariables.UsedDoubleJump);
            sw.WriteLine("Użyty mały skok; " + GlobalVariables.UsedSmallJump);
            sw.WriteLine("Użyty duży skok; " + (GlobalVariables.UsedBigJump - GlobalVariables.UsedSmallJump));
            sw.WriteLine("Użyty mały skok z skoczni; " + GlobalVariables.UsedSmallJumpPad);
            sw.WriteLine("Użyty duży skok z skoczni; " + GlobalVariables.UsedBigJumpPad);
            sw.WriteLine("Użyty ślizg po ścianie; " + GlobalVariables.UsedWallSliding);
            sw.WriteLine("Użyty skok od ściany; " + GlobalVariables.UsedWallJump);
            sw.WriteLine("Użyte kucanie; " + GlobalVariables.UsedCrouch);
            sw.WriteLine("Dotknięcie kolców; " + GlobalVariables.HitSpikes);
            sw.WriteLine("Dotknięcie wody; " + GlobalVariables.HitWater);
            sw.WriteLine("Dotknięcie pocisków; " + GlobalVariables.GetHitBullet);
            sw.WriteLine("Użyta drabina; " + GlobalVariables.UsedLadder);
            sw.WriteLine("Użyty przycisk na podłodze; " + GlobalVariables.UsedPressurePlate);
            sw.WriteLine("Dotknięcie pudełka; " + GlobalVariables.UsedBox);
            sw.WriteLine("Dotknięcie przeciwnika; " + GlobalVariables.GetHitEnemy);
        }

        GlobalVariables.ResetStats();
        Debug.Log(path);
    }
}
