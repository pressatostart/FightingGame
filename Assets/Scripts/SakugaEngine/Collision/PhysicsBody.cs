using SakugaEngine.Resources;
using System.Collections.Generic;
using SakugaEngine.Global;
using UnityEngine;
using System;

namespace SakugaEngine.Collision
{
    public partial class PhysicsBody : MonoBehaviour
    {
        private SakugaActor _owner;
        public bool IsStatic;
        public bool StayOnBounds;
        public bool IgnoreWalls;
        public int HitboxesLimit = 16;

        [HideInInspector] public IDamage Parent;
        [HideInInspector] public uint ID;
        [HideInInspector] public Collider Pushbox;
        [HideInInspector] public Collider[] Hitboxes;

        [HideInInspector] public Vector2Int FixedPosition;
        [HideInInspector] public Vector2Int FixedVelocity;
        [HideInInspector] public bool IsLeftSide;
        [HideInInspector] public int PlayerSide;
        [HideInInspector] public bool IsMovable = true;
        [HideInInspector] public List<uint> HitBodies = new List<uint>();
        [HideInInspector] public bool ProximityBlocked;
        [HideInInspector] public HitboxState CurrentHitbox = null;
        [HideInInspector] public int CurrentGravity = GlobalVariables.DefaultGravity;
        
        [HideInInspector] public bool IsOnGround => FixedPosition.y <= 0;
        [HideInInspector] public bool IsOnLeftWall => FixedPosition.x <= -GlobalVariables.WallLimit;
        [HideInInspector] public bool IsOnRightWall => FixedPosition.x >= GlobalVariables.WallLimit;
        [HideInInspector] public bool IsFalling => !IsOnGround && FixedVelocity.y <= 0;
        [HideInInspector] public bool JustLanded => IsOnGround && FixedVelocity.y < 0;
        [HideInInspector] public bool IsOnWall => IsOnLeftWall || IsOnRightWall;

        public void Initialize(SakugaActor newOwner, IDamage newParent)
        {
            _owner = newOwner;
            Parent = newParent;
            FixedPosition = Vector2Int.zero;
            FixedVelocity = Vector2Int.zero;
            Hitboxes = new Collider[HitboxesLimit];
        }
        public void SetID(uint newID)
        {
            ID = newID;
        }

        public void UpdateSide(bool leftSide)
        {
            IsLeftSide = leftSide;
        }

        public void FlipSide()
        {
            PlayerSide = IsLeftSide ? 1 : -1;
        }
        public void SetVelocity(Vector2Int newVelocity)
        {
            FixedVelocity = newVelocity;
        }
        public void SetLateralVelocity(int newVelocity)
        {
            FixedVelocity.x = newVelocity;
        }
        public void SetVerticalVelocity(int newVelocity)
        {
            FixedVelocity.y = newVelocity;
        }
        public void AddLateralAcceleration(int newVelocity)
        {
            int absVelocity = Mathf.Abs(newVelocity);
            if (newVelocity != 0 && Mathf.Sign(newVelocity) > 0)
            {
                if (FixedVelocity.x < 0)
                    FixedVelocity.x += GlobalVariables.DefaultFriction / GlobalVariables.TicksPerSecond;
                else if (FixedVelocity.x < absVelocity)
                    FixedVelocity.x += GlobalVariables.DefaultAcceleration / GlobalVariables.TicksPerSecond;
                else
                    FixedVelocity.x = absVelocity;
                    
            }
            else if (newVelocity != 0 && Mathf.Sign(newVelocity) < 0)
            {
                if (FixedVelocity.x > 0)
                    FixedVelocity.x -= GlobalVariables.DefaultFriction / GlobalVariables.TicksPerSecond;
                else if (FixedVelocity.x > -absVelocity)
                    FixedVelocity.x -= GlobalVariables.DefaultAcceleration / GlobalVariables.TicksPerSecond;
                else
                    FixedVelocity.x = -absVelocity;
            }
            else
                FixedVelocity.x -= Mathf.Min(Mathf.Abs(FixedVelocity.x), GlobalVariables.DefaultDeceleration / GlobalVariables.TicksPerSecond) * MathF.Sign(FixedVelocity.x);
        }
        public void AddVerticalAcceleration(int newVelocity)
        {
            int absVelocity = Mathf.Abs(newVelocity);
            if (newVelocity != 0 && Mathf.Sign(newVelocity) > 0)
            {
                if (FixedVelocity.y < 0)
                    FixedVelocity.y += GlobalVariables.DefaultFriction / GlobalVariables.TicksPerSecond;
                else if (FixedVelocity.y < absVelocity)
                    FixedVelocity.y += GlobalVariables.DefaultAcceleration / GlobalVariables.TicksPerSecond;
                else
                    FixedVelocity.y = absVelocity;
                    
            }
            else if (newVelocity != 0 && Mathf.Sign(newVelocity) < 0)
            {
                if (FixedVelocity.y > 0)
                    FixedVelocity.y -= GlobalVariables.DefaultFriction / GlobalVariables.TicksPerSecond;
                else if (FixedVelocity.y > -absVelocity)
                    FixedVelocity.y -= GlobalVariables.DefaultAcceleration / GlobalVariables.TicksPerSecond;
                else
                    FixedVelocity.y = -absVelocity;
            }
            else
                FixedVelocity.y -= Mathf.Min(Mathf.Abs(FixedVelocity.y), GlobalVariables.DefaultDeceleration / GlobalVariables.TicksPerSecond) * MathF.Sign(FixedVelocity.y);
        }
        public void AddGravity()
        {
            FixedVelocity.y -= CurrentGravity / GlobalVariables.TicksPerSecond;
        }
        public void MoveTo(Vector2Int destination)
        {
            if (IsStatic) return;
            
            FixedPosition = destination;
            UpdateColliders();
        }
        public void Resolve(Vector2Int depth)
        {
            FixedPosition -= depth;
            UpdateColliders();
        }
        public void Move()
        {
            if (IsStatic) return;
            if (!IsMovable) return;

            FixedPosition += FixedVelocity / GlobalVariables.Delta;
            UpdateColliders();
        }

