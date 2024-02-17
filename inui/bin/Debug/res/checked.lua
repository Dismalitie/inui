-- Open a file in write mode (w)
local file = io.open(".\\checked.txt", "w")

if file then
    -- Write content to the file
    file:write("Hello, Lua!\n")
    file:write("This is a test file.")

    -- Close the file
    file:close()
    print("File created successfully.")
else
    print("Error: Unable to create file.")
end