using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerTransformator;

[CreateAssetMenu(fileName = "FormData" , menuName = "New Form Data")]
public class FormData : ScriptableObject
{
    public PLAYER_FROMS FORM;
    public ABILITY_TYPE ABILITY_TYPE;
    
    public Sprite formSprite;

    public float movementSpeed;
    public float jumpHeight;
    public bool canCrouch;
    public bool canAtack;
    public double AbilityCoolDown;

    public AnimatorOverrideController overrideController;
}
