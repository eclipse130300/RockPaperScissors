using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerTransformator : MonoBehaviour
{
    [SerializeField] private FormData[] allForms;
    [SerializeField] private KeyCode transformKey;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float VFXduration;
    [SerializeField] private int VFXstepsAmount;

    [SerializeField] private int transformationAmount = 3;

    public FormData activeForm;
    
    private FormData paperForm;
    private FormData rockForm;
    private FormData scissorsForm;

    private Animator animator;
    private UIGameController gameController;

/*    [SerializeField] private AnimatorOverrideController paperController;
    [SerializeField] private AnimatorOverrideController rockController;
    [SerializeField] private AnimatorOverrideController scissorsController;*/

    public Action<float, float, bool> HasTransformed = delegate { };

    public Action StartTransform = delegate { };

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameController = FindObjectOfType<UIGameController>();
    }

    private void Start()
    {
        Initialize();
    }


    private void Initialize()
    {
        foreach (FormData data in allForms)
        {
            switch (data.FORM)
            {
                case PLAYER_FROMS.PAPER:
                    paperForm = data;
                    break;
                case PLAYER_FROMS.ROCK:
                    rockForm = data;
                    break;
                case PLAYER_FROMS.SCISSORS:
                    scissorsForm = data;
                    break;
            }
        }

        // 1rst form is active?
        activeForm = paperForm;
        spriteRenderer.sprite = activeForm.formSprite;
        HasTransformed?.Invoke(activeForm.movementSpeed, activeForm.jumpHeight, activeForm.canCrouch);
        animator.runtimeAnimatorController = activeForm.overrideController;
        gameController.transformationCount.text = transformationAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(transformKey))
        {
            Transformate();
        }
    }

    private void Transformate()
    {
        if (transformationAmount <= 0) return;

        var previousForm = activeForm;

        animator.Play("Idle");
        GetComponent<PlayerAbilities>().StopAllCoroutines();

        // switch form according o rules of the game
        switch (activeForm.FORM)
        {
            case PLAYER_FROMS.PAPER:
                activeForm = rockForm;
                break;
            case PLAYER_FROMS.ROCK:
                activeForm = scissorsForm;
                break;
            case PLAYER_FROMS.SCISSORS:
                activeForm = paperForm;
                break;
        }

        //transformation VFX coroutine
        StartCoroutine(TransformVFX(VFXduration, VFXstepsAmount, previousForm, activeForm));
    }

    IEnumerator TransformVFX(float duration, int numSwitches, FormData from, FormData to)
    {
        animator.enabled = false;

        StartTransform?.Invoke();
        for (int i = 0; i < numSwitches; i++)
        {
            var sprite = spriteRenderer.sprite == from.formSprite ? to.formSprite : from.formSprite;
            spriteRenderer.sprite = sprite;

            yield return new WaitForSeconds(duration/numSwitches);
        }
        HasTransformed?.Invoke(activeForm.movementSpeed, activeForm.jumpHeight, activeForm.canCrouch);
        DecreaseTransromation();

        animator.enabled = true;

        animator.runtimeAnimatorController = activeForm.overrideController;
    }

    public void AddTransformation()
    {
        transformationAmount++;
        gameController.transformationCount.text = transformationAmount.ToString();
    }

    public void DecreaseTransromation()
    {
        transformationAmount--;
        gameController.transformationCount.text = transformationAmount.ToString();
    }

    public enum PLAYER_FROMS
    {
        ROCK,
        PAPER,
        SCISSORS
    }
}


