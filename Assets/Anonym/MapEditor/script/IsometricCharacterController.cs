using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Anonym.Isometric {
    [RequireComponent (typeof (CharacterController))]
    public class IsometricCharacterController : IsometricMovement {
        #region Character
        [Header ("CharacterController")]
        [SerializeField]
        CharacterController CC;

        override public bool isOnGround { get { return CC.isGrounded; } }
        override public Transform cTransform { get { return CC.transform; } }

        [SerializeField]
        bool bUseCustomColliderSize = false;

        [SerializeField, Util.ConditionalHide ("bUseCustomColliderSize", hideInInspector : false)]
        Vector2 CCSize;
        #endregion Character
        override public void Jump () {
            if (bJumpWithMove) {
                // In order to ensure the bottom check
                CC.Move (Vector3.down * 1.25f * CC.minMoveDistance);

                if (isOnGround)
                    jumpStart ();
            } else
                EnQueueDirection (InGameDirection.Jump_Move);

            return;
        }

        override public int SortingOrder_Adjustment () {
            // 땅에서 떨어진 정도가 CCSize.x 이상일 때 CCSize.y, CCSize.x 이하일 때 CCSize.x ~ CCSize.y 리턴
            float fXweight = 0f;
            //if ((CC.collisionFlags & CollisionFlags.Below) == 0)
            {
                RaycastHit _hit;
                float fOffset = CC.height * 0.5f + CC.skinWidth;
                if (Physics.Raycast (cTransform.position + CC.center, Vector3.down, out _hit,
                        CCSize.x + fOffset, CollisionLayerMask)) {
                    fXweight = Mathf.Lerp (CCSize.x, 0f,
                        (_hit.distance - fOffset * 0.25f) / CCSize.x);
                }
            }
            Vector3 iv3Resolution = IsoMap.instance.fResolutionOfIsometric;
            return Mathf.RoundToInt (fXweight * CCSize.x * Mathf.Min (iv3Resolution.z, iv3Resolution.x) +
                (1f - fXweight) * CCSize.y * iv3Resolution.y);
        }

        #region MoveFunction
        override protected void ApplyMovement (Vector3 vMovement) {
            if (!vMovement.Equals (Vector3.zero)) {
                CC.Move (vMovement);

                if ((CC.collisionFlags & CollisionFlags.Below) != 0) {
                    Grounding (transform.localPosition, 1f);
                }
                if ((CC.collisionFlags & CollisionFlags.Sides) != 0) {
                    if (bSnapToGroundGrid && bRevertPositionOnCollision)
                        SetHorizontalMovement (GetRevertVector ());
                }
            }
        }
        #endregion

        #region GameObject
        override public void Start () {

            deathImage.enabled = false;
            if (CC == null)
                CC = gameObject.GetComponent<CharacterController> ();
            CC.enabled = true;

            base.Start ();

            if (CCSize.Equals (Vector2.zero) && bUseCustomColliderSize) {
                CCSize = new Vector2 (Mathf.Max (Grid.fGridTolerance, CC.radius * 2f),
                    Mathf.Max (Grid.fGridTolerance, CC.height + CC.center.y));
            }

            SetMinMoveDistance (Mathf.Min (CC.minMoveDistance, fGridTolerance));

            vDestinationCoordinates.Set (Mathf.RoundToInt (cTransform.localPosition.x), 0, Mathf.RoundToInt (cTransform.localPosition.z));
        }

        override public void Update () {
            RaycastHit hit;

            base.Update ();

            if (Input.GetMouseButtonDown (0) && Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 5000)) {

            }
        }
        #endregion
    }
}