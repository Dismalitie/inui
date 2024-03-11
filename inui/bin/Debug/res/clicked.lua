local inui = require("inui")

-- Open a file in write mode (w)
local file = io.open(".\\clicked.txt", "w")

if file then
    -- Write content to the file
    file:write("e")

    -- Close the file
    file:close()
    print("File created successfully.")
else
    print("Error: Unable to create file.")
end

inui:exit()