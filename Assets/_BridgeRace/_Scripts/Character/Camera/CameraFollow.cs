using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Vector3 offsetRotation;
    [SerializeField] private bool initOffsetByDefault;
    private Player playerTarget;

    private void Awake() => OnInit();

    void LateUpdate()
    {
        if (playerTarget == null)
        {
            return;
        }
        FollowTarget();
    }
    void FollowTarget()
    {
        transform.SetPositionAndRotation(
            playerTarget.transform.position + offsetPosition + playerTarget.Stack.Count * 0.1f * Vector3.up,
            Quaternion.Euler(offsetRotation + playerTarget.Stack.Count * 0.1f * Vector3.right));
    }
    void OnInit()
    {
        if (initOffsetByDefault)
        {
            SetOffsetByDefault();
        }
        LevelManager.Instance.OnGameStartEvents += OnGameStart;
        LevelManager.Instance.OnGameOverEvents += OnGameOver;
    }
    void SetOffsetByDefault()
    {
        offsetPosition = GameConfiguration.Instance.CameraConfig.offsetPosition;
        offsetRotation = GameConfiguration.Instance.CameraConfig.offsetRotation;
    }
    void OnGameStart(Player player)
    {
        SetPlayerTarget(player);
    }
    void OnGameOver(Character winner)
    {
        SetOffsetByDefault();
    }
    public void AddMoreOffset(Vector3 offsetPosition, Vector3 offsetRotation)
    {
        this.offsetPosition = offsetPosition;
        this.offsetRotation = offsetRotation;
    }
    public void SetPlayerTarget(Player player)
    {
        playerTarget = player;
    }
}
