---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by likai.
--- DateTime: 2023/7/10 16:49
--- Prefab文件路径配置

local PrefabConfig = {}


---获得Avatar的prefab路径
---@return string
function PrefabConfig.GetAvatarByPic(pic, use2D)
    if use2D == true then
        return "Prefabs/Avatars2D/"..pic..".prefab"
    else
        return "Prefabs/Avatars/"..pic..".prefab"
    end
end

---获得兵种的prefab路径
---@return string
function PrefabConfig.GetTroopPrefabPath(data)
    if data.troop_pic then
        return "Prefabs/Troops/"..data.troop_pic..".prefab"
    end
    local sideStr = data.side == 1 and "att" or "def"
    return "Prefabs/Troops/"..sideStr.."/Troop_"..data.troop_type..".prefab"
end

return PrefabConfig