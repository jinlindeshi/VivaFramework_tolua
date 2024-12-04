---@class UnityEngine.Rigidbody2D : UnityEngine.Component
---@field position UnityEngine.Vector2
---@field rotation float
---@field velocity UnityEngine.Vector2
---@field angularVelocity float
---@field useAutoMass bool
---@field mass float
---@field sharedMaterial UnityEngine.PhysicsMaterial2D
---@field centerOfMass UnityEngine.Vector2
---@field worldCenterOfMass UnityEngine.Vector2
---@field inertia float
---@field drag float
---@field angularDrag float
---@field gravityScale float
---@field bodyType UnityEngine.RigidbodyType2D
---@field useFullKinematicContacts bool
---@field isKinematic bool
---@field freezeRotation bool
---@field constraints UnityEngine.RigidbodyConstraints2D
---@field simulated bool
---@field interpolation UnityEngine.RigidbodyInterpolation2D
---@field sleepMode UnityEngine.RigidbodySleepMode2D
---@field collisionDetectionMode UnityEngine.CollisionDetectionMode2D
---@field attachedColliderCount int
local m = {}
---@param endValue UnityEngine.Vector2
---@param duration float
---@param snapping bool
---@return DG.Tweening.Core.TweenerCore
function m:DOMove(endValue, duration, snapping) end
---@param endValue float
---@param duration float
---@param snapping bool
---@return DG.Tweening.Core.TweenerCore
function m:DOMoveX(endValue, duration, snapping) end
---@param endValue float
---@param duration float
---@param snapping bool
---@return DG.Tweening.Core.TweenerCore
function m:DOMoveY(endValue, duration, snapping) end
---@param endValue float
---@param duration float
---@return DG.Tweening.Core.TweenerCore
function m:DORotate(endValue, duration) end
---@param endValue UnityEngine.Vector2
---@param jumpPower float
---@param numJumps int
---@param duration float
---@param snapping bool
---@return DG.Tweening.Sequence
function m:DOJump(endValue, jumpPower, numJumps, duration, snapping) end
---@param path table
---@param duration float
---@param pathType DG.Tweening.PathType
---@param pathMode DG.Tweening.PathMode
---@param resolution int
---@param gizmoColor System.Nullable
---@return DG.Tweening.Core.TweenerCore
function m:DOPath(path, duration, pathType, pathMode, resolution, gizmoColor) end
---@param path table
---@param duration float
---@param pathType DG.Tweening.PathType
---@param pathMode DG.Tweening.PathMode
---@param resolution int
---@param gizmoColor System.Nullable
---@return DG.Tweening.Core.TweenerCore
function m:DOLocalPath(path, duration, pathType, pathMode, resolution, gizmoColor) end
---@overload fun(rotation:UnityEngine.Quaternion):void
---@param angle float
function m:SetRotation(angle) end
---@param position UnityEngine.Vector2
function m:MovePosition(position) end
---@overload fun(rotation:UnityEngine.Quaternion):void
---@param angle float
function m:MoveRotation(angle) end
---@return bool
function m:IsSleeping() end
---@return bool
function m:IsAwake() end
function m:Sleep() end
function m:WakeUp() end
---@overload fun(collider:UnityEngine.Collider2D, contactFilter:UnityEngine.ContactFilter2D):bool
---@overload fun(contactFilter:UnityEngine.ContactFilter2D):bool
---@param collider UnityEngine.Collider2D
---@return bool
function m:IsTouching(collider) end
---@overload fun(layerMask:int):bool
---@return bool
function m:IsTouchingLayers() end
---@param point UnityEngine.Vector2
---@return bool
function m:OverlapPoint(point) end
---@param collider UnityEngine.Collider2D
---@return UnityEngine.ColliderDistance2D
function m:Distance(collider) end
---@param position UnityEngine.Vector2
---@return UnityEngine.Vector2
function m:ClosestPoint(position) end
---@overload fun(force:UnityEngine.Vector2, mode:UnityEngine.ForceMode2D):void
---@param force UnityEngine.Vector2
function m:AddForce(force) end
---@overload fun(relativeForce:UnityEngine.Vector2, mode:UnityEngine.ForceMode2D):void
---@param relativeForce UnityEngine.Vector2
function m:AddRelativeForce(relativeForce) end
---@overload fun(force:UnityEngine.Vector2, position:UnityEngine.Vector2, mode:UnityEngine.ForceMode2D):void
---@param force UnityEngine.Vector2
---@param position UnityEngine.Vector2
function m:AddForceAtPosition(force, position) end
---@overload fun(torque:float, mode:UnityEngine.ForceMode2D):void
---@param torque float
function m:AddTorque(torque) end
---@param point UnityEngine.Vector2
---@return UnityEngine.Vector2
function m:GetPoint(point) end
---@param relativePoint UnityEngine.Vector2
---@return UnityEngine.Vector2
function m:GetRelativePoint(relativePoint) end
---@param vector UnityEngine.Vector2
---@return UnityEngine.Vector2
function m:GetVector(vector) end
---@param relativeVector UnityEngine.Vector2
---@return UnityEngine.Vector2
function m:GetRelativeVector(relativeVector) end
---@param point UnityEngine.Vector2
---@return UnityEngine.Vector2
function m:GetPointVelocity(point) end
---@param relativePoint UnityEngine.Vector2
---@return UnityEngine.Vector2
function m:GetRelativePointVelocity(relativePoint) end
---@overload fun(contactFilter:UnityEngine.ContactFilter2D, results:table):int
---@param contactFilter UnityEngine.ContactFilter2D
---@param results table
---@return int
function m:OverlapCollider(contactFilter, results) end
---@overload fun(contacts:table):int
---@overload fun(contactFilter:UnityEngine.ContactFilter2D, contacts:table):int
---@overload fun(contactFilter:UnityEngine.ContactFilter2D, contacts:table):int
---@overload fun(colliders:table):int
---@overload fun(colliders:table):int
---@overload fun(contactFilter:UnityEngine.ContactFilter2D, colliders:table):int
---@overload fun(contactFilter:UnityEngine.ContactFilter2D, colliders:table):int
---@param contacts table
---@return int
function m:GetContacts(contacts) end
---@overload fun(results:table):int
---@param results table
---@return int
function m:GetAttachedColliders(results) end
---@overload fun(direction:UnityEngine.Vector2, results:table, distance:float):int
---@overload fun(direction:UnityEngine.Vector2, results:table, distance:float):int
---@overload fun(direction:UnityEngine.Vector2, contactFilter:UnityEngine.ContactFilter2D, results:table):int
---@overload fun(direction:UnityEngine.Vector2, contactFilter:UnityEngine.ContactFilter2D, results:table, distance:float):int
---@overload fun(direction:UnityEngine.Vector2, contactFilter:UnityEngine.ContactFilter2D, results:table, distance:float):int
---@param direction UnityEngine.Vector2
---@param results table
---@return int
function m:Cast(direction, results) end
---@param physicsShapeGroup UnityEngine.PhysicsShapeGroup2D
---@return int
function m:GetShapes(physicsShapeGroup) end
UnityEngine = {}
UnityEngine.Rigidbody2D = m
return m