        public void AddHitBody(PhysicsBody body)
        {
            HitBodies.Add(body.ID);
        }

        public void ResetHits()
        {
            HitBodies.Clear();
        }

        public bool CanHitBody(PhysicsBody body)
        {
            return !HitBodies.Contains(body.ID);
        }

        public void BounceX(int intensity)
        {
            FixedVelocity.x *= -intensity;
            FixedVelocity.x /= 100;
        }

        public void BounceY(int intensity)
        {
            FixedVelocity.y *= -intensity;
            FixedVelocity.y /= 100;
        }

        public void UpdateColliders()
        {
            CurrentHitbox = GetCurrentHitboxSettings();
            //Lock collider on bounds
            if (StayOnBounds)
            {
                if (!IgnoreWalls)
                    FixedPosition.x = Mathf.Clamp(FixedPosition.x, -GlobalVariables.WallLimit, GlobalVariables.WallLimit);
                FixedPosition.y = Mathf.Clamp(FixedPosition.y, 0, GlobalVariables.CeilingLimit);
            }

            if (CurrentHitbox == null || CurrentHitbox.HitboxData == null) //If there's no hitbox active, remove all boxes
            {
                Pushbox.UpdateCollider(FixedPosition, Vector2Int.zero);

                for(int i = 0; i < Hitboxes.Length; i++)
                    Hitboxes[i].UpdateCollider(FixedPosition, Vector2Int.zero);
            }
            else //Update hitboxes
            {
                Vector2Int Side = new Vector2Int(PlayerSide, 1);

                if (CurrentHitbox.HitboxData.Pushbox == null)
                    Pushbox.UpdateCollider(FixedPosition + Vector2Int.zero, Vector2Int.zero);
                else
                    Pushbox.UpdateCollider(FixedPosition + (CurrentHitbox.HitboxData.Pushbox.Center * Side), CurrentHitbox.HitboxData.Pushbox.Size);
                
                for(int i = 0; i < Hitboxes.Length; i++)
                    if (i < CurrentHitbox.HitboxData.Hitboxes.Length)
                        Hitboxes[i].UpdateCollider(FixedPosition + (CurrentHitbox.HitboxData.Hitboxes[i].BoxArea.Center * Side), CurrentHitbox.HitboxData.Hitboxes[i].BoxArea.Size);
                    else
                        Hitboxes[i].UpdateCollider(FixedPosition, Vector2Int.zero);
            }
        }

        public HitboxState GetCurrentHitboxSettings()
        {
            var State = _owner.StateManager.GetCurrentState();
            if (State.AnimationData == null) return null;
            if (State.AnimationData.Hitboxes == null || State.AnimationData.Hitboxes.Length <= 0) return null;
            if (State.AnimationData.Hitboxes.Length == 1)
                return State.AnimationData.Hitboxes[0];

            int anim = 0;

            for (int i = 0; i < State.AnimationData.Hitboxes.Length; i++)
            {
                int nextFrame = (i == State.AnimationData.Hitboxes.Length - 1) ?
                                int.MaxValue :
                                State.AnimationData.Hitboxes[i + 1].AtFrame - 1;
                
                if (_owner.StateManager.CurrentStateFrame >= State.AnimationData.Hitboxes[i].AtFrame && _owner.StateManager.CurrentStateFrame <= nextFrame)
                    anim = i;
            }

            return State.AnimationData.Hitboxes[anim];
        }
    }
}
