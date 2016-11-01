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
                return pSettings.dllPath;
            }
            set
            {
                pSettings.dllPath = value;
            }
        }
        public string TypeName { get; set; }
        public string MethodName { get; set; }

        public bool IsPatched
        {
            get
            {
                return File.Exists(Path.GetDirectoryName(pSettings.dllPath) + @"\" + Path.GetFileName(patchDll));
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
        private string patchDll = Environment.CurrentDirectory + @"\..\..\..\ClustertruckSplit\bin\x86\Debug\ClustertruckSplit.dll";
        private string unityDll = Environment.CurrentDirectory + @"\..\..\..\ClustertruckSplit\bin\x86\Debug\UnityEngine_patch.dll";
#else
        private string patchDll = Environment.CurrentDirectory + @"\ClustertruckSplit.dll"; 
        private string unityDll = Environment.CurrentDirectory + @"\UnityEngine_patch.dll"; 
#endif
        private string tmpPatchDll;
        public const string Author = "Panthere";
        public Patcher(PatchSettings p)
        {

            TypeName = p.typeName;
            MethodName = p.methodName;



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
            string target =  fileName.Replace(".dll", "_backup.dll");
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
                    settingsMeth.Body.Instructions[i].Operand = (SByte)pSettings.sleepTime;

                }
                else if (inst.OpCode == OpCodes.Ldc_I4_0)
                {
                    // by level bool
                    settingsMeth.Body.Instructions[i].OpCode = pSettings.isByLevel ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0;
                }
                else if (inst.OpCode == OpCodes.Ldstr)
                {
                    // pipe name
                    settingsMeth.Body.Instructions[i].Operand = pSettings.pipeName;
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
