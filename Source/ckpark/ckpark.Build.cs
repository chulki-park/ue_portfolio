// Fill out your copyright notice in the Description page of Project Settings.

using UnrealBuildTool;
using System.IO;
using System.Collections.Generic;

public class ckpark : ModuleRules
{
	public ckpark(TargetInfo Target)
	{
		PublicDependencyModuleNames.AddRange(new string[] { "Core", "CoreUObject", "Engine", "InputCore" });

		PrivateDependencyModuleNames.AddRange(new string[] {  });

        // Uncomment if you are using Slate UI
        // PrivateDependencyModuleNames.AddRange(new string[] { "Slate", "SlateCore" });

        // Uncomment if you are using online features
        // PrivateDependencyModuleNames.Add("OnlineSubsystem");

        // To include OnlineSubsystemSteam, add it to the plugins section in your uproject file with the Enabled attribute set to true

        LoadProudNet(Target);
    }

    private string ModulePath
    {
        get
        {
            //return Path.GetDirectoryName(ModulePath.ToString());
            // https://answers.unrealengine.com/questions/396982/cant-build-project-in-ue-412.html?sort=oldest
            RulesAssembly r;
            FileReference CheckProjectFile;
            UProjectInfo.TryGetProjectForTarget("ckpark", out CheckProjectFile);

            r = RulesCompiler.CreateProjectRulesAssembly(CheckProjectFile);
            FileReference f = r.GetModuleFileName(this.GetType().Name);

            return Path.GetDirectoryName(f.CanonicalName);
        }
    }

    private string ThirdPartyPath
    {
        get
        {
            return Path.GetFullPath(Path.Combine(ModulePath, "../../../../external/ProudNet_1.7.36365-master/"));
        }
    }

    public bool LoadProudNet(TargetInfo Target)
    {
        string ProudNetRoot = "ProudNet";
        string TargetPlatform = "";
        List<string> LibraryFileNames = new List<string>();
        bool isLibrarySupported = true;
        string VSVersion = "VS2015";

        switch(Target.Platform)
        {
            case UnrealTargetPlatform.Win64:
                {
                    TargetPlatform = "x64";
                    LibraryFileNames.Add("ProudNetClient.lib");
                }
                break;

            case UnrealTargetPlatform.Win32:
                {
                    TargetPlatform = "Win32";
                    LibraryFileNames.Add("ProudNetClient.lib");
                } break;

            case UnrealTargetPlatform.IOS:
                {
                    TargetPlatform = "IOS";
                    LibraryFileNames.Add("libProudNetClient.a");
                    LibraryFileNames.Add("libiconv.2.tbd");
                } break;

            case UnrealTargetPlatform.Android:
                {
                    TargetPlatform = "Android";
                    LibraryFileNames.Add("libProudNetClient.a");
                } break;

            default:
                {
                    isLibrarySupported = false;
                }
                break;
        }

        if(isLibrarySupported == true)
        {


            string LibrariesPath = Path.Combine(ThirdPartyPath, ProudNetRoot, "lib", TargetPlatform, VSVersion, "Release");

            foreach (string LibraryFileName in LibraryFileNames)
                PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, LibraryFileName));

            //PublicIncludePaths.Add(Path.Combine(ThirdPartyPath, ProudNetRoot, "include"));
            string IncludePath = Path.Combine(ThirdPartyPath, ProudNetRoot, "include");
           // PublicIncludePaths.Add(IncludePath);

        }

        return isLibrarySupported;
    }
}
