using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anonym.Isometric {
    using Util;
    [DisallowMultipleComponent]
    [System.Serializable]
    [ExecuteInEditMode]
    public class Grid : MonoBehaviour {
        #region ForGridMovement
        [SerializeField, HideInInspector]
        bool bUseLocalGrid = true;

        [HideInInspector]
        Grid _parentGrid;
        [ConditionalHide ("!bUseLocalGrid", hideInInspector : true)]
        [SerializeField]
        public Grid parentGrid {
            get {
                if (_parentGrid == null && transform.parent != null)
                    _parentGrid = transform.parent.GetComponent<Grid> ();
                if (_parentGrid == null) // && gameObject != IsoMap.instance.gameObject)
                    _parentGrid = IsoMap.instance.gGrid;

                return _parentGrid;
            }
        }

        [ConditionalHide ("bUseLocalGrid", hideInInspector : true)]
        public bool IsInheritGrid { get { return !bUseLocalGrid; } } // && parentGrid != null; } }

        [ConditionalHide ("bUseLocalGrid", hideInInspector : true)]
        [SerializeField]
        Vector3 _TileSize = Vector3.one;
        public Vector3 TileSize {
            //get {   return IsInheritGrid ? Vector3.Scale(_TileScale, parentGrid.Scale) : _TileScale;    }
            get { return IsInheritGrid ? parentGrid.TileSize : _TileSize; }
        }

        [ConditionalHide ("bUseLocalGrid", hideInInspector : true)]
        [SerializeField]
        Vector3 _GridInterval = new Vector3 (1f, 1f / 3f, 1f);
        public Vector3 GridInterval {
            // get {   return IsInheritGrid ? Vector3.Scale(_Size, parentGrid.Size) : _Size;   }
            get { return IsInheritGrid ? parentGrid.GridInterval : Vector3.Scale (TileSize, _GridInterval); }
        }
        public int CoordinatesCountInTile (Vector3 _direction) {
            Vector3 result = Vector3.Scale (_direction, TileSize);
            Vector3 size = GridInterval;
            return Mathf.Abs (Mathf.RoundToInt (result.x / size.x + result.y / size.y + result.z / size.z));
        }
        public Vector3 CoordinatesToPosition (Vector3 coordinates, bool bSnap = false) {
            coordinates.Scale (GridInterval);

            if (bSnap) {
                coordinates.x = Mathf.RoundToInt (coordinates.x);
                coordinates.y = Mathf.RoundToInt (coordinates.y);
                coordinates.z = Mathf.RoundToInt (coordinates.z);
            }
            return coordinates;
        }
        public Vector3 PositionToCoordinates (Vector3 position, bool bSnap = false) {
            position.x = position.x / GridInterval.x;
            position.y = position.y / GridInterval.y;
            position.z = position.z / GridInterval.z;

            if (bSnap) {
                position.x = Mathf.RoundToInt (position.x);
                position.y = Mathf.RoundToInt (position.y);
                position.z = Mathf.RoundToInt (position.z);
            }
            return position;
        }
        public static float fGridTolerance = 0.01f;
        #endregion 
#if UNITY_EDITOR

        [HideInInspector]
        GridCoordinates _coordinates;
        [HideInInspector]
        public GridCoordinates coordinates {
            get {
                return _coordinates == null ?
                    _coordinates = GetComponent<GridCoordinates> () : _coordinates;
            }
        }

        public Vector3 Centor {
            get {
                if (IsInheritGrid) {
                    Vector3 v3Result = new Vector3 ();
                    v3Result.x = transform.localPosition.x / parentGrid.GridInterval.x;
                    v3Result.y = transform.localPosition.y / parentGrid.GridInterval.y;
                    v3Result.z = transform.localPosition.z / parentGrid.GridInterval.z;
                    //v3Result -= parentGrid.Centor;
                    return v3Result;
                }

                return transform.position;
            }
        }

        public bool bChildUpdatedFlagForEditor = false;
        void OnTransformChildrenChanged () {
            bChildUpdatedFlagForEditor = true;
        }

        public void Sync (Grid with) {
            bUseLocalGrid = with.bUseLocalGrid;
            _TileSize = with._TileSize;
            _GridInterval = with._GridInterval;
        }
#endif
    }
}