using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerObject : MonoBehaviour
{
    public enum Danger
    {
        spike,
        falling,
        shoot
    }

    public Danger danger;
}
