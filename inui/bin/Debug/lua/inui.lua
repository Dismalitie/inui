---@diagnostic disable: need-check-nil
---@runtime priority: lib > this

-- do not ship, not ready yet!
_G.lib =
{
    gui = {},
    long = io.open(".\\lib.eventBinds\\.lib.long", "w"),
    ev = io.open(".\\lib.eventBinds\\.lib.event", "w")
}

function lib:exit()
    lib.lib.ev:write("exit")
    lib.ev:close()
end

function lib:min()
    lib.ev:write("min")
    lib.ev:close()
end

function lib:max()
    lib.ev:write("max")
    lib.ev:close()
end

function lib:dlg(type, desc)
    lib.ev:write("dlg "..type)
    lib.ev:close()
    lib.long:write(desc)
    lib.long:close()
end

function lib.gui:move(ctrl, x, y)
    lib.ev:write("move "..ctrl.." "..x.." "..y)
    lib.ev:close()
end

function lib.gui:set(ctrl, x, y)
    lib.ev:write("set "..ctrl.." "..x.." "..y)
    lib.ev:close()
end

return lib