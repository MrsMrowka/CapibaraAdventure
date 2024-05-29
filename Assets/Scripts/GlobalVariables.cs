using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    private static string keysPressed;
    private static float timer;
    private static int lostHP;
    private static int recoveredHP;
    private static int killedEnemies;
    private static int coinsCollected;
    private static int usedDoubleJump;
    private static int usedSmallJump;
    private static int usedBigJump;
    private static int usedSmallJumpPad;
    private static int usedBigJumpPad;
    private static int usedWallSliding;
    private static int usedWallJump;
    private static int usedCrouch;
    private static int hitSpikes;
    private static int hitWater;
    private static int getHitBullet;
    private static int usedLadder;
    private static int usedPressurePlate;
    private static int usedBox;
    private static int getHitEnemy;
    private static int tryNumber = 1;

    private static int keysCollected;

    public static int CoinsCollected { get => coinsCollected; set => coinsCollected = value; }
    public static float Timer { get => timer; set => timer = value; }
    public static int KeysCollected { get => keysCollected; set => keysCollected = value; }
    public static string KeysPressed { get => keysPressed; set => keysPressed = value; }
    public static int LostHP { get => lostHP; set => lostHP = value; }
    public static int RecoveredHP { get => recoveredHP; set => recoveredHP = value; }
    public static int KilledEnemies { get => killedEnemies; set => killedEnemies = value; }
    public static int UsedDoubleJump { get => usedDoubleJump; set => usedDoubleJump = value; }
    public static int UsedSmallJump { get => usedSmallJump; set => usedSmallJump = value; }
    public static int UsedBigJump { get => usedBigJump; set => usedBigJump = value; }
    public static int UsedSmallJumpPad { get => usedSmallJumpPad; set => usedSmallJumpPad = value; }
    public static int UsedBigJumpPad { get => usedBigJumpPad; set => usedBigJumpPad = value; }
    public static int UsedWallSliding { get => usedWallSliding; set => usedWallSliding = value; }
    public static int UsedWallJump { get => usedWallJump; set => usedWallJump = value; }
    public static int UsedCrouch { get => usedCrouch; set => usedCrouch = value; }
    public static int HitSpikes { get => hitSpikes; set => hitSpikes = value; }
    public static int HitWater { get => hitWater; set => hitWater = value; }
    public static int GetHitBullet { get => getHitBullet; set => getHitBullet = value; }
    public static int UsedLadder { get => usedLadder; set => usedLadder = value; }
    public static int UsedPressurePlate { get => usedPressurePlate; set => usedPressurePlate = value; }
    public static int UsedBox { get => usedBox; set => usedBox = value; }
    public static int GetHitEnemy { get => getHitEnemy; set => getHitEnemy = value; }
    public static int TryNumber { get => tryNumber; set => tryNumber = value; }

    public static void ResetStats()
    {
        keysPressed = "";
        timer = 0F;
        lostHP = 0;
        recoveredHP = 0;
        killedEnemies = 0;
        coinsCollected = 0;
        usedDoubleJump = 0;
        usedSmallJump = 0;
        usedBigJump = 0;
        usedSmallJumpPad = 0;
        usedBigJumpPad = 0;
        usedWallSliding = 0;
        usedWallJump = 0;
        usedCrouch = 0;
        hitSpikes = 0;
        hitWater = 0;
        getHitBullet = 0;
        usedLadder = 0;
        usedPressurePlate = 0;
        usedBox = 0;
        getHitEnemy = 0;
        tryNumber = 1;
    }
}
