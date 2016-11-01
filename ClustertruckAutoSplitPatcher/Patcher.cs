using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dnlib;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.IO;

namespace CTPatcher
{
    public class Patcher
    {
        public string DllPath
        {
            get
            {
                return pSettings.DllPath;
            }
            set
            {
                pSettings.DllPath = value;
            }
        }
        public string TypeName { get; set; }
        public string MethodName { get; set; }

        public bool IsPatched
        {
            get
            {
                return File.Exists(Path.GetDirectoryName(pSettings.DllPath) + @"\" + Path.GetFileName(patchDll));
            }

        }


        public ModuleDefMD injModule;
        public ModuleDefMD asmModule;



        public TypeDef infoType;


        public MethodDef infoCctor;
        public MethodDef injMethod;
        public MethodDef injHookMethod;

        public PatchSettings pSettings;

#if DEBUG
        // Sadly you will have to replace this yourself if you wish to debug :(
        private string patchDll = @"D:\GitHub\ClustertruckAutoSplitter\ClustertruckSplit\bin\x86\Debug\ClustertruckSplit.dll";
        private string unityDll = @"D:\GitHub\ClustertruckAutoSplitter\ClustertruckSplit\bin\x86\Debug\UnityEngine_patch.dll";
#else
        private string patchDll = Environment.CurrentDirectory + @"\ClustertruckSplit.dll"; 
        private string unityDll = Environment.CurrentDirectory + @"\UnityEngine_patch.dll"; 
#endif
        private string tmpPatchDll;
        public const string Author = "Panthere";
        public Patcher(PatchSettings p)
        {

            TypeName = p.TypeName;
            MethodName = p.MethodName;



            ReloadDll(patchDll);


        }

        public void CopyDependencies()
        {
            string patchPath = Path.GetDirectoryName(DllPath) + @"\" + Path.GetFileName(patchDll);
            if (File.Exists(patchPath))
            {
                File.Delete(patchPath);
            }
            File.Copy(tmpPatchDll, patchPath);

            string unityPath = Path.GetDirectoryName(DllPath) + @"\UnityEngine.dll";
            if (File.Exists(unityPath))
            {
                File.Delete(unityPath);
            }
            File.Copy(unityDll, unityPath);

        }

        #region Backups
        public void CreateBackup()
        {
            BackupFile(DllPath);
            BackupFile(Path.GetDirectoryName(DllPath) + @"\UnityEngine.dll");
        }

        public void BackupFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return;
            }
            string target = fileName.Replace(".dll", "_backup.dll");
            if (File.Exists(target))
            {
                File.Delete(target);
            }

            File.Copy(fileName, target);
        }

        public bool RestoreBackup()
        {
            string targetPath = DllPath.Replace(".dll", "_backup.dll");
            if (File.Exists(targetPath))
            {
                if (File.Exists(DllPath))
                {
                    File.Delete(DllPath);
                }
                File.Move(targetPath, DllPath);
            }


            targetPath = Path.GetDirectoryName(DllPath) + @"\UnityEngine_backup.dll";
            string unityPath = Path.GetDirectoryName(DllPath) + @"\UnityEngine.dll";
            if (File.Exists(targetPath))
            {
                if (File.Exists(unityPath))
                {
                    File.Delete(unityPath);
                }
                File.Move(targetPath, unityPath);
            }


            File.Delete(Path.GetDirectoryName(DllPath) + @"\" + Path.GetFileName(patchDll));
            return true;
        }
        #endregion

        public void Patch()
        {
            // Apply our settings
            ApplySettings();

            if (!IsPatched)
            {
                CreateBackup();
            }

        }

        public void Unpatch()
        {

            RestoreBackup();

        }

        public void Write()
        {
            CopyDependencies();
        }

        private void ReloadDll(string fileName)
        {

            injModule = ModuleDefMD.Load(fileName);

            TypeDef injClass = injModule.Find(TypeName, false);
            if (injClass == null)
                throw new NullReferenceException("injClass");

            injMethod = injClass.FindMethod(MethodName);
            if (injMethod == null)
                throw new NullReferenceException("injMethod");

            TypeDef hookClass = injModule.Find("ClustertruckSplit.Hook", false);
            if (hookClass == null)
                throw new NullReferenceException("hookClass");

            injHookMethod = hookClass.FindMethod("Initialize");
            if (injHookMethod == null)
                throw new NullReferenceException("injHookMethod");
        }


        private void ApplySettings()
        {
            // apply settings here

            TypeDef sType = injModule.Find("ClustertruckSplit.Settings", false);

            if (sType == null)
                throw new NullReferenceException("Settings Type not Found");

            MethodDef settingsMeth = sType.FindStaticConstructor();

            for (int i = 0; i < settingsMeth.Body.Instructions.Count; i++)
            {
                Instruction inst = settingsMeth.Body.Instructions[i];
                if (inst.OpCode == OpCodes.Ldc_I4_S && ((SByte)inst.Operand) == 10)
                {
                    // sleep time
                    settingsMeth.Body.Instructions[i].Operand = (SByte)pSettings.SleepTime;

                }
                else if (inst.OpCode == OpCodes.Ldstr)
                {
                    switch ((string)inst.Operand)
                    {
                        case "LEVEL":
                            settingsMeth.Body.Instructions[i].Operand = pSettings.LevelModeEnabled.ToString();
                            break;
                        case "PAUSE":
                            settingsMeth.Body.Instructions[i].Operand = pSettings.PauseEnabled.ToString();
                            break;
                        case "RESET":
                            settingsMeth.Body.Instructions[i].Operand = pSettings.ResetEnabled.ToString();
                            break;
                        case "LiveSplit":
                            settingsMeth.Body.Instructions[i].Operand = pSettings.PipeName;
                            break;
                    }
                    
                }
            }

            tmpPatchDll = Environment.CurrentDirectory + @"\Data\DEL_" + Guid.NewGuid().ToString() + ".dll";

            if (!Directory.Exists(tmpPatchDll))
                Directory.CreateDirectory(Path.GetDirectoryName(tmpPatchDll));

            dnlib.DotNet.Writer.ModuleWriterOptions mr = new dnlib.DotNet.Writer.ModuleWriterOptions(injModule);
            mr.MetaDataOptions.Flags = dnlib.DotNet.Writer.MetaDataFlags.PreserveAll;

            injModule.Write(tmpPatchDll, mr);

            ReloadDll(tmpPatchDll);

            // dnlib pls why you no unload?

        }
    }
}
