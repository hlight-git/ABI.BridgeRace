using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGuideCane : MonoBehaviour
{
    [SerializeField] private float raycastLength = GameConstant.Character.HEIGHT + 10f;
    private Character character;

    void Awake() => character = GetComponent<Character>();

    private Ray ray = new Ray(Vector3.zero, Vector3.down);
    private bool RaycastInLayer(string layerName, float raycastLength, out RaycastHit hit)
    {
        int layerMask = 1 << LayerMask.NameToLayer(layerName);
        return Physics.Raycast(ray, out hit, raycastLength, layerMask);
    }
    private bool NextStepIsASurface(out RaycastHit surfaceHit)
    {
        return RaycastInLayer(GameConstant.Layer.SURFACE, raycastLength, out surfaceHit);
    }
    private bool NextStepIsAUnBrickSurface(RaycastHit surfaceHit)
    {
        return surfaceHit.transform.CompareTag(GameConstant.Tag.SURFACE);
    }

    private bool NextStepOnUnBrickSurfaceIsValid(RaycastHit surfaceHit)
    {
        if (RaycastInLayer(GameConstant.Layer.UNBRICK, raycastLength, out RaycastHit unBrickHit))
        {
            Bridge unBrickSurface = surfaceHit.transform.GetComponentInParent<Bridge>();
            bool isTurnBack = (unBrickSurface.UnBrickRoot.position - unBrickHit.transform.position).sqrMagnitude <
                (unBrickSurface.UnBrickRoot.position - transform.position).sqrMagnitude;
            bool isSameColorBrick = unBrickHit.transform.GetComponent<UnBrick>().Color == character.Color;
            bool stackNotEmpty = character.Stack.Count > 0;
            return stackNotEmpty || isSameColorBrick || isTurnBack;
        }
        return false;
    }
    public bool CanMoveToward(Vector3 forwardDirection, out Vector3 moveDirection)
    {
        moveDirection = default;
        ray.origin =
            transform.position +
            Vector3.up * GameConstant.Character.HEIGHT +
            forwardDirection * GameConstant.Brick.UNBRICK_WIDTH / 2;

        if (!NextStepIsASurface(out RaycastHit surfaceHit))
        {
            return false;
        }

        moveDirection = surfaceHit.point - transform.position;

        if (!NextStepIsAUnBrickSurface(surfaceHit))
        {
            return true;
        }
        return NextStepOnUnBrickSurfaceIsValid(surfaceHit);
    }
}
