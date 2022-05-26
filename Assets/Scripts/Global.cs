using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static bool ISaim;
    public static bool isDead;
    public static bool moving;
    public static bool reloading;
    public static bool groundedPlayer;
    public static bool ISgrappling = false;
    public static bool ISpicking = false;
    public static bool isTurretDead = false;
    public static bool isRedentorDead = false;

    public static int totalJump = 0;
    public static int witchAvatarIsOn = 1;

    public static GameObject PickedObject;
}
