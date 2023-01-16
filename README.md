# 7 Days Ti Die Mods Builder

This application is usefull to automate the process of building **7 Days To Die** mods.

## Installation

 1. Clone or download the repository.
 2. Build the project.
 3. Copy the build files to a directory Example: C:\7DTDModsBuilder\files
 4. Execute the windows.install.bat file

## Clone or download the repository
![image](https://user-images.githubusercontent.com/35576682/212575616-43020828-22b1-401b-96aa-5423493342b6.png)


Copy this command and paste it to a terminal to clone the repository via git

    git clone https://github.com/DJREMiX6/7DTDModsBuilder.git
  

## Build the project
Use the Visual Studio C# compiler to build the application.
Copy and paste into a terminal, opened to the project folder, the following command:

    dotnet build -c Release
This should compile the project into some files like:
inserire immagine

## Copy the build files to a directory
![image](https://user-images.githubusercontent.com/35576682/212575699-13b2651b-36c1-45aa-8b4f-a8432e82da7e.png)

Copy the content of the "net7.0-windows10.0.17763.0" into a new folder (call it whatever you want).
![image](https://user-images.githubusercontent.com/35576682/212575774-b43f7379-2860-406e-8e96-e1fb275843c2.png)

## Execute the windows.install.bat file
Execute the "windows.install.bat" file from a terminal, this will add the path of the "7dtd-build.bat" file to the PATH environment variables, this will make te command executable from a terminal opened in any directory.

    windows.install.bat

# How it works
In a folder where you are developing a 7 Days To Die mod, add a "build.json" file with a structure like that:

    {
	  "Comment": "Consider this just comments to explain how the file works, do not insert 'Comment' elements in your own file",
	  "Comment": " 'GameModsPath' is the full path of the 'Mods' folder of 7 Days To Die",
      "GameModsPath": "D:\\Games\\Steam\\steamapps\\common\\7 Days To Die\\Mods",
      "Comment": " 'ModFolderName' is the name of the mod folder in 7 Days To Die 'Mods' folder that is going to be created",
      "ModFolderName": "ModletExample",
      "Comment": "Here is an array of relative paths of the directories to exclude from the 'build' action",
      "ExcludedDirectories": [
        "dirToExclude",
        "dir1\\dirToExclude"
      ],
      "Comment": "Here is an array of relative paths of the files to exclude from the 'build' action",
      "ExcludedFiles": [
        "fileToExclude.xml",
        "dir1\\fileToExclude.xml",
        "dir1\\dir11\\fileToExclude.xml",
        "dir1\\dir11\\fileToExclude1.xml"
      ]
    }

![image](https://user-images.githubusercontent.com/35576682/212576006-fdc680e8-90da-4e58-8ddb-7f2be6ef6032.png)

Now call the following command from a terminal:

    7dtd-build
And the application should build the files to the 7 Days To Die 'Mods' folder.
