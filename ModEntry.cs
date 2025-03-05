
using HarmonyLib;
using Lidgren.Network;
using System;


namespace StardewModdingAPI;



public class ModEntry: Mod
{   

    
    public override void Entry(IModHelper helper)

    {   
        
        //int oldPort = this.Helper.Reflection.GetField<int>(typeof(LidgrenServer),"defaultPort").GetValue();
        //Monitor.Log($"Old port is {oldPort}", LogLevel.Info);

        var harmony = new Harmony("bilibili.Lixeer.changeserverport");
         harmony.Patch(
                original: AccessTools.Method(typeof(NetPeerConfiguration), "set_Port"), 
                prefix: new HarmonyMethod(typeof(ModEntry), nameof(SetPortPrefix))     
            );       

    }
    public static bool SetPortPrefix(ref int value)
    {
        string path = @"Mods/ChangeServerPort/config.txt";
        try{
            string text = File.ReadAllText(path);
            Console.WriteLine("[ChangeServerPort]The new port is: " + text);
            int port = int.Parse(text);
            value = port;

        }
        catch (Exception ex)
        {
            
            Console.WriteLine("[ChangeServerPort]读取文件时出错: " + ex.Message);
            value = 24643;
        }

        
        
            

            
        return true; // 继续执行原始方法
    }

}
