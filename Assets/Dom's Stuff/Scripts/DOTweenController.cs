using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DOTweenController : MonoBehaviour
{
    [SerializeField] private Vector3 target = Vector3.zero;
    [SerializeField] private float MoveDuration = 1f;
    [SerializeField] private Ease MoveEase = Ease.Linear;
    [SerializeField] private DOTweenType _DoTweenType = DOTweenType.MovementOneWay;

    public GameObject Moveable;

    private enum DOTweenType
    {
        MovementOneWay,
        MovementTwoWay,
    }

    private void Start()
    {
        if (_DoTweenType == DOTweenType.MovementOneWay)
        {
            if (target == Vector3.zero)
            {
                target = transform.position;
                Moveable.transform.DOMove(target, MoveDuration).SetEase(MoveEase);
            }
        }

        else if (_DoTweenType == DOTweenType.MovementTwoWay)
        {
            if (target == Vector3.zero)
            {
                target = transform.position;
                StartCoroutine(MoveBothWays());
            }
        }
    }

    private IEnumerator MoveBothWays()
    {
        Vector3 OrginalLocation = transform.position;
        transform.DOMove(target, MoveDuration).SetEase(MoveEase);
        yield return new WaitForSeconds(MoveDuration);
        transform.DOMove(OrginalLocation, MoveDuration).SetEase(MoveEase);
    }

}
