local lib = {}

function lib:get(control)
    local file = io.open(".\\tmp\\"..control)
    if file == nil then error("Could not get value of "..control.."!") end
    return file:read("a")
end

function lib:set(control, value)
    local file = io.open(".\\tmp\\"..control)
    if file == nil then error("Could not set value of "..control.."!") end
    file:write(value)
    file:flush()
end

function lib:exit()
    local file = io.open(".\\.event")
    if file == nil then error("Could not find the communication file") end
    file:write("exit")
    file:flush()
end

function lib:dlg(text, title, type)
    local file = io.open(".\\.event")
    if file == nil then error("Could not find the communication file") end
    file:write(text.."|"..title.."|"..type) -- this is a sin
    file:flush()
    file:close()
    print(io.read("a", ".\\.event"))
end

return lib