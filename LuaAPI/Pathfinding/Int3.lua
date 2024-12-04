---@class Pathfinding.Int3
---@field zero Pathfinding.Int3
---@field Item int
---@field magnitude float
---@field costMagnitude int
---@field sqrMagnitude float
---@field sqrMagnitudeLong long
---@field x int
---@field y int
---@field z int
---@field Precision int
---@field FloatPrecision float
---@field PrecisionFactor float
local m = {}
---@param lhs Pathfinding.Int3
---@param rhs Pathfinding.Int3
---@return bool
function m.op_Equality(lhs, rhs) end
---@param lhs Pathfinding.Int3
---@param rhs Pathfinding.Int3
---@return bool
function m.op_Inequality(lhs, rhs) end
---@overload fun(ob:Pathfinding.Int3):UnityEngine.Vector3
---@param ob UnityEngine.Vector3
---@return Pathfinding.Int3
function m.op_Explicit(ob) end
---@param lhs Pathfinding.Int3
---@param rhs Pathfinding.Int3
---@return Pathfinding.Int3
function m.op_Subtraction(lhs, rhs) end
---@param lhs Pathfinding.Int3
---@return Pathfinding.Int3
function m.op_UnaryNegation(lhs) end
---@param lhs Pathfinding.Int3
---@param rhs Pathfinding.Int3
---@return Pathfinding.Int3
function m.op_Addition(lhs, rhs) end
---@overload fun(lhs:Pathfinding.Int3, rhs:float):Pathfinding.Int3
---@overload fun(lhs:Pathfinding.Int3, rhs:double):Pathfinding.Int3
---@param lhs Pathfinding.Int3
---@param rhs int
---@return Pathfinding.Int3
function m.op_Multiply(lhs, rhs) end
---@param lhs Pathfinding.Int3
---@param rhs float
---@return Pathfinding.Int3
function m.op_Division(lhs, rhs) end
---@param lhs Pathfinding.Int3
---@param rhs Pathfinding.Int3
---@return float
function m.Angle(lhs, rhs) end
---@param lhs Pathfinding.Int3
---@param rhs Pathfinding.Int3
---@return int
function m.Dot(lhs, rhs) end
---@param lhs Pathfinding.Int3
---@param rhs Pathfinding.Int3
---@return long
function m.DotLong(lhs, rhs) end
---@return Pathfinding.Int3
function m:Normal2D() end
---@param obj Pathfinding.Int3
---@return string
function m.op_Implicit(obj) end
---@return string
function m:ToString() end
---@overload fun(other:Pathfinding.Int3):bool
---@param obj object
---@return bool
function m:Equals(obj) end
---@return int
function m:GetHashCode() end
Pathfinding = {}
Pathfinding.Int3 = m
return m