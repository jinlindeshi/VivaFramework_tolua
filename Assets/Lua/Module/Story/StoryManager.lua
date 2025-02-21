---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by VIVA.
--- DateTime: 2023/12/31 16:13
--- 剧情管理
local StoryManager = {}

StoryManager.STORY_LIST = {
    --"Story_Begin",
    --"Story_WorldToCity",
    --"Story_WorldBattle",
    --"Story_CityScene",
    "Story_MyArmy",
    --"Story_DrillGround",
    --"Story_Dungeon",
    --"Story_NeiZheng",
}
StoryManager.curStoryName = nil
StoryManager.playedStorys = {}
---@return string
function StoryManager.NextStory(endCur)
    if endCur == true then
        StoryManager.EndCurStory()
    end
    if StoryManager.curStoryName then
        StoryManager.playedStorys[StoryManager.curStoryName] = true
    end
    local storyName = nil
    for i = 1, #StoryManager.STORY_LIST do
        local state = PlayerPrefs.GetInt(UserData.userName.."-LK".."-"..StoryManager.STORY_LIST[i])
        --print("你妹啊~", i, state, StoryManager.STORY_LIST[i])
        if state ~= 1 and StoryManager.playedStorys[StoryManager.STORY_LIST[i]] ~= true then
            storyName = StoryManager.STORY_LIST[i]
            break
        end
    end
    --print("你妹啊~", storyName, debug.traceback())
    if storyName then
        StoryManager.curStoryName = storyName
        require("Module.Story."..storyName).Run()
        return storyName
    else
        print("没有要播放的引导啊~~~")
    end
    return nil
end

function StoryManager.ResetRecordTo(storyName)
    print("你妹啊~ResetRecordTo", storyName)
    local isOk = false

    for i = 1, #StoryManager.STORY_LIST do
        if isOk == true then
            PlayerPrefs.SetInt(UserData.userName.."-LK".."-"..StoryManager.STORY_LIST[i], 0)
        else
            if storyName == StoryManager.STORY_LIST[i] then
                isOk = true
                PlayerPrefs.SetInt(UserData.userName.."-LK".."-"..StoryManager.STORY_LIST[i], 0)
            else
                PlayerPrefs.SetInt(UserData.userName.."-LK".."-"..StoryManager.STORY_LIST[i], 1)
            end
        end
    end
    PlayerPrefs.Save()
end

---结束当前剧情并记录进度
function StoryManager.EndCurStory()
    if not StoryManager.curStoryName then
        return
    end
    PlayerPrefs.SetInt(UserData.userName.."-LK".."-"..StoryManager.curStoryName, 1)
    PlayerPrefs.Save()
    StoryManager.curStoryName = nil
end

return StoryManager