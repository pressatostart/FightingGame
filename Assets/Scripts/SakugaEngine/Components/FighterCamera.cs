using UnityEngine;
using SakugaEngine.Global;

namespace SakugaEngine
{
    public partial class FighterCamera : MonoBehaviour
    {
        //[Export] private AudioListener3D Listener;
        public CameraFocus CurrentFocus = CameraFocus.CENTER;
        //[Export] public Vector2 minBounds = new Vector2(-12.0f, 0.0f), maxBounds = new Vector2(12.0f, 20.0f);
        public Vector3 nearOffset = new Vector3(2.7f, 1.4f, 6.5f), farOffset = new Vector3(7.25f, 3.35f, 16.0f);
        //[Export] public float minSmoothDistance = 4;
        public float nearDistance = 4.0f, farDistance = 16.0f;
        public float nearFlatOffset = 0.25f, farFlatOffset = 0.62f;

        [SerializeField] private Camera mainCam;
        [SerializeField] private Camera charCam;

        const float DELTA = 10f / GlobalVariables.TicksPerSecond;

        float _wallLimit = GlobalFunctions.ToScaledFloat(GlobalVariables.WallLimit);
        float _ceilingLimit = GlobalFunctions.ToScaledFloat(GlobalVariables.CeilingLimit);
        float _groundLimit = 0f;

        public void UpdateCamera(SakugaActor player1, SakugaActor player2)
        {
            if (player1 == null || player2 == null) return;

            Vector3 _p1Position = GlobalFunctions.ToScaledVector3(player1.Body.FixedPosition);
            Vector3 _p2Position = GlobalFunctions.ToScaledVector3(player2.Body.FixedPosition);

            float playerDistance = Mathf.Clamp(Mathf.Abs(_p2Position.x - _p1Position.x), nearDistance, farDistance);
            float pl = (playerDistance - nearDistance) / (farDistance - nearDistance);

            float FinalYOffset = nearOffset.y;
            float FinalZOffset = -nearOffset.z;
            float BoundsAdd = nearOffset.x;
            float FlatOffset = nearFlatOffset;

            float finalCamY = FinalYOffset;
            float actualCenter = (_p1Position.x + _p2Position.x) / 2;
            switch (CurrentFocus)
            {
                case CameraFocus.CENTER:
                    FinalYOffset = Mathf.Lerp(nearOffset.y, farOffset.y, pl);
                    FinalZOffset = Mathf.Lerp(-nearOffset.z, -farOffset.z, pl);
                    BoundsAdd = Mathf.Lerp(nearOffset.x, farOffset.x, pl);
                    FlatOffset = Mathf.Lerp(nearFlatOffset, farFlatOffset, pl);

                    //actualCenter = (_p1Position.X + _p2Position.X) / 2;
                    if (Mathf.Max(_p1Position.y, _p2Position.y) >= FinalYOffset)
                        finalCamY = Mathf.Max(_p1Position.y, _p2Position.y);
                    break;
                case CameraFocus.PLAYER1:
                    //pl = nearDistance;
                    actualCenter = _p1Position.x;
                    if (_p1Position.y >= FinalYOffset)
                        finalCamY = _p1Position.y;
                    break;
                case CameraFocus.PLAYER2:
                    //pl = nearDistance;
                    actualCenter = _p2Position.x;
                    if (_p2Position.y >= FinalYOffset)
                        finalCamY = _p2Position.y;
                    break;
                default:
                    break;
            }

            Vector3 newCamPosition = new Vector3(actualCenter, finalCamY, 0);
            var clampedCamPosition = new Vector3(
                Mathf.Clamp(newCamPosition.x, -_wallLimit + BoundsAdd, _wallLimit - BoundsAdd),
                Mathf.Clamp(newCamPosition.y, _groundLimit + FinalYOffset, _ceilingLimit - FinalYOffset),
                0
            );
            mainCam.transform.position = new Vector3(
                Mathf.Clamp(Mathf.Lerp(mainCam.transform.position.x, newCamPosition.x, DELTA), -_wallLimit + BoundsAdd, _wallLimit - BoundsAdd),
                Mathf.Clamp(Mathf.Lerp(mainCam.transform.position.y, newCamPosition.y, DELTA), _groundLimit + FinalYOffset, _ceilingLimit - FinalYOffset),
                FinalZOffset);
            
            if (charCam != null)
            {
                charCam.transform.position = mainCam.transform.position;
                charCam.transform.rotation = mainCam.transform.rotation;
                
                var dist = Vector3.Distance(mainCam.transform.position, clampedCamPosition);
                if (mainCam.orthographic)
                {
                    charCam.orthographic = mainCam.orthographic;
                    mainCam.orthographicSize = mainCam.transform.position.z;
                    charCam.orthographicSize = mainCam.orthographicSize;
                }
                else
                {
                    if (charCam.orthographic)
                    {
                        charCam.orthographicSize = dist * Mathf.Tan(mainCam.fieldOfView * Mathf.Deg2Rad / 2) - FlatOffset;
                    }
                    else
                    {
                        charCam.fieldOfView = mainCam.fieldOfView;
                    }
                }
            }
            
            //if (Listener != null) Listener.GlobalPosition = new Vector3(Position.X, Position.Y, 0);
        }
    }
}
