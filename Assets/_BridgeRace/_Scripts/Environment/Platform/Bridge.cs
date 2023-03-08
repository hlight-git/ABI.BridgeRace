using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bridge : MonoBehaviour
{
    public UnBrick UnBrickPrefab;

    [Header("Components")]
    public Transform UnBrickRoot;
    public Transform Surface;
    public Transform LeftRail;
    public Transform RightRail;
    public FloorAdapter NextFloorAdapter;

    public List<UnBrick> UnBricks;

    [Header("Builder")]
    public Vector3 UnBrickRootPosOffset = Vector3.down * 0.45f;
    public int Length;
    public int Width;
    public bool IsStairs;

    public void SetUnBrickList(List<UnBrick> unBricks)
    {
        UnBricks = unBricks;
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(Bridge))]
public class BridgeEditor : Editor
{
    Bridge bridge;

    void OnEnable()
    {
        bridge = (Bridge)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Build"))
        {
            CreateButtonClicked();
        }
    }
    void ClearUnBricks()
    {
        while (bridge.UnBrickRoot.childCount > 0)
        {
            DestroyImmediate(bridge.UnBrickRoot.GetChild(0).gameObject);
        }
    }
    void CreateUnBricks()
    {
        List<UnBrick> unBricks = new List<UnBrick>();
        for (int i = 0; i < bridge.Length; i++)
        {
            UnBrick unBrick = PrefabUtility.InstantiatePrefab(bridge.UnBrickPrefab, bridge.UnBrickRoot) as UnBrick;
            unBrick.transform.localPosition = Vector3.forward * i;
            unBrick.transform.localRotation = bridge.IsStairs ? Quaternion.Euler(-bridge.transform.rotation.eulerAngles.x, 0, 0) : Quaternion.identity;
            unBricks.Add(unBrick);
        }
        bridge.SetUnBrickList(unBricks);
    }

    void CustomRails()
    {
        // Position
        bridge.LeftRail.localPosition = Vector3.left * (bridge.Width / 2f + 0.25f);
        bridge.RightRail.localPosition = Vector3.right * (bridge.Width / 2f + 0.25f);
        // Scale
        bridge.LeftRail.localScale = new Vector3(bridge.LeftRail.localScale.x, bridge.Length / 2f, bridge.LeftRail.localScale.z);
        bridge.RightRail.localScale = new Vector3(bridge.RightRail.localScale.x, bridge.Length / 2f, bridge.LeftRail.localScale.z);
    }
    void InItBrickSurface()
    {
        Vector3 unBrickRootScale = bridge.UnBrickRoot.localScale;
        bridge.Surface.localScale = new Vector3(bridge.Width * 0.1f, 1, bridge.Length * 0.1f);
        bridge.UnBrickRoot.localScale = new Vector3(bridge.Width, unBrickRootScale.y, unBrickRootScale.z);
        bridge.UnBrickRoot.localPosition = Vector3.back * (bridge.Length - 1) / 2f + bridge.UnBrickRootPosOffset;
        CreateUnBricks();
    }
    void ScaledAtTheEndOfTheBridge(Transform target)
    {
        target.localScale = bridge.UnBrickRoot.localScale;
        target.localPosition = Vector3.forward * bridge.Length / 2f;
        target.transform.rotation = bridge.transform.rotation;
        target.transform.Rotate(Vector3.left, bridge.transform.rotation.eulerAngles.x);
    }
    void InitFloorAdapter()
    {
        ScaledAtTheEndOfTheBridge(bridge.NextFloorAdapter.transform);
        Transform floorAdapterTransform = bridge.NextFloorAdapter.transform;
        floorAdapterTransform.position += floorAdapterTransform.up / 2 + floorAdapterTransform.forward / 4;
    }
    void CreateButtonClicked()
    {
        ClearUnBricks();
        InItBrickSurface();
        InitFloorAdapter();
        CustomRails();
        EditorUtility.SetDirty(target);
    }
}

#endif