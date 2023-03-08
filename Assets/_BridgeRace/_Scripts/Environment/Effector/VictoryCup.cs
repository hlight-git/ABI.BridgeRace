using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCup : MonoBehaviour
{
    [SerializeField] private ParticleSystem partical;
    [SerializeField] private VictoryCupAnimationDictionary animDict;
    private Animator animator;
    public VictoryCupAnimState CurrentAnimState;
    private void Awake() => OnInit();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.Tag.CHARACTER))
        {
            LevelManager.Instance.GameOver(other.GetComponent<Character>());
        }
    }
    void OnInit()
    {
        animator = GetComponent<Animator>();
        CurrentAnimState = VictoryCupAnimState.None;
        LevelManager.Instance.OnGameOverEvents += OnGameOver;
    }
    void OnGameOver(Character winner)
    {
        winner.transform.position = new Vector3(transform.position.x, winner.transform.position.y, transform.position.z);
        StartCoroutine(OnGameOver());
    }
    IEnumerator OnGameOver()
    {
        ChangeAnim(VictoryCupAnimState.Victory);
        yield return new WaitForSeconds(animDict.GetData(VictoryCupAnimState.Victory).Duration);
        ChangeAnim(VictoryCupAnimState.Rotating);
    }
    public void ChangeAnim(VictoryCupAnimState state)
    {
        if (state == CurrentAnimState)
        {
            return;
        }
        if (CurrentAnimState != VictoryCupAnimState.None)
        {
            string currentAnimStateTriggerName = animDict.GetData(CurrentAnimState).Trigger;
            animator.ResetTrigger(currentAnimStateTriggerName);
        }
        string stateTriggerName = animDict.GetData(state).Trigger;
        CurrentAnimState = state;
        animator.SetTrigger(stateTriggerName);
    }
    private void OnDestroy()
    {
        LevelManager.Instance.OnGameOverEvents -= OnGameOver;
    }
}
