using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurseManager : MonoBehaviour
{
    [SerializeField] public PlayerController player;
    [SerializeField] public GameObject curseEffect;

    [Header("Timing")]
    [SerializeField] private Vector2 curseIntervalRange = new Vector2(5f, 10f); //how long between curses
    [SerializeField] private Vector2 curseDurationRange = new Vector2(8f, 15f); //how long a curse will last

    private float originalJumpForce;
    private bool originalInvertControls;
    private float originalAirControl;
    private bool originalCanMove;

    private float curseTimer;
    private float curseDuration;

    private bool curseActive = false;
    private CurseType currentCurse;

    private enum CurseType
    {
        //SlickShoes,
        Sticky,
        //GravityMax,
        //GravityNone,
        HeavyJump,
        //LightAsAFeather,
        InvertControls,
    }

    // Start is called before the first frame update
    void Start()
    {
        originalJumpForce = player.JumpForce;
        originalInvertControls = player.InvertControls;
        originalAirControl = player.AirControlMultiplier;
        originalCanMove = player.CanMove;

        StartCoroutine(CurseTimer());
    }

    private void ApplyRandomCurse() 
    { 
        currentCurse = (CurseType)Random.Range(0, System.Enum.GetValues(typeof(CurseType)).Length);
        curseDuration = Random.Range(curseDurationRange.x, curseDurationRange.y);
        curseActive = true;

        switch (currentCurse)
        { 
            case CurseType.Sticky:
                StartCoroutine(StickyLoop());
                Debug.Log("Sticky Curse applied");
                break;
            case CurseType.HeavyJump:
                player.JumpForce = player.JumpForce * 0.5f;
                Debug.Log("Heavy Jump Curse applied");
                break;
            case CurseType.InvertControls:
                player.InvertControls = true;
                Debug.Log("Invert Controls Curse applied");
                break;
        }
    }

    private void ClearCurse()
    {
        // Reset all player modifiers
        player.JumpForce = originalJumpForce;
        player.InvertControls = originalInvertControls;
        player.AirControlMultiplier = originalAirControl;
        player.CanMove = originalCanMove;

        curseActive = false;
        currentCurse = default;

        Debug.Log("Curse removed");

        // Optional: revert UI/sfx
    }

    IEnumerator CurseTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(curseIntervalRange.x, curseIntervalRange.y));

            ApplyRandomCurse();

            yield return new WaitForSeconds(curseDuration);

            ClearCurse();
        }
    }

    IEnumerator StickyLoop()
    {
        float elapsed = 0f;
        while (elapsed < curseDuration)
        {
            player.CanMove = false;
            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
            player.CanMove = true;
            yield return new WaitForSeconds(Random.Range(0.3f, 1.0f));
            elapsed += Time.deltaTime;
        }
    }
}
