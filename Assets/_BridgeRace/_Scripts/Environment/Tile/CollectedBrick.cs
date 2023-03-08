using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedBrick : GameUnit, IColorChangeable<BaseColor>
{
    [SerializeField] private MeshRenderer rend;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BoxCollider boxCollider;
    public BaseColor Color;

    private void SetInteractable(bool isTrue)
    {
        rb.isKinematic = !isTrue;
        boxCollider.enabled = isTrue;
    }
    public void FallDown()
    {
        ChangeColor(BaseColor.Gray);
        SetInteractable(true);
        rb.velocity = new Vector3 (Random.value, 0, Random.value) * 2;
        transform.parent = null;
    }
    public IEnumerator BeCollected(Vector3 startPos, Vector3 finalPos)
    {
        SetInteractable(false);
        transform.localPosition = finalPos;
        transform.rotation = transform.parent.rotation;
        rend.transform.position = startPos;
        while (!Utility.VectorApproximately(rend.transform.position, transform.position))
        {
            rend.transform.position = Vector3.MoveTowards(rend.transform.position, transform.position, Time.deltaTime * GameConfiguration.Instance.SpeedConfig.collectedBrickSurf);
            yield return new WaitForEndOfFrame();
        }
    }
    public void ChangeColor(BaseColor color)
    {
        rend.material = BaseColorDictionary.Instance.GetData(color).Material;
        Color = color;
    }
}
